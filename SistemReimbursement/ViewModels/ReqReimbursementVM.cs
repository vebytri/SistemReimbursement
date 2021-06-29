using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.ViewModels
{
    public class ReqReimbursementVM
    {
        public int ReimbursementId { get; set; }
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
        //attachment
        public int AttachmentId { get; set; }

        public string[] FileAttachment { get; set; }

        public int[] CategoryId { get; set; }
        public int[] RequestAmount { get; set; }
        public int[] PaidAmount { get; set; }


    }
}
