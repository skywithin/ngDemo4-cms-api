using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ngDemo4_cms_api.Models;

namespace ngDemo4_cms_api.Data
{
    public class CmsApiContext : DbContext
    {
        public CmsApiContext(DbContextOptions<CmsApiContext> options) : base(options)
        {
        }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Sidebar> Sidebar { get; set; }

        public DbSet<User> Users { get; set; }

    }
}
