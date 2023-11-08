using System.ComponentModel.DataAnnotations;

namespace FishingDiaryAPI.Models
{
    public class FisheryDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Title { get; set; }
        public List<string> Images { get; set; }
    }
}
