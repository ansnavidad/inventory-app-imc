using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class ItemDataMapper : IDataMapper
    {
        public object getElements_EntradasSalidas(object element)
        {
            object o = null;
            if (element != null)
            {
                ARTICULO Eprov = (ARTICULO)element;

                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.ITEMs
                               where p.UNID_ARTICULO == Eprov.UNID_ARTICULO
                               select p).ToList();

                    foreach (ITEM trans in ((List<ITEM>)res))
                    {
                        trans.ARTICULO = trans.ARTICULO;
                        trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                        trans.ITEM_STATUS = trans.ITEM_STATUS;
                    }

                    o = (object)res;
                }
            }
            return o;
        }

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
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
