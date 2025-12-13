namespace ExpTracker.DataAccess.PostgreSQL.Models.Filters
{
    public abstract class FiltersBase
    {
        public string? Search { get; set; }
        public bool NoLimit { get; set; } = false;
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
}
