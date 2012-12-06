using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class MonedaDataMapper : IDataMapper
    {
        public object getElements()
        {
            FixupCollection<MONEDA> tp = new FixupCollection<MONEDA>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.MONEDAs
                           where cust.IS_ACTIVE==true
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
                MONEDA moneda = (MONEDA)element;
                var query = (from cust in entitie.MONEDAs
                            where cust.UNID_MONEDA == moneda.UNID_MONEDA
                            select cust).ToList();
                if (query.Count>0)
                {
                    res = query;
                }
                return res;
            }
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MONEDA moneda = (MONEDA)element;
                    var modifiedMoneda = entity.MONEDAs.First(p => p.UNID_MONEDA == moneda.UNID_MONEDA);
                    modifiedMoneda.MONEDA_NAME = moneda.MONEDA_NAME;
                    modifiedMoneda.MONEDA_ABR = moneda.MONEDA_ABR;
                    //Sync
                    modifiedMoneda.IS_MODIFIED = true;
                    modifiedMoneda.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    MONEDA moneda = (MONEDA)element;

                    var validacion = (from cust in entity.MONEDAs
                                      where cust.MONEDA_NAME == moneda.MONEDA_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        moneda.UNID_MONEDA = UNID.getNewUNID();
                        //Sync
                        moneda.IS_MODIFIED = true;
                        moneda.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MONEDAs.AddObject(moneda);
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
                    MONEDA moneda = (MONEDA)element;

                    var deleteMoneda = entity.MONEDAs.First(p => p.UNID_MONEDA == moneda.UNID_MONEDA);

                    deleteMoneda.IS_ACTIVE = false;
                    //Sync
                    deleteMoneda.IS_MODIFIED = true;
                    deleteMoneda.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
