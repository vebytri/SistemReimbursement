using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Models
{
    [Table("TB_M_Account")]
    public class Account
    {   [Key]
        public int Nik { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }

    }
}
