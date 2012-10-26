using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace InventoryApp.Model
{
    public class ArticuloCollection : ObservableCollection<Articulo>
    {
        public ArticuloCollection()
        {
            this.Add(new Articulo("Modems", 12.2f, "Pink"));
            this.Add(new Articulo("Modems", 12.2f, "Pink"));
            this.Add(new Articulo("Modems", 12.2f, "Pink"));
        }
    }
}
