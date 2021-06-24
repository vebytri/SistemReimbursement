using API.Repository;
using Castle.Core.Configuration;
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
        private readonly MyContext conn;
        private readonly IConfiguration configuration;
        public UserRepository(MyContext myContext,IConfiguration config) : base(myContext) {
            this.conn = myContext;
            this.configuration = config;
        }
    }
}
