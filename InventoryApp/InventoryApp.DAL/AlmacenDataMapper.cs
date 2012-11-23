using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class AlmacenDataMapper : IDataMapper
    {
        public object getElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.ALMACENs
                           where p.IS_ACTIVE == true
                           select p).ToList();
                foreach (ALMACEN alm in ((List<ALMACEN>)res))
                {
                    alm.CIUDAD = alm.CIUDAD;
                }
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                ALMACEN almacen = (ALMACEN)element;
                var query = (from cust in entitie.ALMACENs
                             where cust.UNID_ALMACEN == almacen.UNID_ALMACEN
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
                    ALMACEN almacen = (ALMACEN)element;
                    var modifiedAlmacen = entity.ALMACENs.First(p => p.UNID_ALMACEN == almacen.UNID_ALMACEN);
                    modifiedAlmacen.ALMACEN_NAME = almacen.ALMACEN_NAME;
                    modifiedAlmacen.CONTACTO = almacen.CONTACTO;
                    modifiedAlmacen.TECNICO = almacen.TECNICO;
                    modifiedAlmacen.DIRECCION = almacen.DIRECCION;
                    modifiedAlmacen.UNID_CIUDAD = almacen.UNID_CIUDAD;
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
                    ALMACEN almacen = (ALMACEN)element;

                    var validacion = (from cust in entity.ALMACENs
                                      where cust.ALMACEN_NAME == almacen.ALMACEN_NAME
                                      select cust).ToList();
                    
                    if (validacion.Count == 0)
                    {
                        almacen.UNID_ALMACEN = UNID.getNewUNID();
                        entity.ALMACENs.AddObject(almacen);
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
                    ALMACEN almacen = (ALMACEN)element;
                    var modifiedAlamacen = entity.ALMACENs.First(p => p.UNID_ALMACEN == almacen.UNID_ALMACEN);
                    modifiedAlamacen.IS_ACTIVE = false;
                    entity.SaveChanges();
                }
            }
        }
    }
}
