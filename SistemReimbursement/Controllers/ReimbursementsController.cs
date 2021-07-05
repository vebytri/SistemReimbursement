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
    public class ReimbursementsController : BaseController<Reimbursement, ReimbursementRepository, int>
    {
        ReimbursementRepository repo;
        public ReimbursementsController(ReimbursementRepository reimbursement) : base(reimbursement)
        {
            this.repo = reimbursement;
        }
        [HttpGet("getallbynik/{nik}")]
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
        [HttpGet("getallbystatus/{status}")]
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
        [HttpGet("getallbystatusandnik/{status}/{nik}")]
        public ActionResult GetbyStatusandNik(string status, int nik)
        {

            var get = repo.GetAllbystatusandNik(status,nik);
            if (get != null)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data Tidak ditemukan");
            }
        }
        [HttpPut("updatefinance/{id}/{status}/{statusstring}")]
        public ActionResult updatefinances(int id, int status, string statusstring)
        {
            var get = repo.updatefinance(id,status,statusstring);
            if (get != 0)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data Tidak ditemukan");
            }
        }

        [HttpPut("updatemanager/{id}/{status}/{statusstring}")]
        public ActionResult updatemanager(int id, int status, string statusstring)
        {
            var get = repo.updatemanager(id, status, statusstring);
            if (get != 0)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("Data Tidak ditemukan");
            }
        }

        [HttpPut("updatemanager2/{id}/{status}/{statusstring}")]
        public ActionResult updatemanager2(int id, int status, string statusstring)
        {
            var get = repo.updatemanager2(id, status, statusstring);
            if (get != 0)
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
