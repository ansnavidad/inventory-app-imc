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
    public class MarcaDataMapper : IDataMapper
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
                resul = (from marca in entity.MARCAs
                         where marca.IS_ACTIVE == true
                         where marca.IS_MODIFIED == false
                         select marca.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonMarca(long? Last_Modified_Date)
        {
            string res = null;
            List<MARCA> listMarca = new List<MARCA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MARCAs
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listMarca.Add(new MARCA
                     {
                         UNID_MARCA = row.UNID_MARCA,
                         MARCA_NAME = row.MARCA_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMarca.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMarca);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {

            if (element != null)
            {
                MARCA poco = (MARCA)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.MARCAs
                                 where poco.UNID_MARCA == cust.UNID_MARCA
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
                        insertElementSync((object)poco);
                    }

                    var modifiedCotizacion = entity.MARCAs.First(p => p.UNID_MARCA == poco.UNID_MARCA);
                    modifiedCotizacion.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public object getElements()
        {

            FixupCollection<MARCA> modelos = new FixupCollection<MARCA>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.MARCAs
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
                MARCA Tmp = (MARCA)element;

                res = (from tipo in entity.MARCAs
                       where tipo.UNID_MARCA == Tmp.UNID_MARCA
                       select tipo).ToList();

                return res;
            }
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MARCA marca = (MARCA)element;
                    var modifiedMarca = entity.MARCAs.First(p => p.UNID_MARCA == marca.UNID_MARCA);
                    modifiedMarca.MARCA_NAME = marca.MARCA_NAME;
                    //Sync
                    modifiedMarca.IS_MODIFIED = true;
                    modifiedMarca.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    MARCA marca = (MARCA)element;

                    var validacion = (from cust in entity.MARCAs
                                      where cust.MARCA_NAME == marca.MARCA_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        marca.UNID_MARCA = UNID.getNewUNID();
                        //Sync
                        marca.IS_MODIFIED = true;
                        marca.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MARCAs.AddObject(marca);
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
                    MARCA marca = (MARCA)element;

                    var validacion = (from cust in entity.MARCAs
                                      where cust.MARCA_NAME == marca.MARCA_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        //Sync
                        
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MARCAs.AddObject(marca);
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
                    MARCA marca = (MARCA)element;

                    var deleteMarca = entity.MARCAs.First(p => p.UNID_MARCA == marca.UNID_MARCA);

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
        /// Método que serializa una List<MARCA> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de MARCA</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonMarca()
        {
            string res = null;
            List<MARCA> listMarca = new List<MARCA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MARCAs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listMarca.Add(new MARCA
                     {
                         UNID_MARCA=row.UNID_MARCA,
                         MARCA_NAME=row.MARCA_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMarca.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMarca);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<MARCA> 
        /// </summary>
        /// <returns>Regresa List<MARCA></returns>
        /// <returns>Si no regresa null</returns>
        public List<MARCA> GetDeserializeMarca(string listPocos)
        {
            List<MARCA> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<MARCA>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetMarca()
        {

            List<MARCA> reset = new List<MARCA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MARCAs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new MARCA
                     {
                         UNID_MARCA = row.UNID_MARCA,
                         MARCA_NAME = row.MARCA_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.MARCAs.First(p => p.UNID_MARCA == item.UNID_MARCA);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
