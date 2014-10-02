<Query Kind="Expression">
  <Connection>
    <ID>f62e220c-2430-4edc-b22f-624343258e4f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from cat in MenuCategories
orderby cat.Description
select new
{
Description = cat.Description, 
MenuItems = from food in cat.Items
	where food.Active
	select new 
	{
		Description = food.Description, 
		Price = food.CurrentPrice
	
	}
}