using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingList.Controllers
{
    public class ItemController : ApiController
    {
        // POST: api/Item
        public IHttpActionResult Post([FromBody]Item item)
        {
            ShopList shoppingList =
                ShoppingListController.shoppingLists
                .Where(s => s.Id == item.ShoppingListId)
                .FirstOrDefault();

            if (shoppingList == null)
                return NotFound();

            item.Id = shoppingList.Items.Max(i => i.Id) + 1;
            shoppingList.Items.Add(item);
            return Ok(shoppingList);
        }

        // PUT: api/Item/5
        public IHttpActionResult Put(int id, [FromBody]Item item)
        {
            ShopList shoppingList =
                ShoppingListController.shoppingLists
                .Where(s => s.Id == item.ShoppingListId)
                .FirstOrDefault();

            if (shoppingList == null)
                return NotFound();

            Item changedItem = shoppingList.Items.Where(i => i.Id == id).FirstOrDefault();

            if (changedItem == null)
                return NotFound();

            changedItem.Checked = item.Checked;
            return Ok(shoppingList);
        }

        // DELETE: api/Item/5
        public IHttpActionResult Delete(int id)
        {
            //let's pretend that the items we are looking for belong to first shopping list
            ShopList shoppingList = ShoppingListController.shoppingLists[0];
            Item item = shoppingList.Items.FirstOrDefault(i => i.Id == id);

            if (item == null)
                return NotFound();

            shoppingList.Items.Remove(item);

            return Ok(shoppingList);
        }
    }
}
