using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeDBlog.Services
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
