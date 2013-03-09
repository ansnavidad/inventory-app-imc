using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;

namespace InventoryApp.Model.Recibo
{
    public class OrigenClienteModel:ClienteModel,IOrigenModel
    {
        public string OrigenName
        {
            get
            {
                return this.ClienteName;
            }
        }

        public OrigenClienteModel():base(new ClienteDataMapper())
        {

        }
    }
}
