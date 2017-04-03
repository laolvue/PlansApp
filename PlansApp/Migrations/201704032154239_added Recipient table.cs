namespace PlansApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRecipienttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipients",
                c => new
                    {
                        recipientId = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        lastName = c.String(),
                        UserEmail = c.String(),
                        emailAddress = c.String(),
                        nickName = c.String(),
                        recipientCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.recipientId)
                .ForeignKey("dbo.RecipientCategories", t => t.recipientCategoryId, cascadeDelete: true)
                .Index(t => t.recipientCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipients", "recipientCategoryId", "dbo.RecipientCategories");
            DropIndex("dbo.Recipients", new[] { "recipientCategoryId" });
            DropTable("dbo.Recipients");
        }
    }
}
