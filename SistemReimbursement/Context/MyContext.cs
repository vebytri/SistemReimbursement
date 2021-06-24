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


             //   modelBuilder.Entity<Account>()
             //.HasOne(a => a.User)
             //.WithOne(b => b.Account)
             //.HasForeignKey<User>(b => b.Nik);
            modelBuilder.Entity<User>()
               .HasOne<Account>(a => a.Account).WithOne(p => p.User).HasForeignKey<Account>(ac => ac.Nik);
            //modelBuilder.Entity<Role>()
            //.HasOne<Account>(a => a.Account).WithOne(p => p.Role).HasForeignKey<Account>(ac => ac.RoleId);

            // modelBuilder.Entity<AccountRole>()
            //.HasOne(a => a.Role)
            //.WithMany(b => b.AccountRole)
            //.HasForeignKey(a => a.RoleId);

            // //person to account
            // modelBuilder.Entity<Person>()
            //.HasOne(a => a.Account)
            //.WithOne(b => b.Person)
            //.HasForeignKey<Account>(b => b.NIK);

            // //account to profiling
            // modelBuilder.Entity<Account>()
            //.HasOne(a => a.Profiling)
            //.WithOne(b => b.Account)
            //.HasForeignKey<Profiling>(b => b.NIK);

            // //Education to Profiling
            // modelBuilder.Entity<Education>()
            //.HasMany(a => a.Profiling)
            //.WithOne(b => b.Education);

            // //uiniversity to education
            // modelBuilder.Entity<University>()
            //.HasMany(a => a.Education)
            //.WithOne(b => b.University);


            // modelBuilder.Entity<RegisterVM>().HasNoKey();
        }
    }


}
