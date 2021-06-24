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
    public class AttachmentsController : BaseController<Attachment, AttachmentRepository, int>
    {
        public AttachmentsController(AttachmentRepository attachment) : base(attachment) { }
    }
}
