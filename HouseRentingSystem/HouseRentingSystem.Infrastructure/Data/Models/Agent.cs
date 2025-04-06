using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AgentConst = HouseRentingSystem.Infrastructure.Constants.DataConstants.Agent;

namespace HouseRentingSystem.Infrastructure.Data.Models
{
    [Comment("House agent")]
    public class Agent
    {
        [Key]
        [Comment("Agent identifier")]
        public int Id { get; set; }

        [Required]
        [StringLength(AgentConst.PhoneNumberMaxLength)]
        [Comment("Agent phone number")]
        public required string PhoneNumber { get; set; }

        [Required]
        [Comment("Identity user identifier")]
        public required string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public virtual IdentityUser User { get; set; } = null!;
    }
}