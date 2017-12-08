using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalServiceAPI.Model
{
    public class SettingsValueType : Entity<int>
    {
        public string DisplayName { get; set; }
        public ValueFormat ValueFormat { get; set; }

        public virtual ICollection<SettingsValue> SettingsValues { get; set; } = new List<SettingsValue>();
    }

    public enum ValueFormat
    {
        Int,
        String,
        Decimal
    }
}
