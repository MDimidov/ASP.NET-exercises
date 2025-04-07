using System.ComponentModel.DataAnnotations;
using AgentConst = HouseRentingSystem.Infrastructure.Constants.DataConstants.Agent;

namespace HouseRentingSystem.Core.Models.Agent
{
    public class BecomeAgentFormModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        [StringLength(AgentConst.PhoneNumberMaxLength, MinimumLength = AgentConst.PhoneNumberMinLength)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
