using System.Collections.Generic;
using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Models;

namespace GigHub.Controllers.Api
{
    public class VenuesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public VenuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetVenues(string query)
        {
            return Ok(_unitOfWork.Venues.GetVenues(query));
        }
    }
}
