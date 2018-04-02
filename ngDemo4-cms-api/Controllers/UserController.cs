using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ngDemo4_cms_api.Data;
using ngDemo4_cms_api.Models;

namespace ngDemo4_cms_api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly CmsApiContext _context;

        public UserController(CmsApiContext context)
        {
            _context = context;
        }

        // POST api/user/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                return Json("userExists");
            }
            else
            {
                _context.Users.Add(user);
                _context.SaveChanges();

                return Json("ok");
            }
        }
    }
}