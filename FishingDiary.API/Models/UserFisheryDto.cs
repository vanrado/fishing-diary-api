using FishingDiary.API.Models;

namespace FishingDiary.API;

public class UserFisheryDto
{
    public Guid UserId { get; set; }
    public Guid FisheryId { get; set; }
}
