using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using RentalServiceAPI.Model;
using RentalServiceAPI.Service.Interfaces;
using RentalServiceAPIExample.Helpers;
using RentalServiceAPIExample.Models;

namespace RentalServiceAPIExample.Controllers
{
    [Authorize]
    [RoutePrefix("api/Titles")]
    public class TitlesController : ApiController
    {
        private ITitleService _titleService;
        private IRentalHistoryService _rentalHistoryService;
        private ISettingsValueService _settingsValueService;
        private ITitleMetaValueService _titleMetaValueService;

        public TitlesController(ITitleService titleService, IRentalHistoryService rentalHistoryService, ISettingsValueService settingsValueService, ITitleMetaValueService titleMetaValueService)
        {
            _titleService = titleService;
            _rentalHistoryService = rentalHistoryService;
            _settingsValueService = settingsValueService;
            _titleMetaValueService = titleMetaValueService;
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
        public HttpResponseMessage GetTitle([FromBody]string id)
        {
            Guid output;
            var isValid = Guid.TryParse(id, out output);
            if (!isValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Invalid Title Id.");
            }
            var result = _titleService.GetById(output);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    "Invalid Title Id.");
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }

        [Authorize(Roles = "Admin")]
        [Route("Create")]
        [HttpPost]
        public HttpResponseMessage CreateTitle(Title title)
        {
            _titleService.Create(title);
            var response = Request.CreateResponse(HttpStatusCode.Created);
            return response;
        }
        [Authorize(Roles = "Admin")]
        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage UpdateTitle(Title title)
        {
            _titleService.Update(title);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        [Authorize(Roles = "Admin")]
        [Route("Delete")]
        [HttpPost]
        public HttpResponseMessage DeleteTitle([FromBody]string id)
        {
            Guid output;
            var isValid = Guid.TryParse(id, out output);
            if (!isValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Invalid Title Id.");
            }
            var result = _titleService.GetById(output);
            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    "Invalid Title Id.");
            }
            _titleService.Delete(result);
            var response = Request.CreateResponse(HttpStatusCode.NoContent);
            return response;
        }
        [Authorize(Roles = "Admin")]
        [Route("ProcessReturn")]
        [HttpPost]
        public HttpResponseMessage ProcessReturn([FromBody]string rentalHistoryId)
        {
            Guid output;
            var isValid = Guid.TryParse(rentalHistoryId, out output);
            if (!isValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Malformed Rental History Id.");
            }

            var history = _rentalHistoryService.GetById(output);
            if (history == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    "Invalid Rental History Id.");
            }
            if (history.CurrentStatus == CurrentStatus.Returned)
            {
                return Request.CreateResponse(HttpStatusCode.Conflict, "This rental has already been returned.");
            }
            history.CurrentStatus = CurrentStatus.Returned;
            history.Returned = true;
            history.ReturnedDate = DateTime.Today;
            //TODO: Process Late Fees.
            _rentalHistoryService.Update(history);
            _titleService.Update(history.Title); //updates available count

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        [Authorize(Roles = "Admin")]
        [Route("AddMetaTagToTitle")]
        [HttpPost]
        public HttpResponseMessage AddMetaTagToTitle(MetaTagModel metaTagModel)
        {

            if (metaTagModel.MetaTagType != 7 && metaTagModel.MetaTagType != 8 && metaTagModel.MetaTagType != 9)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Invalid Metatag Type. Please check the Documentation and try again");
            }
            Guid output;
            var isValid = Guid.TryParse(metaTagModel.TitleId, out output);
            if (!isValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Invalid Title Id.");
            }
            var title = _titleService.GetById(output);
            if (title == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    "Title not Found");
            }
            var tag = new TitleMetaValue
            {
                TitleId = title.Id,
                Value = metaTagModel.Value,
                ValueTypeId = metaTagModel.MetaTagType
            };
            _titleMetaValueService.Create(tag);

            return Request.CreateResponse(HttpStatusCode.Created,
                    "Success.");
        }
        [Route("CheckOutTitle")]
        [HttpPost]
        public HttpResponseMessage CheckOutTitle(CheckOutModel checkOutModel)
        {
            Guid outputTitleId;           
            var isValid = Guid.TryParse(checkOutModel.TitleId, out outputTitleId);
            if (!isValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Invalid Title Id.");
            }

            Guid outputPaymentId;
            isValid = Guid.TryParse(checkOutModel.PaymentMethodId, out outputPaymentId);
            if (!isValid)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest,
                    "Invalid Payment Id.");
            }
            var title = _titleService.GetById(outputTitleId);
            if (title == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    "Invalid Title Id.");
            }
            if (title.AvailableStock == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No Available Stock to Rent");  
            }
            
            var userId = RequestContext.Principal.Identity.GetUserId();
            
            if (!_settingsValueService.ValidateUserPaymentId(outputPaymentId, userId))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Payment Method!");
            }

            if (!ProcessPayment(_settingsValueService.GetById(outputPaymentId)))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "An Error Occurred while processing your payment. Please update your payment method.");
            }

            _rentalHistoryService.Create(new RentalHistory
            {
                UserId = userId,
                Returned = false,
                CurrentStatus = CurrentStatus.Processing,
                TitleId = title.Id
            });

            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        [Route("GetMyRentalHistory")]
        [HttpGet]
        public HttpResponseMessage GetMyRentalHistory()
        {
            var results = _rentalHistoryService.GetByUserId(RequestContext.Principal.Identity.GetUserId());
            var response = Request.CreateResponse(HttpStatusCode.OK, results);
            return response;
        }

        //Never did get around to tagging films to test this.

        //[Route("Suggestion")]
        //[HttpGet]
        //public HttpResponseMessage Suggestion()
        //{
        //    var tags =
        //        _rentalHistoryService.GetByUserId(RequestContext.Principal.Identity.GetUserId())
        //            .SelectMany(x => x.Title.TitleMetaValues)
        //            .Where(x=> x.ValueTypeId == 8) //Only get Genre Meta Tags
        //            .GroupBy(x => x.Value)
        //            .Select(x => new {Key = x.Key, Value = x.Distinct().Count()}).OrderByDescending(x=> x.Value);
        //    //TODO: Be less random about how the meta tags are used to suggest a title
        //    var titles = new List<Title>();
        //    var e = tags.GetEnumerator();
        //    if (e.MoveNext())
        //    {
        //        titles.AddRange(_titleService.GetTitlesByGenreMetaTag(e.Current.Key));
        //        if (e.MoveNext())
        //        {
        //            titles.AddRange(_titleService.GetTitlesByGenreMetaTag(e.Current.Key));
        //        }

        //        var result = titles.ToArray();
        //        FisherYatesShuffle.Shuffle(result);
        //        var response = Request.CreateResponse(HttpStatusCode.OK, result);
        //        return response;
        //    }
        //    else
        //    {
        //        var result = _titleService.GetAll().ToArray();
        //        FisherYatesShuffle.Shuffle(result);
        //        var response = Request.CreateResponse(HttpStatusCode.OK, result);
        //        return response;

        //    }
        //}

        private static bool ProcessPayment(SettingsValue paymentMethod)
        {
            //TODO
            return true;
        }


    }
}
