using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

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
    }
}
