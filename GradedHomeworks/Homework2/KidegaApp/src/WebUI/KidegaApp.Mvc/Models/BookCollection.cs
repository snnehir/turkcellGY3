namespace KidegaApp.Mvc.Models
{
    public class BookCollection
    {
        public List<BookItem> BookItems { get; set; } = new List<BookItem>();
        public void ClearAll() => BookItems.Clear();
        public decimal TotalPrice() => BookItems.Sum(p => (decimal)p.Book.UnitPrice * p.Quantity);

        public int TotalBooksCount() => BookItems.Sum(p => p.Quantity);

        public void AddNewBook(BookItem bookItem)
        {
            var exists = BookItems.Any(p => p.Book.Id == bookItem.Book.Id);
            if (exists)
            {
                var existingBook = BookItems.FirstOrDefault(p => p.Book.Id == bookItem.Book.Id);
                existingBook.Quantity += bookItem.Quantity;
            }
            else
            {
                BookItems.Add(bookItem);
            }
        }
    }
    public class BookItem
    {
        public BookBasketResponse Book { get; set; }
        public int Quantity { get; set; }
    }
}
