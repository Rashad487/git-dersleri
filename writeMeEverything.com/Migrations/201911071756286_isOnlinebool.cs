namespace writeMeEverything.com.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isOnlinebool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "isOnline", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "isOnline", c => c.String());
        }
    }
}
