using API.Repository;
using Microsoft.Extensions.Configuration;
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

        private readonly MyContext conn;
        private readonly IConfiguration configuration;
        public AccountRepository(MyContext myContext, IConfiguration config) : base(myContext)
        {
            this.conn = myContext;
            this.configuration = config;
        }
        public int RequestReimbursement(ReqReimbursementVM reqReimbursement,int length)
        {
            var result = 0;
            var cekAccount = conn.Accounts.FirstOrDefault(p => p.Nik == reqReimbursement.Nik);
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
            }
            return result;
        }
        
    }
}
