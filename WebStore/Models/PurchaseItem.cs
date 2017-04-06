namespace WebStore.Models
{
    public class PurchaseItem
    {
        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public int StockItemId { get; set; }
        public StockItem StockItem { get; set; }
    }
}