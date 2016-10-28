using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CourseManagmentSystem.Models;


namespace CourseManagmentSystem
{
    public partial class Pdf : System.Web.UI.Page
    {
        private AppDbContext db = new AppDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {

            var id = int.Parse(Request.QueryString["pdfId"]);
            var file = db.PdfFiles.Find(id);
            Response.ContentType = file.ContentType;
            Response.AddHeader("Content-disposition", "inline");
            Response.BinaryWrite(file.Content);
        }
    }
}