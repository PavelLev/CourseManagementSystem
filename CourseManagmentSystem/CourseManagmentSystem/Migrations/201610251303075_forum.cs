namespace CourseManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forum : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Lessons", new[] { "Course_CourseID" });
            CreateTable(
                "dbo.CourseForums",
                c => new
                    {
                        CourseForumId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseForumId)
                .ForeignKey("dbo.Courses", t => t.CourseForumId)
                .Index(t => t.CourseForumId);
            
            CreateTable(
                "dbo.CourseThreads",
                c => new
                    {
                        CourseThreadId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastChangeDateTime = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        CourseForumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseThreadId)
                .ForeignKey("dbo.CourseForums", t => t.CourseForumId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CourseForumId);
            
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
            
            AddColumn("dbo.Courses", "CourseForumId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lessons", "Course_CourseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseThreads", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseMessages", "Thread_CourseThreadId", "dbo.CourseThreads");
            DropForeignKey("dbo.CourseThreads", "CourseForumId", "dbo.CourseForums");
            DropForeignKey("dbo.CourseForums", "CourseForumId", "dbo.Courses");
            DropIndex("dbo.CourseMessages", new[] { "Thread_CourseThreadId" });
            DropIndex("dbo.CourseMessages", new[] { "UserId" });
            DropIndex("dbo.CourseThreads", new[] { "CourseForumId" });
            DropIndex("dbo.CourseThreads", new[] { "UserId" });
            DropIndex("dbo.Lessons", new[] { "Course_CourseId" });
            DropIndex("dbo.CourseForums", new[] { "CourseForumId" });
            DropColumn("dbo.Courses", "CourseForumId");
            DropTable("dbo.CourseMessages");
            DropTable("dbo.CourseThreads");
            DropTable("dbo.CourseForums");
            CreateIndex("dbo.Lessons", "Course_CourseID");
        }
    }
}
