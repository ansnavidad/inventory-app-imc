using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
     public class ModeloDataMapper : IDataMapper
    {
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

                         if (UNID.compareUNIDS(aux.LAST_MODIFIED_DATE, poco.LAST_MODIFIED_DATE))
                             udpateElement((object)poco);
                     }
                     //Inserción
                     else
                     {
                         insertElement((object)poco);
                     }

                     var modifiedMenu = entity.MODELOes.First(p => p.UNID_MODELO == poco.UNID_MODELO);
                     modifiedMenu.IS_ACTIVE = false;
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
                        modelo.UNID_MODELO = UNID.getNewUNID();
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
    }
}
