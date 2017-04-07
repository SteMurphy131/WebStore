using WebStore.Models;

namespace WebStore.Patterns
{
    public class CommentBuilder
    {
        private int UserId { get; set; }
        private int StockItemId { get; set; }
        private string Title { get; set; }
        private string Text { get; set; }
        private User User { get; set; }
        private StockItem StockItem { get; set; }

        public CommentBuilder Create(int userId)
        {
            UserId = userId;
            return this;
        }

        public CommentBuilder WithItemId(int itemId)
        {
            StockItemId = itemId;
            return this;
        }

        public CommentBuilder WithUser(User user)
        {
            User = user;
            return this;
        }

        public CommentBuilder WithItem(StockItem item)
        {
            StockItem = item;
            return this;
        }

        public CommentBuilder WithText(string text)
        {
            Text = text;
            return this;
        }

        public CommentBuilder WithTitle(string title)
        {
            Title = title;
            return this;
        }

        public Comment Build()
        {
            return new Comment()
            {
                UserId = UserId,
                StockItemId = StockItemId,
                StockItem = StockItem,
                User = User,
                Title = Title,
                Text = Text
            };
        }
    }
}