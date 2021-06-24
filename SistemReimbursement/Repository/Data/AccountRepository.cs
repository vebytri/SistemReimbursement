using API.Repository;
using SistemReimbursement.Context;
using SistemReimbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, int>
    {
   
            public AccountRepository(MyContext myContext) : base(myContext) { }
      
    }
}
