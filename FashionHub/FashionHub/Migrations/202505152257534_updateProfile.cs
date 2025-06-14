namespace FashionHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProfile : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProfiles", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "Email", c => c.String());
        }
    }
}
