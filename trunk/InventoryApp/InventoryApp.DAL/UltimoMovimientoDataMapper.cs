using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class UltimoMovimientoDataMapper : IDataMapper
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
                resul = (from articulo in entity.ULTIMO_MOVIMIENTO
                         where articulo.IS_ACTIVE == true
                         where articulo.IS_MODIFIED == false
                         select articulo.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonUltimoMovimiento(long? LMD)
        {
            string res = null;
            List<ULTIMO_MOVIMIENTO> listUltimoMovimiento = new List<ULTIMO_MOVIMIENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ULTIMO_MOVIMIENTO
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     listUltimoMovimiento.Add(new ULTIMO_MOVIMIENTO
                     {
                         UNID_ITEM = row.UNID_ITEM,
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_CLIENTE = row.UNID_CLIENTE,
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         UNID_MOVIMIENTO_DETALLE = row.UNID_MOVIMIENTO_DETALLE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         IS_ACTIVE=row.IS_ACTIVE,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         UNID_INFRAESTRUCTURA= row.UNID_INFRAESTRUCTURA,
                         CANTIDAD=row.CANTIDAD,
                         UNID_ULTIMO_MOVIMIENTO= row.UNID_ULTIMO_MOVIMIENTO
                     });
                 });
                if (listUltimoMovimiento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listUltimoMovimiento);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                ULTIMO_MOVIMIENTO poco = (ULTIMO_MOVIMIENTO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ULTIMO_MOVIMIENTO
                                 where poco.UNID_ITEM == cust.UNID_ITEM
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        var aux = query.First();

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElementSync((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElementSny((object)poco);
                    }

                    var modifiedMenu = entity.ULTIMO_MOVIMIENTO.First(p => p.UNID_ITEM == poco.UNID_ITEM);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
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
                    ULTIMO_MOVIMIENTO ultimoMov = (ULTIMO_MOVIMIENTO)element;

                    var query = (from p in entity.ULTIMO_MOVIMIENTO
                                 where p.UNID_ITEM == ultimoMov.UNID_ITEM
                                 select p).ToList();

                    if (query.Count == 0)
                    {
                        ultimoMov.UNID_ULTIMO_MOVIMIENTO = UNID.getNewUNID();
                        //Sync
                        ultimoMov.IS_MODIFIED = true;
                        ultimoMov.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.ULTIMO_MOVIMIENTO.AddObject(ultimoMov);                       
                    }
                    else {

                        var modifiedMov = entity.ULTIMO_MOVIMIENTO.First(p => p.UNID_ITEM == ultimoMov.UNID_ITEM);

                        modifiedMov.UNID_ALMACEN = ultimoMov.UNID_ALMACEN;
                        modifiedMov.UNID_CLIENTE = ultimoMov.UNID_CLIENTE;
                        modifiedMov.UNID_MOVIMIENTO_DETALLE = ultimoMov.UNID_MOVIMIENTO_DETALLE;
                        modifiedMov.UNID_PROVEEDOR = ultimoMov.UNID_PROVEEDOR;
                        modifiedMov.UNID_INFRAESTRUCTURA = ultimoMov.UNID_INFRAESTRUCTURA;
                        //Sync
                        modifiedMov.IS_MODIFIED = true;
                        modifiedMov.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                    }

                    entity.SaveChanges();
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ULTIMO_MOVIMIENTO ultimoMov = (ULTIMO_MOVIMIENTO)element;

                    var query = (from p in entity.ULTIMO_MOVIMIENTO
                                 where p.UNID_ITEM == ultimoMov.UNID_ITEM
                                 select p).ToList();

                    if (query.Count == 0)
                    {
                        //Sync
                        ultimoMov.IS_MODIFIED = true;
                        ultimoMov.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.ULTIMO_MOVIMIENTO.AddObject(ultimoMov);
                        entity.SaveChanges();
                    }
                    else
                    {

                        var modifiedMov = entity.ULTIMO_MOVIMIENTO.First(p => p.UNID_ITEM == ultimoMov.UNID_ITEM);

                        modifiedMov.UNID_ALMACEN = ultimoMov.UNID_ALMACEN;
                        modifiedMov.UNID_CLIENTE = ultimoMov.UNID_CLIENTE;
                        modifiedMov.UNID_MOVIMIENTO_DETALLE = ultimoMov.UNID_MOVIMIENTO_DETALLE;
                        modifiedMov.UNID_PROVEEDOR = ultimoMov.UNID_PROVEEDOR;
                        modifiedMov.UNID_INFRAESTRUCTURA = ultimoMov.UNID_INFRAESTRUCTURA;
                        modifiedMov.IS_ACTIVE = ultimoMov.IS_ACTIVE;
                        //Sync
                        modifiedMov.IS_MODIFIED = true;
                        modifiedMov.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                    }

                    entity.SaveChanges();
                }
            }
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElementSny(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ULTIMO_MOVIMIENTO ulMov = (ULTIMO_MOVIMIENTO)element;
                        //Sync

                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.ULTIMO_MOVIMIENTO.AddObject(ulMov);
                        entity.SaveChanges();
                    }
                }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<ULTIMO_MOVIMIENTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de ULTIMO_MOVIMIENTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonUltimoMovimiento()
        {
            string res = null;
            List<ULTIMO_MOVIMIENTO> listUltimoMovimiento = new List<ULTIMO_MOVIMIENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ULTIMO_MOVIMIENTO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listUltimoMovimiento.Add(new ULTIMO_MOVIMIENTO
                     {
                         UNID_ITEM=row.UNID_ITEM,
                         UNID_ALMACEN=row.UNID_ALMACEN,
                         UNID_CLIENTE=row.UNID_CLIENTE,
                         UNID_PROVEEDOR=row.UNID_PROVEEDOR,
                         UNID_MOVIMIENTO_DETALLE=row.UNID_MOVIMIENTO_DETALLE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         IS_ACTIVE=row.IS_ACTIVE,
                         UNID_INFRAESTRUCTURA = row.UNID_INFRAESTRUCTURA,
                         CANTIDAD = row.CANTIDAD,
                         UNID_ULTIMO_MOVIMIENTO=row.UNID_ULTIMO_MOVIMIENTO
                     });
                 });
                if (listUltimoMovimiento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listUltimoMovimiento);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<ULTIMO_MOVIMIENTO>
        /// </summary>
        /// <returns>Regresa List<ULTIMO_MOVIMIENTO></returns>
        /// <returns>Si no regresa null</returns>
        public List<ULTIMO_MOVIMIENTO> GetDeserializeUltimoMovimiento(string listPocos)
        {
            List<ULTIMO_MOVIMIENTO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<ULTIMO_MOVIMIENTO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetUltimoMovimiento()
        {
            List<ULTIMO_MOVIMIENTO> reset = new List<ULTIMO_MOVIMIENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ULTIMO_MOVIMIENTO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new ULTIMO_MOVIMIENTO
                     {
                         UNID_ITEM = row.UNID_ITEM,
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_CLIENTE = row.UNID_CLIENTE,
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         UNID_MOVIMIENTO_DETALLE = row.UNID_MOVIMIENTO_DETALLE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         IS_ACTIVE=row.IS_ACTIVE,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         UNID_INFRAESTRUCTURA = row.UNID_INFRAESTRUCTURA,
                         CANTIDAD = row.CANTIDAD,
                         UNID_ULTIMO_MOVIMIENTO=row.UNID_ULTIMO_MOVIMIENTO
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.ULTIMO_MOVIMIENTO.First(p => p.UNID_ITEM == item.UNID_ITEM);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
