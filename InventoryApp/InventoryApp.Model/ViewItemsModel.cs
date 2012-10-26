using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class ViewItemsModel
    {
        private ItemCollection _items;
        private Item _selectedItem;

        public void loadItems(ItemDataMapper itemDataMapper)
        {
            throw new System.NotImplementedException();
        }
    }
}
