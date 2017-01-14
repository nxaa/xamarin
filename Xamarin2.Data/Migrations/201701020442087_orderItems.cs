namespace Xamarin2.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderItems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderMenuItems", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderMenuItems", "MenuItem_MenuItemID", "dbo.MenuItems");
            DropIndex("dbo.OrderMenuItems", new[] { "Order_OrderID" });
            DropIndex("dbo.OrderMenuItems", new[] { "MenuItem_MenuItemID" });
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        MenuItem_MenuItemID = c.Int(),
                        Order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderItemID)
                .ForeignKey("dbo.MenuItems", t => t.MenuItem_MenuItemID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .Index(t => t.MenuItem_MenuItemID)
                .Index(t => t.Order_OrderID);
            
            DropTable("dbo.OrderMenuItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderMenuItems",
                c => new
                    {
                        Order_OrderID = c.Int(nullable: false),
                        MenuItem_MenuItemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderID, t.MenuItem_MenuItemID });
            
            DropForeignKey("dbo.OrderItems", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderItems", "MenuItem_MenuItemID", "dbo.MenuItems");
            DropIndex("dbo.OrderItems", new[] { "Order_OrderID" });
            DropIndex("dbo.OrderItems", new[] { "MenuItem_MenuItemID" });
            DropTable("dbo.OrderItems");
            CreateIndex("dbo.OrderMenuItems", "MenuItem_MenuItemID");
            CreateIndex("dbo.OrderMenuItems", "Order_OrderID");
            AddForeignKey("dbo.OrderMenuItems", "MenuItem_MenuItemID", "dbo.MenuItems", "MenuItemID", cascadeDelete: true);
            AddForeignKey("dbo.OrderMenuItems", "Order_OrderID", "dbo.Orders", "OrderID", cascadeDelete: true);
        }
    }
}
