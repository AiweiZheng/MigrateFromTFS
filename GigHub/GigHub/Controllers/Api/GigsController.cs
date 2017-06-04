using System.Net;
using System.Web.Http;
using GigHub.Controllers.Api.Filters;
using GigHub.Core;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    [ApiValidateHeaderAntiForgeryToken]
    [ApiAuthorizeActivatedAccount]
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gig == null || gig.IsCancelled)
                return Content(HttpStatusCode.NotFound, ErrorMsg.GigDoesNotExist);

            if (gig.ArtistId != userId)
                return Content(HttpStatusCode.Unauthorized, ErrorMsg.AuthorizedDenied);

            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult Resume(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendees(id);

            if (gig == null || !gig.IsCancelled)
                return Content(HttpStatusCode.NotFound, ErrorMsg.GigDoesNotExist);

            if (gig.ArtistId != userId)
                return Content(HttpStatusCode.Unauthorized, ErrorMsg.AuthorizedDenied);

            gig.Reopen();

            _unitOfWork.Complete();

            return Ok();
        }

    }
}
