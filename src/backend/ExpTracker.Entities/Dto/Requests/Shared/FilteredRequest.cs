namespace ExpTracker.Entities.Dto.Requests.Shared
{
    public abstract class FilteredRequest
    {
        public string? Search { get; set; }
        public bool NoLimit { get; set; } = false;
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
    }
}
