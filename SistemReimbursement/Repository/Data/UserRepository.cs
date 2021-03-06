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
using MimeKit;
using MailKit.Net.Smtp;

namespace SistemReimbursement.Repository.Data
{

    public class UserRepository : GeneralRepository<MyContext, User, int>
    {
        private readonly User user1 = new User();

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
                    Image = register.Image,
                    ManagerNik = register.ManagerNik,
                    Gender = register.Gender,
                    Address = register.Address
                };
                conn.Add(User);
                result = conn.SaveChanges();
                Account account = new Account()
                {
                    Nik = User.Nik,
                    Password = BC.HashPassword(register.Password)
                };
                conn.Add(account);
                result = conn.SaveChanges();
                AccountRole accountrole = new AccountRole
                {
                    Nik = account.Nik,
                    RoleId = register.RoleId
                };
                conn.Add(accountrole);
                result = conn.SaveChanges();

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sy-Ment", "hai.infodigital@gmail.com"));
                message.To.Add(new MailboxAddress($"{register.FirstName}", $"{register.Email}"));
                message.Subject = "Success Registration Account";
                var br = "<br>";
                var str = "<strong>";
                var str2 = "</strong>";
                message.Body = new TextPart("plain")
                {
                    Text = $"Thank you for your time. {br}" +
                    $"Your Account Sy-Ment Successfully Created {br} {br}{br}" +

                    $"if you have any questions,please contact us :{br}" +
                    $"Email:{str}admin@SyMent.com{str2}, {br}" +
                    $"We will be more than happy to assist you.{br}{br} {br}" +
                    $"Best regards,{br}" +
                    $"{str}Sy-Ment Team.{str2}{br}"


                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("hai.infodigital@gmail.com", "#Naufal1998");
                    client.Send(message);
                    client.Disconnect(true);
                }
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
            var check2nd = conn.AccountRoles.Single(e => e.Nik == check.Nik);
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub,configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                    new Claim("Email",Login.Email.ToString()),
                    new Claim("NIK",check.Nik.ToString()),
                    new Claim("FirstName",check.FirstName.ToString()),
                    new Claim("LastName",check.LastName.ToString()),
                    new Claim("ManagerNik",check.ManagerNik.ToString()),
                    new Claim("role",check2nd.Roles.RoleName.ToString())
                   // new Claim(ClaimTypes.Role, check2nd.Roles.Name.ToString())
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            configuration["Jwt:Issuer"],
            configuration["Jwt:Audience"],
            claims,
            expires: DateTime.UtcNow.AddDays(1),
            signingCredentials: signin);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public int forgotpassword(LoginVM login)
        {

            var result = 0;
            var cekPerson = conn.Users.FirstOrDefault(p => p.Email == login.Email);
            var email = cekPerson.Account.User.Email;
            var first = cekPerson.Account.User.FirstName;
            var nik = cekPerson.Account.User.Nik;



            var pass = "NEWPASSWORD";

            if (cekPerson != null)
            {
                var br = "<br>";
                var str = "<strong>";
                var str2 = "</strong>";
                //var link = "<a href='" + "https://localhost:44382/login/newpassword/"+$"{email}"+ "'>Reset Password</a>";
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sy-Ment", "hai.infodigital@gmail.com"));
                message.To.Add(new MailboxAddress($"{first}", $"{email}"));
                message.Subject = "Forgot Password Account";
        
                message.Body = new TextPart("plain")
                {

                    Text = $"Dear, {str}{first}{str2} {br}{br}" +
                    $"Thank you for your time. {br}" +
                    $"Your password has been reset,{br}" +
                    $"It's Your New Password : {br} {br}" +
                    $"{str}{pass}{str2} {br} {br}{br}" +


                    $"if you have any questions,please contact us :{br}" +
                    $"Email:{str}admin@SyMent.com{str2}, {br}" +
                    $"We will be more than happy to assist you.{br}{br} {br}" +
                    $"Best regards,{br}" +
                    $"{str}Sy-Ment Team.{str2}{br}"


                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("hai.infodigital@gmail.com", "#Naufal1998");
                    client.Send(message);
                    client.Disconnect(true);

                }

                cekPerson.Account.Nik = nik;
                cekPerson.Account.Password = BC.HashPassword(pass);
                
                result = conn.SaveChanges();



            }

            return result;
        }

        public int updatepassword(LoginVM login)
        {
           

            var result = 0;
            var cekPerson = conn.Users.FirstOrDefault(p => p.Email == login.Email);
            var pass = login.Password;
            cekPerson.Account.Password = BC.HashPassword(pass);
            result = conn.SaveChanges();
            return result;
        }
        public int updatemnik(RegisterVM register)
        {
            var result = 2;
            var cekPerson = conn.Users.FirstOrDefault(p => p.Nik == register.Nik);
            cekPerson.ManagerNik = register.ManagerNik;
            result = conn.SaveChanges();
            return result;
        }
        public int updaterole(int nik ,int RoleId)
        {
            var result = 2;
            var cekPerson = conn.AccountRoles.FirstOrDefault(x => x.Nik == nik);
            cekPerson.RoleId = RoleId;
            result = conn.SaveChanges();
            return result;
        }


        //public int updateprofiles(User user)
        //{
        //    var result = 0;
        //    var cekPerson = conn.Users.FirstOrDefault(p => p.Nik == user.Nik);
        //    if (cekPerson != null)
        //    {

        //        cekPerson.FirstName = user.FirstName;
        //        cekPerson.LastName = user.LastName;
        //        cekPerson.Gender = user.Gender;
        //        cekPerson.BirthDate = user.BirthDate;
        //        cekPerson.Email = user.Email;
        //        cekPerson.Address = user.Address;
        //        result = conn.SaveChanges();
        //        return result;
        //    }

        //    return result;
        //}
        public int updateprofiles(RegisterVM user)
        {
            var result = 2;
            var cekPerson = conn.Users.FirstOrDefault(p => p.Nik == user.Nik);
            if (cekPerson != null)
            {
                cekPerson.Nik = user.Nik;
                cekPerson.FirstName = user.FirstName;
                cekPerson.LastName = user.LastName;
                cekPerson.Gender = user.Gender;
                cekPerson.BirthDate = user.BirthDate;
                cekPerson.Email = user.Email;
                cekPerson.Address = user.Address;
                cekPerson.Image = user.Image;
                result = conn.SaveChanges();
                return result;
            }

            return result;
        }
    }
}
