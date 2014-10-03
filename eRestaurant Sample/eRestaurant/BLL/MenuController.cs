using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;// needed for lambda version of .Include() method
using eRestaurant.DAL; 


namespace eRestaurant.BLL
{
    [DataObject]
   public class MenuController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Item> ListMenuItems()
        {
            using (var context = new RestaurantContext())
            {
                return context.Items.Include(x => x.Category).ToList();
            
            }
        
        }
    }
}
