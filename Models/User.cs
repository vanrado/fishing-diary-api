using System.ComponentModel.DataAnnotations;

namespace FishingDiaryAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public ICollection<Fishery> fisheries {  get; set; } = new List<Fishery>();
    }
}
