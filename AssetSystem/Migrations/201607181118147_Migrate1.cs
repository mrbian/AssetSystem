namespace AssetSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Histories", "UserName", c => c.String());
            AddColumn("dbo.Histories", "AdminAccount", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Histories", "AdminAccount");
            DropColumn("dbo.Histories", "UserName");
        }
    }
}
