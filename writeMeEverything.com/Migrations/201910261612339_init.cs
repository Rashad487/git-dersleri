namespace writeMeEverything.com.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateAt = c.DateTime(nullable: false),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        ChatId = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Users", t => t.ReceiverId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: false)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 50),
                        Lastname = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 100),
                        Verify = c.Boolean(nullable: false),
                        VerifyText = c.String(),
                        Lastseen = c.DateTime(nullable: false),
                        Tocken = c.String(),
                        ResetText = c.String(),
                        Avatar = c.String(),
                        About = c.String(maxLength: 500),
                        Phone = c.String(maxLength: 10),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Friends",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        AcceptorId = c.Int(nullable: false),
                        IsFriends = c.Boolean(nullable: false),
                        IsBlocked = c.Boolean(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.AcceptorId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.SenderId)
                .Index(t => t.AcceptorId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FriendId = c.Int(nullable: false),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Friends", t => t.FriendId, cascadeDelete: true)
                .Index(t => t.FriendId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        ChatId = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chats", t => t.ChatId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ReceiverId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.SenderId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.ChatId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.SocialLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SocialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Socials", t => t.SocialId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SocialId);
            
            CreateTable(
                "dbo.Socials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chats", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Chats", "ReceiverId", "dbo.Users");
            DropForeignKey("dbo.SocialLinks", "UserId", "dbo.Users");
            DropForeignKey("dbo.SocialLinks", "SocialId", "dbo.Socials");
            DropForeignKey("dbo.Messages", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ReceiverId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ChatId", "dbo.Chats");
            DropForeignKey("dbo.Friends", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Friends", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Media", "FriendId", "dbo.Friends");
            DropForeignKey("dbo.Friends", "AcceptorId", "dbo.Users");
            DropForeignKey("dbo.Chats", "User_Id", "dbo.Users");
            DropIndex("dbo.SocialLinks", new[] { "SocialId" });
            DropIndex("dbo.SocialLinks", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "User_Id" });
            DropIndex("dbo.Messages", new[] { "ChatId" });
            DropIndex("dbo.Messages", new[] { "ReceiverId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.Media", new[] { "FriendId" });
            DropIndex("dbo.Friends", new[] { "User_Id" });
            DropIndex("dbo.Friends", new[] { "AcceptorId" });
            DropIndex("dbo.Friends", new[] { "SenderId" });
            DropIndex("dbo.Chats", new[] { "User_Id" });
            DropIndex("dbo.Chats", new[] { "ReceiverId" });
            DropIndex("dbo.Chats", new[] { "SenderId" });
            DropTable("dbo.Socials");
            DropTable("dbo.SocialLinks");
            DropTable("dbo.Messages");
            DropTable("dbo.Media");
            DropTable("dbo.Friends");
            DropTable("dbo.Users");
            DropTable("dbo.Chats");
        }
    }
}
