namespace BugTracker.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OptionalUserIdForErrors : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Errors", "UserId", "dbo.Users");
            DropIndex("dbo.Errors", new[] { "UserId" });
            AlterColumn("dbo.Errors", "UserId", c => c.Int());
            AddForeignKey("dbo.Errors", "UserId", "dbo.Users", "UserId");
            CreateIndex("dbo.Errors", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Errors", new[] { "UserId" });
            DropForeignKey("dbo.Errors", "UserId", "dbo.Users");
            AlterColumn("dbo.Errors", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Errors", "UserId");
            AddForeignKey("dbo.Errors", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
