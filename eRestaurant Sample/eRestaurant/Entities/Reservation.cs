using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRestaurant.Entities
{
    public class Reservation
    {
        // constants for valid reservation status values
        // B = Booked; C = Complete; N = No-show; X = Cancelled
        public const string Booked = "B";
        public const string Complete = "C";
        public const string NoShow = "N";
        public const string Cancelled = "X";

        public int ReservationID { get; set; }

        [Required]
        [StringLength(40)]
        public string CustomerName { get; set; }
        public DateTime ReservationDate { get; set; }
        [Range(1, 16)]
        public int NumberInParty { get; set; }
        [StringLength(15)]
        public string ContactPhone { get; set; }
        // TODO: (in BLL) Validate acceptable values using constants above
        [Required, StringLength(1, MinimumLength = 1)]
        public string ReservationStatus { get; set; }
        [StringLength(1)]
        public string EventCode { get; set; }

      

        #region Navigation Properties
        public virtual ICollection<Table> Tables { get; set; }
        public virtual SpecialEvent SpecialEvent { get; set; }
        #endregion

    }
}
