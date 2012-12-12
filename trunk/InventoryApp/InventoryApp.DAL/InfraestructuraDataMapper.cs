using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class InfraestructuraDataMapper : IDataMapper
    {
        public object getElements()
        {

            FixupCollection<INFRAESTRUCTURA> tp = new FixupCollection<INFRAESTRUCTURA>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.INFRAESTRUCTURAs
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
                INFRAESTRUCTURA TEmp = (INFRAESTRUCTURA)element;

                res = (from tipo in entity.INFRAESTRUCTURAs
                       where tipo.UNID_INFRAESTRUCTURA == TEmp.UNID_INFRAESTRUCTURA
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
                    INFRAESTRUCTURA infraestructura = (INFRAESTRUCTURA)element;
                    var modifiedItemStatus = entity.INFRAESTRUCTURAs.First(p => p.UNID_INFRAESTRUCTURA == infraestructura.UNID_INFRAESTRUCTURA);
                    modifiedItemStatus.INFRAESTRUCTURA_NAME = infraestructura.INFRAESTRUCTURA_NAME;
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
                    INFRAESTRUCTURA tra = (INFRAESTRUCTURA)element;

                    var validacion = (from cust in entity.INFRAESTRUCTURAs
                                      where cust.INFRAESTRUCTURA_NAME == tra.INFRAESTRUCTURA_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        tra.UNID_INFRAESTRUCTURA = UNID.getNewUNID();
                        //Sync
                        tra.IS_MODIFIED = true;
                        tra.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.INFRAESTRUCTURAs.AddObject(tra);
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
                    INFRAESTRUCTURA infraestructura = (INFRAESTRUCTURA)element;

                    var deleteTipoEmpresa = entity.INFRAESTRUCTURAs.First(p => p.UNID_INFRAESTRUCTURA == infraestructura.UNID_INFRAESTRUCTURA);

                    deleteTipoEmpresa.IS_ACTIVE = false;
                    //Sync
                    deleteTipoEmpresa.IS_MODIFIED = true;
                    deleteTipoEmpresa.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
