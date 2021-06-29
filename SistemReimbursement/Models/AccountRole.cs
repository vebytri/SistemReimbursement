using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemReimbursement.Models
{
    [Table("TB_TR_AccountRole")]
    public class AccountRole
    {   [Key]
        public int AccountRoleId { get; set; }
        public int Nik { get; set; }
        public int RoleId { get; set; }
        public virtual Role Roles { get; set; }
        public virtual Account Account { get; set; }
    }
}
