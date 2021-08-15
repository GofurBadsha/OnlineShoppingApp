using System;
using System.Collections.Generic;
using System.Text;
using OnlineShoppingApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineShoppingApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<SignUp> SignUps { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<AddToCart> AddToCarts { get; set; }
        

    }
}
