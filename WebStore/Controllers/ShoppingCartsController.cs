using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebStore.Helpers;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ShoppingCartsController : Controller
    {
        public IActionResult Details()
        {
            var json = HttpContext.Session.GetString(SessionKeys.Cart);
            var shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(json);

            if (shoppingCart == null)
                return NotFound();
            
            return View(shoppingCart);
        }
    }
}
