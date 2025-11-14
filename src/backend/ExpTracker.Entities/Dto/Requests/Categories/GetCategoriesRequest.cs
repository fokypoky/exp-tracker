namespace ExpTracker.Entities.Dto.Requests.Categories
{
    public class GetCategoriesRequest
    {
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
    }
}
