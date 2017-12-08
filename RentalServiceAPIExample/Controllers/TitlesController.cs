using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RentalServiceAPI.Model;
using RentalServiceAPI.Service.Interfaces;

namespace RentalServiceAPIExample.Controllers
{
    [Authorize]
    [RoutePrefix("api/Titles")]
    public class TitlesController : ApiController
    {
        private ITitleService _titleService;

        public TitlesController(ITitleService titleService)
        {
            _titleService = titleService;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public HttpResponseMessage GetTitles()
        {
            var result = _titleService.GetAll();
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("GetTitle")]
        public HttpResponseMessage GetTitle(string id)
        {
            var result = _titleService.GetById(new Guid(id));
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [Authorize(Roles = "Admin")]
        [Route("Create")]
        public HttpResponseMessage CreateTitle([FromBody] Title title)
        {
            _titleService.Create(title);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        [Authorize(Roles = "Admin")]
        [Route("Update")]
        public HttpResponseMessage UpdateTitle([FromBody] Title title)
        {
            _titleService.Update(title);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        [Authorize(Roles = "Admin")]
        [Route("Delete")]
        [HttpPost]
        public HttpResponseMessage DeleteTitle([FromBody] Title title)
        {
            _titleService.Delete(title);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        [Route("CheckOutTitle")]
        [HttpPost]
        public HttpResponseMessage CheckOutTitle(string id)
        {
            return null;
        }

        [Route("GetMyRentals")]
        [HttpGet]
        public HttpResponseMessage GetMyRentals()
        {
            return null;
        }
    }
}
