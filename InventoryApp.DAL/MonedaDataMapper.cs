using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class MonedaDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.MONEDAs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from moneda in entity.MONEDAs
                         where moneda.IS_ACTIVE == true
                         where moneda.IS_MODIFIED == false
                         select moneda.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonMoneda(long? LastModifiedDate)
        {
            string res = null;
            List<MONEDA> listMoneda = new List<MONEDA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MONEDAs
                 where p.LAST_MODIFIED_DATE > LastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listMoneda.Add(new MONEDA
                     {
                         UNID_MONEDA = row.UNID_MONEDA,
                         MONEDA_NAME = row.MONEDA_NAME,
                         MONEDA_ABR = row.MONEDA_ABR,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMoneda.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMoneda);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                MONEDA poco = (MONEDA)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.MONEDAs
                                 where poco.UNID_MONEDA == cust.UNID_MONEDA
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

                    var modifiedMenu = entity.MONEDAs.First(p => p.UNID_MONEDA == poco.UNID_MONEDA);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getElements()
        {
            FixupCollection<MONEDA> tp = new FixupCollection<MONEDA>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.MONEDAs
                           where cust.IS_ACTIVE==true
                           select cust).ToList();

               if (query.Count > 0)
               {
                    res = query;
               }

                return res;
            } 
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                MONEDA moneda = (MONEDA)element;
                var query = (from cust in entitie.MONEDAs
                            where cust.UNID_MONEDA == moneda.UNID_MONEDA
                            select cust).First();
                res = query;
                return res;
            }
        }

        public void udpateElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MONEDA moneda = (MONEDA)element;
                    var modifiedMoneda = entity.MONEDAs.First(p => p.UNID_MONEDA == moneda.UNID_MONEDA);
                    modifiedMoneda.MONEDA_NAME = moneda.MONEDA_NAME;
                    modifiedMoneda.MONEDA_ABR = moneda.MONEDA_ABR;
                    //Sync
                    modifiedMoneda.IS_MODIFIED = true;
                    modifiedMoneda.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    UNID.Master(moneda, u, -1, "Modificación");
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MONEDA moneda = (MONEDA)element;
                    var modifiedMoneda = entity.MONEDAs.First(p => p.UNID_MONEDA == moneda.UNID_MONEDA);
                    modifiedMoneda.MONEDA_NAME = moneda.MONEDA_NAME;
                    modifiedMoneda.MONEDA_ABR = moneda.MONEDA_ABR;
                    modifiedMoneda.IS_ACTIVE = moneda.IS_ACTIVE;
                    //Sync
                    modifiedMoneda.IS_MODIFIED = true;
                    modifiedMoneda.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    MONEDA moneda = (MONEDA)element;

                    var validacion = (from cust in entity.MONEDAs
                                      where cust.MONEDA_NAME == moneda.MONEDA_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        moneda.UNID_MONEDA = UNID.getNewUNID();
                        //Sync
                        moneda.IS_MODIFIED = true;
                        moneda.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MONEDAs.AddObject(moneda);
                        entity.SaveChanges();

                        UNID.Master(moneda, u, -1, "Inserción");
                    }
                }
            }
        }

        public void insertElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MONEDA moneda = (MONEDA)element;

                    //Sync
                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MONEDAs.AddObject(moneda);
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
                    MONEDA moneda = (MONEDA)element;

                    var deleteMoneda = entity.MONEDAs.First(p => p.UNID_MONEDA == moneda.UNID_MONEDA);

                    deleteMoneda.IS_ACTIVE = false;
                    //Sync
                    deleteMoneda.IS_MODIFIED = true;
                    deleteMoneda.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    UNID.Master(moneda, u, -1, "Emininación");
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MONEDA moneda = (MONEDA)element;

                    var deleteMoneda = entity.MONEDAs.First(p => p.UNID_MONEDA == moneda.UNID_MONEDA);

                    deleteMoneda.IS_ACTIVE = false;
                    //Sync
                    deleteMoneda.IS_MODIFIED = true;
                    deleteMoneda.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<MONEDA> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de MONEDA</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonMoneda()
        {
            string res = null;
            List<MONEDA> listMoneda = new List<MONEDA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MONEDAs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listMoneda.Add(new MONEDA
                     {
                         UNID_MONEDA=row.UNID_MONEDA,
                         MONEDA_NAME=row.MONEDA_NAME,
                         MONEDA_ABR=row.MONEDA_ABR,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMoneda.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMoneda);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<MONEDA>
        /// </summary>
        /// <returns>Regresa List<MONEDA></returns>
        /// <returns>Si no regresa null</returns>
        public List<MONEDA> GetDeserializeMoneda(string listPocos)
        {
            List<MONEDA> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<MONEDA>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetMoneda()
        {
            List<MONEDA> reset = new List<MONEDA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MONEDAs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new MONEDA
                     {
                         UNID_MONEDA = row.UNID_MONEDA,
                         MONEDA_NAME = row.MONEDA_NAME,
                         MONEDA_ABR = row.MONEDA_ABR,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.MONEDAs.First(p => p.UNID_MONEDA == item.UNID_MONEDA);
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
