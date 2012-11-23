using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class PropiedadDataMapper : IDataMapper
    {
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
                    entity.SaveChanges();
                }
            }
        }
    }
}
