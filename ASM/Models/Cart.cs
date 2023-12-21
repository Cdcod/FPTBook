namespace ASM.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; } = [];
        public void AddItem(Book book, int quantity)
        {
            var existingItem = Items.FirstOrDefault(item => item.BookId == book.BookId);
            if (existingItem != null){   existingItem.Quantity += quantity;  }
            else
            {   Items.Add(new CartItem{
                    BookId = book.BookId,
                    Image = book.CoverImage,
                    Title = book.Title,
                    Price = book.Price,
                    Quantity = quantity
                }); 
            }
        }
        public void RemoveItem(int bookId)
        {   var itemToRemove = Items.FirstOrDefault(item => item.BookId == bookId);
            if (itemToRemove != null)
            {    Items.Remove(itemToRemove);    }
        }
        public decimal CalculateTotal()
        {   return Items.Sum(item => item.Price * item.Quantity); }

        public void Clear()
        {   Items.Clear();  }
    }
}
