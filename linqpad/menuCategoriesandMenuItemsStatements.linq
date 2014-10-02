<Query Kind="Statements">
  <Connection>
    <ID>f62e220c-2430-4edc-b22f-624343258e4f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

var data = from cat in MenuCategories
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
};

data.Dump("Menu Items by Category");  
// .Dump() is an extension method that is available in linq only