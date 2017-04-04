using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public class StoreAccessProvider : IDataAccessProvider
    {
        public Task<bool> AddUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddStockItem(StockItem item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddComment(Comment comment)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> AddRating(Rating rating)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUser(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<StockItem> GetItem(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> LogIn(User u)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CheckForUser(User u)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<StockItem>> GetAllItems()
        {
            throw new System.NotImplementedException();
        }

        public WebStoreContext GetContext()
        {
            throw new System.NotImplementedException();
        }
    }
}