<Query Kind="Expression">
  <Connection>
    <ID>5063a95f-38c9-453b-8c4e-b74471715ef8</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from data in Bills 
where data.PaidStatus == false
select new // UnpaidBillWithItemCount() // <-- name of poco/dto 
{
BillID= data.BillID,
ItemCount = data.BillItems.Count()
}