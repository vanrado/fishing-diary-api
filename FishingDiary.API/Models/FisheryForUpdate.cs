using System.ComponentModel.DataAnnotations;

namespace FishingDiaryAPI.Models
{
    public class FisheryForUpdate
    {
        [Required]
        public required string Title { get; set; }
    }
}
