using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin2.Data.Models;

namespace Xamarin2.Test
{
    class TestTableDbSet : TestDbSet<Table>
    {
        public override Table Find(params object[] keyValues)
        {
            return this.SingleOrDefault(table => table.TableID == (int)keyValues.Single());
        }
    }
}
