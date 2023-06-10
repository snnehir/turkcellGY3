namespace KidegaApp.Entities
{
    public class Book: IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; } = new HashSet<BookCategory>();

    }
}