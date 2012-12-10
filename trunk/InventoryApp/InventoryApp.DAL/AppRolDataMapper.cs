using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class AppRolDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {

            if (element != null)
            {
                ROL poco = (ROL)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ROLs
                                 where poco.UNID_ROL == cust.UNID_ROL
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }
                }
            }
        }

        public object getElements()
        {
            FixupCollection<ROL> tp = new FixupCollection<ROL>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.ROLs
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
                ROL Rol = (ROL)element;
                var query = (from cust in entitie.ROLs
                             where cust.UNID_ROL == Rol.UNID_ROL
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
                    ROL rol = (ROL)element;
                    var modifiedRol = entity.ROLs.First(p => p.UNID_ROL == rol.UNID_ROL);
                    modifiedRol.ROL_NAME = rol.ROL_NAME;
                    //Sync
                    modifiedRol.IS_MODIFIED = true;
                    modifiedRol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
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
                    ROL rol = (ROL)element;

                    var validacion = (from cust in entity.ROLs
                                      where cust.UNID_ROL == rol.UNID_ROL
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        rol.UNID_ROL = UNID.getNewUNID();
                        //Sync
                        rol.IS_MODIFIED = true;
                        rol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.ROLs.AddObject(rol);
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
                    ROL rol = (ROL)element;
                    var modifiedRol = entity.ROLs.First(p => p.UNID_ROL == rol.UNID_ROL);
                    //Sync
                    modifiedRol.IS_MODIFIED = true;
                    modifiedRol.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
