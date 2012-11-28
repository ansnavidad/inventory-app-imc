using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL.Recibo
{
    public class FacturaCompraDetalleDataMapper : IDataMapper
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
                using (var entity = new TAE2Entities())
                {
                    FACTURA_DETALLE facturaDetalle = (FACTURA_DETALLE)element;
                    entity.FACTURA_DETALLE.AddObject(facturaDetalle);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
