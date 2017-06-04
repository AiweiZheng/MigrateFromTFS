using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public bool Activated { get; private set; }
        public string Description { get; set; }

        [NotMapped]
        public string Role { get; set; }

        public ICollection<Following> Followers { get; set; }
        public ICollection<Following> Followees { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }
        public ICollection<SystemNotification> SystemNotifications { get; set; }

        public ApplicationUser()
        {
            Followees = new Collection<Following>();
            Followers = new Collection<Following>();
            UserNotifications = new Collection<UserNotification>();
            SystemNotifications = new List<SystemNotification>();
        }

        public void ChangeUserStatus(bool status)
        {
            Activated = status;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public void Notify(Notification notification)
        {
            UserNotifications.Add(new UserNotification(this, notification));
        }

        public void MapRoleToUser(ApplicationUser user, ILookup<string, IdentityRole> roles)
        {
            if (user.Roles.Count == 0)
                return;

            var id = user.Roles.First().RoleId;
            user.Role = roles[id].First().Name;
        }
    }
}