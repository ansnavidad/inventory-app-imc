using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class ServicioDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from ser in entity.SERVICIOs
                         where ser.IS_ACTIVE == true
                         where ser.IS_MODIFIED == false
                         select ser.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonServicio(long? LMD)
        {
            string res = null;
            List<SERVICIO> listServicio = new List<SERVICIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.SERVICIOs
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     listServicio.Add(new SERVICIO
                     {
                         UNID_SERVICIO = row.UNID_SERVICIO,
                         SERVICIO_NAME = row.SERVICIO_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listServicio.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listServicio);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                SERVICIO poco = (SERVICIO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.SERVICIOs
                                 where poco.UNID_SERVICIO == cust.UNID_SERVICIO
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

                    var modifiedMenu = entity.SERVICIOs.First(p => p.UNID_SERVICIO == poco.UNID_SERVICIO);
                    modifiedMenu.IS_ACTIVE = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getElements()
        {
            FixupCollection<SERVICIO> tp = new FixupCollection<SERVICIO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.SERVICIOs
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
            using (var entitie = new TAE2Entities())
            {
                SERVICIO servicio = (SERVICIO)element;
                var query = (from cust in entitie.SERVICIOs
                             where cust.UNID_SERVICIO == servicio.UNID_SERVICIO
                             select cust).ToList();
                if (query.Count > 0)
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
                    SERVICIO servicio = (SERVICIO)element;
                    var modifiedServicio = entity.SERVICIOs.First(p => p.UNID_SERVICIO == servicio.UNID_SERVICIO);
                    modifiedServicio.SERVICIO_NAME = servicio.SERVICIO_NAME;
                    //Sync
                    modifiedServicio.IS_MODIFIED = true;
                    modifiedServicio.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    SERVICIO servicio = (SERVICIO)element;

                    var validacion = (from cust in entity.SERVICIOs
                                      where cust.SERVICIO_NAME == servicio.SERVICIO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        servicio.UNID_SERVICIO = UNID.getNewUNID();
                        //Sync
                        servicio.IS_MODIFIED = true;
                        servicio.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.SERVICIOs.AddObject(servicio);
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
                    SERVICIO servicio = (SERVICIO)element;
                    var modifiedServicio = entity.SERVICIOs.First(p => p.UNID_SERVICIO == servicio.UNID_SERVICIO);
                    modifiedServicio.IS_ACTIVE = false;
                    //Sync
                    modifiedServicio.IS_MODIFIED = true;
                    modifiedServicio.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<SERVICIO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de SERVICIO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonServicio()
        {
            string res = null;
            List<SERVICIO> listServicio = new List<SERVICIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.SERVICIOs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listServicio.Add(new SERVICIO
                     {
                         UNID_SERVICIO=row.UNID_SERVICIO,
                         SERVICIO_NAME=row.SERVICIO_NAME,
                         IS_ACTIVE=row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listServicio.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listServicio);
                }
                return res;
            }
        }
    }
}
