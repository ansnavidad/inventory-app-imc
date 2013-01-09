using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
     public class ModeloDataMapper : IDataMapper
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
                 resul = (from model in entity.MODELOes
                          where model.IS_ACTIVE == true
                          where model.IS_MODIFIED == false
                          select model.LAST_MODIFIED_DATE).Max();
                 return resul;
             }

         }

         public string GetJsonModelo(long? LastModifiedDate)
         {
             string res = null;
             List<MODELO> listModelo = new List<MODELO>();
             using (var Entity = new TAE2Entities())
             {
                 (from p in Entity.MODELOes
                  where p.LAST_MODIFIED_DATE > LastModifiedDate
                  select p).ToList().ForEach(row =>
                  {
                      listModelo.Add(new MODELO
                      {
                          UNID_MODELO = row.UNID_MODELO,
                          MODELO_NAME = row.MODELO_NAME,
                          IS_ACTIVE = row.IS_ACTIVE,
                          IS_MODIFIED = row.IS_MODIFIED,
                          LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                      });
                  });
                 if (listModelo.Count > 0)
                 {
                     res = SerializerJson.SerializeParametros(listModelo);
                 }
                 return res;
             }
         }

         public void loadSync(object element)
         {

             if (element != null)
             {
                 MODELO poco = (MODELO)element;
                 using (var entity = new TAE2Entities())
                 {
                     var query = (from cust in entity.MODELOes
                                  where poco.UNID_MODELO == cust.UNID_MODELO
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

                     var modifiedMenu = entity.MODELOes.First(p => p.UNID_MODELO == poco.UNID_MODELO);
                     modifiedMenu.IS_MODIFIED = false;
                     entity.SaveChanges();
                 }
             }
         }

        public object getElements()
        {

            FixupCollection<MODELO> tp = new FixupCollection<MODELO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.MODELOes
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
                MODELO Tmp = (MODELO)element;

                res = (from tipo in entity.MODELOes
                       where tipo.UNID_MODELO == Tmp.UNID_MODELO
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
                    MODELO modelo = (MODELO)element;
                    var modifiedItemStatus = entity.MODELOes.First(p => p.UNID_MODELO == modelo.UNID_MODELO);
                    modifiedItemStatus.MODELO_NAME = modelo.MODELO_NAME;
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
                    MODELO modelo = (MODELO)element;
                    var modifiedItemStatus = entity.MODELOes.First(p => p.UNID_MODELO == modelo.UNID_MODELO);
                    modifiedItemStatus.MODELO_NAME = modelo.MODELO_NAME;
                    modifiedItemStatus.IS_ACTIVE = modelo.IS_ACTIVE;
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
                    MODELO modelo = (MODELO)element;

                    var validacion = (from cust in entity.MODELOes
                                      where cust.MODELO_NAME == modelo.MODELO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        
                        //Sync
                        modelo.IS_MODIFIED = true;
                        modelo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MODELOes.AddObject(modelo);
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
                    MODELO modelo = (MODELO)element;

                    var validacion = (from cust in entity.MODELOes
                                      where cust.MODELO_NAME == modelo.MODELO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        //Sync
                        
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MODELOes.AddObject(modelo);
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
                    MODELO modelo = (MODELO)element;

                    var deleteModelo = entity.MODELOes.First(p => p.UNID_MODELO == modelo.UNID_MODELO);

                    deleteModelo.IS_ACTIVE = false;
                    //Sync
                    deleteModelo.IS_MODIFIED = true;
                    deleteModelo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<MODELO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de MODELO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonModelo()
        {
            string res = null;
            List<MODELO> listModelo = new List<MODELO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MODELOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listModelo.Add(new MODELO
                     {
                         UNID_MODELO=row.UNID_MODELO,
                         MODELO_NAME=row.MODELO_NAME,
                         IS_ACTIVE=row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listModelo.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listModelo);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<MODELO>
        /// </summary>
        /// <returns>Regresa List<MODELO></returns>
        /// <returns>Si no regresa null</returns>
        public List<MODELO> GetDeserializeModelo(string listPocos)
        {
            List<MODELO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<MODELO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetModelo()
        {

            List<MODELO> reset = new List<MODELO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MODELOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new MODELO
                     {
                         UNID_MODELO = row.UNID_MODELO,
                         MODELO_NAME = row.MODELO_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.MODELOes.First(p => p.UNID_MODELO == item.UNID_MODELO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
