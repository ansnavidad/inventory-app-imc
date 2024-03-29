﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class LoteDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.LOTEs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from lote in entity.LOTEs
                         where lote.IS_ACTIVE == true
                         where lote.IS_MODIFIED == false
                         select lote.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonLote(long? Last_Modified_Date)
        {
            string res = null;
            List<LOTE> listLote = new List<LOTE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.LOTEs
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listLote.Add(new LOTE
                     {
                         UNID_LOTE = row.UNID_LOTE,
                         UNID_POM = row.UNID_POM,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listLote.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listLote);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                LOTE poco = (LOTE)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.LOTEs
                                 where poco.UNID_LOTE == cust.UNID_LOTE
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
                        insertElementSny((object)poco);
                    }

                    var modifiedCotizacion = entity.LOTEs.First(p => p.UNID_LOTE == poco.UNID_LOTE);
                    modifiedCotizacion.IS_MODIFIED = false;
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
                    LOTE lote = (LOTE)element;
                    var modifiedLote = entity.LOTEs.First(p => p.UNID_LOTE == lote.UNID_LOTE);
                    modifiedLote.IS_ACTIVE = lote.IS_ACTIVE;                    
                    modifiedLote.UNID_POM = lote.UNID_POM;
                    //Sync
                    modifiedLote.IS_MODIFIED = true;
                    modifiedLote.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    LOTE lote = (LOTE)element;
                    //lote.UNID_LOTE = lote.UNID_LOTE;
                    //Sync
                    lote.IS_MODIFIED = true;
                    lote.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.LOTEs.AddObject(lote);
                    entity.SaveChanges();
                }
            }
        }

        public void insertElementSny(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    LOTE lote = (LOTE)element;
                    
                    //Sync
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.LOTEs.AddObject(lote);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element, USUARIO u)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<LOTE> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de LOTE</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonLote()
        {
            string res = null;
            List<LOTE> listLote = new List<LOTE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.LOTEs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listLote.Add(new LOTE
                     {
                         UNID_LOTE=row.UNID_LOTE,
                         UNID_POM=row.UNID_POM,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listLote.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listLote);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<LOTE>
        /// </summary>
        /// <returns>Regresa List<LOTE></returns>
        /// <returns>Si no regresa null</returns>
        public List<LOTE> GetDeserializeLote(string listPocos)
        {
            List<LOTE> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<LOTE>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetLote()
        {
            List<LOTE> reset = new List<LOTE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.LOTEs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new LOTE
                     {
                         UNID_LOTE = row.UNID_LOTE,
                         UNID_POM = row.UNID_POM,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.LOTEs.First(p => p.UNID_LOTE == item.UNID_LOTE);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }


        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
