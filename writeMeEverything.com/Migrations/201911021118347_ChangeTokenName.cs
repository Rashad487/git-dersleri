namespace writeMeEverything.com.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTokenName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Token", c => c.String());
            DropColumn("dbo.Users", "Tocken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Tocken", c => c.String());
            DropColumn("dbo.Users", "Token");
        }
    }
}
