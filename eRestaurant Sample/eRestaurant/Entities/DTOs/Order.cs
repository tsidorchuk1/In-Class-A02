using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Entities.DTOs
{
    public class Order
    {
        public int TableNumber { get; set; }
        public string Waiter { get; set; }
        public int WaiterID { get; set; }
        public int? BillID { get; set; }
        public bool Served { get; set; }
        public string OrderComments { get; set; }

        public decimal TotalAmount {
            get
            {
                decimal value = 0;
                if (Items != null)
                    value = Items.Sum(x => x.ItemTotal);
                return value;
            }
            
            
             }

        // create a property called TotalAmount which will
        // sum up the item totals in the item collection
        // remember to check if ITems is null
        // use a linq method .sum() with a lambda to get the total



        public IList<OrderItem> Items { get; set; }

    }
}
