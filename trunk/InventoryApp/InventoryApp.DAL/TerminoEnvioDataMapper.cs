using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class TerminoEnvioDataMapper : IDataMapper
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
                resul = (from termino in entity.TERMINO_ENVIO
                         where termino.IS_ACTIVE == true
                         where termino.IS_MODIFIED == false
                         select termino.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonTerminoEnvio(long? LMD)
        {
            string res = null;
            List<TERMINO_ENVIO> listTerminoEnvio = new List<TERMINO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TERMINO_ENVIO
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     listTerminoEnvio.Add(new TERMINO_ENVIO
                     {
                         UNID_TERMINO_ENVIO = row.UNID_TERMINO_ENVIO,
                         CLAVE = row.CLAVE,
                         TERMINO = row.TERMINO,
                         SIGNIFICADO = row.SIGNIFICADO,
                         GENERA_LOTES = row.GENERA_LOTES,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTerminoEnvio.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTerminoEnvio);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                TERMINO_ENVIO poco = (TERMINO_ENVIO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.TERMINO_ENVIO
                                 where poco.UNID_TERMINO_ENVIO == cust.UNID_TERMINO_ENVIO
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

                    var modifiedMenu = entity.TERMINO_ENVIO.First(p => p.UNID_TERMINO_ENVIO == poco.UNID_TERMINO_ENVIO);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public object getElements()
        {
            object o = null;
            FixupCollection<TERMINO_ENVIO> tp = new FixupCollection<TERMINO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
               var query= (from p in Entity.TERMINO_ENVIO
                           where p.IS_ACTIVE==true
                           select p).ToList();

               if (query.Count > 0)
                {
                    o = query;
                }

                return o;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                TERMINO_ENVIO Eprov = (TERMINO_ENVIO)element;
                FixupCollection<TERMINO_ENVIO> tp = new FixupCollection<TERMINO_ENVIO>();
                using (var Entity = new TAE2Entities())
                {
                    var query = (from p in Entity.TERMINO_ENVIO
                                 where p.UNID_TERMINO_ENVIO == Eprov.UNID_TERMINO_ENVIO
                                 select p).ToList();

                    if (query.Count > 0)
                    {
                        o = query;
                    }
                }
            }
            return o;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TERMINO_ENVIO terminoEnvio = (TERMINO_ENVIO)element;

                    var modifiedTerminoEnvio = entity.TERMINO_ENVIO.First(p => p.UNID_TERMINO_ENVIO == terminoEnvio.UNID_TERMINO_ENVIO);
                    modifiedTerminoEnvio.CLAVE = terminoEnvio.CLAVE;
                    modifiedTerminoEnvio.GENERA_LOTES = terminoEnvio.GENERA_LOTES;
                    modifiedTerminoEnvio.SIGNIFICADO = terminoEnvio.SIGNIFICADO;
                    modifiedTerminoEnvio.TERMINO = terminoEnvio.TERMINO;
                    //Sync
                    modifiedTerminoEnvio.IS_MODIFIED = true;
                    modifiedTerminoEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    TERMINO_ENVIO terminoEnvio = (TERMINO_ENVIO)element;

                    var modifiedTerminoEnvio = entity.TERMINO_ENVIO.First(p => p.UNID_TERMINO_ENVIO == terminoEnvio.UNID_TERMINO_ENVIO);
                    modifiedTerminoEnvio.CLAVE = terminoEnvio.CLAVE;
                    modifiedTerminoEnvio.GENERA_LOTES = terminoEnvio.GENERA_LOTES;
                    modifiedTerminoEnvio.SIGNIFICADO = terminoEnvio.SIGNIFICADO;
                    modifiedTerminoEnvio.TERMINO = terminoEnvio.TERMINO;
                    modifiedTerminoEnvio.IS_ACTIVE = terminoEnvio.IS_ACTIVE;
                    //Sync
                    modifiedTerminoEnvio.IS_MODIFIED = true;
                    modifiedTerminoEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    TERMINO_ENVIO terminoEnvio = (TERMINO_ENVIO)element;

                    var validacion = (from cust in entity.TERMINO_ENVIO
                                      where cust.TERMINO == terminoEnvio.TERMINO
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        terminoEnvio.UNID_TERMINO_ENVIO = UNID.getNewUNID();
                        //Sync
                        terminoEnvio.IS_MODIFIED = true;
                        terminoEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.TERMINO_ENVIO.AddObject(terminoEnvio);
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
                    TERMINO_ENVIO terminoEnvio = (TERMINO_ENVIO)element;

                    var validacion = (from cust in entity.TERMINO_ENVIO
                                      where cust.TERMINO == terminoEnvio.TERMINO
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        //Sync
                        
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.TERMINO_ENVIO.AddObject(terminoEnvio);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TERMINO_ENVIO terminoEnvio = (TERMINO_ENVIO)element;

                    var deleteTerminoEnvio = entity.TERMINO_ENVIO.First(p => p.UNID_TERMINO_ENVIO == terminoEnvio.UNID_TERMINO_ENVIO);

                    deleteTerminoEnvio.IS_ACTIVE = false;
                    //Sync
                    deleteTerminoEnvio.IS_MODIFIED = true;
                    deleteTerminoEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<TERMINO_ENVIO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de TERMINO_ENVIO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonTerminoEnvio()
        {
            string res = null;
            List<TERMINO_ENVIO> listTerminoEnvio = new List<TERMINO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TERMINO_ENVIO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listTerminoEnvio.Add(new TERMINO_ENVIO
                     {
                         UNID_TERMINO_ENVIO=row.UNID_TERMINO_ENVIO,
                         CLAVE=row.CLAVE,
                         TERMINO=row.TERMINO,
                         SIGNIFICADO=row.SIGNIFICADO,
                         GENERA_LOTES=row.GENERA_LOTES,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTerminoEnvio.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTerminoEnvio);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<TERMINO_ENVIO>
        /// </summary>
        /// <returns>Regresa List<TERMINO_ENVIO></returns>
        /// <returns>Si no regresa null</returns>
        public List<TERMINO_ENVIO> GetDeserializeTerminoEnvio(string listPocos)
        {
            List<TERMINO_ENVIO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<TERMINO_ENVIO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetTerminoEnvio()
        {
            List<TERMINO_ENVIO> reset = new List<TERMINO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TERMINO_ENVIO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new TERMINO_ENVIO
                     {
                         UNID_TERMINO_ENVIO = row.UNID_TERMINO_ENVIO,
                         CLAVE = row.CLAVE,
                         TERMINO = row.TERMINO,
                         SIGNIFICADO = row.SIGNIFICADO,
                         GENERA_LOTES = row.GENERA_LOTES,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.TERMINO_ENVIO.First(p => p.UNID_TERMINO_ENVIO == item.UNID_TERMINO_ENVIO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
