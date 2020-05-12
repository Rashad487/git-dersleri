namespace writeMeEverything.com.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropSocial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SocialLinks", "SocialId", "dbo.Socials");
            DropForeignKey("dbo.SocialLinks", "UserId", "dbo.Users");
            DropIndex("dbo.SocialLinks", new[] { "UserId" });
            DropIndex("dbo.SocialLinks", new[] { "SocialId" });
            DropTable("dbo.SocialLinks");
            DropTable("dbo.Socials");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Socials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SocialLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SocialId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.SocialLinks", "SocialId");
            CreateIndex("dbo.SocialLinks", "UserId");
            AddForeignKey("dbo.SocialLinks", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SocialLinks", "SocialId", "dbo.Socials", "Id", cascadeDelete: true);
        }
    }
}
