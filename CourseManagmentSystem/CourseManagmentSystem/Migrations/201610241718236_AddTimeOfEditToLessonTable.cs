namespace CourseManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeOfEditToLessonTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "TimeOfEdit", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lessons", "TimeOfEdit");
        }
    }
}
