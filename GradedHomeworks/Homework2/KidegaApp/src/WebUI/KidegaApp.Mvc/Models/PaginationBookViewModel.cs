namespace KidegaApp.Mvc.Models
{
    public class PaginationBookViewModel
    {
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<BookDisplayResponse> Books { get; set; }
    }
}
