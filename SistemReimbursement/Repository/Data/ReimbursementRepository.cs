using API.Repository;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SistemReimbursement.Context;
using SistemReimbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Repository.Data
{
    
    public class ReimbursementRepository : GeneralRepository<MyContext, Reimbursement, int>
    {
        private readonly User user1 = new User();

        private readonly MyContext conn;
        private readonly IConfiguration configuration;
        public ReimbursementRepository(MyContext myContext, IConfiguration config) : base(myContext)
        {
            this.conn = myContext;
            this.configuration = config;
        }
        public IEnumerable<Reimbursement> GetAllbyNik(int nik)
        {
            return conn.Reimbursement.Where(x => x.Nik == nik);
        }
        public IEnumerable<Reimbursement> GetAllbystatus(string status)
        {
            return conn.Reimbursement.Where(x => x.Status == status);
        }
        public IEnumerable<Reimbursement> GetAllbystatusandNik(string status,int nik)
        {
            return conn.Reimbursement.Where(x => x.Status == status && x.Account.User.ManagerNik == nik);
        }


        public int updatefinance(int id, int status, string statusstring)
        {
            var result = 0;
            var cekPerson = conn.Reimbursement.FirstOrDefault(p => p.ReimbursementId == id);
            var email = cekPerson.Account.User.Email;
            var first = cekPerson.Account.User.FirstName;

            cekPerson.Status= statusstring;
            cekPerson.FinanceApprovalDate = DateTime.Now;
            cekPerson.FinanceApprovalStatus = status;
            result = conn.SaveChanges();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sy-Ment", "hai.infodigital@gmail.com"));
            message.To.Add(new MailboxAddress($"{first}", $"{email}"));
            message.Subject = "Update Request Reimburstment From Finance";
            var br = "<br>";
            var str = "<strong>";
            var str2 = "</strong>";
            message.Body = new TextPart("plain")
            {

                Text = $"Dear, {str}{first}{str2} {br}{br}" +
                $"Thank you for your time. {br}" +
                $"Your request reimbursement status is {str}APPROVED BY FINANCE{str2},{br}" +
                $"Your money will be transfer to your account bank in 2x24 Hours.{br} {br}{br}" +
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
            return result;
        }
        public int updatefinance2(int id, int status, string statusstring)
        {
            var result = 0;
            var cekPerson = conn.Reimbursement.FirstOrDefault(p => p.ReimbursementId == id);
            var email = cekPerson.Account.User.Email;
            var first = cekPerson.Account.User.FirstName;

            cekPerson.Status = statusstring;
            cekPerson.FinanceApprovalDate = DateTime.Now;
            cekPerson.FinanceApprovalStatus = status;
            result = conn.SaveChanges();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sy-Ment", "hai.infodigital@gmail.com"));
            message.To.Add(new MailboxAddress($"{first}", $"{email}"));
            message.Subject = "Update Request Reimburstment From Finance";
            var br = "<br>";
            var str = "<strong>";
            var str2 = "</strong>";
            message.Body = new TextPart("plain")
            {
                //$"Your money Not  be transfer to your account bank in 2x24 Hours.{br} {br}{br}" +

                Text = $"Dear, {str}{first}{str2} {br}{br}" +
                $"Thank you for your time. {br}" +
                $"Your request reimbursement status is {str}REJECTED BY FINANCE{str2},{br}{br}{br}" +
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
            return result;
        }
        public int updatemanager2(int id, int status, string statusstring)
        {
            var result = 0;
            var cekPerson = conn.Reimbursement.FirstOrDefault(p => p.ReimbursementId == id);
            var email = cekPerson.Account.User.Email;
            var first = cekPerson.Account.User.FirstName;

            cekPerson.FinanceApprovalStatus = 2;
            cekPerson.Status = statusstring;
            cekPerson.ManagerApprovalDate = DateTime.Now;
            cekPerson.ManagerApprovalStatus = status;
            result = conn.SaveChanges();


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Sy-Ment", "hai.infodigital@gmail.com"));
            message.To.Add(new MailboxAddress($"{first}", $"{email}"));
            message.Subject = "Update Request Reimburstment From Manager";
            var br = "<br>";
            var str = "<strong>";
            var str2 = "</strong>";
            message.Body = new TextPart("plain")
            {
                //$"Your money Not  be transfer to your account bank in 2x24 Hours.{br} {br}{br}" +

                Text = $"Dear, {str}{first}{str2} {br}{br}" +
                $"Thank you for your time. {br}" +
                $"Your request reimbursement status is {str}REJECTED BY MANAGER{str2},{br}{br}{br}" +
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
            return result;
        }
        public int updatemanager(int id, int status, string statusstring)
        {
            var result = 0;
            var cekPerson = conn.Reimbursement.FirstOrDefault(p => p.ReimbursementId == id);
            var email = cekPerson.Account.User.Email;
            var first = cekPerson.Account.User.FirstName;
            cekPerson.Status = statusstring;
            cekPerson.ManagerApprovalDate = DateTime.Now;
            cekPerson.ManagerApprovalStatus = status;
            result = conn.SaveChanges();


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("admin@reimbursement", "hai.infodigital@gmail.com"));
            message.To.Add(new MailboxAddress($"{first}", $"{email}"));
            message.Subject = "Update Request Reimburstment From Manager";
            var br = "<br>";
            var str = "<strong>";
            var str2 = "</strong>";
            message.Body = new TextPart("plain")
            {

                Text = $"Dear, {str}{first}{str2} {br}{br}" +
                $"Thank you for your time. {br}" +
                $"Your request reimbursement status is {str}APPROVED BY MANAGER{str2},{br}" +
                $"Wait until 2x24 Hours your status will update by Finance.{br} {br}{br}" +
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
            return result;
        }
    }
    
}
