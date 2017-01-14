using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin2.Data.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public OrderStatus Status { get; set; }

        public virtual Reservation Reservation { get; set; }

        public virtual ICollection<Table> Tables { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

    public enum OrderStatus
    {
        InProgress,
        Completed
    }
}
