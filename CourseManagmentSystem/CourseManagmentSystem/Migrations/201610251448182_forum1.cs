namespace CourseManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forum1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CourseForums", "CourseForumId", "dbo.Courses");
            DropForeignKey("dbo.CourseThreads", "CourseForumId", "dbo.CourseForums");
            DropIndex("dbo.CourseForums", new[] { "CourseForumId" });
            DropIndex("dbo.CourseThreads", new[] { "CourseForumId" });
            AddColumn("dbo.CourseThreads", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.CourseThreads", "CourseId");
            AddForeignKey("dbo.CourseThreads", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
            DropColumn("dbo.CourseThreads", "CourseForumId");
            DropTable("dbo.CourseForums");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CourseForums",
                c => new
                    {
                        CourseForumId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseForumId);
            
            AddColumn("dbo.CourseThreads", "CourseForumId", c => c.Int(nullable: false));
            DropForeignKey("dbo.CourseThreads", "CourseId", "dbo.Courses");
            DropIndex("dbo.CourseThreads", new[] { "CourseId" });
            DropColumn("dbo.CourseThreads", "CourseId");
            CreateIndex("dbo.CourseThreads", "CourseForumId");
            CreateIndex("dbo.CourseForums", "CourseForumId");
            AddForeignKey("dbo.CourseThreads", "CourseForumId", "dbo.CourseForums", "CourseForumId", cascadeDelete: true);
            AddForeignKey("dbo.CourseForums", "CourseForumId", "dbo.Courses", "CourseId");
        }
    }
}
