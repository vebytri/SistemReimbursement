using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemReimbursement.Base;
using SistemReimbursement.Models;
using SistemReimbursement.Repository.Data;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpGet("download/{name}")]
        public async Task<IActionResult> Download(string name)
        {
            var path = @$"C:\Users\WIN 10 TRIAL\Desktop\Bootcamp Metrodata\APP\SistemReimbursement\ReimbursementFrontEnd\Files\{name}";
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return File(memory, GetMimeTypes()[ext], Path.GetFileName(path));

        }

        private Dictionary<string, string> GetMimeTypes()
        {

            return new Dictionary<string, string>
            {
                { ".txt","text/plain"},
                { ".pdf","application/pdf"},
                { ".doc","application/vnd.ms-word"},
                { ".docx","application/vnd.ms-word"},
                { ".png","iamage/png"},
                { ".jpg","iamage/jpg"},
                { ".jpeg","iamage/jpeg"},

            };
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
        [HttpPut("updatepaid/{id}/{paid}")]
        public ActionResult updatepaidc(int id, int paid)
        {

            var get = repo.updatepaid(id, paid);
            if (get != 0)
            {
                return Ok(get);
            }
            else
            {
                return NotFound("gagalupdate");
            }
        }

    }
}
