using API.Repository;
using Microsoft.Extensions.Configuration;
using SistemReimbursement.Context;
using SistemReimbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Repository.Data
{
   
    public class AttachmentRepository : GeneralRepository<MyContext, Attachment, int>
    {
        private readonly MyContext conn;
        private readonly IConfiguration configuration;
        public AttachmentRepository(MyContext myContext, IConfiguration config) : base(myContext)
        {
            this.conn = myContext;
            this.configuration = config;
        }
        public IEnumerable<Attachment> GetDetailReimbursement(int reimbursementId)
        {
            return conn.Attachment.Where(x => x.ReimbursementId == reimbursementId);
        }
        public int updatepaid(int id,int paid)
        {
            var result = 0;
            var cekPerson = conn.Attachment.FirstOrDefault(p => p.AttachmentId == id);
            cekPerson.PaidAmount = paid;
            result= conn.SaveChanges();
            return result;
        }


    }
}
