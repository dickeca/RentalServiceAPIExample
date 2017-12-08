using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalServiceAPI.Model
{
    public class Title : Entity<Guid>
    {
        public string DisplayName { get; set; }
        public int TotalStock { get; set; }
        public int AvailableStock { get; set; } //ideally should always equal total - rentalHistories where Returned = false. 
        public MediaType MediaType { get; set; }
        public virtual ICollection<RentalHistory> RentalHistories { get; set; }

    }

    public enum MediaType
    {
        DVD,
        BluRay
    }
}
