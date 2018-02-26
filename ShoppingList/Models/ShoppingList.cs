using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingList.Models
{
    public class ShopList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Item> Items { get; set; }

        public ShopList()
        {
            Id = 0;
            Name = String.Empty;
            Items = new List<Item>();
        }
    }
}