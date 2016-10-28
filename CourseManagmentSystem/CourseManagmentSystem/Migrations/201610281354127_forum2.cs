namespace CourseManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forum2 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LessonThreads", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LessonMessages", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.LessonMessages", "Thread_LessonThreadId", "dbo.LessonThreads");
            DropForeignKey("dbo.LessonThreads", "LessonId", "dbo.Lessons");
            DropIndex("dbo.LessonMessages", new[] { "Thread_LessonThreadId" });
            DropIndex("dbo.LessonMessages", new[] { "UserId" });
            DropIndex("dbo.LessonThreads", new[] { "LessonId" });
            DropIndex("dbo.LessonThreads", new[] { "UserId" });
            DropTable("dbo.LessonMessages");
            DropTable("dbo.LessonThreads");
        }
    }
}
