namespace writeMeEverything.com.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isOnline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "isOnline", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "isOnline");
        }
    }
}
