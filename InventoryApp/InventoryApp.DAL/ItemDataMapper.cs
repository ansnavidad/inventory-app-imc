using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class ItemDataMapper : IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                var resul0 = (from prov in entity.ITEMs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return resul;

                resul = (from item in entity.ITEMs
                         where item.IS_ACTIVE == true
                         where item.IS_MODIFIED == false
                         select item.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonItem(long? Last_Modified_Date)
        {
            string res = null;
            List<ITEM> listItem = new List<ITEM>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ITEMs
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listItem.Add(new ITEM
                     {
                         UNID_ITEM = row.UNID_ITEM,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         SKU = row.SKU,
                         NUMERO_SERIE = row.NUMERO_SERIE,
                         UNID_ITEM_STATUS = row.UNID_ITEM_STATUS,
                         COSTO_UNITARIO = row.COSTO_UNITARIO,
                         UNID_FACTURA_DETALE = row.UNID_FACTURA_DETALE,
                         UNID_EMPRESA = row.UNID_EMPRESA,
                         STATUS = row.STATUS,
                         CANTIDAD= row.CANTIDAD,
                         PEDIMENTO_EXPO= row.PEDIMENTO_EXPO,
                         PEDIMENTO_IMPO= row.PEDIMENTO_IMPO,
                         IS_ACTIVE = row.IS_ACTIVE,
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

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElementSyn((object)poco);
                    }

                    var modifiedCotizacion = entity.ITEMs.First(p => p.UNID_ITEM == poco.UNID_ITEM);
                    modifiedCotizacion.IS_MODIFIED = false;
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
                               where i.NUMERO_SERIE == numSerie && u2.UNID_ALMACEN == almacen.UNID_ALMACEN && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                                   where i.NUMERO_SERIE == numSerie && u1.UNID_ALMACEN == null && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                               where i.NUMERO_SERIE == numSerie && u2.UNID_ALMACEN != almacen.UNID_ALMACEN && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                                   where i.SKU == SKU && u1.UNID_ALMACEN == null && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                               where i.SKU == SKU && u2.UNID_ALMACEN == almacen.UNID_ALMACEN && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                               where i.ITEM.NUMERO_SERIE == numSerie && i.IS_ACTIVE == true
                               select i).ToList();
                    if (((List<ULTIMO_MOVIMIENTO>)res).Count > 0)
                    {
                        foreach (ULTIMO_MOVIMIENTO um in ((List<ULTIMO_MOVIMIENTO>)res))
                        {
                            um.ALMACEN = um.ALMACEN;
                            um.CLIENTE = um.CLIENTE;
                            um.PROVEEDOR = um.PROVEEDOR;
                            um.INFRAESTRUCTURA = um.INFRAESTRUCTURA;
                        }
                    }
                    o = (object)res;
                }
                else
                {
                    var res = (from i in Entity.ULTIMO_MOVIMIENTO
                               where i.ITEM.SKU == SKU && i.IS_ACTIVE == true
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
                               where i.NUMERO_SERIE == numSerie && u2.UNID_ALMACEN == almacen.UNID_ALMACEN && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                               where i.SKU == SKU && u2.UNID_ALMACEN == almacen.UNID_ALMACEN && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                               where i.NUMERO_SERIE == numSerie && u2.UNID_CLIENTE == cliente.UNID_CLIENTE && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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

        public object getElements_EntradasSalidasSerie(INFRAESTRUCTURA infraestructura, string numSerie, string SKU)
        {
            object o = null;
            using (var Entity = new TAE2Entities())
            {
                if (!numSerie.Equals(""))
                {
                    var res = (from i in Entity.ITEMs
                               join u1 in Entity.ULTIMO_MOVIMIENTO on i.UNID_ITEM equals u1.UNID_ITEM
                               join u2 in Entity.INFRAESTRUCTURAs on u1.UNID_INFRAESTRUCTURA equals u2.UNID_INFRAESTRUCTURA
                               where i.NUMERO_SERIE == numSerie && u2.UNID_INFRAESTRUCTURA == infraestructura.UNID_INFRAESTRUCTURA && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                               join u2 in Entity.INFRAESTRUCTURAs on u1.UNID_INFRAESTRUCTURA equals u2.UNID_INFRAESTRUCTURA
                               where i.SKU == SKU && u2.UNID_INFRAESTRUCTURA == infraestructura.UNID_INFRAESTRUCTURA && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                                   where i.NUMERO_SERIE == numSerie && u2.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                                   where i.SKU == SKU && u2.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                                   where i.NUMERO_SERIE == numSerie && u2.UNID_CLIENTE == cliente.UNID_CLIENTE && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                                   where i.SKU == SKU && u2.UNID_CLIENTE == cliente.UNID_CLIENTE && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
                               where i.NUMERO_SERIE == numSerie && u2.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR && i.IS_ACTIVE == true && u1.IS_ACTIVE == true
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
            object o = null;

            try
            {
                if (element != null)
                {
                    ITEM item = (ITEM)element;
                    using (var entity = new TAE2Entities())
                    {
                        if (!String.IsNullOrEmpty(item.SKU))
                        {
                            var res = (from i in entity.ITEMs
                                       where i.SKU == item.SKU
                                       select i).First();
                            res.ARTICULO = res.ARTICULO;
                            res.ARTICULO.UNID_CATEGORIA = res.ARTICULO.UNID_CATEGORIA;
                            res.ITEM_STATUS = res.ITEM_STATUS;
                            res.FACTURA_DETALLE = res.FACTURA_DETALLE;
                            res.PROPIEDAD = res.PROPIEDAD;
                            o = res;
                        }
                        else
                        {
                            var res = (from i in entity.ITEMs
                                       where i.NUMERO_SERIE == item.NUMERO_SERIE
                                       select i).First();
                            res.ARTICULO = res.ARTICULO;
                            res.ARTICULO.UNID_CATEGORIA = res.ARTICULO.UNID_CATEGORIA;
                            res.ITEM_STATUS = res.ITEM_STATUS;
                            res.FACTURA_DETALLE = res.FACTURA_DETALLE;
                            res.PROPIEDAD = res.PROPIEDAD;
                            o = res;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                
                ;
            }

            return o;
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
                    modifiedItem.CANTIDAD = item.CANTIDAD;
                    modifiedItem.PEDIMENTO_EXPO = item.PEDIMENTO_EXPO;
                    modifiedItem.PEDIMENTO_IMPO = item.PEDIMENTO_IMPO;
                    modifiedItem.UNID_PROPIEDAD = item.UNID_PROPIEDAD;
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
                    item.IS_ACTIVE = true;
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

        public void insertElementSyn(object element)
        {
            if (element != null && (element as ITEM) != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ITEM item = (ITEM)element;
                    //Sync
                    
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
                         CANTIDAD = row.CANTIDAD,
                         PEDIMENTO_EXPO = row.PEDIMENTO_EXPO,
                         PEDIMENTO_IMPO = row.PEDIMENTO_IMPO,
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

        /// <summary>
        /// Método que Deserializa JSon a List<ITEM>
        /// </summary>
        /// <returns>Regresa List<ITEM></returns>
        /// <returns>Si no regresa null</returns>
        public List<ITEM> GetDeserializeItem(string listPocos)
        {
            List<ITEM> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<ITEM>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetItem()
        {
            List<ITEM> reset = new List<ITEM>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ITEMs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new ITEM
                     {
                         UNID_ITEM = row.UNID_ITEM,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         SKU = row.SKU,
                         NUMERO_SERIE = row.NUMERO_SERIE,
                         UNID_ITEM_STATUS = row.UNID_ITEM_STATUS,
                         COSTO_UNITARIO = row.COSTO_UNITARIO,
                         UNID_FACTURA_DETALE = row.UNID_FACTURA_DETALE,
                         UNID_EMPRESA = row.UNID_EMPRESA,
                         STATUS = row.STATUS,
                         CANTIDAD = row.CANTIDAD,
                         PEDIMENTO_EXPO = row.PEDIMENTO_EXPO,
                         PEDIMENTO_IMPO = row.PEDIMENTO_IMPO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.ITEMs.First(p => p.UNID_ITEM == item.UNID_ITEM);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
