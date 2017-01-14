namespace Xamarin2.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        MenuItemID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        Active = c.Boolean(nullable: false),
                        MenuItemCategory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuItemID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        CloseDate = c.DateTime(nullable: false),
                        ReservationID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Reservations", t => t.ReservationID)
                .Index(t => t.ReservationID);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        NumberOfPeople = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Used = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationID);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        TableID = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        NumberOfPeople = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TableID);
            
            CreateTable(
                "dbo.OrderMenuItems",
                c => new
                    {
                        Order_OrderID = c.Int(nullable: false),
                        MenuItem_MenuItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderID, t.MenuItem_MenuItemID })
                .ForeignKey("dbo.Orders", t => t.Order_OrderID, cascadeDelete: true)
                .ForeignKey("dbo.MenuItems", t => t.MenuItem_MenuItemID, cascadeDelete: true)
                .Index(t => t.Order_OrderID)
                .Index(t => t.MenuItem_MenuItemID);
            
            CreateTable(
                "dbo.TableOrders",
                c => new
                    {
                        Table_TableID = c.Int(nullable: false),
                        Order_OrderID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Table_TableID, t.Order_OrderID })
                .ForeignKey("dbo.Tables", t => t.Table_TableID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID, cascadeDelete: true)
                .Index(t => t.Table_TableID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.TableReservations",
                c => new
                    {
                        Table_TableID = c.Int(nullable: false),
                        Reservation_ReservationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Table_TableID, t.Reservation_ReservationID })
                .ForeignKey("dbo.Tables", t => t.Table_TableID, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.Reservation_ReservationID, cascadeDelete: true)
                .Index(t => t.Table_TableID)
                .Index(t => t.Reservation_ReservationID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TableReservations", "Reservation_ReservationID", "dbo.Reservations");
            DropForeignKey("dbo.TableReservations", "Table_TableID", "dbo.Tables");
            DropForeignKey("dbo.TableOrders", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.TableOrders", "Table_TableID", "dbo.Tables");
            DropForeignKey("dbo.Orders", "ReservationID", "dbo.Reservations");
            DropForeignKey("dbo.OrderMenuItems", "MenuItem_MenuItemID", "dbo.MenuItems");
            DropForeignKey("dbo.OrderMenuItems", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.TableReservations", new[] { "Reservation_ReservationID" });
            DropIndex("dbo.TableReservations", new[] { "Table_TableID" });
            DropIndex("dbo.TableOrders", new[] { "Order_OrderID" });
            DropIndex("dbo.TableOrders", new[] { "Table_TableID" });
            DropIndex("dbo.OrderMenuItems", new[] { "MenuItem_MenuItemID" });
            DropIndex("dbo.OrderMenuItems", new[] { "Order_OrderID" });
            DropIndex("dbo.Orders", new[] { "ReservationID" });
            DropTable("dbo.TableReservations");
            DropTable("dbo.TableOrders");
            DropTable("dbo.OrderMenuItems");
            DropTable("dbo.Tables");
            DropTable("dbo.Reservations");
            DropTable("dbo.Orders");
            DropTable("dbo.MenuItems");
        }
    }
}
