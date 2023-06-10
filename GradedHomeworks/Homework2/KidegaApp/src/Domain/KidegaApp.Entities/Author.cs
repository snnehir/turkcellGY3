namespace KidegaApp.Entities
{
    public class Author: IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Biography { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}