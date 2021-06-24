using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Models
{
    [Table("TB_TR_Attachment")]

    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }
        
        public string FileAttachment { get; set; }

        public int CategoryId { get; set; }

        public int ReimbursementId { get; set; }
    }
}
