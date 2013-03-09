using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;

namespace InventoryApp.Model.Recibo
{
    public class OrigenProveedorModel:ProveedorModel,IOrigenModel
    {
        public string OrigenName
        {
            get
            {
                return this.ProveedorName;
            }
        }

        public OrigenProveedorModel():base(new ProveedorDataMapper())
        {

        }
    }
}
