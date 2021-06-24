using API.Repository;
using SistemReimbursement.Context;
using SistemReimbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Repository.Data
{
   
    public class UserRepository : GeneralRepository<MyContext, User, int>
    {
        public UserRepository(MyContext myContext) : base(myContext) { }
    }
}
