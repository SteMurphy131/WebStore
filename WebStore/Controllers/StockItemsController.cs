using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebStore.Data;
using WebStore.Helpers;
using WebStore.Models;
using WebStore.Patterns;

namespace WebStore.Controllers
{
    public class StockItemsController : Controller
    {
        private readonly IDataAccessProvider _accessProvider;

        public StockItemsController(IDataAccessProvider context)
        {
            _accessProvider = context;    
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            @ViewData["currentFilter"] = searchString;
            IQueryable<StockItem> items = _accessProvider.GetAllItems();

            if (!string.IsNullOrEmpty(searchString))
                items = _accessProvider.GetItemsByCategory(searchString);
            
            return View(_accessProvider.SortItems(items, sortOrder).ToList());
        }

        public async Task<IActionResult> Details(int id)
        {
            var stockItem = await _accessProvider.GetItem(id);
            if (stockItem == null)
                return NotFound();
            
            return View(stockItem);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Manufacturer,Price,Category,ImageUrl, StockLevel")] StockItem stockItem)
        {
            if (!ModelState.IsValid)
                return View(stockItem);

            await _accessProvider.AddStockItem(stockItem);
            return RedirectToAction("Index");
        }

        // GET: StockItems/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var stockItem = await _accessProvider.GetItem(id);
            if (stockItem == null)
                return NotFound();
            
            return View(stockItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Manufacturer,Price,Category,ImageUrl, StockLevel")] StockItem stockItem)
        {
            if (id != stockItem.ID)
                return NotFound();

            if (!ModelState.IsValid)
                return View(stockItem);

            try
            {
                await _accessProvider.UpdateItem(stockItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await StockItemExists(stockItem.ID))
                    return NotFound();
                    
                throw; 
            }
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var stockItem = await _accessProvider.GetItem(id);
            if (stockItem == null)
                return NotFound();
            
            return View(stockItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockItem = await _accessProvider.GetItem(id);
            await _accessProvider.DeleteItem(stockItem);
            return RedirectToAction("Index");
        }

        private async Task<bool> StockItemExists(int id)
        {
            var item = await _accessProvider.GetItem(id);
            return await _accessProvider.CheckForItem(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment()
        {
            int id = (int)HttpContext.Session.GetInt32("_ID");
            string title = HttpContext.Request.Form["title"];
            string text = HttpContext.Request.Form["text"];
            int itemId = int.Parse(HttpContext.Request.Form["itemID"]);

            var item = await _accessProvider.GetItem(itemId);
            var user = await _accessProvider.GetUser(id);

            CommentBuilder cBuilder = new CommentBuilder();
            var comment = cBuilder.Create(id)
                .WithText(text)
                .WithTitle(title)
                .WithUser(user)
                .WithItem(item)
                .WithItemId(itemId)
                .Build();

            await _accessProvider.AddComment(comment);

            return View("Details", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRating()
        {
            int id = (int)HttpContext.Session.GetInt32("_ID");
            int itemId = int.Parse(HttpContext.Request.Form["itemID"]);
            var scoreString = HttpContext.Request.Form["rating"];

            var user = await _accessProvider.GetUser(id);
            var item = await _accessProvider.GetItem(itemId);
            int score = NumberConverter.ConvertStringToInt(scoreString);

            var rBuilder = new RatingBuilder();
            var rating = rBuilder.Create(id)
                .WithScore(score)
                .WithItem(item)
                .WithUser(user)
                .WithItemId(itemId)
                .Build();
            
            await _accessProvider.AddRating(rating);

            return View("Details", item);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var str = HttpContext.Session.GetString(SessionKeys.Cart);
            var obj = JsonConvert.DeserializeObject<ShoppingCart>(str);

            var item = await _accessProvider.GetItem(id);

            obj.Items.Add(item);

            var jsonCart = JsonConvert.SerializeObject(obj);
            HttpContext.Session.SetString(SessionKeys.Cart, jsonCart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Purchase()
        {
            int userId = (int) HttpContext.Session.GetInt32(SessionKeys.Id);
            var user = await _accessProvider.GetUser(userId);
            var str = HttpContext.Session.GetString(SessionKeys.Cart);
            var cart = JsonConvert.DeserializeObject<ShoppingCart>(str);

            var builder = new PurchaseBuilder();
            Purchase purchase = builder.Create(userId)
                .WithUser(user)
                .WithPurchaseItems(cart.Items)
                .Build();

            await PurchaseManager.ProcessPurchase(_accessProvider, cart);

            cart.Items.Clear();
            var jsonCart = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(SessionKeys.Cart, jsonCart);

            await _accessProvider.AddPurchase(purchase);
            
            return RedirectToAction("Index");
        }
    }
}

