using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace InventoryApp.DAL.CatalogInventario
{
    public class InventarioDataMapper
    {
        public void insert(INVENTARIO element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    element.UNID_INVENTARIO = UNID.getNewUNID();
                    //Sync
                    element.IS_ACTIVE = true;
                    element.IS_MODIFIED = true;
                    element.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.INVENTARIOs.AddObject(element);
                    entity.SaveChanges();
                    //Master
                    UNID.Master(element, u, -1, "Inserción");
                    
                }
            }
        }

        public void delete(long unid, USUARIO u) {

            if (unid != null)
            {
                using (var entity = new TAE2Entities())
                {
                    var del = (from p in entity.INVENTARIOs
                               where p.UNID_SEGMENTO == unid && p.IS_ACTIVE == true
                               select p).ToList();

                    while (del.Count > 0) {

                        var del1 = (from p in entity.INVENTARIOs
                                    where p.UNID_SEGMENTO == unid && p.IS_ACTIVE == true
                                    select p).First();
                        //Sync
                        del1.IS_ACTIVE = false;
                        del1.IS_MODIFIED = true;
                        del1.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //Master
                        UNID.Master(del1, u, -1, "Emininación");

                        del = (from p in entity.INVENTARIOs
                               where p.UNID_SEGMENTO == unid && p.IS_ACTIVE == true
                               select p).ToList();
                    }
                }
            }
        }

        public object getElements()
        {
            List<INVENTARIO> tp = new List<INVENTARIO>();

            using (var entity = new TAE2Entities())
            {              
                var query = entity.INVENTARIOs.Include("ALMACEN").OrderByDescending(p => p.UNID_SEGMENTO).Where(p => p.IS_ACTIVE == true).ToList();

                if (query.Count > 0)
                {
                    foreach (INVENTARIO r in query)
                    {
                        tp.Add(r);
                    }
                }
                return tp;
            }
        }

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
                var resul0 = (from prov in entity.INVENTARIOs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from banco in entity.INVENTARIOs
                         where banco.IS_ACTIVE == true
                         where banco.IS_MODIFIED == false
                         select banco.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonInventarios(long? Last_Modified_Date)
        {
            string res = null;
            List<INVENTARIO> list = new List<INVENTARIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.INVENTARIOs
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     list.Add(new INVENTARIO
                     {
                         UNID_INVENTARIO = row.UNID_INVENTARIO,
                         DESCRIPTOR = row.DESCRIPTOR,
                         UNID_SEGMENTO= row.UNID_SEGMENTO,
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         FECHA = row.FECHA,
                         FINISHED = row.FINISHED,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (list.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(list);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                INVENTARIO poco = (INVENTARIO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.INVENTARIOs
                                 where poco.UNID_INVENTARIO == cust.UNID_INVENTARIO
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
                        insertElementSync((object)poco);
                    }

                    var modified = entity.INVENTARIOs.First(p => p.UNID_INVENTARIO == poco.UNID_INVENTARIO);
                    modified.IS_MODIFIED = false;
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
                    INVENTARIO inven = (INVENTARIO)element;
                    var modifiedInve = entity.INVENTARIOs.First(p => p.UNID_INVENTARIO == inven.UNID_INVENTARIO);
                    modifiedInve.DESCRIPTOR = inven.DESCRIPTOR;
                    modifiedInve.FECHA = inven.FECHA;
                    modifiedInve.FINISHED = inven.FINISHED;
                    modifiedInve.IS_ACTIVE = inven.IS_ACTIVE;
                    modifiedInve.UNID_ALMACEN = inven.UNID_ALMACEN;
                    modifiedInve.UNID_SEGMENTO = inven.UNID_SEGMENTO;
                    
                    //Sync
                    modifiedInve.IS_MODIFIED = true;
                    modifiedInve.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //                   
                    entity.SaveChanges();
                }
            }
        }

        public void insertElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    INVENTARIO inve = (INVENTARIO)element;

                    //Sync

                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.INVENTARIOs.AddObject(inve);
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<INVENTARIO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de INVENTARIO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonInventarios()
        {
            string res = null;
            List<INVENTARIO> list = new List<INVENTARIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.INVENTARIOs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     list.Add(new INVENTARIO
                     {
                         UNID_INVENTARIO = row.UNID_INVENTARIO,
                         DESCRIPTOR = row.DESCRIPTOR,
                         UNID_SEGMENTO = row.UNID_SEGMENTO,
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         FECHA = row.FECHA,
                         FINISHED = row.FINISHED,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (list.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(list);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<INVENTARIO> 
        /// </summary>
        /// <returns>Regresa List<INVENTARIO></returns>
        /// <returns>Si regresa null</returns>
        public List<INVENTARIO> GetDeserializeInventaios(string listPocos)
        {
            List<INVENTARIO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<INVENTARIO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetInventarios()
        {
            List<INVENTARIO> reset = new List<INVENTARIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.INVENTARIOs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new INVENTARIO
                     {
                         UNID_INVENTARIO = row.UNID_INVENTARIO,
                         DESCRIPTOR = row.DESCRIPTOR,
                         UNID_SEGMENTO = row.UNID_SEGMENTO,
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         FECHA = row.FECHA,
                         FINISHED = row.FINISHED,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.INVENTARIOs.First(p => p.UNID_INVENTARIO == item.UNID_INVENTARIO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
