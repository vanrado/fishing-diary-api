using System.ComponentModel.DataAnnotations;

namespace FishingDiaryAPI.Entities
{
    public class UserFishery
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid FisheryId { get; set; }

        // Navigation property
        public Fishery Fishery { get; set; }
    }
}
