namespace writeMeEverything.com.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFrendsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Friends", "Message", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Friends", "Message");
        }
    }
}
