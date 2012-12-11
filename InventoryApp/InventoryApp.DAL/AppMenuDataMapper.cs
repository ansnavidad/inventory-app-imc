using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class AppMenuDataMapper : IDataMapper
    {
        public void loadSync(object element) {

            if (element != null)
            {
                MENU poco = (MENU)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.MENUs
                                 where poco.UNID_MENU == cust.UNID_MENU
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

                    var modifiedMenu = entity.MENUs.First(p => p.UNID_MENU == poco.UNID_MENU);
                    modifiedMenu.IS_ACTIVE = false;
                    entity.SaveChanges();            
                }
            }
        }
        
        public object getElements()
        {
            FixupCollection<MENU> tp = new FixupCollection<MENU>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.MENUs                             
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
                MENU menu = (MENU)element;
                var query = (from cust in entitie.MENUs
                             where cust.UNID_MENU == menu.UNID_MENU
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
                    MENU menu = (MENU)element;
                    var modifiedMenu = entity.MENUs.First(p => p.UNID_MENU == menu.UNID_MENU);
                    modifiedMenu.IS_LEAF = menu.IS_LEAF;
                    modifiedMenu.MENU_NAME = menu.MENU_NAME;
                    //Sync
                    modifiedMenu.IS_MODIFIED = true;
                    modifiedMenu.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    MENU menu = (MENU)element;

                    var validacion = (from cust in entity.MENUs
                                      where cust.MENU_NAME == menu.MENU_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        menu.UNID_MENU = UNID.getNewUNID();
                        //Sync
                        menu.IS_MODIFIED = true;
                        menu.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MENUs.AddObject(menu);
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
                    MENU menu = (MENU)element;
                    var modifiedMenu = entity.MENUs.First(p => p.UNID_MENU == menu.UNID_MENU);                    
                    //Sync
                    modifiedMenu.IS_MODIFIED = true;
                    modifiedMenu.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<MENU> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de MENU</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonMenu()
        {
            string res = null;
            List<MENU> listMenu = new List<MENU>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MENUs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listMenu.Add(new MENU
                     {
                         UNID_MENU = row.UNID_MENU,
                         UNID_MENU_PARENT = row.UNID_MENU_PARENT,
                         MENU_NAME = row.MENU_NAME,
                         IS_LEAF = row.IS_LEAF,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMenu.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMenu);
                }
                return res;
            }
        }
    }
}
