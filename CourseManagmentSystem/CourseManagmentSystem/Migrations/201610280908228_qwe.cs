namespace CourseManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qwe : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "CourseForumId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "CourseForumId", c => c.Int(nullable: false));
        }
    }
}
