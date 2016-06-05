namespace HelpDeskTrain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CabNumber = c.String(nullable: false, maxLength: 50),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hardwares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Lifecycles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Opened = c.DateTime(nullable: false),
                        Distributed = c.DateTime(),
                        Proccesing = c.DateTime(),
                        Checking = c.DateTime(),
                        Closed = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orgtechnics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 50),
                        Serial = c.String(nullable: false, maxLength: 50),
                        UserId = c.Int(nullable: false),
                        HardwareId = c.Int(nullable: false),
                        ProducerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hardwares", t => t.HardwareId, cascadeDelete: true)
                .ForeignKey("dbo.Producers", t => t.ProducerId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.HardwareId)
                .Index(t => t.ProducerId);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Login = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Position = c.String(maxLength: 50),
                        DepartmentId = c.Int(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.DepartmentId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 200),
                        Comment = c.String(maxLength: 200),
                        Status = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        ActivId = c.Int(),
                        File = c.String(),
                        CategoryId = c.Int(),
                        UserId = c.Int(),
                        ExecutorId = c.Int(),
                        LifecycleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activs", t => t.ActivId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Users", t => t.ExecutorId)
                .ForeignKey("dbo.Lifecycles", t => t.LifecycleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ActivId)
                .Index(t => t.CategoryId)
                .Index(t => t.UserId)
                .Index(t => t.ExecutorId)
                .Index(t => t.LifecycleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "UserId", "dbo.Users");
            DropForeignKey("dbo.Requests", "LifecycleId", "dbo.Lifecycles");
            DropForeignKey("dbo.Requests", "ExecutorId", "dbo.Users");
            DropForeignKey("dbo.Requests", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Requests", "ActivId", "dbo.Activs");
            DropForeignKey("dbo.Orgtechnics", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Users", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Orgtechnics", "ProducerId", "dbo.Producers");
            DropForeignKey("dbo.Orgtechnics", "HardwareId", "dbo.Hardwares");
            DropForeignKey("dbo.Activs", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Requests", new[] { "LifecycleId" });
            DropIndex("dbo.Requests", new[] { "ExecutorId" });
            DropIndex("dbo.Requests", new[] { "UserId" });
            DropIndex("dbo.Requests", new[] { "CategoryId" });
            DropIndex("dbo.Requests", new[] { "ActivId" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Users", new[] { "DepartmentId" });
            DropIndex("dbo.Orgtechnics", new[] { "ProducerId" });
            DropIndex("dbo.Orgtechnics", new[] { "HardwareId" });
            DropIndex("dbo.Orgtechnics", new[] { "UserId" });
            DropIndex("dbo.Activs", new[] { "DepartmentId" });
            DropTable("dbo.Requests");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Producers");
            DropTable("dbo.Orgtechnics");
            DropTable("dbo.Lifecycles");
            DropTable("dbo.Hardwares");
            DropTable("dbo.Categories");
            DropTable("dbo.Departments");
            DropTable("dbo.Activs");
        }
    }
}
