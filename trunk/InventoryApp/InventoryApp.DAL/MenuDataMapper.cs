using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class MenuDataMapper:IDataMapper
    {
        public object getElements()
        {
            object res = null;
            using (var entity = new TAE2Entities())
            {
                MENU query=null;
                try
                {
                    query = (from child in entity.MENUs
                             where child.UNID_MENU_PARENT == null
                             select child).First<MENU>();
                }
                catch (Exception ex)
                {
                    ;
                }
                res = query;
            }
            return res;
        }

        public object getElement(object element)
        {
            object res = null;
            MENU menu=element as MENU;
            if (menu != null)
            {
                using(var entity=new TAE2Entities())
                {
                    var query = (from child in entity.MENUs
                                 where child.UNID_MENU_PARENT == menu.UNID_MENU
                                 select child).ToList<MENU>();
                    res = query;
                }
            }
            return res;
        }

        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
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
                         UNID_MENU=row.UNID_MENU,
                         UNID_MENU_PARENT=row.UNID_MENU_PARENT,
                         MENU_NAME=row.MENU_NAME,
                         IS_LEAF=row.IS_LEAF,
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
