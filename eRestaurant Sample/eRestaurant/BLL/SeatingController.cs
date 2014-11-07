using eRestaurant.DAL;
using eRestaurant.Entities;
using eRestaurant.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.BLL
{
    [DataObject]


    public class SeatingController
    {
        #region Query Methods
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SeatingSummary> AvailableSeatingByDateTime(DateTime date, TimeSpan time)
        {
            var result = from seats in SeatingByDateTime(date, time)
                         where !seats.Taken
                         select seats;
            return result.ToList();
        }




        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<ReservationCollection> ReservationsByTime(DateTime date)
        {

            using (var context = new RestaurantContext())
            {
                var result = from data in context.Reservations
                             where data.ReservationDate.Year == date.Year
                                 && data.ReservationDate.Month == date.Month
                                 && data.ReservationDate.Day == date.Day
                                 && data.ReservationStatus == Reservation.Booked //Reservation.Booked
                             select new ReservationSummary()//DTOs.ReservationSumary()
                             {
                                 ID=data.ReservationID,
                                 Name = data.CustomerName,
                                 Date = data.ReservationDate,
                                 NumberInParty = data.NumberInParty,
                                 Status = data.ReservationStatus,
                                 Event = data.SpecialEvent.Description,
                                 Contact = data.ContactPhone
                                 //,
                                 //Tables = from seat in data.ReservationTables
                                 //         select seat.Table.TableNumber
                             };

                var finalResult = from item in result
                                  group item by item.Date.Hour into itemGroup
                                  select new ReservationCollection() { 
                                  
                                        Hour = itemGroup.Key,
                                        Reservations = itemGroup.ToList()
                                  };
                return finalResult.ToList();

            }


        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<SeatingSummary> SeatingByDateTime(DateTime date, TimeSpan time)
        {
            using (var context = new RestaurantContext())
            {
                //step 1 - get the table info along with any walk in bills and reservations bills for the specific time slot
                var step1 = from data in context.Tables
                            select new
                            {
                                Table = data.TableNumber,
                                Seating = data.Capacity,
                                //this sub query gets the bills for walk in custoemrs
                                Bills = from billing in data.Bills
                                        where
                                             billing.BillDate.Year == date.Year
                                          && billing.BillDate.Month == date.Month
                                          && billing.BillDate.Day == date.Day
                                          // the following wont work in ef to entities - it will return this exception:
                                          // "the specified type member 'TimeOfDay' is not supported...."
                                          
                                          // && billing.BillDate.TimeOfDay <= time
                                          && DbFunctions.CreateTime(billing.BillDate.Hour, billing.BillDate.Minute, billing.BillDate.Second) <= time
                                          && (!billing.OrderPaid.HasValue || billing.OrderPaid.Value >= time)
                                        //&& (!billing,
                                        select billing,
                                //this sub query gets the bills for reservations
                                Reservations = from booking in data.Reservations
                                               from billing in booking.Bills
                                               where

                                             billing.BillDate.Year == date.Year
                                          && billing.BillDate.Month == date.Month
                                          && billing.BillDate.Day == date.Day
                                         // && billing.BillDate.TimeOfDay <= time
                                         && DbFunctions.CreateTime(billing.BillDate.Hour, billing.BillDate.Minute, billing.BillDate.Second) <= time 
                                         
                                         && (!billing.OrderPaid.HasValue || billing.OrderPaid.Value >= time)
                                               //&& (!billing,
                                               select billing



                            };
               


                // Step 2 - Union the walk-in bills and the reservation bills while extracting the relebant bill info
                var step2 = from data in step1.ToList() //to.list forces the first result set to be in memory
                            select new
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                CommonBilling = from info in data.Bills.Union(data.Reservations)
                                                select new //info
                                                {

                                                    BillID = info.BillID,
                                                    BillTotal = info.Items.Sum(bi => bi.Quantity * bi.SalePrice),
                                                    Waiter = info.Waiter.FirstName,
                                                    Reservation = info.Reservation

                                                }
                            };

               

                //step 3- get just the first commonBIlling item
                //(persumes no overlap can occur -i.e two groups at the same table at the same time)
                var step3 = from data in step2.ToList()
                            select new
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.CommonBilling.Count() > 0,
                                // .FirstOrDefault() is effectibly "flattening" my collection of 1 item into 
                                // a single object whose properties I can get in step 4 using the dot (.) operator
                                CommonBilling = data.CommonBilling.FirstOrDefault()
                            };
                


                // step 4 - builf our intended seating summary info
                var step4 = from data in step3
                            select new SeatingSummary()
                            {
                                Table = data.Table,
                                Seating = data.Seating,
                                Taken = data.Taken,
                                //use a tenary expression to conditonally get a bill id (if it exists)
                                BillID = data.Taken ? //if (data.Taken)
                                         data.CommonBilling.BillID //value to use if true
                                         :  // else

                                         (int?)null, //value to use if false

                                // note: going back to step 2 to be more selective of my billing Information
                                BillTotal = data.Taken ?
                                            data.CommonBilling.BillTotal : (decimal?)null,

                                Waiter = data.Taken ? data.CommonBilling.Waiter : (string)null,
                                ReservationName = data.Taken ? (data.CommonBilling.Reservation != null ?
                                                            data.CommonBilling.Reservation.CustomerName : (string)null)
                                                                : (string)null

                            };

                return step4.ToList();
            
            }

        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Seats a customer that is a walk-in
        /// </summary>
        /// <param name="when">A mock value of the date/time (Temporary - see remarks)</param>
        /// <param name="tableNumber">Table number to be seated</param>
        /// <param name="customerCount">Number of customers being seated</param>
        /// <param name="waiterId">Id of waiter that is serving</param>
        public void SeatCustomer(DateTime when, byte tableNumber, int customerCount, int waiterId)
        {
            var availableSeats = AvailableSeatingByDateTime(when.Date, when.TimeOfDay);
            using (var context = new RestaurantContext())
            {
                List<string> errors = new List<string>();
                // Rule checking:
                // - Table must be available - typically a direct check on the table, but proxied based on the mocked time here
                // - Table must be big enough for the # of customers
                if (!availableSeats.Exists(x => x.Table == tableNumber))
                    errors.Add("Table is currently not available");
                else if (!availableSeats.Exists(x => x.Table == tableNumber && x.Seating >= customerCount))
                    errors.Add("Insufficient seating capacity for number of customers.");
                if (errors.Count > 0)
                    throw new BusinessRuleException("Unable to seat customer", errors);
                Bill seatedCustomer = new Bill()
                {
                    BillDate = when,
                    NumberInParty = customerCount,
                    WaiterID = waiterId,
                    TableID = context.Tables.Single(x => x.TableNumber == tableNumber).TableID
                };
                context.Bills.Add(seatedCustomer);
                context.SaveChanges();
            }
        }


        public void SeatCustomer(DateTime when, int reservationId, List<byte> tables, int waiterId)
        {
            var availableSeats = AvailableSeatingByDateTime(when.Date, when.TimeOfDay);
            using (var context = new RestaurantContext())
            {
                List<string> errors = new List<string>();
                // Rule checking:
                // - Reservation must be in Booked status
                // - Table must be available - typically a direct check on the table, but proxied based on the mocked time here
                // - Table must be big enough for the # of customers
                var reservation = context.Reservations.Find(reservationId);
                if (reservation == null)
                    errors.Add("The specified reservation does not exist");
                else if (reservation.ReservationStatus != Reservation.Booked)
                    errors.Add("The reservation's status is not valid for seating. Only booked reservations can be seated.");
                var capacity = 0;
                foreach (var tableNumber in tables)
                {
                    if (!availableSeats.Exists(x => x.Table == tableNumber))
                        errors.Add("Table " + tableNumber + " is currently not available");
                    else
                        capacity += availableSeats.Single(x => x.Table == tableNumber).Seating;
                }
                if (capacity < reservation.NumberInParty)
                    errors.Add("Insufficient seating capacity for number of customers. Alternate tables must be used.");
                if (errors.Count > 0)
                    throw new BusinessRuleException("Unable to seat customer", errors);
                // 1) Create a blank bill with assigned waiter
                Bill seatedCustomer = new Bill()
                {
                    BillDate = when,
                    NumberInParty = reservation.NumberInParty,
                    WaiterID = waiterId,
                    ReservationID = reservation.ReservationID
                };
                context.Bills.Add(seatedCustomer);
                // 2) Add the tables for the reservation and change the reservation's status to arrived
                foreach (var tableNumber in tables)
                    reservation.Tables.Add(context.Tables.Single(x => x.TableNumber == tableNumber));
                reservation.ReservationStatus = Reservation.Arrived;
                var updatable = context.Entry(context.Reservations.Attach(reservation));
                updatable.Property(x => x.ReservationStatus).IsModified = true;
                //updatable.Reference(x=>x.Tables).
                // 3) Save changes -  all of the modifications to the context(DAL) are processed as a transaction
                context.SaveChanges();
            }
            //string message = String.Format("Not yet implemented. Need to seat reservation {0} for waiter {1} at tables {2}", reservationId, waiterId, string.Join(", ", tables));
            //throw new NotImplementedException(message);
        }











        #endregion

       
    }
}
