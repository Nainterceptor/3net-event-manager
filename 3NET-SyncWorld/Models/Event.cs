using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace _3NET_SyncWorld.Models
{
    public class Event
    {
        public Event()
        {
            InvitedUsers = new HashSet<ApplicationUser>();
        }

        [HiddenInput(DisplayValue = false)]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Event name")]
        public string Name { get; set; }

        [Display(Name = "Event location")]
        public string Address { get; set; }

        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Summary")]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        public virtual Status Status { get; set; }

        [Display(Name = "Type")]
        public int TypeId { get; set; }

        public virtual EventType Type { get; set; }

        [Display(Name = "Invited users")]
        public virtual ICollection<ApplicationUser> InvitedUsers { get; set; }

        public virtual ICollection<Contribution> Contributions { get; set; }
    }
}