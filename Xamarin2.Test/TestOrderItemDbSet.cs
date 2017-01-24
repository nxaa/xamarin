using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin2.Data.Models;

namespace Xamarin2.Test
{
    class TestOrderItemDbSet : TestDbSet<OrderItem>
    {
        public override OrderItem Find(params object[] keyValues)
        {
            return this.SingleOrDefault(orderItem => orderItem.OrderItemID == (int)keyValues.Single());
        }
    }
}