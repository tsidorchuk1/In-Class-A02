using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Entities
{
   public class Table
    {
      // in EF, by convention , if there is a property withb the same name as the class but with the suffix id , then EF will assume that this property is mapping to a primary key column on the  database tables 
       
     public  Table()
     {
             Available = true;
      }
       
       public int TableID { get; set; }
       [Required(ErrorMessage = "Table Number is required")]
       [Range(1, 25, ErrorMessage = "Table Number must be a positive number")]
       public byte TableNumber { get; set; }
       public bool Smoking { get; set; }
       public int Capacity { get; set; }
       public bool Available { get; set; }


        #region Navigation Properties
       public virtual ICollection<Reservation> Reservations { get; set; }
        #endregion
    }
}
