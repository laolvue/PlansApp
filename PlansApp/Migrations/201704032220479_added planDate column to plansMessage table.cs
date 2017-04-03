namespace PlansApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedplanDatecolumntoplansMessagetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlansMessages", "planDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlansMessages", "planDate");
        }
    }
}
