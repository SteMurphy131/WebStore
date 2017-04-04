namespace WebStore.Models
{
    public class Rating
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int StockItemId { get; set; }
        public int Score { get; set; }

        public User User { get; set; }
        public StockItem StockItem { get; set; } 
    }
}