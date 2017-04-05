using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Helpers;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class StockItemsController : Controller
    {
        private readonly IDataAccessProvider _accessProvider;

        public StockItemsController(IDataAccessProvider context)
        {
            _accessProvider = context;    
        }

        // GET: StockItems
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            @ViewData["currentFilter"] = searchString;
            IQueryable<StockItem> items = _accessProvider.GetAllItems();

            if (!String.IsNullOrEmpty(searchString))
            {
                items = _accessProvider.GetItemsByCategory(searchString);
            }

            return View(_accessProvider.SortItems(items, sortOrder).ToList());
        }

        // GET: StockItems/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _accessProvider.GetItem(id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // GET: StockItems/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Manufacturer,Price,Category,ImageUrl")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                await _accessProvider.AddStockItem(stockItem);
                return RedirectToAction("Index");
            }
            return View(stockItem);
        }

        // GET: StockItems/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _accessProvider.GetItem(id);
            if (stockItem == null)
            {
                return NotFound();
            }
            return View(stockItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Manufacturer,Price,Category,ImageUrl")] StockItem stockItem)
        {
            if (id != stockItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _accessProvider.UpdateItem(stockItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await StockItemExists(stockItem.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(stockItem);
        }

        // GET: StockItems/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockItem = await _accessProvider.GetItem(id);
            
            if (stockItem == null)
            {
                return NotFound();
            }

            return View(stockItem);
        }

        // POST: StockItems/Delete/5
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

            var comment = new Comment {Title=title, Text=text, StockItemId = itemId, UserId = id, User = user, StockItem = item };
            await _accessProvider.AddComment(comment);

            return View("Details", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRating()
        {
            int id = (int)HttpContext.Session.GetInt32("_ID");
            int itemId = int.Parse(HttpContext.Request.Form["itemID"]);
            var rating = HttpContext.Request.Form["rating"];

            var user = await _accessProvider.GetUser(id);
            var item = await _accessProvider.GetItem(itemId);
            int value = NumberConverter.ConvertStringToInt(rating);

            var score = new Rating {Score = value, User = user, StockItem = item, UserId = id, StockItemId = itemId};
            await _accessProvider.AddRating(score);

            return View("Details", item);
        }
    }
}
