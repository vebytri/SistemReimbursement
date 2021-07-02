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
            message.From.Add(new MailboxAddress("admin@reimbursement", "hai.infodigital@gmail.com"));
            message.To.Add(new MailboxAddress($"{first}", $"{email}"));
            message.Subject = "Update Request Reimburstment From Finance";
            message.Body = new TextPart("plain")
            {
                Text = $"Dear, {first}" +
                $"Your Request Reimbursement was updated by Finance."
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
            message.Body = new TextPart("plain")
            {
                Text = $"Dear, {first}" +
                $"Your Request Reimbursement was updated by Manager"
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
