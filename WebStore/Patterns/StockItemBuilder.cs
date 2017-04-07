using WebStore.Models;

namespace WebStore.Patterns
{
    public class StockItemBuilder
    {
        private string Title { get; set; }
        private string Manufacturer { get; set; }
        private double Price { get; set; }
        private string Category { get; set; }
        private string ImageUrl { get; set; }
        private int StockLevel { get; set; }
        private int Id { get; set; }

        public StockItemBuilder CreateItem(string name)
        {
            Title = name;
            return this;
        }

        public StockItemBuilder WithManufacturer(string man)
        {
            Manufacturer = man;
            return this;
        }

        public StockItemBuilder WithPrice(double price)
        {
            Price = price;
            return this;
        }

        public StockItemBuilder WithCategory(string cat)
        {
            Category = cat;
            return this;
        }

        public StockItemBuilder WithImageUrl(string url)
        {
            ImageUrl = url;
            return this;
        }

        public StockItemBuilder WithStockLevel(int level)
        {
            StockLevel = level;
            return this;
        }

        public StockItemBuilder WithId(int id)
        {
            Id = id;
            return this;
        }

        public static implicit operator StockItem(StockItemBuilder iBuilder)
        {
            return new StockItem
            {
                Title = iBuilder.Title,
                Manufacturer = iBuilder.Manufacturer,
                Price = iBuilder.Price,
                Category = iBuilder.Category,
                ImageUrl = iBuilder.ImageUrl,
                StockLevel = iBuilder.StockLevel,
                ID = iBuilder.Id
            };
        }
    }
}