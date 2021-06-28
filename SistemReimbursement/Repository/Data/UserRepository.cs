using API.Repository;
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
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace SistemReimbursement.Repository.Data
{
    
    public class UserRepository : GeneralRepository<MyContext, User, int>
    {
        private readonly MyContext conn;
        private readonly IConfiguration configuration;

        // public UserRepository(MyContext myContext) : base(myContext) { }

        public UserRepository(MyContext myContext, IConfiguration config) : base(myContext)
        {
            this.conn = myContext;
            this.configuration = config;
        }
        public int Register(RegisterVM register)
        {
            var result = 0;
            var cekPerson = conn.Users.FirstOrDefault(p => p.Email == register.Email);
            if (cekPerson == null)
            {
                User User = new User()
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    BirthDate = register.BirthDate,
                    Email = register.Email,
                    JobPosition=register.JobPosition,
                    ManagerNik = register.ManagerNik,
                    Gender = register.Gender,
                    Address=register.Address
                };
                conn.Add(User);
                result = conn.SaveChanges();
                Account account = new Account
                {
                    Nik = User.Nik,
                    Password = BC.HashPassword(register.Password),
                    RoleId  = 1 // sebagai employee(default)
                };
                conn.Add(account);
                result = conn.SaveChanges();
            }
                return result;
        }

       
        public int Login(LoginVM login) 
        {
            var cek = conn.Users.FirstOrDefault(p => p.Email == login.Email);
            if (cek == null)
            {
                return 404;
            }

            bool isValidPassword = BC.Verify(login.Password, cek.Account.Password);


            if (isValidPassword)
            {
                return 1;
            }

            return 401;
        }
        public string GenerateToken(LoginVM Login)
        {
            var check = conn.Users.FirstOrDefault(e => e.Email == Login.Email);
            var check2nd = conn.Accounts.Single(e => e.Nik == check.Nik);
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub,configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                    new Claim("Email",Login.Email.ToString()),
                    new Claim("NIK",check.Nik.ToString()),
                    new Claim("FirstName",check.FirstName.ToString()),
                    new Claim("LastName",check.LastName.ToString()),
                    new Claim("role",check2nd.Role.RoleName.ToString())
                   // new Claim(ClaimTypes.Role, check2nd.Roles.Name.ToString())
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddMinutes(2),
            signingCredentials: signin);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
