using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using RentalServiceAPI.Model;
using RentalServiceAPI.Model.Context;
using RentalServiceAPI.Service;
using RentalServiceAPI.Service.Interfaces;
using RentalServiceAPIExample.Models;

namespace RentalServiceAPIExample.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _appRoleManager = null;
        private IUserService _userService;

        protected ApplicationRoleManager AppRoleManager => _appRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        protected ApplicationUserManager AppUserManager => _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        protected ApplicationSignInManager AppSigninManager => _signInManager ?? Request.GetOwinContext().GetUserManager<ApplicationSignInManager>();


        public HomeController()
        {
        }
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";      
            var result = AppSigninManager.PasswordSignIn("dickeca@gmail.com", "testing123", true, false);
            return View();
        }

        private static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}
