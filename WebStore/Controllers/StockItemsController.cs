using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStore.Data;
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
        public async Task<IActionResult> Index()
        {
            return View(await _accessProvider.GetAllItems());
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

        // POST: StockItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: StockItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    if (!StockItemExists(stockItem.ID))
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
    }
}
