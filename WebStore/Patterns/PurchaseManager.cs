using System.Threading.Tasks;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Patterns
{
    public static class PurchaseManager
    {
        public static async Task<bool> ProcessPurchase(IDataAccessProvider accessProvider, ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                var stockItem = await accessProvider.GetItem(item.ID);
                stockItem.StockLevel--;
                await accessProvider.UpdateItem(stockItem);
            }

            return true;
        }
    }
}