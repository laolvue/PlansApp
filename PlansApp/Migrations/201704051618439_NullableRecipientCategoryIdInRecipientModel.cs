namespace PlansApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableRecipientCategoryIdInRecipientModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipients", "recipientCategoryId", "dbo.RecipientCategories");
            DropIndex("dbo.Recipients", new[] { "recipientCategoryId" });
            AlterColumn("dbo.Recipients", "recipientCategoryId", c => c.Int());
            CreateIndex("dbo.Recipients", "recipientCategoryId");
            AddForeignKey("dbo.Recipients", "recipientCategoryId", "dbo.RecipientCategories", "recipientCategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipients", "recipientCategoryId", "dbo.RecipientCategories");
            DropIndex("dbo.Recipients", new[] { "recipientCategoryId" });
            AlterColumn("dbo.Recipients", "recipientCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Recipients", "recipientCategoryId");
            AddForeignKey("dbo.Recipients", "recipientCategoryId", "dbo.RecipientCategories", "recipientCategoryId", cascadeDelete: true);
        }
    }
}
