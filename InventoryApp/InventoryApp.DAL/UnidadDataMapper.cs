using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class UnidadDataMapper : IDataMapper
    {
        public object getElements()
        {
            FixupCollection<UNIDAD> tp = new FixupCollection<UNIDAD>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.UNIDADs
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
                UNIDAD unidad = (UNIDAD)element;
                var query = (from cust in entitie.UNIDADs
                             where cust.UNID_UNIDAD == unidad.UNID_UNIDAD
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
                    UNIDAD unidad = (UNIDAD)element;
                    var modifiedUnidad = entity.UNIDADs.First(p => p.UNID_UNIDAD == unidad.UNID_UNIDAD);
                    modifiedUnidad.UNIDAD1 = unidad.UNIDAD1;
                    //Sync
                    modifiedUnidad.IS_MODIFIED = true;
                    modifiedUnidad.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    UNIDAD unidad = (UNIDAD)element;

                    var validacion = (from cust in entity.UNIDADs
                                      where cust.UNIDAD1 == unidad.UNIDAD1
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        unidad.UNID_UNIDAD = UNID.getNewUNID();
                        //Sync
                        unidad.IS_MODIFIED = true;
                        unidad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.UNIDADs.AddObject(unidad);
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
                    UNIDAD unidad = (UNIDAD)element;
                    var modifiedUnidad = entity.UNIDADs.First(p => p.UNID_UNIDAD == unidad.UNID_UNIDAD);
                    modifiedUnidad.IS_ACTIVE = false;
                    //Sync
                    modifiedUnidad.IS_MODIFIED = true;
                    modifiedUnidad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public List<UNIDAD> getListElements()
        {
            List<UNIDAD> unidades = new List<UNIDAD>();

            try
            {
                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.UNIDADs
                     select p).ToList<UNIDAD>().ForEach(o => unidades.Add(o));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return unidades;
        }
    }
}
