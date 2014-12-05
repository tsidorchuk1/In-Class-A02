<Query Kind="Statements">
  <Connection>
    <ID>64c96776-29f5-42e7-8136-cb312309ef39</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eTools</Database>
  </Connection>
</Query>

 var results = from cat in Employees
                     
                          select new // POCOs.
                          {
                            
                              PositionDescription = cat.Position.Description,
                              FullName = cat.FirstName + ", " + cat.LastName,
                              HiredDate = cat.DateHired,
                              ReleasedDate = cat.DateReleased
						};
						
						
						 results.Dump();// this was .Dump() in Linqpad