using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace _3NET_SyncWorld.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public virtual ICollection<Event> EventsInvitedTo { get; set; }
        public virtual IList<ApplicationUser> Friends { get; set; }
    }
}