using FreeDBlog.Services;
using System.IO;
using System.Linq;
using FreeDBlog.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FreeDBlog.Controllers
{
    public class HomeController : Controller
    {
        private AppSettings _appsetting;

        public HomeController(IOptions<AppSettings> config)
        {
            _appsetting = config.Value;
            _appsetting.RequestDomain = _appsetting.RequestDomain != null && _appsetting.RequestDomain != "" ? _appsetting.RequestDomain : "";
        }

        public IActionResult Index(int? Page)
        {
            ViewBag.Title = "Index";
            //Get the configured folder files info and sort it by create time.
            var files = new DirectoryInfo(_appsetting.DNXFolder).GetFileSystemInfos("*.html").OrderByDescending(f => f.CreationTime);
            //---Pagenavi start here ---
            //Calculate the number of pages.
            var pnum = Page ?? 1;
            var PageCount = files.Count() / _appsetting.PageNaviNum + (files.Count() % _appsetting.PageNaviNum == 0 ? 0 : 1);
            pnum = pnum > PageCount ? PageCount : (pnum < 1 ? 1 : pnum);
            ViewBag.Page = pnum;
            ViewBag.PageCount = PageCount;
            //Pick up the number of posts which assigned by appsetting.json file.
            var qf = files.Skip((pnum - 1) * _appsetting.PageNaviNum).Take(_appsetting.PageNaviNum);
            //---Pagenavi end here ---
            var blogs = new List<Blog>();
            foreach (var item in qf)
            {
                var tblog = new Blog();
                tblog.Id = item.Name.Split('.').FirstOrDefault();
                tblog.CreateTime = item.CreationTime;
                //The blog content will fill by get request. System.Net.Http.HttpClient object will request the uri which configured by appsetting.json file.
                tblog.Content = (new System.Net.Http.HttpClient()).GetStringAsync("http://" + (_appsetting.RequestDomain == "" ? Request.Host.ToString() : _appsetting.RequestDomain) + "/" + _appsetting.FilePath + "/" + item.Name).Result;
                blogs.Add(tblog);
            }
            return View(blogs);
        }
    }
}
