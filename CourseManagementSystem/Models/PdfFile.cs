﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CourseManagementSystem.Models
{
    public class PdfFile : HttpPostedFileBase
    {
        public int PdfFileId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int ContentLenght { get; set; }
    }
}