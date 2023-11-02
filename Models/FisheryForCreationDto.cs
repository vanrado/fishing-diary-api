using System.ComponentModel.DataAnnotations;

namespace FishingDiaryAPI.Models
{
    public class FisheryForCreationDto
    {
        [Required]
        public required string Title { get; set; }
        public List<string> Images { get; set; }
    }
}
