using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin2.Data.Interfaces;
using Xamarin2.Data.Models;

namespace Xamarin2.Test
{
    class TestModel : IModel
    {
        public TestModel()
        {
            Reservations = new TestReservationDbSet();
            Tables = new TestTableDbSet();
            Orders = new TestOrderDbSet();
            OrderItems = new TestOrderItemDbSet();
            MenuItems = new TestMenuItemDbSet();
        }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }


        public void Dispose()
        {

        }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
