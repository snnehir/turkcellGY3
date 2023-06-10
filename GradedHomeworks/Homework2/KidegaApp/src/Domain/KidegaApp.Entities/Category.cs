namespace KidegaApp.Entities
{
    public class Category: IEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; } = new HashSet<BookCategory>();

    }
}