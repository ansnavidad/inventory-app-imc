﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using Newtonsoft.Json;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class MaxMinDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.MAX_MIN
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from medio in entity.MAX_MIN
                         where medio.IS_ACTIVE == true
                         where medio.IS_MODIFIED == false
                         select medio.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonMaxMin(long? Last_Modified_Date)
        {
            string res = null;
            List<MAX_MIN> listMaxMin = new List<MAX_MIN>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MAX_MIN
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listMaxMin.Add(new MAX_MIN
                     {
                         UNID_ALMACEN= row.UNID_ALMACEN,
                         UNID_ARTICULO= row.UNID_ARTICULO,
                         UNID_MAX_MIN= row.UNID_MAX_MIN,
                         MAX=row.MAX,
                         MIN=row.MIN,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMaxMin.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMaxMin);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                MAX_MIN poco = (MAX_MIN)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.MAX_MIN
                                 where poco.UNID_MAX_MIN == cust.UNID_MAX_MIN
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

                    var modifiedCotizacion = entity.MAX_MIN.First(p => p.UNID_MAX_MIN == poco.UNID_MAX_MIN);
                    modifiedCotizacion.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public object getElementArticulos(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                MAX_MIN maxMin = (MAX_MIN)element;
                res = (from cust in entitie.MAX_MIN
                       .Include("ARTICULO")
                       .Include("ARTICULO.CATEGORIA")
                       .Include("ARTICULO.EQUIPO")
                       .Include("ARTICULO.MODELO")
                       .Include("ARTICULO.MARCA")
                       .Include("ALMACEN")
                       where cust.UNID_ALMACEN == maxMin.UNID_ALMACEN && cust.IS_ACTIVE==true
                       select cust).ToList();
                return res;
            }
        }

        public object getElements()
        {
            FixupCollection<MAX_MIN> tp = new FixupCollection<MAX_MIN>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                res = (from cust in entity.MAX_MIN
                       .Include("ARTICULO")
                       .Include("ARTICULO.CATEGORIA")
                       .Include("ARTICULO.EQUIPO")
                       .Include("ARTICULO.MODELO")
                       .Include("ARTICULO.MARCA")
                       .Include("ALMACEN")
                       where cust.IS_ACTIVE == true
                       select cust).ToList();

                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                MAX_MIN maxMin = (MAX_MIN)element;
                var query = (from cust in entitie.MAX_MIN
                             where cust.UNID_MAX_MIN == maxMin.UNID_MAX_MIN
                             select cust).ToList();
                if (query.Count > 0)
                {
                    res = query;
                }
                return res;
            }
        }

        public void udpateElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MAX_MIN maxMin = (MAX_MIN)element;
                    var modifiedMaxMin = entity.MAX_MIN.First(p => p.UNID_MAX_MIN == maxMin.UNID_MAX_MIN);
                    modifiedMaxMin.MAX = maxMin.MAX;
                    modifiedMaxMin.MIN = maxMin.MIN;
                    //Sync
                    modifiedMaxMin.IS_MODIFIED = true;
                    modifiedMaxMin.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    UNID.Master(maxMin, u, -1, "Modificación");
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MAX_MIN maxMin = (MAX_MIN)element;

                    var modifiedMaxMin = entity.MAX_MIN.First(p => p.UNID_MAX_MIN == maxMin.UNID_MAX_MIN);
                    modifiedMaxMin.MAX = maxMin.MAX;
                    modifiedMaxMin.MIN = maxMin.MIN;
                    modifiedMaxMin.IS_ACTIVE = maxMin.IS_ACTIVE;
                    //Sync
                    modifiedMaxMin.IS_MODIFIED = true;
                    modifiedMaxMin.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public void insertElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MAX_MIN maxmin = (MAX_MIN)element;

                    
                        maxmin.UNID_MAX_MIN = UNID.getNewUNID();
                        //Sync
                        maxmin.IS_MODIFIED = true;
                        maxmin.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MAX_MIN.AddObject(maxmin);
                        entity.SaveChanges();
                        UNID.Master(maxmin, u, -1, "Inserción");
                }
            }
        }

        public void insertElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MAX_MIN maxMin = (MAX_MIN)element;

                    //Sync

                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MAX_MIN.AddObject(maxMin);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MAX_MIN maxMin = (MAX_MIN)element;
                    var modifiedMaxMin = entity.MAX_MIN.First(p => p.UNID_MAX_MIN == maxMin.UNID_MAX_MIN);
                    modifiedMaxMin.IS_ACTIVE = false;
                    //Sync
                    modifiedMaxMin.IS_MODIFIED = true;
                    modifiedMaxMin.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                    UNID.Master(maxMin, u, -1, "Emininación");
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MAX_MIN maxMin = (MAX_MIN)element;
                    var modifiedMaxMin = entity.MAX_MIN.First(p => p.UNID_MAX_MIN == maxMin.UNID_MAX_MIN);
                    modifiedMaxMin.IS_ACTIVE = false;
                    //Sync
                    modifiedMaxMin.IS_MODIFIED = true;
                    modifiedMaxMin.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<MAX_MIN> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de MAX_MIN</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonMaxMin()
        {
            string res = null;
            List<MAX_MIN> listMaxMin = new List<MAX_MIN>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MAX_MIN
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listMaxMin.Add(new MAX_MIN
                     {
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         UNID_MAX_MIN = row.UNID_MAX_MIN,
                         MAX = row.MAX,
                         MIN = row.MIN,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMaxMin.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMaxMin);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<MAX_MIN>
        /// </summary>
        /// <returns>Regresa List<MAX_MIN></returns>
        /// <returns>Si no regresa null</returns>
        public List<MAX_MIN> GetDeserializeMaxMin(string listPocos)
        {
            List<MAX_MIN> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<MAX_MIN>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetMaxMin()
        {
            List<MAX_MIN> reset = new List<MAX_MIN>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MAX_MIN
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new MAX_MIN
                     {
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         UNID_MAX_MIN = row.UNID_MAX_MIN,
                         MAX = row.MAX,
                         MIN = row.MIN,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.MAX_MIN.First(p => p.UNID_MAX_MIN == item.UNID_MAX_MIN);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }


        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
