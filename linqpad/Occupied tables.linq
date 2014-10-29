<Query Kind="Statements">
  <Connection>
    <ID>4aa93dde-870a-48f3-a0de-3506489effb8</ID>
    <Persist>true</Persist>
    <Server>sidorchuk-pc\sqlexpress</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//find out what information on the tables in the restraunt at a specific date/time

//create a date and time object to use for sample input data
var date = Bills.Max(b => b.BillDate).Date;
var time = Bills.Max(b => b.BillDate).TimeOfDay.Add(new TimeSpan(0, 30, 0));
date.Add(time).Dump("The Test data/time i am using");

//step 1 - get the table info along with any walk in bills and reservations bills for the specific time slot
var step1 = from data in Tables
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
						  && billing.BillDate.TimeOfDay <= time
						  && (!billing.OrderPaid.HasValue || billing.OrderPaid.Value >= time)
						  //&& (!billing,
						  select billing,
						  //this sub query gets the bills for reservations
				Reservations = from booking in data.ReservationTables
							   from billing in booking.Reservation.Bills
							   where
							   		
							 billing.BillDate.Year == date.Year
						  && billing.BillDate.Month == date.Month
						  && billing.BillDate.Day == date.Day
						  && billing.BillDate.TimeOfDay <= time
						  && (!billing.OrderPaid.HasValue || billing.OrderPaid.Value >= time)
						  //&& (!billing,
						  select billing
				
				
				
			 };
step1.Dump();


// Step 2 - Union the walk-in bills and the reservation bills while extracting the relebant bill info
var step2 = from data in step1.ToList() //to.list forces the first result set to be in memory
			select new
			{
				Table = data.Table,
				Seating = data.Seating,
				CommonBilling = from info in data.Bills.Union(data.Reservations)
				select info
				
			};
			
step2.Dump();

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
step3.Dump();


// step 4 - builf our intended seating summary info
var step4 = from data in step3
select new //seatingSummary()
			{
				Table = data.Table,
				Seating = data.Seating,
				Taken = data.Taken,
				//use a tenary expression to conditonally get a bill id (if it exists)
				BillID = data.Taken ? //if (data.Taken)
						 data.CommonBilling.BillID //value to use if true
						 : (int?) null //value to use if false
				
			};
	step4.Dump();