using System.Linq;
using WebStore.Models;

namespace WebStore.Data
{
    public static class DbInitializer
    {
        public static void Initialize(WebStoreContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any()) { return;}

            var users = new []
            {
                new User{Admin = true, Name = "Stephen", Password = "root", Email ="SteMurphy131@hotmail.com", AddressOne = "2 Wooddale Drive", AddressTwo = "Ballycullen, Dublin"}, 
                new User{Admin = false, Name = "Richard", Password = "root", Email ="RichMurphy131@hotmail.com", AddressOne = "2 Wooddale Drive", AddressTwo = "Ballycullen, Dublin"}, 
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();

            var items = new[]
            {
                new StockItem{Title = "IPhone 6", Category = "Phones", Manufacturer = "Apple", Price = 600, StockLevel = 100, ImageUrl = "http://blogs-images.forbes.com/ewanspence/files/2016/06/uSwitch_iPhone8_render1-1200x577.jpg?width=960"}, 
                new StockItem{Title = "IPhone 4", Category = "Phones", Manufacturer = "Apple", Price = 300, StockLevel = 100, ImageUrl = "http://i.dailymail.co.uk/i/pix/2016/10/18/12/117D8597000005DC-3847234-The_iPhone_4_was_first_released_in_2010_and_was_succeeded_by_the-m-5_1476788714974.jpg"}, 
                new StockItem{Title = "Galaxy S5", Category = "Phones", Manufacturer = "Samsung", Price = 450, StockLevel = 100, ImageUrl = "http://cdn2.gsmarena.com/vv/bigpic/samsung-galaxy-s5-g900f.jpg"}, 
                new StockItem{Title = "Galaxy S6", Category = "Phones", Manufacturer = "Samsung", Price = 600, StockLevel = 100, ImageUrl = "http://cdn2.gsmarena.com/vv/pics/samsung/samsung-galaxy-s6-3.jpg"}, 
                new StockItem{Title = "3400D", Category = "Cameras", Manufacturer = "Nixon", Price = 1000, StockLevel = 100, ImageUrl = "http://img.bbystatic.com/BestBuy_US/store/ee/2016/cam/pr/SOL-3739-nikon/sol-3739-nikon-d3400-pm-panel.png"}, 
                new StockItem{Title = "DSLR", Category = "Cameras", Manufacturer = "Nixon", Price = 800, StockLevel = 100, ImageUrl = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcS0PgukJl8eQac33fluDyZeqr0CKX1ads2ReQhZdcDFbWLZbPMeMA"}, 
                new StockItem{Title = "Inspiron", Category = "Laptops", Manufacturer = "Dell", Price = 550, StockLevel = 100, ImageUrl = "http://i.ebayimg.com/00/z/rzYAAOxyZwpSVl7u/$(KGrHqRHJ!4FJR,Zm1BcBSVl7uWVcQ~~_32.JPG"}, 
                new StockItem{Title = "540X", Category = "Laptops", Manufacturer = "Hewlett Packard", Price = 400, StockLevel = 100, ImageUrl = "http://ssl-product-images.www8-hp.com/digmedialib/prodimg/lowres/c05059975.png"}, 
                new StockItem{Title = "IPad", Category = "Tablets", Manufacturer = "Apple", Price = 800, StockLevel = 100, ImageUrl = "https://d3nevzfk7ii3be.cloudfront.net/igi/Mwfvd5qH3sPoRlF1.large"}, 
                new StockItem{Title = "Pendo", Category = "Tablets", Manufacturer = "Windows", Price = 250, StockLevel = 100, ImageUrl = "http://www.pendo.com.au/media/catalog/product/p/e/pendo_win8_10_1.jpg"}, 
            };

            foreach (var item in items)
            {
                context.StockItems.Add(item);
            }

            context.SaveChanges();
        }
    }
}