using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ngDemo4_cms_api.Data;
using ngDemo4_cms_api.Models;

namespace ngDemo4_cms_api
{
    public class Startup
    {
        readonly string CustomPolicyName = "AllowAll";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(CustomPolicyName, 
                p => p.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()));

            // Use in-memony database
            services.AddDbContext<CmsApiContext>(opt => opt.UseInMemoryDatabase("CmsDb"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(CustomPolicyName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CmsApiContext>();
                SeedData(context);
            }

            app.UseMvc();
        }

        /// <summary>
        /// Populate initial values for in-memory database
        /// </summary>
        /// <param name="context"></param>
        private static void SeedData(CmsApiContext context)
        {
            context.Database.EnsureCreated();

            if (context.Pages.Any())
            {
                return;
            }

            // Populate Pages table
            var pages = new Page[]
            {
                new Page{ Title="Home", Slug="home", Content="Home content", HasSidebar = "no" },
                new Page{ Title="About", Slug="about", Content="About content", HasSidebar = "no" },
                new Page{ Title="Services", Slug="services", Content="Services content", HasSidebar = "no" },
                new Page{ Title="Contact", Slug="contact", Content="Contact content", HasSidebar = "no" }
            };
            foreach (var p in pages)
            {
                context.Pages.Add(p);
            }
            context.SaveChanges();

            // Populate Sidebar table
            var sidebar = new Sidebar
            {
                Content = "Sidebar content"
            };
            context.Sidebar.Add(sidebar);
            context.SaveChanges();

            // Populate Sidebar table
            var user = new User
            {
                Username = "admin",
                Password = "pass",
                IsAdmin = "yes"
            };
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
