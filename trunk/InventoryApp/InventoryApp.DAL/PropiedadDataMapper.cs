using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class PropiedadDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {
            if (element != null)
            {
                PROPIEDAD poco = (PROPIEDAD)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.PROPIEDADs
                                 where poco.UNID_PROPIEDAD == cust.UNID_PROPIEDAD
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

                    var modifiedMenu = entity.PROPIEDADs.First(p => p.UNID_PROPIEDAD == poco.UNID_PROPIEDAD);
                    modifiedMenu.IS_ACTIVE = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getElements()
        {
            FixupCollection<PROPIEDAD> tp = new FixupCollection<PROPIEDAD>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.PROPIEDADs
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
                PROPIEDAD propiedad = (PROPIEDAD)element;
                var query = (from cust in entitie.PROPIEDADs
                             where cust.UNID_PROPIEDAD == propiedad.UNID_PROPIEDAD
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
                    PROPIEDAD propiedad = (PROPIEDAD)element;
                    var modifiedPropiedad = entity.PROPIEDADs.First(p => p.UNID_PROPIEDAD == propiedad.UNID_PROPIEDAD);
                    modifiedPropiedad.PROPIEDAD1 = propiedad.PROPIEDAD1;
                    //Sync
                    modifiedPropiedad.IS_MODIFIED = true;
                    modifiedPropiedad.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    PROPIEDAD propiedad = (PROPIEDAD)element;

                    var validacion = (from cust in entity.PROPIEDADs
                                      where cust.PROPIEDAD1 == propiedad.PROPIEDAD1
                                      select cust).ToList();
                    
                    if (validacion.Count == 0)
                    {
                        propiedad.UNID_PROPIEDAD = UNID.getNewUNID();
                        //Sync
                        propiedad.IS_MODIFIED = true;
                        propiedad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.PROPIEDADs.AddObject(propiedad);
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
                    PROPIEDAD propiedad = (PROPIEDAD)element;
                    var modifiedPropiedad = entity.PROPIEDADs.First(p => p.UNID_PROPIEDAD == propiedad.UNID_PROPIEDAD);
                    modifiedPropiedad.IS_ACTIVE = false;
                    //Sync
                    modifiedPropiedad.IS_MODIFIED = true;
                    modifiedPropiedad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<PROPIEDAD> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PROPIEDAD</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonPropiedad()
        {
            string res = null;
            List<PROPIEDAD> listPropiedad = new List<PROPIEDAD>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROPIEDADs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listPropiedad.Add(new PROPIEDAD
                     {
                         UNID_PROPIEDAD=row.UNID_PROPIEDAD,
                         PROPIEDAD1=row.PROPIEDAD1,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPropiedad.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPropiedad);
                }
                return res;
            }
        }
    }
}
