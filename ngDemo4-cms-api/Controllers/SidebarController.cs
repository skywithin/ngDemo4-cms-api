using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ngDemo4_cms_api.Data;
using ngDemo4_cms_api.Models;

namespace ngDemo4_cms_api.Controllers
{
    [Route("api/[controller]")]
    public class SidebarController : Controller
    {
        private readonly CmsApiContext _context;

        public SidebarController(CmsApiContext context)
        {
            _context = context;
        }

        // GET api/sidebar
        public IActionResult Get()
        {
            var sidebar = _context.Sidebar.FirstOrDefault();
            return Json(sidebar);
        }

        // PUT api/sidebar/edit
        [HttpPut("edit")]
        public IActionResult Edit([FromBody] Sidebar sidebar)
        {
            _context.Update(sidebar);
            _context.SaveChanges();

            return Json("ok");
        }

    }
}