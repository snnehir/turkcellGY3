namespace KidegaApp.DataTransferObjects.Responses
{
    public class BookDisplayResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public string AuthorId { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
