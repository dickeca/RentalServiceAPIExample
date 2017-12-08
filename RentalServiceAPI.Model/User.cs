using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RentalServiceAPI.Model
{
    public partial class User : IdentityUser, IEntity<string>, IAuditableEntity
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<SettingsValue> SettingsValues { get; set; } = new List<SettingsValue>();
        public virtual ICollection<RentalHistory> RentalHistories { get; set; } = new List<RentalHistory>();
    }
}
