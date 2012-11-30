using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class ReciboMovimientoDataMapper:IDataMapper
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
            if (element != null && (element as RECIBO_MOVIMIENTO) != null)
            {
                using (var entity = new TAE2Entities())
                {
                    RECIBO_MOVIMIENTO item = (RECIBO_MOVIMIENTO)element;

                    entity.RECIBO_MOVIMIENTO.AddObject(item);
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
