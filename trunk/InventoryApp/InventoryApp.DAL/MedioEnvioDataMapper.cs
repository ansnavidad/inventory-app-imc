using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class MedioEnvioDataMapper :  IDataMapper
    {
        public object getElements()
        {
            object res = null;
            FixupCollection<MEDIO_ENVIO> tp = new FixupCollection<MEDIO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
               var query= (from p in Entity.MEDIO_ENVIO
                           where p.IS_ACTIVE ==true
                           select p).ToList();

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
            if (element != null)
            {
                MEDIO_ENVIO Eprov = (MEDIO_ENVIO)element;
                FixupCollection<MEDIO_ENVIO> tp = new FixupCollection<MEDIO_ENVIO>();

                using (var Entity = new TAE2Entities())
                {
                    var query=(from p in Entity.MEDIO_ENVIO
                               where p.UNID_MEDIO_ENVIO == Eprov.UNID_MEDIO_ENVIO
                               select p).ToList();
                    if (query.Count > 0)
                    {
                        res = query;
                    }
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
                    MEDIO_ENVIO medioEnvio = (MEDIO_ENVIO)element;

                    var modifiedMedioEnvio = entity.MEDIO_ENVIO.First(p => p.UNID_MEDIO_ENVIO == medioEnvio.UNID_MEDIO_ENVIO);
                    modifiedMedioEnvio.MEDIO_ENVIO_NAME = medioEnvio.MEDIO_ENVIO_NAME;
                    //Sync
                    modifiedMedioEnvio.IS_MODIFIED = true;
                    modifiedMedioEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    MEDIO_ENVIO medioEnvio = (MEDIO_ENVIO)element;

                    var validacion = (from cust in entity.MEDIO_ENVIO
                                      where cust.MEDIO_ENVIO_NAME == medioEnvio.MEDIO_ENVIO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        medioEnvio.UNID_MEDIO_ENVIO = UNID.getNewUNID();
                        //Sync
                        medioEnvio.IS_MODIFIED = true;
                        medioEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MEDIO_ENVIO.AddObject(medioEnvio);
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
                    MEDIO_ENVIO medioEnvio = (MEDIO_ENVIO)element;

                    var deleteMarca = entity.MEDIO_ENVIO.First(p => p.UNID_MEDIO_ENVIO == medioEnvio.UNID_MEDIO_ENVIO);

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
