namespace WebStore.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int StockItemId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public User User { get; set; }
        public StockItem StockItem { get; set; }
    }
}