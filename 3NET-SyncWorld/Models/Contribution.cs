using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace _3NET_SyncWorld.Models
{
    public class Contribution
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public int EventId { get; set; }
        public virtual Event Event { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int TypeId { get; set; }
        public virtual ContributionType Type { get; set; }
    }
}