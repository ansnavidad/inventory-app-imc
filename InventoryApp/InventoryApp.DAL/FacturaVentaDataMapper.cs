using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class FacturaVentaDataMapper : IDataMapper
    {
        public object getElements()
        {
            throw new NotImplementedException();
        }

        public object getElement(object element)
        {
            throw new NotImplementedException();
        }

        public void udpateElement(object element)
        {
            throw new NotImplementedException();

        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var oAWEntities = new TAE2Entities())
                {
                    FACTURA_VENTA tra = (FACTURA_VENTA)element;
                  

                    oAWEntities.FACTURA_VENTA.AddObject(tra);
                    oAWEntities.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
