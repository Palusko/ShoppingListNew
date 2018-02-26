using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingList.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public int ShoppingListId { get; set; }

        public Item()
        {
            Id = 0;
            Name = string.Empty;
            Checked = false;
            ShoppingListId = -1;
        }
    }
}