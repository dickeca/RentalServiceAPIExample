using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RentalServiceAPI.Model;
using RentalServiceAPI.Service.Interfaces;
using RentalServiceAPIExample.Models;

namespace RentalServiceAPIExample.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _appRoleManager;
        private ISettingsValueService _settingsValueService;
        private IUserService _userService;

        protected ApplicationRoleManager AppRoleManager => _appRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        protected ApplicationUserManager AppUserManager => _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        protected ApplicationSignInManager AppSigninManager => _signInManager ?? Request.GetOwinContext().GetUserManager<ApplicationSignInManager>();

        public UserController(IUserService userService, ISettingsValueService settingsValueService)
        {
            _userService = userService;
            _settingsValueService = settingsValueService;
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Login(LoginModel loginModel)
        {
            var result = AppSigninManager.PasswordSignIn(loginModel.Username, loginModel.Password, loginModel.Persist, false);
            switch (result)
            {
                case SignInStatus.Success:
                {
                    var response = Request.CreateResponse(HttpStatusCode.OK, result);
                    return response;
                }

                case SignInStatus.LockedOut:
                    throw new NotImplementedException();
                case SignInStatus.RequiresVerification:
                    throw new NotImplementedException();
                case SignInStatus.Failure:
                default:
                {
                    var response = Request.CreateResponse(HttpStatusCode.Forbidden, "Invalid Login");
                    return response;
                }

            }
        }

        [AllowAnonymous]
        [HttpPost]
        public HttpResponseMessage Register(AccountModel accountModel)
        {
            var result = AppUserManager.Create(new User {UserName = accountModel.Username, Email = accountModel.Email},
                accountModel.Password);
            if (result.Succeeded)
            {
                var signinResult = AppSigninManager.PasswordSignIn(accountModel.Username, accountModel.Password, false,
                    false);
                switch (signinResult)
                {
                    case SignInStatus.Success:
                    {
                        var response = Request.CreateResponse(HttpStatusCode.OK, result);
                        return response;
                    }

                    case SignInStatus.LockedOut:
                        throw new NotImplementedException();
                    case SignInStatus.RequiresVerification:
                        throw new NotImplementedException();
                    case SignInStatus.Failure:
                    default:
                    {
                        var response = Request.CreateResponse(HttpStatusCode.Forbidden, "Failed to Login post user creation. Please attempt to log in Manually.");
                        return response;
                    }
                }
            }
            else
            {
                var response = Request.CreateResponse(HttpStatusCode.BadRequest, "Failed to create user. Please double check your details and try again.");
                return response;
            }
        }
        [HttpPost]
        public HttpResponseMessage LogOut()
        {
            Request.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public HttpResponseMessage GetAccountDetails()
        {
            var result = _userService.GetById(RequestContext.Principal.Identity.GetUserId());
            var model = new AccountModel
            {
                Email = result.Email,
                Username = result.UserName,
                RentalHistories = result.RentalHistories.ToList(),
                SettingsValues = result.SettingsValues.ToList()
            };


            var response = Request.CreateResponse(HttpStatusCode.OK, model);
            return response;
        }
        [HttpPost]
        public HttpResponseMessage ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (changePasswordModel.Password != changePasswordModel.ConfirmPassword)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Your new password and the confirm password field did not match.");             
            }

            var result = AppUserManager.ChangePassword(RequestContext.Principal.Identity.GetUserId(),
                changePasswordModel.CurrentPassword, changePasswordModel.Password);
            return result.Succeeded ? Request.CreateResponse(HttpStatusCode.NoContent) : Request.CreateResponse(HttpStatusCode.BadRequest, "Your password did not meet the security requirements.");
            
        }

        [HttpPost]
        public HttpResponseMessage AddAPaymentMethod(PaymentMethodModel paymentMethodModel)
        {
            //TODO: Validate payment method.
            _settingsValueService.Create(new SettingsValue
            {
                UserId = RequestContext.Principal.Identity.GetUserId(),
                ValueTypeId = 5, //Using Magic numbers like a scrub. Really should have done this with an enumerable. Or maybe as a config value? Not really sure.
                Value = paymentMethodModel.NotACCNumber.ToString()
            });
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpPost]
        public HttpResponseMessage RemoveAPaymentMethod([FromBody] string paymentMethodId)
        {
            Guid output;
            var isValid = Guid.TryParse(paymentMethodId, out output);
            if (!isValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Malformed Payment Id.");
            }
            if (!_settingsValueService.ValidateUserPaymentId(output,
                RequestContext.Principal.Identity.GetUserId()))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Payment Method was not found.");
            }
            _settingsValueService.Delete(_settingsValueService.GetById(output));
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public HttpResponseMessage UpdateUserAddress(AddressModel addressModel)
        {
            var user = _userService.GetById(RequestContext.Principal.Identity.GetUserId());
            if (user.SettingsValues.Any(x => x.ValueTypeId == 1))
            {
                if (addressModel.Address1 != null)
                {
                    var address1 = user.SettingsValues.First(x => x.ValueTypeId == 1);
                    address1.Value = addressModel.Address1;
                    _settingsValueService.Update(address1);
                }

                if (addressModel.Address2 != null)
                {
                    var address2 = user.SettingsValues.First(x => x.ValueTypeId == 2);
                    address2.Value = addressModel.Address2;
                    _settingsValueService.Update(address2);
                }
                if (addressModel.City != null)
                {
                    var city = user.SettingsValues.First(x => x.ValueTypeId == 3);
                    city.Value = addressModel.City;
                    _settingsValueService.Update(city);
                }

                if (addressModel.State != null)
                {
                    var state = user.SettingsValues.First(x => x.ValueTypeId == 4);
                    state.Value = addressModel.State;
                    _settingsValueService.Update(state);
                }
                
                if (addressModel.Zip != null)
                {
                    var zip = user.SettingsValues.First(x => x.ValueTypeId == 6);
                    zip.Value = addressModel.Zip;
                    _settingsValueService.Update(zip);
                }
                

            }
            else
            {
                var address1 = new SettingsValue
                {
                    UserId = user.Id,
                    ValueTypeId = 1,
                    Value = addressModel.Address1 ?? ""
                };
                _settingsValueService.Create(address1);
                var address2 = new SettingsValue
                {
                    UserId = user.Id,
                    ValueTypeId = 2,
                    Value = addressModel.Address2 ?? ""
                };
                _settingsValueService.Create(address2);
                var city = new SettingsValue
                {
                    UserId = user.Id,
                    ValueTypeId = 3, 
                    Value = addressModel.City ?? ""
                };
                _settingsValueService.Create(city);
                var state = new SettingsValue
                {
                    UserId = user.Id,
                    ValueTypeId = 4,
                    Value = addressModel.State ?? ""
                };
                _settingsValueService.Create(state);
                var zip = new SettingsValue
                {
                    UserId = user.Id,
                    ValueTypeId = 6,
                    Value = addressModel.Zip ?? ""
                };
                _settingsValueService.Create(zip);
            }

            return Request.CreateResponse(HttpStatusCode.NoContent); 
        }
    }
}
