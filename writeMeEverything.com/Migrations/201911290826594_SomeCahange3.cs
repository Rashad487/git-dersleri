namespace writeMeEverything.com.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeCahange3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "isOnlineUser", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "isOnlieUser");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "isOnlieUser", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "isOnlineUser");
        }
    }
}
