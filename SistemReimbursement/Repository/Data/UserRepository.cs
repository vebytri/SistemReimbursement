using API.Repository;
using Castle.Core.Configuration;
using SistemReimbursement.Context;
using SistemReimbursement.Models;
using SistemReimbursement.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace SistemReimbursement.Repository.Data
{
    
    public class UserRepository : GeneralRepository<MyContext, User, int>
    {
        private readonly MyContext conn;
        private readonly IConfiguration configuration;
        public UserRepository(MyContext myContext,IConfiguration config) : base(myContext) {
            this.conn = myContext;
            this.configuration = config;
        }
        public int Register(RegisterVM register)
        {
            var result = 0;
            var cekPerson = conn.Users.FirstOrDefault(p => p.Email == register.Email);
            if (cekPerson == null)
            {
                //User User = new User()
                //{

                //    FirstName = register.FirstName,
                //    LastName = register.LastName,
                //    BirthDate = register.BirthDate,
                //    Phone = register.Phone,
                //    Email = register.Email,
                //    Salary = register.Salary
                //};
                //conn.Add(user);
                //result = conn.SaveChanges();
                //Acount account = new Acount
                //{
                //    NIK = person.NIK,
                //    Password = BC.HashPassword(register.Password)
                //};
                //conn.Add(account);
                //result = conn.SaveChanges();
               
            }
            return result;
        }

        public int Login(LoginVM login) {
            return 0;
        }
    }
}
