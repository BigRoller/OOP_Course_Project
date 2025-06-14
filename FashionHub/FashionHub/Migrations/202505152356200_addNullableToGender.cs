namespace FashionHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addNullableToGender : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "Gender", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "Gender", c => c.Boolean(nullable: false));
        }
    }
}
