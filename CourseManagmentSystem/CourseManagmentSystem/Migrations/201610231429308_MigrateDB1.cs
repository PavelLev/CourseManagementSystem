namespace CourseManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lessons", "Course_CourseID", "dbo.Courses");
            DropIndex("dbo.Lessons", new[] { "Course_CourseID" });
            RenameColumn(table: "dbo.Lessons", name: "Course_CourseID", newName: "CourseId");
            AlterColumn("dbo.Lessons", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lessons", "CourseId");
            AddForeignKey("dbo.Lessons", "CourseId", "dbo.Courses", "CourseID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "CourseId", "dbo.Courses");
            DropIndex("dbo.Lessons", new[] { "CourseId" });
            AlterColumn("dbo.Lessons", "CourseId", c => c.Int());
            RenameColumn(table: "dbo.Lessons", name: "CourseId", newName: "Course_CourseID");
            CreateIndex("dbo.Lessons", "Course_CourseID");
            AddForeignKey("dbo.Lessons", "Course_CourseID", "dbo.Courses", "CourseID");
        }
    }
}
