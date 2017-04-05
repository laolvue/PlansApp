namespace PlansApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CheckInMessageAndEmailPropertiesImproved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckInMessages", "planDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CheckInMessages", "planDate");
        }
    }
}
