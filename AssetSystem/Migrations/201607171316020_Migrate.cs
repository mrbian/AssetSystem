namespace AssetSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(),
                        Password = c.String(),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogicId = c.String(),
                        Title = c.String(),
                        Worth = c.Double(nullable: false),
                        PurchasingDate = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        Remark = c.String(),
                        EquipmentType_Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                        Admin_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentTypes", t => t.EquipmentType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Admins", t => t.Admin_Id, cascadeDelete: true)
                .Index(t => t.EquipmentType_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Admin_Id);
            
            CreateTable(
                "dbo.EquipmentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Type = c.Int(nullable: false),
                        BigEquipmentType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EquipmentTypes", t => t.BigEquipmentType_Id)
                .Index(t => t.BigEquipmentType_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogicId = c.String(),
                        Name = c.String(),
                        Position = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Histories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReturnDate = c.DateTime(nullable: false),
                        EquipmentTitle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "Admin_Id", "dbo.Admins");
            DropForeignKey("dbo.Equipments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.EquipmentTypes", "BigEquipmentType_Id", "dbo.EquipmentTypes");
            DropForeignKey("dbo.Equipments", "EquipmentType_Id", "dbo.EquipmentTypes");
            DropIndex("dbo.Equipments", new[] { "Admin_Id" });
            DropIndex("dbo.Equipments", new[] { "User_Id" });
            DropIndex("dbo.EquipmentTypes", new[] { "BigEquipmentType_Id" });
            DropIndex("dbo.Equipments", new[] { "EquipmentType_Id" });
            DropTable("dbo.Histories");
            DropTable("dbo.Users");
            DropTable("dbo.EquipmentTypes");
            DropTable("dbo.Equipments");
            DropTable("dbo.Admins");
        }
    }
}
