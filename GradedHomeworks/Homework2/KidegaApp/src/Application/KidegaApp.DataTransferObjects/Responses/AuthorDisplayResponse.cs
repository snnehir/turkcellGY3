namespace KidegaApp.DataTransferObjects.Responses
{
    public class AuthorDisplayResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Biography { get; set; }
        public IEnumerable<AuthorBook> Books { get; set; }

    }

    public class AuthorBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public string CategoryName { get; set; }
    }
}
