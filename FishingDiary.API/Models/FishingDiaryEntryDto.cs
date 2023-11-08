namespace FishingDiary.API.Models
{
    public class FishingDiaryEntryDto
    {
        public string FishSpecies { get; set; }
        public DateTime DateTimeOfCatch { get; set; }
        public string FishingLocation { get; set; }
    }
}
