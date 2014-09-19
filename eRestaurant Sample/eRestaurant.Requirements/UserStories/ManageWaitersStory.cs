using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.BDDfy;
using Xunit;
using Xunit.Extensions;

namespace eRestaurant.Requirements.UserStories
{
   [Story(AsA="Reataurant Manage",
       IWant="To manage muy wiaters",
       SoThat="I have enoguh staff")
    ]
    
  public  class ManageWaitersStory
    {
       [Fact]
       [AutoRollback]
       public void AddWaiterScenario()
       {
           int waiterId = -1;
           Waiter newGuy = new Waiter();
           this.Given(_ => GivenWaiterInformation(newGuy))
               .When(_ => WhenIAddTheWaiter(newGuy, out waiterId))
                .Then(_ => ThenTheWaiterExists(waiterId))
                .And(_ => TheWaiterDetailsMatch(waiterId, newGuy))
                .BDDfy();
       }

       private Task TheWaiterDetailsMatch(int waiterId, Waiter newGuy)
       {
           throw new NotImplementedException();
       }

       private Task ThenTheWaiterExists(int waiterId)
       {
           throw new NotImplementedException();
       }

       private Task WhenIAddTheWaiter(Waiter newGuy, out int waiterId)
       {
           throw new NotImplementedException();
       }

       private Task GivenWaiterInformation(Waiter newGuy)
       {
           throw new NotImplementedException();
       }

    }
}
