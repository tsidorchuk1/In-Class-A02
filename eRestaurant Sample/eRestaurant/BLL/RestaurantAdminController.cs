using eRestaurant.DAL;
using eRestaurant.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.BLL
{
   public class RestaurantAdminController
   {
       #region Mangage Waiters



       #region Command

       public int AddWaiter(Waiter item)
       {
           using (RestaurantContext context = new RestaurantContext())
           {
               //todo Validation rules
               var added = context.Waiters.Add(item);
               context.SaveChanges();
               return added.WaiterID;

           }
       }

       public void UpdateWaiter(Waiter item)
       {
           using (RestaurantContext context = new RestaurantContext())

           
           {
               // to do validatiom
               var attached = context.Waiters.Attach(item);
               var existing = context.Entry<Waiter>(attached);
               existing.State = System.Data.Entity.EntityState.Modified;
               context.SaveChanges();
           }
       }

       public void DeleteWaiter(Waiter item)
       {
           using (RestaurantContext context = new RestaurantContext())
           {
               var existing = context.Waiters.Find(item.WaiterID);
               context.Waiters.Remove(existing);
               context.SaveChanges();

           }
       }


       #endregion
       #region Query

       public List<Waiter> ListAllWaiters()
       {
           using (RestaurantContext context = new RestaurantContext())
           {
               return context.Waiters.ToList();
           }
       
       }

       public Waiter GetWaiter(int waiterId)
       {
           using (RestaurantContext context = new RestaurantContext())
           {

               return context.Waiters.Find(waiterId);
           }
       
       
       }

       #endregion
       #endregion

       #region Mangage Tables
       #region Command
       #endregion
       #region Query
       #endregion
       #endregion

       #region Mangage Items
       #region Command
       #endregion
       #region Query
       #endregion
       #endregion

       #region Mangage Special Events
       #region Command
       #endregion
       #region Query
       #endregion
       #endregion




   }
}
