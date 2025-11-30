using ExpTracker.Entities.Dto.Requests.Shared;

namespace ExpTracker.Entities.Dto.Requests.Transactions
{
    public class GetTransactionsRequest : FilteredRequest
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
