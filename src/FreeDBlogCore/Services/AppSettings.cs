using System;

namespace FreeDBlogCore.Services
{
    public class AppSettings
    {
        public string FilePath { get; set; }
        public String RequestDomain { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String DNXFolder { get; set; }
        public int PageNaviNum { get; set; }
    }
}