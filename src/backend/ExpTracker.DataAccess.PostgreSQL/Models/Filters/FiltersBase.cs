namespace ExpTracker.DataAccess.PostgreSQL.Models.Filters
{
    public abstract class FiltersBase
    {
        public string? Search { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
