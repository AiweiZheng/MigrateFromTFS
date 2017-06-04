using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Models;

namespace GigHub.Controllers.Api
{
    public class SearchController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetAll(string query = null)
        {
            var artistRoleId = _unitOfWork.Roles.GetRoleIdBy(RoleName.Artist);
            var artists = _unitOfWork.Users.GetUsersByRoleId(artistRoleId, query).Select(u => u.Name);

            var generes = _unitOfWork.Genres.GetGenres(query).Select(g => g.Name);
            var venues = _unitOfWork.Venues.GetVenues(query).Select(v => v.Name);


            var all = artists.Concat(generes)
                .Concat(venues);

            return Ok(all);
        }
    }
}
