using System.Collections.Generic;
using System.Linq;
using WebStore.Models;

namespace WebStore.Patterns
{
    public class PurchaseBuilder
    {
        private int Id { get; set; }
        private int UserId { get; set; }
        private double TotalPrice { get; set; }
        private ICollection<PurchaseItem> PurchaseItems { get; set; }
        private User User { get; set; }
        private readonly Purchase _purchase = new Purchase();

        public PurchaseBuilder CreatePurchase(int userId)
        {
            UserId = userId;
            _purchase.UserID = userId;
            return this;
        }

        public PurchaseBuilder WithPurchaseItems(ICollection<StockItem> items)
        {
            TotalPrice = items.Sum(i => i.Price);
            PurchaseItems = items.Select(item => new PurchaseItem {Purchase = _purchase, StockItem = item}).ToList();
            return this;
        }

        public PurchaseBuilder WithUser(User user)
        {
            User = user;
            _purchase.User = user;
            return this;
        }

        public Purchase Build()
        {
            _purchase.TotalPrice = TotalPrice;
            _purchase.PurchaseItems = PurchaseItems;
            return _purchase;
        }
    }
}