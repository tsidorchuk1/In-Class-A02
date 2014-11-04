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
                                Reservation = data.Taken ? (data.CommonBilling.Reservation != null ?
                                                            data.CommonBilling.Reservation.CustomerName : (string)null)
                                                                : (string)null

                            };

                return step4.ToList();
            
            }

        }





        public bool DBFunctions { get; set; }
    }
}
