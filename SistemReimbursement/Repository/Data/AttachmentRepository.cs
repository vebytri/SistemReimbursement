using API.Repository;
using SistemReimbursement.Context;
using SistemReimbursement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Repository.Data
{
   
    public class AttachmentRepository : GeneralRepository<MyContext, Attachment, int>
    {
        public AttachmentRepository (MyContext myContext) : base(myContext) { }
    }
}
