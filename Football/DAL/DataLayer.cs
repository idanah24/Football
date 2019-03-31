using Football.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Football.DAL
{

    /*This class handles interactions with database*/
    public class DataLayer : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<User>().ToTable("Users");
        }

        /*Database sets*/
        public DbSet<Player> players { get; set; }
        public DbSet<Staff> staffs { get; set; }
        public DbSet<Contact> contacts { get; set; }
        public DbSet<User> users { get; set; }
    }
}