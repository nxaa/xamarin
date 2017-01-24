using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin2.Data.Models;

namespace Xamarin2.Test
{
    class TestMenuItemDbSet : TestDbSet<MenuItem>
    {
        public override MenuItem Find(params object[] keyValues)
        {
            return this.SingleOrDefault(menuItem => menuItem.MenuItemID == (int)keyValues.Single());
        }
    }
}
