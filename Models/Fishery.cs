using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FishingDiaryAPI.Models
{
    public class Fishery
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Title { get; set; }
    }
}
