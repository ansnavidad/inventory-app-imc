using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace InventoryApp.Model
{
    public class ItemCollection : ObservableCollection<Item>
    {
        public ItemCollection()
        {
            this.Add(new Item(1, "Item 1"));
            this.Add(new Item(1, "Item 2"));
        }
    }
}
