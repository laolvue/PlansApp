namespace PlansApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcheckinmessagetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CheckInMessages",
                c => new
                    {
                        checkInMessageId = c.Int(nullable: false, identity: true),
                        recipientCategoryId = c.Int(nullable: false),
                        recipientId = c.Int(nullable: false),
                        Location = c.String(),
                        introMessage = c.String(),
                        closingMessage = c.String(),
                        deadline = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.checkInMessageId)
                .ForeignKey("dbo.Recipients", t => t.recipientId, cascadeDelete: true)
                .ForeignKey("dbo.RecipientCategories", t => t.recipientCategoryId, cascadeDelete: false)
                .Index(t => t.recipientCategoryId)
                .Index(t => t.recipientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckInMessages", "recipientCategoryId", "dbo.RecipientCategories");
            DropForeignKey("dbo.CheckInMessages", "recipientId", "dbo.Recipients");
            DropIndex("dbo.CheckInMessages", new[] { "recipientId" });
            DropIndex("dbo.CheckInMessages", new[] { "recipientCategoryId" });
            DropTable("dbo.CheckInMessages");
        }
    }
}
