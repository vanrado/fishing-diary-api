using System.ComponentModel.DataAnnotations;

namespace FishingDiary.API.Models
{
    public class FisheryForUpdate
    {
        [Required]
        public required string Title { get; set; }
    }
}
