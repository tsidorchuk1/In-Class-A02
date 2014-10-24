<Query Kind="Program">
  <Connection>
    <ID>99f2eb11-8558-4bcc-b27f-54a54984977e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
	// USe a lambda expression to get the maximimum
	// a lambda is simply a shorthanded version of a function call
	// that is ideal for anonymous delegates
	Bills.Max(x=>x.BillDate).Dump();
	
	
	// longer version using an actual method name
	// that we pass in to the max method
	// note that the max method is overloaded therefore we
	// need to specify inthe generic identifier of the method
	// which version we are using
	Bills.Max<Bills, DateTime>(GetProperty).Dump();
}

// Define other methods and classes here
private DateTime GetProperty(Bills x)
{

return x.BillDate;


}