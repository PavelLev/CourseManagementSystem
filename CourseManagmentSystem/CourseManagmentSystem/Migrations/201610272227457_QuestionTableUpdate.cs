namespace CourseManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionTableUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Lesson_LessonID", "dbo.Lessons");
            DropIndex("dbo.Questions", new[] { "Lesson_LessonID" });
            RenameColumn(table: "dbo.Questions", name: "Lesson_LessonID", newName: "LessonId");
            AlterColumn("dbo.Questions", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Questions", "LessonId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "LessonId");
            AddForeignKey("dbo.Questions", "LessonId", "dbo.Lessons", "LessonID", cascadeDelete: false);
            DropColumn("dbo.Questions", "maxMark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "maxMark", c => c.Int(nullable: false));
            DropForeignKey("dbo.Questions", "LessonId", "dbo.Lessons");
            DropIndex("dbo.Questions", new[] { "LessonId" });
            AlterColumn("dbo.Questions", "LessonId", c => c.Int());
            AlterColumn("dbo.Questions", "Text", c => c.String());
            RenameColumn(table: "dbo.Questions", name: "LessonId", newName: "Lesson_LessonID");
            CreateIndex("dbo.Questions", "Lesson_LessonID");
            AddForeignKey("dbo.Questions", "Lesson_LessonID", "dbo.Lessons", "LessonID");
        }
    }
}
