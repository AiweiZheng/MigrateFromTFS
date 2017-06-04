using AutoMapper;
using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using GigHub.Controllers.Api.Filters;
using WebGrease.Css.Extensions;

namespace GigHub.Controllers.Api
{

    [ApiAuthorizeActivatedAccount]
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public NotificationsDto GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _unitOfWork.UserNotifications.GetNotificationsFor(userId);

            return new NotificationsDto
            {
                NewNotificationCount = _unitOfWork.UserNotifications.GetNewNotificationNumFor(userId),
                Notifications = notifications.Select(Mapper.Map<Notification, NotificationDto>)
            };
        }

        [HttpPost]
        [ApiValidateHeaderAntiForgeryToken]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _unitOfWork.UserNotifications.GetUnreadNotificationsFor(userId);

            notifications.ForEach(n => n.Read());

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
