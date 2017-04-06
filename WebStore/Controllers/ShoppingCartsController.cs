using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private const string SessionKeyName = "_Name";
        private const string SessionKeyID = "_ID";
        private const string SessionKeyLoggedIn = "_Logged";
        private const string SessionKeyCart = "_Cart";

        public IActionResult Details()
        {
            var json = HttpContext.Session.GetString(SessionKeyCart);
            var shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(json);

            if (shoppingCart == null)
                return NotFound();
            
            return View(shoppingCart);
        }
    }
}
