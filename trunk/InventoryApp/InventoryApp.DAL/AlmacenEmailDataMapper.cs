using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class AlmacenEmailDataMapper : IDataMapper
    {
        public object getElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.ALMACEN_EMAIL
                           where p.IS_ACTIVE == true
                           select p).ToList();
                foreach (ALMACEN_EMAIL alm in ((List<ALMACEN_EMAIL>)res))
                {
                    alm.ALMACEN = alm.ALMACEN;
                }
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                ALMACEN_EMAIL almacenEmail = (ALMACEN_EMAIL)element;
                var query = (from cust in entitie.ALMACEN_EMAIL
                             where cust.UNID_ALMACEN_EMAIL == almacenEmail.UNID_ALMACEN_EMAIL
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
                    ALMACEN_EMAIL almacenEmail = (ALMACEN_EMAIL)element;
                    var modifiedAlmacenEmail = entity.ALMACEN_EMAIL.First(p => p.UNID_ALMACEN_EMAIL == almacenEmail.UNID_ALMACEN_EMAIL);
                    modifiedAlmacenEmail.EMAIL = almacenEmail.EMAIL;
                    modifiedAlmacenEmail.UNID_ALMACEN = almacenEmail.UNID_ALMACEN;
                    modifiedAlmacenEmail.IS_DEFAULT = almacenEmail.IS_DEFAULT;
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
                    ALMACEN_EMAIL almacenEmail = (ALMACEN_EMAIL)element;
                    almacenEmail.UNID_ALMACEN_EMAIL = UNID.getNewUNID();
                    entity.ALMACEN_EMAIL.AddObject(almacenEmail);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ALMACEN_EMAIL almacenEmail = (ALMACEN_EMAIL)element;
                    var modifiedAlmacenEmail = entity.ALMACEN_EMAIL.First(p => p.UNID_ALMACEN_EMAIL == almacenEmail.UNID_ALMACEN_EMAIL);
                    modifiedAlmacenEmail.IS_ACTIVE = false;
                    entity.SaveChanges();
                }
            }
        }
    }
}
