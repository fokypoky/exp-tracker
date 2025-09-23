namespace ExpTracker.Core.Models
{
    public class PaginatedResponse<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
