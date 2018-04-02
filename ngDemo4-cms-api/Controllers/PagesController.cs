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

        // POST api/pages/create
        [HttpPost("create")]
        public IActionResult Create([FromBody] Page page)
        {
            page.Slug = page.Title.Replace(" ", "-").ToLower();
            page.HasSidebar = page.HasSidebar ?? "no";

            var existingSlug = _context.Pages.FirstOrDefault(p => p.Slug == page.Slug);
            if(existingSlug != null)
            {
                return Json("pageExists");
            }
            else
            {
                _context.Pages.Add(page);
                _context.SaveChanges();

                return Json("ok");
            }
        }

        // GET api/pages/edit/{id}
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            Page page = _context.Pages.SingleOrDefault(p => p.ID == id);

            if (page == null)
            {
                return Json("PageNotFound");
            }
            return Json(page);
        }

        // PUT api/pages/edit/{id}
        [HttpPut("edit/{id}")]
        public IActionResult Edit(int id, [FromBody] Page page)
        {
            page.Slug = page.Title.Replace(" ", "-").ToLower();
            page.HasSidebar = page.HasSidebar ?? "no";

            var existingPage = _context.Pages.FirstOrDefault(
                p => 
                    p.Slug == page.Slug && 
                    p.ID != id);

            if (existingPage != null)
            {
                return Json("pageExists");
            }
            else
            {
                _context.Update(page);
                _context.SaveChanges();

                return Json("ok");
            }
        }
    }
}