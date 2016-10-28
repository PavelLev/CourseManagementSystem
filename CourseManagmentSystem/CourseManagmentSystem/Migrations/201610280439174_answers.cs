namespace CourseManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class answers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Lesson_LessonID", "dbo.Lessons");
            DropForeignKey("dbo.QuestionAnswers", "QuestionID", "dbo.Questions");
            DropIndex("dbo.QuestionAnswers", new[] { "QuestionID" });
            DropIndex("dbo.Questions", new[] { "Lesson_LessonID" });
            RenameColumn(table: "dbo.Lessons", name: "Course_CourseId", newName: "CourseId");
            RenameColumn(table: "dbo.Questions", name: "Lesson_LessonID", newName: "LessonID");
            RenameColumn(table: "dbo.QuestionAnswers", name: "QuestionID", newName: "Question_QuestionID");
            RenameIndex(table: "dbo.Lessons", name: "IX_Course_CourseId", newName: "IX_CourseId");
            AddColumn("dbo.QuestionAnswers", "Question", c => c.String());
            AddColumn("dbo.QuestionAnswers", "LessonID", c => c.Int(nullable: false));
            AddColumn("dbo.Lessons", "Name", c => c.String());
            AlterColumn("dbo.QuestionAnswers", "Question_QuestionID", c => c.Int());
            AlterColumn("dbo.QuestionAnswers", "mark", c => c.Int());
            AlterColumn("dbo.Questions", "LessonID", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionAnswers", "LessonID");
            CreateIndex("dbo.QuestionAnswers", "Question_QuestionID");
            CreateIndex("dbo.Questions", "LessonID");
            AddForeignKey("dbo.QuestionAnswers", "LessonID", "dbo.Lessons", "LessonID", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "LessonID", "dbo.Lessons", "LessonID", cascadeDelete: true);
            AddForeignKey("dbo.QuestionAnswers", "Question_QuestionID", "dbo.Questions", "QuestionID");
            DropColumn("dbo.Questions", "maxMark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "maxMark", c => c.Int(nullable: false));
            DropForeignKey("dbo.QuestionAnswers", "Question_QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Questions", "LessonID", "dbo.Lessons");
            DropForeignKey("dbo.QuestionAnswers", "LessonID", "dbo.Lessons");
            DropIndex("dbo.Questions", new[] { "LessonID" });
            DropIndex("dbo.QuestionAnswers", new[] { "Question_QuestionID" });
            DropIndex("dbo.QuestionAnswers", new[] { "LessonID" });
            AlterColumn("dbo.Questions", "LessonID", c => c.Int());
            AlterColumn("dbo.QuestionAnswers", "mark", c => c.Int(nullable: false));
            AlterColumn("dbo.QuestionAnswers", "Question_QuestionID", c => c.Int(nullable: false));
            DropColumn("dbo.Lessons", "Name");
            DropColumn("dbo.QuestionAnswers", "LessonID");
            DropColumn("dbo.QuestionAnswers", "Question");
            RenameIndex(table: "dbo.Lessons", name: "IX_CourseId", newName: "IX_Course_CourseId");
            RenameColumn(table: "dbo.QuestionAnswers", name: "Question_QuestionID", newName: "QuestionID");
            RenameColumn(table: "dbo.Questions", name: "LessonID", newName: "Lesson_LessonID");
            RenameColumn(table: "dbo.Lessons", name: "CourseId", newName: "Course_CourseId");
            CreateIndex("dbo.Questions", "Lesson_LessonID");
            CreateIndex("dbo.QuestionAnswers", "QuestionID");
            AddForeignKey("dbo.QuestionAnswers", "QuestionID", "dbo.Questions", "QuestionID", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "Lesson_LessonID", "dbo.Lessons", "LessonID");
        }
    }
}
