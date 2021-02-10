namespace PepegaRequiem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Games", "DeveloperID", "dbo.Developers");
            DropIndex("dbo.Games", new[] { "DeveloperID" });
            DropIndex("dbo.Games", new[] { "CategoryID" });
            AlterColumn("dbo.Games", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Games", "DeveloperID", c => c.Int(nullable: false));
            AlterColumn("dbo.Games", "CategoryID", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            CreateIndex("dbo.Games", "DeveloperID");
            CreateIndex("dbo.Games", "CategoryID");
            AddForeignKey("dbo.Games", "CategoryID", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Games", "DeveloperID", "dbo.Developers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "DeveloperID", "dbo.Developers");
            DropForeignKey("dbo.Games", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Games", new[] { "CategoryID" });
            DropIndex("dbo.Games", new[] { "DeveloperID" });
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Games", "CategoryID", c => c.Int());
            AlterColumn("dbo.Games", "DeveloperID", c => c.Int());
            AlterColumn("dbo.Games", "Name", c => c.String());
            CreateIndex("dbo.Games", "CategoryID");
            CreateIndex("dbo.Games", "DeveloperID");
            AddForeignKey("dbo.Games", "DeveloperID", "dbo.Developers", "Id");
            AddForeignKey("dbo.Games", "CategoryID", "dbo.Categories", "Id");
        }
    }
}
