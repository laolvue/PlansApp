namespace PlansApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedplansmessagetable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlansMessages",
                c => new
                    {
                        plansMessageId = c.Int(nullable: false, identity: true),
                        recipientCategoryId = c.Int(nullable: false),
                        recipientId = c.Int(nullable: false),
                        Location = c.String(),
                        introMessage = c.String(),
                        closingMessage = c.String(),
                    })
                .PrimaryKey(t => t.plansMessageId)
                .ForeignKey("dbo.Recipients", t => t.recipientId, cascadeDelete: true)
                .ForeignKey("dbo.RecipientCategories", t => t.recipientCategoryId, cascadeDelete: false)
                .Index(t => t.recipientCategoryId)
                .Index(t => t.recipientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlansMessages", "recipientCategoryId", "dbo.RecipientCategories");
            DropForeignKey("dbo.PlansMessages", "recipientId", "dbo.Recipients");
            DropIndex("dbo.PlansMessages", new[] { "recipientId" });
            DropIndex("dbo.PlansMessages", new[] { "recipientCategoryId" });
            DropTable("dbo.PlansMessages");
        }
    }
}
