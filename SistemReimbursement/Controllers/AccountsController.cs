using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemReimbursement.Base;
using SistemReimbursement.Models;
using SistemReimbursement.Repository.Data;
using SistemReimbursement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, int>
    {

        AccountRepository repo;
        public AccountsController(AccountRepository account) : base(account)
        {
            this.repo = account;
        }
        //  [AllowAnonymous]
        [HttpPost]
        [Route("request/{length}")]
        public ActionResult reqReimbursement(ReqReimbursementVM request,int length)
        {

            var get = repo.RequestReimbursement(request, length);

            if (get > 0)
            {
                return Ok("Berhasil Request");
            }
            else
            {
                return BadRequest("Gagal Request");
            }
        }
    }
}
