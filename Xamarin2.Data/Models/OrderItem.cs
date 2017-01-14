using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin2.Data.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }

        public int Quantity { get; set; }

        public virtual Order Order { get; set; }

        public virtual MenuItem MenuItem { get; set; }
    }
}
