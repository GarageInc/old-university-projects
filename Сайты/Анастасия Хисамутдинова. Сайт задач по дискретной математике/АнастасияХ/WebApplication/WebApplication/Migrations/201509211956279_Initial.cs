namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthorId = c.String(maxLength: 128),
                        Karma = c.Int(nullable: false),
                        Text = c.String(),
                        ParentId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        AddDateTime = c.DateTime(nullable: false),
                        ReqId = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                        Request_Id = c.Int(),
                        ApplicationUser_Id2 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .ForeignKey("dbo.Requests", t => t.Request_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id2)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.RequestSolutions", t => t.ReqId)
                .Index(t => t.AuthorId)
                .Index(t => t.ReqId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1)
                .Index(t => t.Request_Id)
                .Index(t => t.ApplicationUser_Id2);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        Karma = c.Int(nullable: false),
                        LastVisition = c.DateTime(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        UserInfo = c.String(),
                        IsBlocked = c.Boolean(nullable: false),
                        BlockDate = c.DateTime(nullable: false),
                        BlockReason = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Request_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Requests", t => t.Request_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Request_Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Size = c.Int(nullable: false),
                        Type = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactAdress = c.String(nullable: false, maxLength: 200),
                        AuthorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.RecallMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        AuthorId = c.String(maxLength: 128),
                        Karma = c.Int(nullable: false),
                        ParentId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        AddDateTime = c.DateTime(nullable: false),
                        AboutSite = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                        ApplicationUser_Id2 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id1)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id2)
                .Index(t => t.AuthorId)
                .Index(t => t.UserId)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id1)
                .Index(t => t.ApplicationUser_Id2);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        DocumentId = c.Int(),
                        AuthorId = c.String(maxLength: 128),
                        Deadline = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        RightRequestSolutionId = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .ForeignKey("dbo.Documents", t => t.DocumentId)
                .ForeignKey("dbo.RequestSolutions", t => t.RightRequestSolutionId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.DocumentId)
                .Index(t => t.AuthorId)
                .Index(t => t.RightRequestSolutionId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.RequestSolutions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RequestId = c.Int(),
                        IsRightSolution = c.Boolean(nullable: false),
                        DocumentId = c.Int(),
                        AuthorId = c.String(maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        IsChecked = c.Boolean(nullable: false),
                        Right = c.Boolean(nullable: false),
                        Request_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.DocumentId)
                .ForeignKey("dbo.Requests", t => t.RequestId)
                .ForeignKey("dbo.Requests", t => t.Request_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
                .Index(t => t.RequestId)
                .Index(t => t.DocumentId)
                .Index(t => t.AuthorId)
                .Index(t => t.Request_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Comments", "ReqId", "dbo.RequestSolutions");
            DropForeignKey("dbo.Comments", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RecallMessages", "ApplicationUser_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ApplicationUser_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RequestSolutions", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Requests", "RightRequestSolutionId", "dbo.RequestSolutions");
            DropForeignKey("dbo.RequestSolutions", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.RequestSolutions", "RequestId", "dbo.Requests");
            DropForeignKey("dbo.RequestSolutions", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.AspNetUsers", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.Requests", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.Comments", "Request_Id", "dbo.Requests");
            DropForeignKey("dbo.Requests", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RecallMessages", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RecallMessages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.RecallMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.RecallMessages", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Contacts", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Documents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.RequestSolutions", new[] { "Request_Id" });
            DropIndex("dbo.RequestSolutions", new[] { "AuthorId" });
            DropIndex("dbo.RequestSolutions", new[] { "DocumentId" });
            DropIndex("dbo.RequestSolutions", new[] { "RequestId" });
            DropIndex("dbo.Requests", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Requests", new[] { "RightRequestSolutionId" });
            DropIndex("dbo.Requests", new[] { "AuthorId" });
            DropIndex("dbo.Requests", new[] { "DocumentId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.RecallMessages", new[] { "ApplicationUser_Id2" });
            DropIndex("dbo.RecallMessages", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.RecallMessages", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RecallMessages", new[] { "UserId" });
            DropIndex("dbo.RecallMessages", new[] { "AuthorId" });
            DropIndex("dbo.Contacts", new[] { "AuthorId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Documents", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Request_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "ApplicationUser_Id2" });
            DropIndex("dbo.Comments", new[] { "Request_Id" });
            DropIndex("dbo.Comments", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Comments", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Comments", new[] { "ReqId" });
            DropIndex("dbo.Comments", new[] { "AuthorId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.RequestSolutions");
            DropTable("dbo.Requests");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.RecallMessages");
            DropTable("dbo.Contacts");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Documents");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
        }
    }
}
