using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin2.Data.Models
{
    public class Table
    {
        public int TableID { get; set; }

        public int Number { get; set; }

        public int NumberOfPeople { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
