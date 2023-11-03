using System.ComponentModel.DataAnnotations;

namespace FishingDiaryAPI.Models
{
    public class FisheryForCreationDto
    {
        [Required]
        [StringLength(100, MinimumLength =3)]
        public required string Title { get; set; }
        public List<string> Images { get; set; }
    }
}
