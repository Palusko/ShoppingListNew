using ShoppingList.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShoppingList.Controllers
{
    public class ShoppingListController : ApiController
    {
        /*
        // GET: api/ShoppingList
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        */

        public static List<ShopList> shoppingLists = new List<ShopList>
        {
            new ShopList()
            {
                Id = 0,
                Name = "Groceries",
                Items =
                {
                    new Item { Id = 0, Name = "Milk", ShoppingListId = 0 },
                    new Item { Id = 1, Name = "Chocolate", ShoppingListId = 0},
                    new Item { Id = 2, Name = "Corn Flakes", ShoppingListId = 0},
                }
            },
            new ShopList()
            {
                Id = 1,
                Name = "Hardware"
            }
        };

        // GET: api/ShoppingList/5
        //rather than string, we return IHttpActionResult. This will easily let front end know if an error occured
        public IHttpActionResult Get(int id)
        {
            ShopList result = shoppingLists.FirstOrDefault(s => s.Id == id);

            if (result == null)
                return NotFound();

            return Ok(result); //200 ok
        }

        // POST: api/ShoppingList
        public IEnumerable Post([FromBody]ShopList newList)
        {
            newList.Id = shoppingLists.Count;
            shoppingLists.Add(newList);

            return shoppingLists;
        }

        /*
        // PUT: api/ShoppingList/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ShoppingList/5
        public void Delete(int id)
        {
        }
        */
    }
}
