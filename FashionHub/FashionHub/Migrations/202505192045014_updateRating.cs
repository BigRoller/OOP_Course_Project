namespace FashionHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateRating : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Rating", c => c.Single(nullable: false));
        }
    }
}
