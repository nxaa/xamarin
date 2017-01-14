namespace Xamarin2.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableOrderCloseDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "CloseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "CloseDate", c => c.DateTime(nullable: false));
        }
    }
}
