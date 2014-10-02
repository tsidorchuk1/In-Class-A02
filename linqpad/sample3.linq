<Query Kind="Expression">
  <Connection>
    <ID>f62e220c-2430-4edc-b22f-624343258e4f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from cat in MenuCategories
select new
{
Description = cat.Description, 
NumberofMenuItems= cat.Items.Count
}