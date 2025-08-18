namespace ExpTracker.Entities.Dto.Responses.Profile
{
    public class ProfileResponse
    {
        public Guid Guid { get; set; }
        public string Login { get; set; }
        public DateTime Registered { get; set; }
    }
}