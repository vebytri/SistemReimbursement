using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SistemReimbursement.Models
{
    [Table("TB_M_Category")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int MaxAmount { get; set; }
        [JsonIgnore]

        public virtual ICollection<Attachment> Attachment { get; set; }

    }
}
