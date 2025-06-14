namespace FashionHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NearestDeliveryDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "NearestDeliveryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "NearestDeliveryDate");
        }
    }
}
