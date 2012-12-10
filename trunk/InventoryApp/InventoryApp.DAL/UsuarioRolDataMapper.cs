using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL.AUTH
{
    public class UsuarioRolDataMapper : IDataMapper
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
        /// Método que serializa una List<USUARIO_ROL> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de USUARIO_ROL</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonUsuarioRol()
        {
            string res = null;
            List<USUARIO_ROL> listUsuarioRol = new List<USUARIO_ROL>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.USUARIO_ROL
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listUsuarioRol.Add(new USUARIO_ROL
                     {
                         UNID_ROL=row.UNID_ROL,
                         UNID_USUARIO = row.UNID_USUARIO,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listUsuarioRol.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listUsuarioRol);
                }
                return res;
            }
        }
    }
}
