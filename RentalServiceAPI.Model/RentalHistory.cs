using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalServiceAPI.Model
{
    public class RentalHistory : AuditableEntity<Guid>
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public Guid TitleId { get; set; }
        [Required]
        public CurrentStatus CurrentStatus { get; set; }
        public DateTime? ReturnByDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        [Required]
        public bool Returned { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("TitleId")]
        public virtual Title Title { get; set; }

    }

    public enum CurrentStatus
    {
        Processing,
        Shipping,
        Shipped,
        Arrived,
        Returned
    }
}
