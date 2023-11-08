using System.ComponentModel.DataAnnotations;

namespace FishingDiary.API.Models
{
    public class FisheryForCreationDto
    {
        [Required]
        [StringLength(100, MinimumLength =3)]
        public required string Title { get; set; }
        public List<string> Images { get; set; }
        public string Number { get; set; }
        public string WaterNature { get; set; }
        public string Organization { get; set; }
        public string Area { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public List<MarkerDto> Markers { get; set; }
    }
}
