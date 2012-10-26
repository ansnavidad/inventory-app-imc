using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.DAL
{
    public class ItemDataMapper
    {
        public ItemCollection getItems(Item item)
        {
            //toda la l{ogica
            if (item != null)
            {
                //store sin parametro
            }
            else
            {
                //store con parametro
            }
        }

        public void insertItems()
        {
            
        }

        public ItemCollection getItems()
        {
            throw new System.NotImplementedException();
        }

        public ItemCollection getItems(Articulo articulo)
        {
            throw new System.NotImplementedException();
        }

        public void deleteItems(Item item)
        {
            throw new System.NotImplementedException();
        }

        public void updateItems(Item item)
        {
            throw new System.NotImplementedException();
        }
    }
}
