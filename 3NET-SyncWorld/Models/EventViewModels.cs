using System.ComponentModel.DataAnnotations;

namespace _3NET_SyncWorld.Models
{
    public class InviteFriendViewModel
    {
        [Display(Name = "Friend")]
        public string FriendUserId { get; set; }

        public virtual ApplicationUser Friend { get; set; }
    }
}