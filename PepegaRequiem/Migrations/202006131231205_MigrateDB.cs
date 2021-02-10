namespace PepegaRequiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DeveloperID = c.Int(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        CategoryID = c.Int(),
                        Image = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Developers", t => t.DeveloperID)
                .Index(t => t.DeveloperID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameId = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        User = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Users",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Email = c.String(),
                    Password = c.String(),
                    IsAdmin = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "DeveloperID", "dbo.Developers");
            DropForeignKey("dbo.Games", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Games", new[] { "CategoryID" });
            DropIndex("dbo.Games", new[] { "DeveloperID" });
            DropTable("dbo.Users");
            DropTable("dbo.Purchases");
            DropTable("dbo.Developers");
            DropTable("dbo.Games");
            DropTable("dbo.Categories");
        }
    }
}
