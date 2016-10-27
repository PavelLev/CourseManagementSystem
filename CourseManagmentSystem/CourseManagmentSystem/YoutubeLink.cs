using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CourseManagmentSystem
{
    public class YoutubeLink
    {
        private static readonly Regex YoutubeVideoRegex = new Regex("youtu(?:\\.be|be\\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)");

        public static string GetVideoId(string link)
        {
            return YoutubeVideoRegex.Match(link).Groups[1].Value;
        }
    }
}