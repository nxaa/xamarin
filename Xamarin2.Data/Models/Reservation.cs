using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin2.Data.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public DateTime Date { get; set; }

        public int NumberOfPeople { get; set; }

        public ReservationStatus Status { get; set; }

        public virtual Order Order { get; set; }

        public virtual ICollection<Table> Tables { get; set; }
    }

    public enum ReservationStatus
    {
        New,
        Completed,
        Cancelled
    }
}
