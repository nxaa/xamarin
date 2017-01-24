using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin2.Data.Models;

namespace Xamarin2.Data.Interfaces
{
    public interface IModel : IDisposable
    {
        DbSet<Order> Orders { get; }
        DbSet<MenuItem> MenuItems { get; }
        DbSet<Reservation> Reservations { get; }
        DbSet<Table> Tables { get; }
        DbSet<OrderItem> OrderItems { get; }
        int SaveChanges();
    }
}
