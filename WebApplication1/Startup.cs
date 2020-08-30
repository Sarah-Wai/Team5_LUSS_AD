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
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidAudience = "https://www.yogihosting.com",
                   ValidIssuer = "https://www.yogihosting.com",
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyTeamIsTeam5SuperHeroes"))
               };
           });
            services.AddControllers().AddNewtonsoftJson(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<MyDbContext>
               (opt => opt.UseLazyLoadingProxies()
            .UseSqlServer(Configuration.GetConnectionString("DbConn")));

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

            app.UseCors("EnableCors");

            app.UseAuthentication();
            app.UseAuthorization();
            //  app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //run all at the same time

            //dbcontext.Database.EnsureDeleted();
            //dbcontext.Database.EnsureCreated();

            //List<CollectionPoint> allCollectionPoints = AddCollectionPoints.getAllCollectionPoints();
            //dbcontext.AddRange(allCollectionPoints);
            //List<Department> allDepartments = AddDepartment.getAllDepartment(allCollectionPoints);
            //dbcontext.AddRange(allDepartments);
            //List<User> allUsers = AddUsers.getAllUsers();
            //dbcontext.AddRange(allUsers);
            //List<Supplier> suppliers = AddSupplier.getAllSuppliers();
            //dbcontext.AddRange(suppliers);
            //List<ItemCategory> itemCategories = AddItemCategory.getAllItemCategories();
            //dbcontext.AddRange(itemCategories);
            //List<Item> items = AddItems.getAllItem();
            //dbcontext.AddRange(items);

            //List<Retrieval> retrievals = AddRetrieval.getAllRetrievals();
            //dbcontext.AddRange(retrievals);
            //dbcontext.SaveChanges();
            //List<Request> requests = AddRequests.getAllRequest(allUsers, retrievals);
            //dbcontext.AddRange(requests);

            //dbcontext.SaveChanges();
            //List<ItemPrice> itemPrices = AddItemPrice.getAllItemPrice();
            //dbcontext.AddRange(itemPrices);
            //dbcontext.SaveChanges();


            //List<AdjustmentVoucher> adjustmentVouchers = AddAdjustmentVouchers.getAllAdjustmentVoucher(allUsers, items);
            //dbcontext.AddRange(adjustmentVouchers);
            //List<Notification> notifications = AddNotification.getAllNotification();
            //dbcontext.AddRange(notifications);
            //List<PurchaseOrder> purchaseOrders = AddPO.getAllPO(suppliers, allUsers);
            //dbcontext.AddRange(purchaseOrders);


            //List<RequestDetails> requestDetails = AddRequestDetail.getAllRequestDetails(items, requests);
            //dbcontext.AddRange(requestDetails);

            //dbcontext.SaveChanges();

        }
    }
}
