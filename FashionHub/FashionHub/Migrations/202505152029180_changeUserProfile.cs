namespace FashionHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUserProfile : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfiles", "Phone");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "Phone", c => c.String());
        }
    }
}
