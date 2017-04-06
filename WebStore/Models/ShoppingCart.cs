using System.Collections.Generic;

namespace WebStore.Models
{
    public class ShoppingCart
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public List<StockItem> Items { get; set; }
    }
}