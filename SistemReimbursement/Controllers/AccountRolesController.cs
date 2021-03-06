using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemReimbursement.Base;
using SistemReimbursement.Models;
using SistemReimbursement.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        public AccountRolesController(AccountRoleRepository accountrole) : base(accountrole) { }
    }
   
}
