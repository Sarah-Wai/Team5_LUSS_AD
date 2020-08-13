using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LUSS_API.DB;
using LUSS_API.Models;

namespace LUSS_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<MyDbContext>
               (opt => opt.UseLazyLoadingProxies()
            .UseSqlServer(Configuration.GetConnectionString("DbConn"))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyDbContext dbcontext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //dbcontext.Database.EnsureDeleted();
            //dbcontext.Database.EnsureCreated();


            //static string Encrypt(string value)
            //{
            //    using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            //    {
            //        UTF8Encoding utf8 = new UTF8Encoding();
            //        byte[] data = md5.ComputeHash(utf8.GetBytes(value));
            //        return Convert.ToBase64String(data);
            //    }
            //}

            //List<ItemCategory> itemCategories = AddItemCategory.getAllItemCategories();
            //dbcontext.AddRange(itemCategories);

            //List<CollectionPoint> allCollectionPoints = AddCollectionPoints.getAllCollectionPoints();
            //dbcontext.AddRange(allCollectionPoints);

            //List<Supplier> suppliers = AddSupplier.getAllSuppliers();
            //dbcontext.AddRange(suppliers);


            //List<Department> allDepartments = AddDepartment.getAllDepartment(allCollectionPoints);
            //dbcontext.AddRange(allDepartments);

            //List<User> allUsers = AddUsers.getAllUsers();
            //dbcontext.AddRange(allUsers);

            //dbcontext.SaveChanges();

        }
    }
}
