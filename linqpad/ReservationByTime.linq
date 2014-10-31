<Query Kind="Program">
  <Connection>
    <ID>5063a95f-38c9-453b-8c4e-b74471715ef8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main ()
{
	var date = new DateTime(2014,10,24);
	date.Dump();
	ReservationsByTime(date).Dump();
}

//define other methoids and classes here

public List<dynamic> ReservationsByTime(DateTime date)
{
	var result = from data in Reservations
	where data.ReservationDate.Year == date.Year
		&& data.ReservationDate.Month == date.Month
		&& data.ReservationDate.Day == date.Day
		&& data.ReservationStatus == 'B' //Reservation.Booked
		select new //DTOs.ReservationSumary()
	{
		Name = data.CustomerName,
		Date = data.ReservationDate,
		NumberInparty = data.NumberInParty,
		Status = data.ReservationStatus,
		Event = data.SpecialEvents.Description,
		Contact = data.ContactPhone,
		Tables = from seat in data.ReservationTables
				select seat.Table.TableNumber
	};

var finalResult = from item in result	
				group item by item.Date.TimeOfDay;
return finalResult.ToList<dynamic>();
	
}