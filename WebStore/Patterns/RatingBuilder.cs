using WebStore.Models;

namespace WebStore.Patterns
{
    public class RatingBuilder
    {
        private int UserId { get; set; }
        private int StockItemId { get; set; }
        private int Score { get; set; }
        private User User { get; set; }
        private StockItem StockItem { get; set; }

        public RatingBuilder Create(int userId)
        {
            UserId = userId;
            return this;
        }

        public RatingBuilder WithItemId(int itemId)
        {
            StockItemId = itemId;
            return this;
        }

        public RatingBuilder WithUser(User user)
        {
            User = user;
            return this;
        }

        public RatingBuilder WithItem(StockItem item)
        {
            StockItem = item;
            return this;
        }

        public RatingBuilder WithScore(int score)
        {
            Score = score;
            return this;
        }

        public Rating Build()
        {
            return new Rating()
            {
                UserId = UserId,
                StockItemId = StockItemId,
                Score = Score,
                User = User,
                StockItem = StockItem
            };
        }
    }
}