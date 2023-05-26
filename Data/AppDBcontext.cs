using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WajedApi.Models;

namespace WajedApi.Data
{
    public class AppDBcontext : IdentityDbContext<User>
    {
        public AppDBcontext(DbContextOptions<AppDBcontext> options) : base(options)
        {


            
        }
        public DbSet<Address>? Addresses { get; set; }

        public DbSet<Alert>? Alerts { get; set; }

        public DbSet<AppConfig>? AppConfigs { get; set; }

        public DbSet<Cart>? Carts { get; set; }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Field>? Fields { get; set; }

        public DbSet<Offer>? Offers { get; set; }

        public DbSet<Order>? Orders { get; set; }

        public DbSet<OrderItem>? OrderItems { get; set; }

        public DbSet<OrderItemOption>? OrderItemOptions { get; set; }

        public DbSet<Product>? Products { get; set; }

        public DbSet<ProductsOption>? ProductsOptions { get; set; }

         public DbSet<Market>? Markets { get; set; }

          public DbSet<Rate>? Rates { get; set; }

         public DbSet<Coupon>? Coupons { get; set; }
    
    
    
    }
}