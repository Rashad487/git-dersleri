namespace writeMeEverything.com.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeCahange4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "isOnlineUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "isOnlineUser", c => c.Boolean(nullable: false));
        }
    }
}
