<Query Kind="Statements">
  <Connection>
    <ID>f4a20afb-04bd-4e25-a272-ee5c5d8fe9a7</ID>
    <Persist>true</Persist>
    <Server>sidorchuk-pc\sqlexpress</Server>
    <Database>WorkSchedule</Database>
  </Connection>
</Query>

  var results = from info in EmployeeSkills
                           orderby info.Skill.Description   
                             select new
                             {
                                 Description = info.Skill.Description,
                                 FullName = info.Employee.FirstName + ", " + info.Employee.LastName,
                                 Phone = info.Employee.HomePhone,
                                 Level = info.Level.ToString(),
                                 YearsOfExperience = info.YearsOfExperience,
                             };
   results.Count().Dump();			
results.Dump();           