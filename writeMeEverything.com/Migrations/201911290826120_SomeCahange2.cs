namespace writeMeEverything.com.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeCahange2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "isOnlieUser", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "isOnlieUser");
        }
    }
}
