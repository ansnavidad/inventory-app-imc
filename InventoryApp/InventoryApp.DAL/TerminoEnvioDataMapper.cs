using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class TerminoEnvioDataMapper : IDataMapper
    {
        public object getElements()
        {
            object o = null;
            FixupCollection<TERMINO_ENVIO> tp = new FixupCollection<TERMINO_ENVIO>();
            using (var Entity = new TAE2Entities())
            {
               var query= (from p in Entity.TERMINO_ENVIO
                           where p.IS_ACTIVE==true
                           select p).ToList();

               if (query.Count > 0)
                {
                    o = query;
                }

                return o;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                TERMINO_ENVIO Eprov = (TERMINO_ENVIO)element;
                FixupCollection<TERMINO_ENVIO> tp = new FixupCollection<TERMINO_ENVIO>();

                using (var Entity = new TAE2Entities())
                {
                    var query = (from p in Entity.TERMINO_ENVIO
                                 where p.UNID_TERMINO_ENVIO == Eprov.UNID_TERMINO_ENVIO
                                 select p).ToList();

                    if (query.Count > 0)
                    {
                        o = query;
                    }
                }
            }
            return o;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TERMINO_ENVIO terminoEnvio = (TERMINO_ENVIO)element;

                    var modifiedTerminoEnvio = entity.TERMINO_ENVIO.First(p => p.UNID_TERMINO_ENVIO == terminoEnvio.UNID_TERMINO_ENVIO);
                    modifiedTerminoEnvio.CLAVE = terminoEnvio.CLAVE;
                    modifiedTerminoEnvio.GENERA_LOTES = terminoEnvio.GENERA_LOTES;
                    modifiedTerminoEnvio.SIGNIFICADO = terminoEnvio.SIGNIFICADO;
                    modifiedTerminoEnvio.TERMINO = terminoEnvio.TERMINO;
                    //Sync
                    modifiedTerminoEnvio.IS_MODIFIED = true;
                    modifiedTerminoEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    TERMINO_ENVIO terminoEnvio = (TERMINO_ENVIO)element;

                    var validacion = (from cust in entity.TERMINO_ENVIO
                                      where cust.TERMINO == terminoEnvio.TERMINO
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        terminoEnvio.UNID_TERMINO_ENVIO = UNID.getNewUNID();
                        //Sync
                        terminoEnvio.IS_MODIFIED = true;
                        terminoEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.TERMINO_ENVIO.AddObject(terminoEnvio);
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
                    TERMINO_ENVIO terminoEnvio = (TERMINO_ENVIO)element;

                    var deleteTerminoEnvio = entity.TERMINO_ENVIO.First(p => p.UNID_TERMINO_ENVIO == terminoEnvio.UNID_TERMINO_ENVIO);

                    deleteTerminoEnvio.IS_ACTIVE = false;
                    //Sync
                    deleteTerminoEnvio.IS_MODIFIED = true;
                    deleteTerminoEnvio.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
