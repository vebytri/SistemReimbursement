using API.Repository;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SistemReimbursement.Context;
using SistemReimbursement.Models;
using SistemReimbursement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, int>
    {


        private readonly User user1 = new User(); 
        private readonly MyContext conn;
        private readonly IConfiguration configuration;
        public AccountRepository(MyContext myContext, IConfiguration config) : base(myContext)
        {


            this.conn = myContext;
            this.configuration = config;
        }
        public int RequestReimbursement(ReqReimbursementVM reqReimbursement, int length)
        {
            var result = 0;
            var cekAccount = conn.Accounts.FirstOrDefault(p => p.Nik == reqReimbursement.Nik);
            //var cek1 = conn.Users.Find(reqReimbursement.Nik);
            var email = cekAccount.User.Email;
            var first = cekAccount.User.FirstName;



            if (cekAccount != null)
            {
                Reimbursement reimbursement = new Reimbursement()
                {
                    Nik = reqReimbursement.Nik,
                    RequestDate = reqReimbursement.RequestDate,
                    FinanceApprovalNik = reqReimbursement.FinanceApprovalNik,

                    Status = reqReimbursement.Status,
                    Notes = reqReimbursement.Notes
                };
                conn.Add(reimbursement);
                result = conn.SaveChanges();
                for (int i = 0; i < length; i++)
                {
                    Attachment attachment = new Attachment()
                    {
                        ReimbursementId = reimbursement.ReimbursementId,
                        FileAttachment = reqReimbursement.FileAttachment[i],
                        CategoryId = reqReimbursement.CategoryId[i],
                        RequestAmount = reqReimbursement.RequestAmount[i]
                    };
                    conn.Add(attachment);
                    result = conn.SaveChanges();
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("admin@reimbursement", "hai.infodigital@gmail.com"));
                message.To.Add(new MailboxAddress($"{first}", $"{email}"));
                message.Subject = "Request Reimbursement ";
                message.Body = new TextPart("plain")
                {
                    Text = $"Dear, {first}" +
                    $" Request Reimbursement Successfully."
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

    }
}
