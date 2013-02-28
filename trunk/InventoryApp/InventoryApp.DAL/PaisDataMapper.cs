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
    public class PaisDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.PAIS
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from pais in entity.PAIS
                         where pais.IS_ACTIVE == true
                         where pais.IS_MODIFIED == false
                         select pais.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonPais(long? LastModifiedDate)
        {
            string res = null;
            List<PAI> listPais = new List<PAI>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PAIS
                 where p.LAST_MODIFIED_DATE > LastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listPais.Add(new PAI
                     {
                         UNID_PAIS = row.UNID_PAIS,
                         PAIS = row.PAIS,
                         ISO = row.ISO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPais.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPais);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                PAI poco = (PAI)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.PAIS
                                 where poco.UNID_PAIS == cust.UNID_PAIS
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

                    var modifiedMenu = entity.PAIS.First(p => p.UNID_PAIS == poco.UNID_PAIS);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getElements()
        {
            object res = null;
            FixupCollection<PAI> tp = new FixupCollection<PAI>();
            using (var Entity = new TAE2Entities())
            {
                var query= (from p in Entity.PAIS
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
                PAI Eprov = (PAI)element;
                FixupCollection<PAI> tp = new FixupCollection<PAI>();
                using (var Entity = new TAE2Entities())
                {
                   var query= (from p in Entity.PAIS
                     where p.UNID_PAIS == Eprov.UNID_PAIS
                     select p).ToList();

                   if (query.Count > 0)
                   {
                        res = query;
                   }
                }
            }
            return res;
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PAI pais = (PAI)element;
                    var modifiedPais = entity.PAIS.First(p => p.UNID_PAIS == pais.UNID_PAIS);
                    modifiedPais.PAIS = pais.PAIS;
                    modifiedPais.ISO = pais.ISO;
                    modifiedPais.IS_ACTIVE = pais.IS_ACTIVE;
                    //Sync
                    modifiedPais.IS_MODIFIED = true;
                    modifiedPais.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PAI pais = (PAI)element;
                    var modifiedPais = entity.PAIS.First(p => p.UNID_PAIS == pais.UNID_PAIS);
                    modifiedPais.PAIS = pais.PAIS;
                    modifiedPais.ISO = pais.ISO;
                    //Sync
                    modifiedPais.IS_MODIFIED = true;
                    modifiedPais.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    PAI pais = (PAI)element;

                    var validacion = (from cust in entity.PAIS
                                      where cust.PAIS == pais.PAIS
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        pais.UNID_PAIS = UNID.getNewUNID();
                        //Sync
                        pais.IS_MODIFIED = true;
                        pais.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.PAIS.AddObject(pais);
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
                    PAI pais = (PAI)element;

                    //Sync
                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.PAIS.AddObject(pais);
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
                    PAI pais = (PAI)element;

                    var deletePais = entity.PAIS.First(p => p.UNID_PAIS == pais.UNID_PAIS);

                    deletePais.IS_ACTIVE = false;
                    //Sync
                    deletePais.IS_MODIFIED = true;
                    deletePais.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    PAI pais = (PAI)element;

                    var deletePais = entity.PAIS.First(p => p.UNID_PAIS == pais.UNID_PAIS);

                    deletePais.IS_ACTIVE = false;
                    //Sync
                    deletePais.IS_MODIFIED = true;
                    deletePais.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<PAIS> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PAIS</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonPais()
        {
            string res = null;
            List<PAI> listPais = new List<PAI>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PAIS
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listPais.Add(new PAI
                     {
                         UNID_PAIS=row.UNID_PAIS,
                         PAIS=row.PAIS,
                         ISO=row.ISO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPais.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPais);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<PAIS>
        /// </summary>
        /// <returns>Regresa List<PAIS></returns>
        /// <returns>Si no regresa null</returns>
        public List<PAI> GetdeserializePais(string listPocos)
        {
            List<PAI> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<PAI>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetPais()
        {
            List<PAI> reset = new List<PAI>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PAIS
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new PAI
                     {
                         UNID_PAIS = row.UNID_PAIS,
                         PAIS = row.PAIS,
                         ISO = row.ISO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.PAIS.First(p => p.UNID_PAIS == item.UNID_PAIS);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
