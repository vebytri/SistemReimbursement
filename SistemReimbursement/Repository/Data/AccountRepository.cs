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
                message.From.Add(new MailboxAddress("Sy-Ment", "hai.infodigital@gmail.com"));
                message.To.Add(new MailboxAddress($"{first}", $"{email}"));
                message.Subject = "Request Reimbursement ";

                var br = "<br>";
                var str = "<strong>";
                var str2 = "</strong>";
                message.Body = new TextPart("plain")
                {

                    Text = $"Dear, {str}{first}{str2} {br}{br}" +
                    $"Thank you for your time. {br}" +
                    $"Your request reimbursement has been {str}process{str2} to manager,{br}" +
                    $"we will send your email if status from manager was updated.{br} {br}{br}"+
                    $"if you have any questions,please contact us :{br}" +
                    $"Email:{str}admin@SyMent.com{str2}, {br}" +
                    $"We will be more than happy to assist you.{br}{br} {br}"+
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

    }
}
