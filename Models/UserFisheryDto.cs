using FishingDiaryAPI.Models;

namespace FishingDiaryAPI;

public class UserFisheryDto
{
    public Guid UserId { get; set; }
    public Guid FisheryId { get; set; }
}
