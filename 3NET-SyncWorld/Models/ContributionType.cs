using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace _3NET_SyncWorld.Models
{
    public class ContributionType
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Type")]
        public string Label { get; set; }
    }
}