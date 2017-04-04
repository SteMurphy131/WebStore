using System.Collections.Generic;

namespace WebStore.Models
{
    public class StockItem
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}