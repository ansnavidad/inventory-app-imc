using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class ItemDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {

            if (element != null)
            {
                ITEM poco = (ITEM)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ITEMs
                                 where poco.UNID_ITEM == cust.UNID_ITEM
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        var aux = query.First();

                        if (UNID.compareUNIDS(aux.LAST_MODIFIED_DATE, poco.LAST_MODIFIED_DATE))
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }

                    var modifiedCotizacion = entity.ITEMs.First(p => p.UNID_ITEM == poco.UNID_ITEM);
                    modifiedCotizacion.IS_ACTIVE = false;
                    entity.SaveChanges();
                }
            }
        }
        
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
                            //trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                            trans.ITEM_STATUS = trans.ITEM_STATUS;
                            trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                            trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                            trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                            trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                        }
                        o = (object)res;
                    }
                }
            }
            return o;
        }

        public object getElements_EntradasSalidasSerie(ALMACEN almacen, string numSerie, string SKU)
        {
            object o = null;
                
                using (var Entity = new TAE2Entities())
                {
                    if (!numSerie.Equals(""))
                    {
                        List<ITEM> final = new List<ITEM>();
                        
                        var res = (from i in Entity.ITEMs
                                   join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                                   where i.NUMERO_SERIE == numSerie && u1.UNID_ALMACEN == null && i.IS_ACTIVE == true
                                   select i).ToList();                        

                        if (((List<ITEM>)res).Count > 0)
                        {
                            foreach (ITEM trans in ((List<ITEM>)res))
                            {
                                trans.ARTICULO = trans.ARTICULO;
                                //trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                                trans.ITEM_STATUS = trans.ITEM_STATUS;
                                trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                                trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                                trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                                trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                                final.Add(trans);
                            }                           
                        }

                        res = (from i in Entity.ITEMs
                                   join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                                   join u2 in Entity.ALMACENs on u1.UNID_ALMACEN equals u2.UNID_ALMACEN
                                   where i.NUMERO_SERIE == numSerie && u2.UNID_ALMACEN != almacen.UNID_ALMACEN && i.IS_ACTIVE == true
                                   select i).ToList();

                        if (((List<ITEM>)res).Count > 0)
                        {
                            foreach (ITEM trans in ((List<ITEM>)res))
                            {
                                trans.ARTICULO = trans.ARTICULO;
                                //trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                                trans.ITEM_STATUS = trans.ITEM_STATUS;
                                trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                                trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                                trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                                trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                                final.Add(trans);
                            }                            
                        }
                        o = (object)final;
                    }
                    else {

                        List<ITEM> final = new List<ITEM>();

                        var res = (from i in Entity.ITEMs
                                   join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                                   where i.SKU == SKU && u1.UNID_ALMACEN == null && i.IS_ACTIVE == true
                                   select i).ToList();

                        if (((List<ITEM>)res).Count > 0)
                        {
                            foreach (ITEM trans in ((List<ITEM>)res))
                            {
                                trans.ARTICULO = trans.ARTICULO;
                                //trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                                trans.ITEM_STATUS = trans.ITEM_STATUS;
                                trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                                trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                                trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                                trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                                final.Add(trans);
                            }
                        }

                        res = (from i in Entity.ITEMs
                               join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                               join u2 in Entity.ALMACENs on u1.UNID_ALMACEN equals u2.UNID_ALMACEN
                               where i.SKU == SKU && u2.UNID_ALMACEN == almacen.UNID_ALMACEN && i.IS_ACTIVE == true
                               select i).ToList();

                        if (((List<ITEM>)res).Count > 0)
                        {
                            foreach (ITEM trans in ((List<ITEM>)res))
                            {
                                trans.ARTICULO = trans.ARTICULO;
                                //trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                                trans.ITEM_STATUS = trans.ITEM_STATUS;
                                trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                                trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                                trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                                trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                                final.Add(trans);
                            }
                        }
                        o = (object)final;
                    }                    
                }
            
            return o;
        }

        public object getAlmacenDisponible(string numSerie, string SKU)
        {
            object o = null;
            using (var Entity = new TAE2Entities())
            {
                if (!numSerie.Equals(""))
                {
                    var res = (from i in Entity.ULTIMO_MOVIMIENTO
                               where i.ITEM.NUMERO_SERIE == numSerie
                               select i).ToList();
                    if (((List<ULTIMO_MOVIMIENTO>)res).Count > 0)
                    {
                        foreach (ULTIMO_MOVIMIENTO um in ((List<ULTIMO_MOVIMIENTO>)res))
                        {
                            um.ALMACEN = um.ALMACEN;
                            um.CLIENTE = um.CLIENTE;
                            um.PROVEEDOR = um.PROVEEDOR;
                        }
                    }
                    o = (object)res;
                }
                else
                {
                    var res = (from i in Entity.ULTIMO_MOVIMIENTO
                               where i.ITEM.SKU == SKU
                               select i).ToList();
                    if (((List<ULTIMO_MOVIMIENTO>)res).Count > 0)
                    {
                        foreach (ULTIMO_MOVIMIENTO um in ((List<ULTIMO_MOVIMIENTO>)res))
                        {
                            um.ALMACEN = um.ALMACEN;
                            um.CLIENTE = um.CLIENTE;
                            um.PROVEEDOR = um.PROVEEDOR;
                        }
                    }
                    o = (object)res;
                }
                
            }
            return (o);
            

        }
        public object getElements_EntradasSalidasSerie2(ALMACEN almacen, string numSerie, string SKU)
        {
            object o = null;

            using (var Entity = new TAE2Entities())
            {
                if (!numSerie.Equals(""))
                {
                    var res = (from i in Entity.ITEMs
                               join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                               join u2 in Entity.ALMACENs on u1.UNID_ALMACEN equals u2.UNID_ALMACEN
                               where i.NUMERO_SERIE == numSerie && u2.UNID_ALMACEN == almacen.UNID_ALMACEN && i.IS_ACTIVE == true
                               select i).ToList();

                    //var res = (from cust in Entity.ITEMs
                    //           where cust.NUMERO_SERIE == numSerie && cust.IS_ACTIVE == true
                    //           select cust).ToList();

                    if (((List<ITEM>)res).Count > 0)
                    {
                        foreach (ITEM trans in ((List<ITEM>)res))
                        {
                            trans.ARTICULO = trans.ARTICULO;
                            //trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                            trans.ITEM_STATUS = trans.ITEM_STATUS;
                            trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                            trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                            trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                            trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                        }
                        o = (object)res;
                    }
                }
                else
                {

                    var res = (from i in Entity.ITEMs
                               join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                               join u2 in Entity.ALMACENs on u1.UNID_ALMACEN equals u2.UNID_ALMACEN
                               where i.SKU == SKU && u2.UNID_ALMACEN == almacen.UNID_ALMACEN && i.IS_ACTIVE == true
                               select i).ToList();

                    if (((List<ITEM>)res).Count > 0)
                    {
                        foreach (ITEM trans in ((List<ITEM>)res))
                        {
                            trans.ARTICULO = trans.ARTICULO;
                            //trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                            trans.ITEM_STATUS = trans.ITEM_STATUS;
                            trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                            trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                            trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                            trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                        }
                        o = (object)res;
                    }
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
                            //trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                            trans.ITEM_STATUS = trans.ITEM_STATUS;
                            trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                            trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                            trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                            trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                        }
                        o = (object)res;
                    }
                }
            }
            return o;
        }

        public object getElements_EntradasSalidasSerie(PROVEEDOR proveedor, CLIENTE cliente, string numSerie, string SKU)
        {
            object o = null;

            using (var Entity = new TAE2Entities())
            {
                if (proveedor != null)
                {
                    if (!numSerie.Equals(""))
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
                                trans.ITEM_STATUS = trans.ITEM_STATUS;
                                trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                                trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                                trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                                trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                            }
                            o = (object)res;
                        }
                    }
                    else {

                        var res = (from i in Entity.ITEMs
                                   join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                                   join u2 in Entity.PROVEEDORs on u1.UNID_PROVEEDOR equals u2.UNID_PROVEEDOR
                                   where i.SKU == SKU && u2.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR && i.IS_ACTIVE == true
                                   select i).ToList();

                        if (((List<ITEM>)res).Count > 0)
                        {
                            foreach (ITEM trans in ((List<ITEM>)res))
                            {
                                trans.ARTICULO = trans.ARTICULO;
                                trans.ITEM_STATUS = trans.ITEM_STATUS;
                                trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                                trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                                trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                                trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                            }
                            o = (object)res;
                        }
                    }
                }
                else
                {
                    if (!numSerie.Equals(""))
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
                                trans.ITEM_STATUS = trans.ITEM_STATUS;
                                trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                                trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                                trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                                trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                            }
                            o = (object)res;
                        }
                    }
                    else
                    {
                        var res = (from i in Entity.ITEMs
                                   join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                                   join u2 in Entity.CLIENTEs on u1.UNID_CLIENTE equals u2.UNID_CLIENTE
                                   where i.SKU == SKU && u2.UNID_CLIENTE == cliente.UNID_CLIENTE && i.IS_ACTIVE == true
                                   select i).ToList();

                        if (((List<ITEM>)res).Count > 0)
                        {
                            foreach (ITEM trans in ((List<ITEM>)res))
                            {
                                trans.ARTICULO = trans.ARTICULO;
                                trans.ITEM_STATUS = trans.ITEM_STATUS;
                                trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                                trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                                trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                                trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;
                            }
                            o = (object)res;
                        }
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
                            //trans.FACTURA_DETALLE = trans.FACTURA_DETALLE;
                            trans.ITEM_STATUS = trans.ITEM_STATUS;
                            trans.ARTICULO.EQUIPO = trans.ARTICULO.EQUIPO;
                            trans.ARTICULO.CATEGORIA = trans.ARTICULO.CATEGORIA;
                            trans.ARTICULO.MARCA = trans.ARTICULO.MARCA;
                            trans.ARTICULO.MODELO = trans.ARTICULO.MODELO;                            
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
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ITEM item = (ITEM)element;
                    var modifiedItem = entity.ITEMs.First(p => p.UNID_ITEM == item.UNID_ITEM);
                    modifiedItem.UNID_ITEM_STATUS = item.UNID_ITEM_STATUS;
                    modifiedItem.UNID_FACTURA_DETALE = item.UNID_FACTURA_DETALE;
                    modifiedItem.UNID_EMPRESA = item.UNID_EMPRESA;
                    modifiedItem.UNID_ARTICULO = item.UNID_ARTICULO;
                    modifiedItem.STATUS = item.STATUS;
                    modifiedItem.SKU = item.SKU;
                    modifiedItem.NUMERO_SERIE = item.NUMERO_SERIE;
                    modifiedItem.IS_ACTIVE = item.IS_ACTIVE;
                    modifiedItem.COSTO_UNITARIO = item.COSTO_UNITARIO;
                    //Sync
                    modifiedItem.IS_MODIFIED = true;
                    modifiedItem.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public void insertElement(object element)
        {
            if (element != null && (element as ITEM)!=null)
            {
                using (var entity = new TAE2Entities())
                {
                    ITEM item = (ITEM)element;
                    //Sync
                    item.IS_MODIFIED = true;
                    item.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.ITEMs.AddObject(item);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<ITEM> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de ITEM</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonItem()
        {
            string res = null;
            List<ITEM> listItem = new List<ITEM>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ITEMs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listItem.Add(new ITEM
                     {
                         UNID_ITEM=row.UNID_ITEM,
                         UNID_ARTICULO=row.UNID_ARTICULO,
                         SKU=row.SKU,
                         NUMERO_SERIE=row.NUMERO_SERIE,
                         UNID_ITEM_STATUS=row.UNID_ITEM_STATUS,
                         COSTO_UNITARIO=row.COSTO_UNITARIO,
                         UNID_FACTURA_DETALE=row.UNID_FACTURA_DETALE,
                         UNID_EMPRESA=row.UNID_EMPRESA,
                         STATUS=row.STATUS,
                         IS_ACTIVE=row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listItem.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listItem);
                }
                return res;
            }
        }
    }
}
