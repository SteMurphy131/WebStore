using System.Collections.Generic;

namespace WebStore.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public double TotalPrice { get; set; }

        public ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

        public User User { get; set; }
    }
}