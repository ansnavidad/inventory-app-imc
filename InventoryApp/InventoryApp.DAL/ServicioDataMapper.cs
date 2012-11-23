using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class ServicioDataMapper : IDataMapper
    {
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
                    entity.SaveChanges();
                }
            }
        }
    }
}
