using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

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

                    entity.SaveChanges();
                }
            }
        }
    }
}
