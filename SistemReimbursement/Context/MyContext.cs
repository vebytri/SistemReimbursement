using Microsoft.EntityFrameworkCore;
using SistemReimbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<Reimbursement> Reimbursement { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //user to account
           modelBuilder.Entity<User>()
           .HasOne(a => a.Account)
           .WithOne(p => p.User)
           .HasForeignKey<Account>(ac => ac.Nik);

            //role to account
            modelBuilder.Entity<Role>()
            .HasMany(a => a.Account)
            .WithOne(p => p.Role);

            //account to reimburstment (request)
            modelBuilder.Entity<Account>()
           .HasMany(a => a.Reimbursement)
           .WithOne(b => b.Account);


            // reimburstment to attachment
            modelBuilder.Entity<Reimbursement>()
           .HasMany(a => a.Attachment)
           .WithOne(b => b.Reimbursement);

            //attachment to category
           modelBuilder.Entity<Attachment>()
          .HasOne(a => a.Category)
          .WithMany(b => b.Attachment);




        }
    }


}
