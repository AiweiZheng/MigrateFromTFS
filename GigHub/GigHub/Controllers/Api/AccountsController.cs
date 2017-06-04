using System.Linq;
using System.Net;
using System.Web.Http;
using AutoMapper;
using GigHub.Controllers.Api.Filters;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using WebGrease.Css.Extensions;

namespace GigHub.Controllers.Api
{
    [ApiAuthorizeActivatedAccount]
    [Authorize(Roles = RoleName.AccountManager)]
    public class AccountsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IHttpActionResult GetAccounts()
        {
            var roles = _unitOfWork.Roles.GetRoles().ToLookup(k => k.Id);

            var users = _unitOfWork.Users.GetUsers().ToList();

            users.ForEach(u => u.MapRoleToUser(u, roles));

            var accountDtos = users
                .ToList()
                .Select(Mapper.Map<ApplicationUser, UserDto>);

            return Ok(accountDtos);
        }

        [HttpGet]
        public IHttpActionResult GetAccount(string id)
        {
            var user = _unitOfWork.Users.GetUser(id);

            return Ok(Mapper.Map<ApplicationUser, UserDto>(user));
        }

        [HttpPut]
        [ApiValidateHeaderAntiForgeryToken]
        public IHttpActionResult UpdateAccount(string id, [FromBody]UserDto userDto)
        {
            var user = _unitOfWork.Users.GetUser(userDto.Id);

            Mapper.Map(userDto, user);

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPut]
        [ApiValidateHeaderAntiForgeryToken]
        [Route("api/accounts/{id}/role")]
        public IHttpActionResult UpdateUserRole(string id, [FromBody]RoleDto roleDto)
        {
            var user = _unitOfWork.Users.GetUser(id);
            var oldRole = user.Roles.FirstOrDefault();
            user.Roles.Remove(oldRole);
            user.Roles.Add(new IdentityUserRole { RoleId = roleDto.Id, UserId = user.Id });

            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPut]
        [ApiValidateHeaderAntiForgeryToken]
        [Route("api/accounts/{id}/status")]
        public IHttpActionResult UpdateAccountStatus(string id, [FromBody]UserDto userDto)
        {
            var user = _unitOfWork.Users.GetUser(id);

            if (user == null)
                return Content(HttpStatusCode.BadRequest, ErrorMsg.NoUserFound);

            user.ChangeUserStatus(userDto.Activated);

            if (!userDto.Activated)
                _unitOfWork.Gigs.GetUpcomingGigsByArtist(id).ForEach(g => g.Cancel());

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
