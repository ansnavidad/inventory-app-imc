using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class InfraestructuraDataMapper : IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public string GetJsonInfraestructura(long? LMD)
        {
            string res = null;
            List<INFRAESTRUCTURA> listInfra = new List<INFRAESTRUCTURA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.INFRAESTRUCTURAs
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     


                     listInfra.Add(new INFRAESTRUCTURA {
                      UNID_INFRAESTRUCTURA = row.UNID_INFRAESTRUCTURA,
                       INFRAESTRUCTURA_NAME = row.INFRAESTRUCTURA_NAME,
                        IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                          LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                           
                     });
                 });
                if (listInfra.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listInfra);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                INFRAESTRUCTURA poco = (INFRAESTRUCTURA)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.INFRAESTRUCTURAs
                                 where poco.UNID_INFRAESTRUCTURA == cust.UNID_INFRAESTRUCTURA
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

                    var modifiedInfra = entity.INFRAESTRUCTURAs.First(p => p.UNID_INFRAESTRUCTURA == poco.UNID_INFRAESTRUCTURA);
                    modifiedInfra.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                var resul0 = (from prov in entity.INFRAESTRUCTURAs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from infraestructura in entity.INFRAESTRUCTURAs
                         where infraestructura.IS_ACTIVE == true
                         where infraestructura.IS_MODIFIED == false
                         select infraestructura.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }
        public object getElements()
        {

            FixupCollection<INFRAESTRUCTURA> tp = new FixupCollection<INFRAESTRUCTURA>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.INFRAESTRUCTURAs
                             where cust.IS_ACTIVE == true
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

            using (var entity = new TAE2Entities())
            {
                //INFRAESTRUCTURA TEmp = (INFRAESTRUCTURA)element;

                //res = (from tipo in entity.INFRAESTRUCTURAs
                //       where tipo.UNID_INFRAESTRUCTURA == TEmp.UNID_INFRAESTRUCTURA
                //       select tipo).ToList();

                //return res;
                INFRAESTRUCTURA TEmp = (INFRAESTRUCTURA)element;

                res = (from tipo in entity.INFRAESTRUCTURAs
                       where tipo.UNID_INFRAESTRUCTURA == TEmp.UNID_INFRAESTRUCTURA
                       select tipo).First();

                return res;
            }
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    INFRAESTRUCTURA infraestructura = (INFRAESTRUCTURA)element;
                    var modifiedItemStatus = entity.INFRAESTRUCTURAs.First(p => p.UNID_INFRAESTRUCTURA == infraestructura.UNID_INFRAESTRUCTURA);
                    modifiedItemStatus.INFRAESTRUCTURA_NAME = infraestructura.INFRAESTRUCTURA_NAME;
                    //Sync
                    modifiedItemStatus.IS_MODIFIED = true;                    
                    modifiedItemStatus.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
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
                    INFRAESTRUCTURA infraestructura = (INFRAESTRUCTURA)element;
                    var modifiedItemStatus = entity.INFRAESTRUCTURAs.First(p => p.UNID_INFRAESTRUCTURA == infraestructura.UNID_INFRAESTRUCTURA);
                    modifiedItemStatus.INFRAESTRUCTURA_NAME = infraestructura.INFRAESTRUCTURA_NAME;
                    modifiedItemStatus.IS_ACTIVE = infraestructura.IS_ACTIVE;
                    //Sync
                    modifiedItemStatus.IS_MODIFIED = true;
                    modifiedItemStatus.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    INFRAESTRUCTURA tra = (INFRAESTRUCTURA)element;

                    var validacion = (from cust in entity.INFRAESTRUCTURAs
                                      where cust.INFRAESTRUCTURA_NAME == tra.INFRAESTRUCTURA_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        tra.UNID_INFRAESTRUCTURA = UNID.getNewUNID();
                        //Sync
                        tra.IS_MODIFIED = true;
                        tra.IS_ACTIVE = true;
                        tra.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.INFRAESTRUCTURAs.AddObject(tra);
                        entity.SaveChanges();
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
                    INFRAESTRUCTURA tra = (INFRAESTRUCTURA)element;

                    //Sync
                    tra.IS_MODIFIED = true;
                    tra.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.INFRAESTRUCTURAs.AddObject(tra);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    INFRAESTRUCTURA infraestructura = (INFRAESTRUCTURA)element;

                    var deleteTipoEmpresa = entity.INFRAESTRUCTURAs.First(p => p.UNID_INFRAESTRUCTURA == infraestructura.UNID_INFRAESTRUCTURA);

                    deleteTipoEmpresa.IS_ACTIVE = false;
                    //Sync
                    deleteTipoEmpresa.IS_MODIFIED = true;
                    deleteTipoEmpresa.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<INFRAESTRUCTURA> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de INFRAESTRUCTURA</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonInfraestructura()
        {
            string res = null;
            List<INFRAESTRUCTURA> listInfraestructura = new List<INFRAESTRUCTURA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.INFRAESTRUCTURAs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listInfraestructura.Add(new INFRAESTRUCTURA
                     {
                         UNID_INFRAESTRUCTURA= row.UNID_INFRAESTRUCTURA,
                         INFRAESTRUCTURA_NAME= row.INFRAESTRUCTURA_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listInfraestructura.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listInfraestructura);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<INFRAESTRUCTURA>
        /// </summary>
        /// <returns>Regresa List<INFRAESTRUCTURA></returns>
        /// <returns>Si no regresa null</returns>
        public List<INFRAESTRUCTURA> GetDeserializeInfraestructura(string listPocos)
        {
            List<INFRAESTRUCTURA> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<INFRAESTRUCTURA>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetInfraestructura()
        {
            List<INFRAESTRUCTURA> reset = new List<INFRAESTRUCTURA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.INFRAESTRUCTURAs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new INFRAESTRUCTURA
                     {
                         UNID_INFRAESTRUCTURA = row.UNID_INFRAESTRUCTURA,
                         INFRAESTRUCTURA_NAME = row.INFRAESTRUCTURA_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.INFRAESTRUCTURAs.First(p => p.UNID_INFRAESTRUCTURA == item.UNID_INFRAESTRUCTURA);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
