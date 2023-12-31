using System;
using System.Collections.Generic;

namespace ASM.Models;

public partial class Book
{
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public int? AuthorId { get; set; }
    public int? GenreId { get; set; }
    public string? Isbn { get; set; }
    public int? PublicationYear { get; set; }
    public decimal Price { get; set; }
    public string? CoverImage { get; set; }
    public int StockQuantity { get; set; }
    public string? Description { get; set; }
    public virtual Genre? Genre { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
