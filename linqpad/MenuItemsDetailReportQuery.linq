<Query Kind="Expression">
  <Connection>
    <ID>99f2eb11-8558-4bcc-b27f-54a54984977e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//this query is for pulling out data to be used in a details report
//the query gets all th emenu items and there categories sorting them by category
//description aand then by the menu item description


from cat in Items
orderby cat.MenuCategory.Description, cat.Description
select new
{
	CategoryDescription = cat.MenuCategory.Description,
	ItemDescription = cat.Description,
	Price = cat.CurrentPrice,
	Calories = cat.Calories,
	Comment = cat.Comment,

}