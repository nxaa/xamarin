namespace Xamarin2.Data
{
    using Interfaces;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Model : DbContext, IModel
    {
        // Your context has been configured to use a 'Model' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Xamarin2.Data.Model' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model' 
        // connection string in the application configuration file.
        public Model()
            : base("name=LocalConnection")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<Table> Tables { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<MenuItem> MenuItems { get; set; }

        public virtual DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().HasOptional(t => t.Order).WithOptionalPrincipal(t => t.Reservation).Map(t => t.MapKey("ReservationID"));

            //modelBuilder.Entity<Reservation>().HasMany(a => a.Tables).WithMany()
            //.Map(x =>
            //{
            //    x.MapLeftKey("ReservationID");
            //    x.MapRightKey("TableID");
            //    x.ToTable("ReservationTables");
            //});

            //modelBuilder.Entity<Order>().HasMany(a => a.Tables).WithMany()
            //.Map(x =>
            //{
            //    x.MapLeftKey("OrderID");
            //    x.MapRightKey("TableID");
            //    x.ToTable("OrderTables");
            //});

            //modelBuilder.Entity<Order>().HasMany(a => a.MenuItems).WithMany()
            //.Map(x =>
            //{
            //    x.MapLeftKey("OrderID");
            //    x.MapRightKey("MenuItemID");
            //    x.ToTable("OrderMenuItems");
            //});
        }
    }

}