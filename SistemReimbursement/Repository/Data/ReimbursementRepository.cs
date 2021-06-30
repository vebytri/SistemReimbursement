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
    
    public class ReimbursementRepository : GeneralRepository<MyContext, Reimbursement, int>
    {
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
            return conn.Reimbursement.Where(x => x.Status == status && x.FinanceApprovalNik == nik);
        }
    }
    
}
