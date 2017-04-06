using System.Collections.Generic;
using Newtonsoft.Json;

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
        public int StockLevel { get; set; }

        [JsonIgnore]
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        [JsonIgnore]
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        [JsonIgnore]
        public ICollection<PurchaseItem> Purchases { get; set; } = new List<PurchaseItem>();
    }
}