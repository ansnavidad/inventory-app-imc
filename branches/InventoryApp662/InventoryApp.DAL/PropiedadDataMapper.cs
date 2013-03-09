using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class PropiedadDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.PROPIEDADs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from prop in entity.PROPIEDADs
                         where prop.IS_ACTIVE == true
                         where prop.IS_MODIFIED == false
                         select prop.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonPropiedad(long? LastModifiedDate)
        {
            string res = null;
            List<PROPIEDAD> listPropiedad = new List<PROPIEDAD>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROPIEDADs
                 where p.LAST_MODIFIED_DATE > LastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listPropiedad.Add(new PROPIEDAD
                     {
                         UNID_PROPIEDAD = row.UNID_PROPIEDAD,
                         PROPIEDAD1 = row.PROPIEDAD1,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPropiedad.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPropiedad);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                PROPIEDAD poco = (PROPIEDAD)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.PROPIEDADs
                                 where poco.UNID_PROPIEDAD == cust.UNID_PROPIEDAD
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

                    var modifiedMenu = entity.PROPIEDADs.First(p => p.UNID_PROPIEDAD == poco.UNID_PROPIEDAD);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getElements()
        {
            FixupCollection<PROPIEDAD> tp = new FixupCollection<PROPIEDAD>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.PROPIEDADs
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
            using (var entitie = new TAE2Entities())
            {
                PROPIEDAD propiedad = (PROPIEDAD)element;
                var query = (from cust in entitie.PROPIEDADs
                             where cust.UNID_PROPIEDAD == propiedad.UNID_PROPIEDAD
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
                    PROPIEDAD propiedad = (PROPIEDAD)element;
                    var modifiedPropiedad = entity.PROPIEDADs.First(p => p.UNID_PROPIEDAD == propiedad.UNID_PROPIEDAD);
                    modifiedPropiedad.PROPIEDAD1 = propiedad.PROPIEDAD1;
                    //Sync
                    modifiedPropiedad.IS_MODIFIED = true;
                    modifiedPropiedad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    UNID.Master(propiedad, u, -1, "Modificación");
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROPIEDAD propiedad = (PROPIEDAD)element;
                    var modifiedPropiedad = entity.PROPIEDADs.First(p => p.UNID_PROPIEDAD == propiedad.UNID_PROPIEDAD);
                    modifiedPropiedad.PROPIEDAD1 = propiedad.PROPIEDAD1;
                    modifiedPropiedad.IS_ACTIVE = propiedad.IS_ACTIVE;
                    //Sync
                    modifiedPropiedad.IS_MODIFIED = true;
                    modifiedPropiedad.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    PROPIEDAD propiedad = (PROPIEDAD)element;

                    var validacion = (from cust in entity.PROPIEDADs
                                      where cust.PROPIEDAD1 == propiedad.PROPIEDAD1
                                      select cust).ToList();
                    
                    if (validacion.Count == 0)
                    {
                        propiedad.UNID_PROPIEDAD = UNID.getNewUNID();
                        //Sync
                        propiedad.IS_MODIFIED = true;
                        propiedad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.PROPIEDADs.AddObject(propiedad);
                        entity.SaveChanges();

                        UNID.Master(propiedad, u, -1, "Inserción");
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
                    PROPIEDAD propiedad = (PROPIEDAD)element;

            //Sync                        
            var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
            modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
            entity.SaveChanges();
            //
            entity.PROPIEDADs.AddObject(propiedad);
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
                    PROPIEDAD propiedad = (PROPIEDAD)element;
                    var modifiedPropiedad = entity.PROPIEDADs.First(p => p.UNID_PROPIEDAD == propiedad.UNID_PROPIEDAD);
                    modifiedPropiedad.IS_ACTIVE = false;
                    //Sync
                    modifiedPropiedad.IS_MODIFIED = true;
                    modifiedPropiedad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    UNID.Master(propiedad, u, -1, "Emininación");
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROPIEDAD propiedad = (PROPIEDAD)element;
                    var modifiedPropiedad = entity.PROPIEDADs.First(p => p.UNID_PROPIEDAD == propiedad.UNID_PROPIEDAD);
                    modifiedPropiedad.IS_ACTIVE = false;
                    //Sync
                    modifiedPropiedad.IS_MODIFIED = true;
                    modifiedPropiedad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<PROPIEDAD> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PROPIEDAD</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonPropiedad()
        {
            string res = null;
            List<PROPIEDAD> listPropiedad = new List<PROPIEDAD>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROPIEDADs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listPropiedad.Add(new PROPIEDAD
                     {
                         UNID_PROPIEDAD=row.UNID_PROPIEDAD,
                         PROPIEDAD1=row.PROPIEDAD1,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPropiedad.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPropiedad);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<PROPIEDAD> y datos del usuario
        /// </summary>
        /// <returns>Regresa List<PROPIEDAD></returns>
        /// <returns>Si no regresa null</returns>
        public List<PROPIEDAD> GetDeserializePropiedad(string listPocos)
        {
            List<PROPIEDAD> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<PROPIEDAD>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetPropiedad()
        {
            List<PROPIEDAD> reset = new List<PROPIEDAD>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROPIEDADs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new PROPIEDAD
                     {
                         UNID_PROPIEDAD = row.UNID_PROPIEDAD,
                         PROPIEDAD1 = row.PROPIEDAD1,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.PROPIEDADs.First(p => p.UNID_PROPIEDAD == item.UNID_PROPIEDAD);
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
