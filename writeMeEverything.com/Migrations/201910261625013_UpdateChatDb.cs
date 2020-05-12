namespace writeMeEverything.com.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChatDb : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Chats", "ChatId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Chats", "ChatId", c => c.Int(nullable: false));
        }
    }
}
