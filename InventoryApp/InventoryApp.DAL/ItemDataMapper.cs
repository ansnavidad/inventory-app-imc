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
                                    where i.NUMERO_SERIE == numSerie && u2.UNID_ALMACEN == almacen.UNID_ALMACEN && i.IS_ACTIVE == true
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

        public object getElements_EntradasSalidasSerie(string numSerie)
        {
            object o = null;
                using (var Entity = new TAE2Entities())
                {
                    var res = (from cust in Entity.ITEMs
                               where cust.NUMERO_SERIE == numSerie && cust.IS_ACTIVE == true
                               select cust).ToList();

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
            
            return o;
        }
        public object getElements_EntradasSalidasSerie(CLIENTE cliente, string numSerie)
        {
            object o = null;
            if (cliente != null && !numSerie.Equals(""))
            {
                using (var Entity = new TAE2Entities())
                {
                    var res = (from i in Entity.ITEMs
                               join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                               join u2 in Entity.CLIENTEs on u1.UNID_CLIENTE equals u2.UNID_CLIENTE
                               where i.NUMERO_SERIE == numSerie && u2.UNID_CLIENTE == cliente.UNID_CLIENTE && i.IS_ACTIVE == true
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

        public object getElements_EntradasSalidasSerie(PROVEEDOR proveedor, string numSerie)
        {
            object o = null;
            if (proveedor != null && !numSerie.Equals(""))
            {
                using (var Entity = new TAE2Entities())
                {
                    var res = (from i in Entity.ITEMs
                               join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                               join u2 in Entity.PROVEEDORs on u1.UNID_PROVEEDOR equals u2.UNID_PROVEEDOR
                               where i.NUMERO_SERIE == numSerie && u2.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR && i.IS_ACTIVE == true
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
