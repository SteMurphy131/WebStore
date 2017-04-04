using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebStore.Models;

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
            if (await CheckForUser(user))
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AddStockItem(StockItem item)
        {
            if (await CheckForItem(item))
            {
                _context.StockItmes.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
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

        public async Task<User> GetUser(string name)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Name == name);
        }

        public async Task<StockItem> GetItem(string name)
        {
            return await _context.StockItmes.Include(i => i.Comments).Include(i => i.Ratings).SingleOrDefaultAsync(i => i.Title == name);
        }

        public async Task<User> LogIn(User u)
        {
            return await _context.Users.SingleOrDefaultAsync(user => u.Name == user.Name && u.Password == user.Password);
        }

        public async Task<bool> CheckForUser(User u)
        {
            return await _context.Users.AllAsync(user => u.Name != user.Name);
        }

        public async Task<IEnumerable<StockItem>> GetAllItems()
        {
            return await _context.StockItmes.ToListAsync();
        }

        public WebStoreContext GetContext()
        {
            return _context;
        }

        public async Task<StockItem> UpdateItem(StockItem item)
        {
            _context.StockItmes.Update(item);
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
            var entity = await _context.Users.FirstAsync(u => u.Name == user.Name);
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<StockItem> DeleteItem(StockItem item)
        {
            var entity = await _context.StockItmes.FirstAsync(i => i.ID == item.ID);
            _context.StockItmes.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Rating> DeleteRating(Rating rating)
        {
            var entity = await _context.Ratings.FirstAsync(r => r.ID == rating.ID);
            _context.Ratings.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Comment> DeleteComment(Comment comment)
        {
            var entity = await _context.Comments.FirstAsync(c => c.ID == comment.ID);
            _context.Comments.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> GetUser(int name)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.ID == name);
        }

        public async Task<StockItem> GetItem(int name)
        {
            return await _context.StockItmes.Include(s => s.Comments)
                    .Include(s => s.Ratings)
                    .SingleOrDefaultAsync(i => i.ID == name);
        }

        public async Task<bool> CheckForItem(StockItem item)
        {
            return await _context.StockItmes.AllAsync(i => i.Title != item.Title);
        }
    }
}