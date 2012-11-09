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
        public object getElements_EntradasSalidasSerie(object element)
        {
            object o = null;
            if (element != null)
            {
                string Eprov = (string)element;

                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.ITEMs
                               where p.NUMERO_SERIE == Eprov
                               select p).ToList();

                    if (((List<ITEM>)res).Count > 0)
                    {
                        foreach (ITEM trans in ((List<ITEM>)res))
                        {
                            trans.ARTICULO = trans.ARTICULO;
                            trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                            trans.ITEM_STATUS = trans.ITEM_STATUS;
                        }
                        o = (object)res;
                    }
                }
            }
            return o;
        }

        public object getElements_EntradasSalidasSKU(object element)
        {
            object o = null;
            if (element != null)
            {
                string Eprov = (string)element;

                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.ITEMs
                               where p.SKU == Eprov
                               select p).ToList();

                    if (((List<ITEM>)res).Count > 0)
                    {
                        foreach (ITEM trans in ((List<ITEM>)res))
                        {
                            trans.ARTICULO = trans.ARTICULO;
                            trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                            trans.ITEM_STATUS = trans.ITEM_STATUS;
                        }
                        o = (object)res;
                    }
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
