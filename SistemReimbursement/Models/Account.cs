using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SistemReimbursement.Models
{
    [Table("TB_M_Account")]
    public class Account
    {   [Key]
        public int Nik { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRole { get; set; }
        [JsonIgnore]
        public virtual ICollection<Reimbursement> Reimbursement { get; set; }


    }
}
