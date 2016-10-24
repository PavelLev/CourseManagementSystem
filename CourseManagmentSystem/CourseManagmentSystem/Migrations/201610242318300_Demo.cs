namespace CourseManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Demo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lessons", "PdfFileId", "dbo.PdfFiles");
            DropIndex("dbo.Lessons", new[] { "PdfFileId" });
            AlterColumn("dbo.Lessons", "PdfFileId", c => c.Int());
            AlterColumn("dbo.Lessons", "TimeOfEdit", c => c.DateTime());
            CreateIndex("dbo.Lessons", "PdfFileId");
            AddForeignKey("dbo.Lessons", "PdfFileId", "dbo.PdfFiles", "PdfFileId");
            DropColumn("dbo.PdfFiles", "FileType_AllowMultipleCheckout");
            DropColumn("dbo.PdfFiles", "FileType_Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PdfFiles", "FileType_Name", c => c.String());
            AddColumn("dbo.PdfFiles", "FileType_AllowMultipleCheckout", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Lessons", "PdfFileId", "dbo.PdfFiles");
            DropIndex("dbo.Lessons", new[] { "PdfFileId" });
            AlterColumn("dbo.Lessons", "TimeOfEdit", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Lessons", "PdfFileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Lessons", "PdfFileId");
            AddForeignKey("dbo.Lessons", "PdfFileId", "dbo.PdfFiles", "PdfFileId", cascadeDelete: true);
        }
    }
}
