using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class MedioEnvioDataMapper :  IDataMapper
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
                var resul0 = (from prov in entity.MEDIO_ENVIO
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from medio in entity.MEDIO_ENVIO
                         where medio.IS_ACTIVE == true
                         where medio.IS_MODIFIED == false
                         select medio.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonMedioEnvio(long? Last_Modified_Date)
        {
            string res = null;
            List<MEDIO_ENVIO> listMedioEnvio = new List<MEDIO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MEDIO_ENVIO
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listMedioEnvio.Add(new MEDIO_ENVIO
                     {
                         UNID_MEDIO_ENVIO = row.UNID_MEDIO_ENVIO,
                         MEDIO_ENVIO_NAME = row.MEDIO_ENVIO_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMedioEnvio.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMedioEnvio);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                MEDIO_ENVIO poco = (MEDIO_ENVIO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.MEDIO_ENVIO
                                 where poco.UNID_MEDIO_ENVIO == cust.UNID_MEDIO_ENVIO
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

                    var modifiedCotizacion = entity.MEDIO_ENVIO.First(p => p.UNID_MEDIO_ENVIO == poco.UNID_MEDIO_ENVIO);
                    modifiedCotizacion.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getElements()
        {
            object res = null;
            FixupCollection<MEDIO_ENVIO> tp = new FixupCollection<MEDIO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
               var query= (from p in Entity.MEDIO_ENVIO
                           where p.IS_ACTIVE ==true
                           select p).ToList();

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
            if (element != null)
            {
                MEDIO_ENVIO Eprov = (MEDIO_ENVIO)element;
                FixupCollection<MEDIO_ENVIO> tp = new FixupCollection<MEDIO_ENVIO>();

                using (var Entity = new TAE2Entities())
                {
                    var query=(from p in Entity.MEDIO_ENVIO
                               where p.UNID_MEDIO_ENVIO == Eprov.UNID_MEDIO_ENVIO
                               select p).ToList();
                    if (query.Count > 0)
                    {
                        res = query;
                    }
                }
            }
            return res;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MEDIO_ENVIO medioEnvio = (MEDIO_ENVIO)element;

                    var modifiedMedioEnvio = entity.MEDIO_ENVIO.First(p => p.UNID_MEDIO_ENVIO == medioEnvio.UNID_MEDIO_ENVIO);
                    modifiedMedioEnvio.MEDIO_ENVIO_NAME = medioEnvio.MEDIO_ENVIO_NAME;
                    //Sync
                    modifiedMedioEnvio.IS_MODIFIED = true;
                    modifiedMedioEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    MEDIO_ENVIO medioEnvio = (MEDIO_ENVIO)element;

                    var modifiedMedioEnvio = entity.MEDIO_ENVIO.First(p => p.UNID_MEDIO_ENVIO == medioEnvio.UNID_MEDIO_ENVIO);
                    modifiedMedioEnvio.MEDIO_ENVIO_NAME = medioEnvio.MEDIO_ENVIO_NAME;
                    modifiedMedioEnvio.IS_ACTIVE = medioEnvio.IS_ACTIVE;
                    //Sync
                    modifiedMedioEnvio.IS_MODIFIED = true;
                    modifiedMedioEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    MEDIO_ENVIO medioEnvio = (MEDIO_ENVIO)element;

                    var validacion = (from cust in entity.MEDIO_ENVIO
                                      where cust.MEDIO_ENVIO_NAME == medioEnvio.MEDIO_ENVIO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        medioEnvio.UNID_MEDIO_ENVIO = UNID.getNewUNID();
                        //Sync
                        medioEnvio.IS_MODIFIED = true;
                        medioEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MEDIO_ENVIO.AddObject(medioEnvio);
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
                    MEDIO_ENVIO medioEnvio = (MEDIO_ENVIO)element;

                    //Sync
                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MEDIO_ENVIO.AddObject(medioEnvio);
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
                    MEDIO_ENVIO medioEnvio = (MEDIO_ENVIO)element;

                    var deleteMarca = entity.MEDIO_ENVIO.First(p => p.UNID_MEDIO_ENVIO == medioEnvio.UNID_MEDIO_ENVIO);

                    deleteMarca.IS_ACTIVE = false;
                    //Sync
                    deleteMarca.IS_MODIFIED = true;
                    deleteMarca.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
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
                    MEDIO_ENVIO medioEnvio = (MEDIO_ENVIO)element;

                    var deleteMarca = entity.MEDIO_ENVIO.First(p => p.UNID_MEDIO_ENVIO == medioEnvio.UNID_MEDIO_ENVIO);

                    deleteMarca.IS_ACTIVE = false;
                    //Sync
                    deleteMarca.IS_MODIFIED = true;
                    deleteMarca.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<MEDIO_ENVIO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de MEDIO_ENVIO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonMedioEnvio()
        {
            string res = null;
            List<MEDIO_ENVIO> listMedioEnvio = new List<MEDIO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MEDIO_ENVIO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listMedioEnvio.Add(new MEDIO_ENVIO
                     {
                         UNID_MEDIO_ENVIO=row.UNID_MEDIO_ENVIO,
                         MEDIO_ENVIO_NAME=row.MEDIO_ENVIO_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMedioEnvio.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMedioEnvio);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<MEDIO_ENVIO>
        /// </summary>
        /// <returns>Regresa List<MEDIO_ENVIO></returns>
        /// <returns>Si no regresa null</returns>
        public List<MEDIO_ENVIO> GetDeserializeMedioEnvio(string listPocos)
        {
            List<MEDIO_ENVIO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<MEDIO_ENVIO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetMedioEnvio()
        {
            List<MEDIO_ENVIO> reset = new List<MEDIO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MEDIO_ENVIO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new MEDIO_ENVIO
                     {
                         UNID_MEDIO_ENVIO = row.UNID_MEDIO_ENVIO,
                         MEDIO_ENVIO_NAME = row.MEDIO_ENVIO_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.MEDIO_ENVIO.First(p => p.UNID_MEDIO_ENVIO == item.UNID_MEDIO_ENVIO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
