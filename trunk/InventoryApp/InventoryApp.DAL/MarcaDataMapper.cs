using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class MarcaDataMapper : IDataMapper
    {

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
                    var modifiedItemStatus = entity.MARCAs.First(p => p.UNID_MARCA == marca.UNID_MARCA);
                    modifiedItemStatus.MARCA_NAME = marca.MARCA_NAME;
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
    }
}
