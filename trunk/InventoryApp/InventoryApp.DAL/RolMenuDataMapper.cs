using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL.AUTH
{
    public class RolMenuDataMapper : IDataMapper
    {
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
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método que serializa una List<ROL_MENU> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de ROL_MENU</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonRolMenu()
        {
            string res = null;
            List<ROL_MENU> listRolMenu = new List<ROL_MENU>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ROL_MENU
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listRolMenu.Add(new ROL_MENU
                     {
                         UNID_ROL = row.UNID_ROL,
                         UNID_MENU=row.UNID_MENU,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listRolMenu.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listRolMenu);
                }
                return res;
            }
        }
    }
}
