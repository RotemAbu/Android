using HW1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HW1.Dal;
using HW1.ViewModel;
using System.Data.SqlClient;
using System.Data.Entity;

namespace HW1.Dal
{
    public class DataLayer : DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Order>().ToTable("OrdersT"); //connect to orders table in data base
            modelBuilder.Entity<Product>().ToTable("Products"); //connect to products table in data base
            modelBuilder.Entity<User>().ToTable("ManagersT"); //connect to managers table in data base
            modelBuilder.Entity<LoginUser>().ToTable("UsersT"); //connect to users table in data base

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<LoginUser> LogUsr { get; set; }

    }
}