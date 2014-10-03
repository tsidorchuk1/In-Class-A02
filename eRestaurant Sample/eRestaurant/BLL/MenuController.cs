using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;// needed for lambda version of .Include() method
using eRestaurant.DAL;
using eRestaurant.Entities.DTOs; 


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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Category> ListCategorizedItems()
        {
            using (var context = new RestaurantContext())
            {
                var data = from cat in context.MenuCategories
                           orderby cat.Description
                           select new Category()
                           {
                               Description = cat.Description,
                               MenuItems = from item in cat.MenuItems
                                           where item.Active
                                           orderby item.Description
                                           select new MenuItem()
                                           {
                                               Description = item.Description,
                                               Price = item.CurrentPrice,
                                               Calories = item.Calories,
                                               Comment = item.Comment
                                           }



                           };
                return data.ToList();
            }
        
        }
    
    
    }
}
