using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class AppRolMenuDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {

            if (element != null)
            {
                ROL_MENU poco = (ROL_MENU)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ROL_MENU
                                 where poco.UNID_ROL == cust.UNID_ROL && poco.UNID_MENU == cust.UNID_MENU
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        //udpateElement((object)poco);

                        //No hay propiedades que se puedan cambiar
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
            throw new NotImplementedException();
        }

        public object getElement(object element)
        {
            throw new NotImplementedException();
        }

        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ROL_MENU rolMenu = (ROL_MENU)element;

                    var validacion = (from cust in entity.ROL_MENU
                                      where cust.UNID_ROL == rolMenu.UNID_ROL && cust.UNID_MENU == rolMenu.UNID_MENU
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {                        
                        //Sync
                        rolMenu.IS_MODIFIED = true;
                        rolMenu.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.ROL_MENU.AddObject(rolMenu);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
