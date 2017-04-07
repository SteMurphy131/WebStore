using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.Models;
using WebStore.Helpers;

namespace WebStore.Data
{
    public class StoreAccessProvider : IDataAccessProvider
    {
        private readonly WebStoreContext _context;

        public StoreAccessProvider(WebStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(User user)
        {
            if (!await CheckForUser(user))
                return false;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddStockItem(StockItem item)
        {
            if (!await CheckForItem(item))
                return false;

            _context.StockItems.Add(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddComment(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddRating(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPurchase(Purchase purchase)
        {
            await _context.Purchases.AddAsync(purchase);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddPurchaseItem(PurchaseItem pItem)
        {
            await _context.PurchaseItems.AddAsync(pItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> LogIn(User u)
        {
            return await _context.Users
                .SingleOrDefaultAsync(user => u.Name == user.Name && u.Password == user.Password);
        }

        public async Task<bool> CheckForUser(User u)
        {
            return await _context.Users.AllAsync(user => u.Name != user.Name);
        }

        public IQueryable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public IQueryable<StockItem> GetAllItems()
        {
            return _context.StockItems;
        }

        public WebStoreContext GetContext()
        {
            return _context;
        }

        public async Task<StockItem> UpdateItem(StockItem item)
        {
            _context.StockItems.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<Rating> UpdateRating(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
            return rating;
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<User> DeleteUser(User user)
        {
            var entity = await _context.Users
                .FirstAsync(u => u.Name == user.Name);

            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StockItem> DeleteItem(StockItem item)
        {
            var entity = await _context.StockItems
                .FirstAsync(i => i.ID == item.ID);

            _context.StockItems.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Rating> DeleteRating(Rating rating)
        {
            var entity = await _context.Ratings
                .FirstAsync(r => r.ID == rating.ID);

            _context.Ratings.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Comment> DeleteComment(Comment comment)
        {
            var entity = await _context.Comments
                .FirstAsync(c => c.ID == comment.ID);

            _context.Comments.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users
                .Include(u => u.Purchases)
                .ThenInclude(p => p.PurchaseItems)
                .ThenInclude(pi => pi.StockItem)
                .SingleOrDefaultAsync(u => u.ID == id);
        }
        
        public async Task<StockItem> GetItem(int id)
        {
            return await _context.StockItems
                .Include(s => s.Comments)
                .ThenInclude(c => c.User)
                .Include(s => s.Ratings)
                .ThenInclude(r => r.User)
                .SingleOrDefaultAsync(i => i.ID == id);
        }

        public async Task<bool> CheckForItem(StockItem item)
        {
            return await _context.StockItems
                .AllAsync(i => i.Title != item.Title);
        }

        public IQueryable<StockItem> GetItemsByCategory(string category)
        {
            return _context.StockItems
                .Where(i => i.Category.Contains(category, StringComparison.OrdinalIgnoreCase) || i.Title.Contains(category, StringComparison.OrdinalIgnoreCase));
        }

        public IQueryable<StockItem> GetItemsByManufacturer(string man)
        {
            return _context.StockItems
                .Where(i => i.Manufacturer.Contains(man));
        }

        public IEnumerable<StockItem> SortItems(IQueryable<StockItem> items, string sortString)
        {
            switch (sortString)
            {
                case "titleAsc":
                    return items.OrderBy(i => i.Title);
                case "titleDesc":
                    return items.OrderByDescending(i => i.Title);
                case "categoryAsc":
                    return items.OrderBy(i => i.Category);
                case "categoryDesc":
                    return items.OrderByDescending(i => i.Category);
                case "priceAsc":
                    return items.OrderBy(i => i.Price);
                case "priceDesc":
                    return items.OrderByDescending(i => i.Price);
                case "manAsc":
                    return items.OrderBy(i => i.Manufacturer);
                case "manDesc":
                    return items.OrderByDescending(i => i.Manufacturer);
                default:
                    return items;
            }
        }
    }
}