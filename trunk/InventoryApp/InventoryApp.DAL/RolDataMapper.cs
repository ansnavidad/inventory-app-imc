using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL.AUTH
{
    public class RolDataMapper : IDataMapper
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
        /// Método que serializa una List<ROL> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de ROL</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonRol()
        {
            string res = null;
            List<ROL> listRol = new List<ROL>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ROLs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listRol.Add(new ROL
                     {
                         UNID_ROL= row.UNID_ROL,
                         ROL_NAME=row.ROL_NAME,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listRol.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listRol);
                }
                return res;
            }
        }
    }
}
