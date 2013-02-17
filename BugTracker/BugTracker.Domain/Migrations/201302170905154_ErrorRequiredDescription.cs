namespace BugTracker.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ErrorRequiredDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Errors", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Errors", "Description", c => c.String());
        }
    }
}
