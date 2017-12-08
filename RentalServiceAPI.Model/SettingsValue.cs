using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalServiceAPI.Model
{
    public class SettingsValue : AuditableEntity<Guid>
    {
        public int SettingsValueTypeId { get; set; }
        public string UserId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }


        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("SettingsValueTypeId")]
        public virtual SettingsValueType SettingsValueType { get; set; }

        
    }
}
