namespace PlansApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableRecipientCategorIdInCheckInMessageModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CheckInMessages", "recipientCategoryId", "dbo.RecipientCategories");
            DropIndex("dbo.CheckInMessages", new[] { "recipientCategoryId" });
            AlterColumn("dbo.CheckInMessages", "recipientCategoryId", c => c.Int());
            CreateIndex("dbo.CheckInMessages", "recipientCategoryId");
            AddForeignKey("dbo.CheckInMessages", "recipientCategoryId", "dbo.RecipientCategories", "recipientCategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckInMessages", "recipientCategoryId", "dbo.RecipientCategories");
            DropIndex("dbo.CheckInMessages", new[] { "recipientCategoryId" });
            AlterColumn("dbo.CheckInMessages", "recipientCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.CheckInMessages", "recipientCategoryId");
            AddForeignKey("dbo.CheckInMessages", "recipientCategoryId", "dbo.RecipientCategories", "recipientCategoryId", cascadeDelete: true);
        }
    }
}
