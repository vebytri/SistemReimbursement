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
        public DbSet<AccountRole> AccountRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //user to account
           modelBuilder.Entity<User>()
           .HasOne(a => a.Account)
           .WithOne(p => p.User)
           .HasForeignKey<Account>(ac => ac.Nik);

            ////role to account
            //modelBuilder.Entity<Role>()
            //.HasMany(a => a.Account)
            //.WithOne(p => p.Role);
            modelBuilder.Entity<AccountRole>()
               .HasOne(account => account.Account)
               .WithMany(ar => ar.AccountRole)
               .HasForeignKey(account => account.Nik);
            modelBuilder.Entity<AccountRole>()
                .HasOne(role => role.Roles)
                .WithMany(ar => ar.AccountRoles)
                .HasForeignKey(role => role.RoleId);

            //account to reimburstment (request)
           modelBuilder.Entity<Reimbursement>()
         .HasOne(a => a.Account)
         .WithMany(b => b.Reimbursement).HasForeignKey(b => b.Nik);


            // reimburstment to attachment
            modelBuilder.Entity<Reimbursement>()
           .HasMany(a => a.Attachment)
           .WithOne(b => b.Reimbursement);

            //attachment to category
           modelBuilder.Entity<Attachment>()
          .HasOne(a => a.Category)
          .WithMany(b => b.Attachment).HasForeignKey(c => c.CategoryId);

        }
    }


}
