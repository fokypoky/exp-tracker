namespace ExpTracker.DataAccess.PostgreSQL.Models.Filters
{
    public class TransactionFilters : FiltersBase
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
