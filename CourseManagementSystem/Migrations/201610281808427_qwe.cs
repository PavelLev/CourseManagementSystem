namespace CourseManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qwe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseMessages",
                c => new
                    {
                        CourseMessageId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CreationDateTime = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ThreadId = c.Int(nullable: false),
                        Thread_CourseThreadId = c.Int(),
                    })
                .PrimaryKey(t => t.CourseMessageId)
                .ForeignKey("dbo.CourseThreads", t => t.Thread_CourseThreadId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Thread_CourseThreadId);
            
            CreateTable(
                "dbo.CourseThreads",
                c => new
                    {
                        CourseThreadId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastChangeDateTime = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseThreadId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        EnrollmentID = c.Int(nullable: false, identity: true),
                        totalMark = c.Int(),
                        CourseId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.EnrollmentID)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.CourseId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        QuestionAnswerId = c.Int(nullable: false, identity: true),
                        Answer = c.String(),
                        Mark = c.Int(),
                        Question = c.String(),
                        LessonID = c.Int(nullable: false),
                        EnrollmentID = c.Int(nullable: false),
                        Question_QuestionID = c.Int(),
                    })
                .PrimaryKey(t => t.QuestionAnswerId)
                .ForeignKey("dbo.Enrollments", t => t.EnrollmentID, cascadeDelete: true)
                .ForeignKey("dbo.Lessons", t => t.LessonID, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionID)
                .Index(t => t.LessonID)
                .Index(t => t.EnrollmentID)
                .Index(t => t.Question_QuestionID);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CourseId = c.Int(nullable: false),
                        Text = c.String(),
                        VideoLink = c.String(),
                        PdfFileId = c.Int(),
                        TimeOfEdit = c.DateTime(),
                    })
                .PrimaryKey(t => t.LessonID)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: false)
                .ForeignKey("dbo.PdfFiles", t => t.PdfFileId)
                .Index(t => t.CourseId)
                .Index(t => t.PdfFileId);
            
            CreateTable(
                "dbo.PdfFiles",
                c => new
                    {
                        PdfFileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        ContentLenght = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PdfFileId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionID)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.LessonThreads",
                c => new
                    {
                        LessonThreadId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastChangeDateTime = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        LessonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LessonThreadId)
                .ForeignKey("dbo.Lessons", t => t.LessonId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LessonId);
            
            CreateTable(
                "dbo.LessonMessages",
                c => new
                    {
                        LessonMessageId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CreationDateTime = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        ThreadId = c.Int(nullable: false),
                        Thread_LessonThreadId = c.Int(),
                    })
                .PrimaryKey(t => t.LessonMessageId)
                .ForeignKey("dbo.LessonThreads", t => t.Thread_LessonThreadId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Thread_LessonThreadId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
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
            DropForeignKey("dbo.CourseMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseThreads", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseMessages", "Thread_CourseThreadId", "dbo.CourseThreads");
            DropForeignKey("dbo.CourseThreads", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.LessonThreads", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LessonMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enrollments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LessonMessages", "Thread_LessonThreadId", "dbo.LessonThreads");
            DropForeignKey("dbo.LessonThreads", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.QuestionAnswers", "Question_QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Questions", "LessonId", "dbo.Lessons");
            DropForeignKey("dbo.Lessons", "PdfFileId", "dbo.PdfFiles");
            DropForeignKey("dbo.Lessons", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.QuestionAnswers", "LessonID", "dbo.Lessons");
            DropForeignKey("dbo.QuestionAnswers", "EnrollmentID", "dbo.Enrollments");
            DropForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.LessonMessages", new[] { "Thread_LessonThreadId" });
            DropIndex("dbo.LessonMessages", new[] { "UserId" });
            DropIndex("dbo.LessonThreads", new[] { "LessonId" });
            DropIndex("dbo.LessonThreads", new[] { "UserId" });
            DropIndex("dbo.Questions", new[] { "LessonId" });
            DropIndex("dbo.Lessons", new[] { "PdfFileId" });
            DropIndex("dbo.Lessons", new[] { "CourseId" });
            DropIndex("dbo.QuestionAnswers", new[] { "Question_QuestionID" });
            DropIndex("dbo.QuestionAnswers", new[] { "EnrollmentID" });
            DropIndex("dbo.QuestionAnswers", new[] { "LessonID" });
            DropIndex("dbo.Enrollments", new[] { "UserId" });
            DropIndex("dbo.Enrollments", new[] { "CourseId" });
            DropIndex("dbo.Courses", new[] { "UserId" });
            DropIndex("dbo.CourseThreads", new[] { "CourseId" });
            DropIndex("dbo.CourseThreads", new[] { "UserId" });
            DropIndex("dbo.CourseMessages", new[] { "Thread_CourseThreadId" });
            DropIndex("dbo.CourseMessages", new[] { "UserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.LessonMessages");
            DropTable("dbo.LessonThreads");
            DropTable("dbo.Questions");
            DropTable("dbo.PdfFiles");
            DropTable("dbo.Lessons");
            DropTable("dbo.QuestionAnswers");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Courses");
            DropTable("dbo.CourseThreads");
            DropTable("dbo.CourseMessages");
        }
    }
}
