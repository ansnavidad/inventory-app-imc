using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.DAL
{
    public class Item:Articulo
    {
        private Articulo _articulo;
        private string _sku;
        private string _serialNbr;
        private float _precio;
        private float _imputesto;
    }
}
