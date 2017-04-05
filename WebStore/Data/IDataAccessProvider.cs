using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public interface IDataAccessProvider
    {
        Task<bool> AddUser(User user);
        Task<bool> AddStockItem(StockItem item);
        Task<bool> AddComment(Comment comment);
        Task<bool> AddRating(Rating rating);
        Task<StockItem> UpdateItem(StockItem item);
        Task<User> UpdateUser(User user);
        Task<Rating> UpdateRating(Rating rating);
        Task<Comment> UpdateComment(Comment comment);
        Task<User> DeleteUser(User user);
        Task<StockItem> DeleteItem(StockItem item);
        Task<Rating> DeleteRating(Rating rating);
        Task<Comment> DeleteComment(Comment comment);
        Task<User> GetUser(int name);
        Task<StockItem> GetItem(int name);
        Task<bool> CheckForUser(User u);
        Task<bool> CheckForItem(StockItem item);
        Task<User> LogIn(User u);
        IQueryable<StockItem> GetAllItems();
        IQueryable<StockItem> GetItemsByCategory(string category);
        IQueryable<StockItem> GetItemsByManufacturer(string man);
        IEnumerable<StockItem> SortItems(IQueryable<StockItem> items, string sortString);
        WebStoreContext GetContext();
    }
}