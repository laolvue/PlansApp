namespace PlansApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrecipientcategorytable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RecipientCategories",
                c => new
                    {
                        recipientCategoryId = c.Int(nullable: false, identity: true),
                        categoryName = c.String(),
                    })
                .PrimaryKey(t => t.recipientCategoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RecipientCategories");
        }
    }
}
