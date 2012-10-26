using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace InventoryApp.DAL
{
    class ArticuloCollection : ObservableCollection<Articulo>
    {
        public ArticuloCollection() { }
    }
}
