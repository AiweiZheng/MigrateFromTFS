using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GigHub.Core;
using GigHub.Core.Models;
using Utilities;

namespace GigHub.Controllers.Api
{
    public class GenresController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenresController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IHttpActionResult GetGenres(string query)
        {
            return Ok(_unitOfWork.Genres.GetGenres(query));
        }
    }
}
