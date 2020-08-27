using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LUSS_API.Models;
using System.Configuration;
using System.ServiceModel;

namespace LUSS_API.DB
{
    public class MyDbContext : DbContext
    {
        protected IConfiguration configuration;
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
         
        }

        public MyDbContext() {
           
        }

        protected
        override void OnModelCreating(ModelBuilder model)
        {
            //Set Primarykey
            model.Entity<AdjustmentVoucher>().HasKey(x => x.AdjustmentID);
            model.Entity<Cart>().HasKey(x => x.CartID);
            model.Entity<CollectionPoint>().HasKey(x => x.CollectionPointID);
            model.Entity<DelegatedManager>().HasKey(x => x.DelegatedManagerID);
            model.Entity<Department>().HasKey(x => x.DepartmentID);
            model.Entity<Item>().HasKey(x => x.ItemID);
            model.Entity<ItemCategory>().HasKey(x => x.CategoryID);
            model.Entity<ItemPrice>().HasKey(x => x.ItemPriceID);
            model.Entity<Request>().HasKey(x => x.RequestID);
            model.Entity<RequestDetails>().HasKey(x => x.RequestDetailID);
            model.Entity<PurchaseOrder>().HasKey(x => x.POID);
            model.Entity<PurchaseOrderItems>().HasKey(x => x.POItemID);
            model.Entity<Supplier>().HasKey(x => x.SupplierID);
            model.Entity<User>().HasKey(x => x.UserID);
            model.Entity<DelegatedManager>().HasKey(x => x.DelegatedManagerID);

            //HasRequired(x => x.Person).WithMany().HasForeignKey(x => x.PersonId);
            model.Entity<RequestDetails>().HasOne(r => r.Request).WithMany(u => u.RequestDetails).HasForeignKey(r => r.RequestID).OnDelete(DeleteBehavior.Restrict);

            model.Entity<Request>().HasOne(r => r.RequestByUser).WithMany(u => u.RequestMade).HasForeignKey(r => r.RequestBy).OnDelete(DeleteBehavior.Restrict);

            model.Entity<Request>().HasOne(r => r.ModifiedByUser).WithMany(u => u.RequestModified).HasForeignKey(r => r.ModifiedBy).OnDelete(DeleteBehavior.Restrict);

            model.Entity<AdjustmentVoucher>().HasOne(a => a.RequestedByUser).WithMany(u => u.RequestedBy).HasForeignKey(a => a.RequestByID).OnDelete(DeleteBehavior.Restrict);

            model.Entity<AdjustmentVoucher>().HasOne(a => a.ApprovedByUser).WithMany(u => u.ApprovedBy).HasForeignKey(a => a.ApprovedByID).OnDelete(DeleteBehavior.Restrict);

            model.Entity<Item>().HasOne(i => i.ItemCategory).WithMany(c => c.Items).HasForeignKey(i => i.CategoryID);

            model.Entity<PurchaseOrderItems>().HasOne(p => p.PurchaseOrder).WithMany(p => p.PurchaseOrderItems).HasForeignKey(p => p.POID).OnDelete(DeleteBehavior.Restrict);
            //model.Entity<User>().HasOne(e => e.DelegatedManager).WithMany();
        }

        public DbSet<AdjustmentVoucher> AdjustmentVoucher { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CollectionPoint> CollectionPoint { get; set; }
        public DbSet<DelegatedManager> DelegatedManager { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemCategory> ItemCategory { get; set; }
        public DbSet<ItemPrice> ItemPrice { get; set; }
        public DbSet<Request> Request{ get; set; }
        public DbSet<RequestDetails> RequestDetails { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderItems> PurchaseOrderItems { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Retrieval> Retrieval { get; set; }

    }
}
