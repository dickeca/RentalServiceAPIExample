using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalServiceAPI.Model
{
    public class TitleMetaValue : AuditableEntity<Guid>
    {
        public int ValueTypeId { get; set; }
        public Guid TitleId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }


        [ForeignKey("TitleId")]
        public virtual Title Title { get; set; }
        [ForeignKey("ValueTypeId")]
        public virtual ValueType ValueType { get; set; }
    }
}
