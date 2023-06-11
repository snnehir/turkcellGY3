namespace KidegaApp.Entities
{
    public class Category: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; } = new HashSet<Book>();

    }
}