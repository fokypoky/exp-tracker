namespace ExpTracker.DataAccess.PostgreSQL.Models
{
    public class PaginatedCollection<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
