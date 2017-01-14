namespace Xamarin2.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class enumstatuses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Reservations", "Active");
            DropColumn("dbo.Reservations", "Used");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "Used", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reservations", "Active", c => c.Boolean(nullable: false));
            DropColumn("dbo.Reservations", "Status");
            DropColumn("dbo.Orders", "Status");
        }
    }
}
