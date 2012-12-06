using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class ItemStatusDataMapper : IDataMapper
    {
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
    }
}
