using System.Linq;
using System.Web.Http;
using AutoMapper;
using GigHub.Controllers.Api.Filters;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Controllers.Api
{
    [ApiAuthorizeActivatedAccount]
    [Authorize(Roles = RoleName.AccountManager)]
    public class RolesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RolesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult GetRoles()
        {
            var roles = _unitOfWork.Roles.GetRoles().ToList();

            return Ok(roles.Select(Mapper.Map<IdentityRole, RoleDto>));
        }

    }
}
