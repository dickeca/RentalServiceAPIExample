using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalServiceAPIExample.Models
{
    public class CheckOutModel
    {
        public string TitleId { get; set; }
        public string PaymentMethodId { get; set; }
    }

    public class MetaTagModel
    {
        public string Value { get; set; }
        public int MetaTagType { get; set; }
        public string TitleId { get; set; }
    }
}