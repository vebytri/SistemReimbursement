using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SistemReimbursement.Models
{
    [Table("TB_M_Reimbursement")]

    public class Reimbursement
    {
        [Key]
        public  int ReimbursementId { get; set; }
        public DateTime RequestDate { get; set; }
       
        public string Status { get; set; }
        public string Notes { get; set; }

        //manager
        public DateTime ManagerApprovalDate { get; set; }
        public int ManagerApprovalStatus { get; set; }

        //finanace
        public DateTime FinanceApprovalDate { get; set; }
        public int FinanceApprovalStatus { get; set; }
        public int FinanceApprovalNik { get; set; }

        //user
        public int Nik { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; }

        [JsonIgnore]
        public virtual ICollection<Attachment> Attachment { get; set; }




    }

}
