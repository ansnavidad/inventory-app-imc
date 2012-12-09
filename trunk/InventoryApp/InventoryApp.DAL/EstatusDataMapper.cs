using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class EstatusDataMapper : IDataMapper
    {
        public object getElements()
        {
            FixupCollection<ITEM_STATUS> tp = new FixupCollection<ITEM_STATUS>();
            object res = null;
  
            using (var entity = new TAE2Entities())
            {
                var query=(from cust in entity.ITEM_STATUS
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

            if (element!=null)
            {
                using (var entity = new TAE2Entities())
                {
                    ITEM_STATUS ESta = (ITEM_STATUS)element;

                     (from cust in entity.ITEM_STATUS
                                 where cust.UNID_ITEM_STATUS == ESta.UNID_ITEM_STATUS
                                 select cust).ToList().ForEach(d => { tp.Add(d); });

                     if (tp.Count > 0)
                    {
                        res = tp;
                    }
                    return res;
                }    
            }
            return res;
            
        }

        public void udpateElement(object element)
        {
            if (element!=null)
            {
                using (var entity = new TAE2Entities())
                {
                    ITEM_STATUS ESta = (ITEM_STATUS)element;

                    var query = (from cust in entity.ITEM_STATUS
                                 where cust.UNID_ITEM_STATUS == ESta.UNID_ITEM_STATUS
                                 select cust).ToList();

                    if (query.Count > 0)
                    {
                        var status = query.First();

                        status.ITEM_STATUS_NAME = ESta.ITEM_STATUS_NAME;
                        //Sync
                        status.IS_MODIFIED = true;
                        status.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //

                        entity.SaveChanges();
                    }

                }
            }
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ITEM_STATUS ESta = (ITEM_STATUS)element;

                    ITEM_STATUS status = new ITEM_STATUS();

                    status.UNID_ITEM_STATUS = UNID.getNewUNID();
                    //Sync
                    status.IS_MODIFIED = true;
                    status.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //

                    entity.ITEM_STATUS.AddObject(status);

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
                    ITEM_STATUS itemStatus = (ITEM_STATUS)element;

                    var modifiedItemStatus = entity.ITEM_STATUS.First(p => p.UNID_ITEM_STATUS == itemStatus.UNID_ITEM_STATUS);

                    modifiedItemStatus.IS_ACTIVE = false;
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
                         UNID_ITEM_STATUS=row.UNID_ITEM_STATUS,
                         ITEM_STATUS_NAME=row.ITEM_STATUS_NAME,
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
