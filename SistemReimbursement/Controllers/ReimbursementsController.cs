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
    public class ReimbursementsController : BaseController<Reimbursement, ReimbursementRepository, int>
    {
        ReimbursementRepository repo;
        public ReimbursementsController(ReimbursementRepository reimbursement) : base(reimbursement)
        {
            this.repo = reimbursement;
        }
        [HttpGet]
        [Route("getallbynik/{nik}")]
        public ActionResult getReimburesemtByNik( int nik)
        {

            var get = repo.GetAllbyNik(nik);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data Tidak ditemukan");
            }
        }
        [Route("getallbystatus/{status}")]
        public ActionResult getReimburesemtByStatus(string status)
        {

            var get = repo.GetAllbystatus(status);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data Tidak ditemukan");
            }
        }
    }
}
