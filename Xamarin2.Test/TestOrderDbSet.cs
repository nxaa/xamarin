using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin2.Data.Models;

namespace Xamarin2.Test
{
    class TestOrderDbSet : TestDbSet<Order>
    {
        public override Order Find(params object[] keyValues)
        {
            return this.SingleOrDefault(order => order.OrderID == (int)keyValues.Single());
        }
    }
}
