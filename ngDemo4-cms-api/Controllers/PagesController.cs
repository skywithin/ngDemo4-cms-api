using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ngDemo4_cms_api.Data;
using ngDemo4_cms_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ngDemo4_cms_api.Controllers
{
    [Route("api/[controller]")]
    public class PagesController : Controller
    {
        private readonly CmsApiContext _context;

        public PagesController(CmsApiContext context)
        {
            _context = context;
        }

        // GET api/pages
        public IActionResult Get()
        {
            List<Page> pages = _context.Pages.ToList();
            return Json(pages);
        }

        // GET api/pages/slug
        [HttpGet("{slug}")]
        public IActionResult Get(string slug)
        {
            Page page = _context.Pages.SingleOrDefault(p => p.Slug == slug);

            if(page == null)
            {
                return Json("PageNotFound");
            }
            return Json(page);
        }
    }
}