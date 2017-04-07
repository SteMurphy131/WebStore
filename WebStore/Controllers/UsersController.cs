using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebStore.Data;
using WebStore.Helpers;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class UsersController : Controller
    {
        private readonly IDataAccessProvider _accessProvider;

        public UsersController(IDataAccessProvider context)
        {
            _accessProvider = context;    
        }

        public IActionResult Index()
        {
            return View(_accessProvider.GetAllUsers());
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind("Name, Password, ConfirmPassword, Email, ConfirmEmail")] User user)
        {
            if (user.Name == null || user.Password == null)
                return View("LogIn");

            User u = await _accessProvider.LogIn(user);

            if (u == null)
                return View("LogIn");

            var cart = new ShoppingCart { UserID = u.ID, Items = new List<StockItem>() };
            var json = JsonConvert.SerializeObject(cart);

            HttpContext.Session.SetString(SessionKeys.Name, u.Name);
            HttpContext.Session.SetInt32(SessionKeys.LoggedIn, 1);
            HttpContext.Session.SetInt32(SessionKeys.Id, u.ID);
            HttpContext.Session.SetString(SessionKeys.Cart, json);
            HttpContext.Session.SetInt32(SessionKeys.Admin, u.Admin ? 1 : 0);


            return View("Details", u);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.SetString(SessionKeys.Name, "");
            HttpContext.Session.SetInt32(SessionKeys.LoggedIn, 0);
            HttpContext.Session.SetInt32(SessionKeys.Id, 0);
            HttpContext.Session.SetString(SessionKeys.Cart, "");
            HttpContext.Session.SetInt32(SessionKeys.Admin, 0);

            return View("LogIn");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Name, Email, Password, AddressOne, AddressTwo")] User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            await _accessProvider.AddUser(user);
            return RedirectToAction("LogIn");
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _accessProvider.GetUser(id);

            if (user == null)
                return NotFound();
            
            return View(user);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _accessProvider.GetUser(id);

            if (user == null)
                return NotFound();
            
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Admin,Name,Email,Password,AddressOne,AddressTwo")] User user)
        {
            if (id != user.ID)
                return NotFound();

            if (!ModelState.IsValid)
                return View(user);

            try
            {
                await _accessProvider.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (! await UserExists(user.ID))
                    return NotFound();
                
                throw; 
            }

            return RedirectToAction("LogIn");
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var u = await _accessProvider.GetUser(id);
            var user = await _accessProvider.DeleteUser(u);

            if (user == null)
                return NotFound();
            
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _accessProvider.GetUser(id);
            await _accessProvider.DeleteUser(user);
            return RedirectToAction("Details");
        }

        private async Task<bool> UserExists(int id)
        {
            var user = await _accessProvider.GetUser(id);
            return await _accessProvider.CheckForUser(user);
        }
    }
}
