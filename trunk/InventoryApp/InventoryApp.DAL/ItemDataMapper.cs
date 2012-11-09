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
        public object getElements_EntradasSalidasSerie(ALMACEN almacen, string numSerie)
        {
            object o = null;
            if (almacen != null && !numSerie.Equals(""))
            {               
                using (var Entity = new TAE2Entities())
                {
                    var res = (from i in Entity.ITEMs
                                    join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                                    join u2 in Entity.ALMACENs on u1.UNID_ALMACEN equals u2.UNID_ALMACEN
                                    where i.NUMERO_SERIE == numSerie && u2.UNID_ALMACEN == almacen.UNID_ALMACEN
                                    select i).ToList();

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

        public object getElements_EntradasSalidasSKU(ALMACEN almacen, string SKU)
        {
            object o = null;
            if (almacen != null && !SKU.Equals(""))
            {
                using (var Entity = new TAE2Entities())
                {
                    var res = (from i in Entity.ITEMs
                               join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                               join u2 in Entity.ALMACENs on u1.UNID_ALMACEN equals u2.UNID_ALMACEN
                               where i.SKU == SKU && u2.UNID_ALMACEN == almacen.UNID_ALMACEN
                               select i).ToList();

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
