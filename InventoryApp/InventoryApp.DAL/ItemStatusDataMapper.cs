using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class ItemStatusDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from itemStatus in entity.ITEM_STATUS
                         where itemStatus.IS_ACTIVE == true
                         where itemStatus.IS_MODIFIED == false
                         select itemStatus.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonItemStatus(long? Last_Modified_Date)
        {
            string res = null;
            List<ITEM_STATUS> listItemStatus = new List<ITEM_STATUS>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ITEM_STATUS
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listItemStatus.Add(new ITEM_STATUS
                     {
                         UNID_ITEM_STATUS = row.UNID_ITEM_STATUS,
                         ITEM_STATUS_NAME = row.ITEM_STATUS_NAME,
                         UNID_EMPRESA = row.UNID_EMPRESA,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listItemStatus.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listItemStatus);
                }
                return res;
            }
        }
        public void loadSync(object element)
        {
            if (element != null)
            {
                ITEM_STATUS poco = (ITEM_STATUS)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ITEM_STATUS
                                 where poco.UNID_ITEM_STATUS == cust.UNID_ITEM_STATUS
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

                    var modifiedCotizacion = entity.ITEM_STATUS.First(p => p.UNID_ITEM_STATUS == poco.UNID_ITEM_STATUS);
                    modifiedCotizacion.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getElements()
        {
            FixupCollection<ITEM_STATUS> tp = new FixupCollection<ITEM_STATUS>();

            object res = null;

            using (var entity = new TAE2Entities())
            {
              var query = (from cust in entity.ITEM_STATUS
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
            FixupCollection<ITEM_STATUS> tp = new FixupCollection<ITEM_STATUS>();

            object res = null;

            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ITEM_STATUS ESta = (ITEM_STATUS)element;

                   var query= (from cust in entity.ITEM_STATUS
                     where cust.UNID_ITEM_STATUS == ESta.UNID_ITEM_STATUS
                     select cust).ToList();

                   if (query.Count > 0)
                    {
                        res = query;
                    }
                    return res;
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
                    ITEM_STATUS itemStatus = (ITEM_STATUS)element;
                    var modifiedItemStatus=entity.ITEM_STATUS.First(p => p.UNID_ITEM_STATUS == itemStatus.UNID_ITEM_STATUS);
                    modifiedItemStatus.ITEM_STATUS_NAME = itemStatus.ITEM_STATUS_NAME;
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
                    ITEM_STATUS itemStatus = (ITEM_STATUS)element;

                    var validacion = (from cust in entity.ITEM_STATUS
                                      where cust.ITEM_STATUS_NAME == itemStatus.ITEM_STATUS_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        itemStatus.UNID_ITEM_STATUS = UNID.getNewUNID();
                        //Sync
                        itemStatus.IS_MODIFIED = true;
                        itemStatus.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.ITEM_STATUS.AddObject(itemStatus);
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
                    ITEM_STATUS itemStatus = (ITEM_STATUS)element;

                    var deleteItemStatus = entity.ITEM_STATUS.First(p => p.UNID_ITEM_STATUS == itemStatus.UNID_ITEM_STATUS);

                    deleteItemStatus.IS_ACTIVE =false;
                    //Sync
                    deleteItemStatus.IS_MODIFIED = true;
                    deleteItemStatus.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }        
            }
        }

        /// <summary>
        /// Método que serializa una List<ITEM_STATUS> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de ITEM_STATUS</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonItemStatus()
        {
            string res = null;
            List<ITEM_STATUS> listItemStatus = new List<ITEM_STATUS>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ITEM_STATUS
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listItemStatus.Add(new ITEM_STATUS
                     {
                         UNID_ITEM_STATUS = row.UNID_ITEM_STATUS,
                         ITEM_STATUS_NAME = row.ITEM_STATUS_NAME,
                         UNID_EMPRESA = row.UNID_EMPRESA,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listItemStatus.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listItemStatus);
                }
                return res;
            }
        }
    }
}
