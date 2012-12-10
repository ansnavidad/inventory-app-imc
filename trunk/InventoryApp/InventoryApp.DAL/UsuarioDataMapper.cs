using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL.AUTH
{
    public class UsuarioDataMapper : IDataMapper
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
        /// Método que serializa una List<USUARIO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de USUARIO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonUsuario()
        {
            string res = null;
            List<USUARIO> listUsuario = new List<USUARIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.USUARIOs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listUsuario.Add(new USUARIO
                     {
                         UNID_USUARIO= row.UNID_USUARIO,
                         USUARIO_MAIL= row.USUARIO_MAIL,
                         USUARIO_PWD=row.USUARIO_PWD,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listUsuario.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listUsuario);
                }
                return res;
            }
        }
    }
}
