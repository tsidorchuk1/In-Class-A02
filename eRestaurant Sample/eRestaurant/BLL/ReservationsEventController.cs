using eRestaurant.DAL;
using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.BLL
{
    [DataObject]
    
    public class ReservationsEventController
    {
 [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<SpecialEvent> ListMenuItems()
        {
            using (var context = new RestaurantContext())
            {
                return context.Items.Include(x => x.Category).ToList();

            }

        }


    }
}
