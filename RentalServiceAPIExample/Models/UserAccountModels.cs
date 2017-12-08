using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentalServiceAPI.Model;

namespace RentalServiceAPIExample.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Persist { get; set; }
    }

    public class AccountModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<RentalHistory> RentalHistories { get; set; }
        public List<SettingsValue> SettingsValues { get; set; }  
    }

    public class ChangePasswordModel
    {
        public string CurrentPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class PaymentMethodModel
    {
        //TODO Actually secure payment method storage

        public int NotACCNumber { get; set; }
    }

    public class AddressModel
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}