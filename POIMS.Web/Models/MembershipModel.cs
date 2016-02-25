namespace DNRPS.POIMS.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using DNRPS.POIMS.CommonResources;

    public class MembershipModel
    {
        [Display(Name = "UserName", ResourceType = typeof(Names))]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Names))]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}