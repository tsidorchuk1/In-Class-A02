<Query Kind="Statements">
  <Connection>
    <ID>f62e220c-2430-4edc-b22f-624343258e4f</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

var result = from info in BillItems
			orderby info.Item.MenuCategory.Description, info.Item.Description
			select new
			{
				CategoryDescription = info.Item.MenuCategory.Description,
				ItemDescription = info.Item.Description,
				Quantity = info.Quantity,
				Price = info.SalePrice * info.Quantity,
				Cost = info.UnitCost * info.Quantity
			};
result.Count().Dump();			
result.Dump();

	