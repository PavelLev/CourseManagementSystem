namespace CourseManagmentSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPdfFileTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PdfFiles",
                c => new
                    {
                        PdfFileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        FileType_AllowMultipleCheckout = c.Boolean(nullable: false),
                        FileType_Name = c.String(),
                        ContentLenght = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PdfFileId);
            
            AddColumn("dbo.Lessons", "Name", c => c.String());
            AddColumn("dbo.Lessons", "VideoLink", c => c.String());
            AddColumn("dbo.Lessons", "PdfFileId", c => c.Int(nullable: true));
            CreateIndex("dbo.Lessons", "PdfFileId");
            AddForeignKey("dbo.Lessons", "PdfFileId", "dbo.PdfFiles", "PdfFileId", cascadeDelete: true);
            DropColumn("dbo.Lessons", "presentation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Lessons", "presentation", c => c.String());
            DropForeignKey("dbo.Lessons", "PdfFileId", "dbo.PdfFiles");
            DropIndex("dbo.Lessons", new[] { "PdfFileId" });
            DropColumn("dbo.Lessons", "PdfFileId");
            DropColumn("dbo.Lessons", "VideoLink");
            DropColumn("dbo.Lessons", "Name");
            DropTable("dbo.PdfFiles");
        }
    }
}
