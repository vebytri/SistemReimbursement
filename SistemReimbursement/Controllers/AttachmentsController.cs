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
    public class AttachmentsController : BaseController<Attachment, AttachmentRepository, int>
    {
        AttachmentRepository repo;
        public AttachmentsController(AttachmentRepository attachment) : base(attachment)
        {
            this.repo = attachment;
        }
        [HttpGet("getDetail/{reimbursementId}")]
        public ActionResult getDetailReimbursement(int reimbursementId)
        {

            var get = repo.GetDetailReimbursement(reimbursementId);
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
