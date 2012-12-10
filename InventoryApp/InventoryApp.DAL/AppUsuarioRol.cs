using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class AppUsuarioRol : IDataMapper
    {
        public void loadSync(object element)
        {

            if (element != null)
            {
                USUARIO_ROL poco = (USUARIO_ROL)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.USUARIO_ROL
                                 where poco.UNID_ROL == cust.UNID_ROL && poco.UNID_USUARIO == cust.UNID_USUARIO
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        //No hay campos que se puedan actualizar
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
                    USUARIO_ROL usuario = (USUARIO_ROL)element;

                    var validacion = (from cust in entity.USUARIO_ROL
                                      where usuario.UNID_ROL == cust.UNID_ROL && usuario.UNID_USUARIO == cust.UNID_USUARIO
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {                        
                        //Sync
                        usuario.IS_MODIFIED = true;
                        usuario.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.USUARIO_ROL.AddObject(usuario);
                        entity.SaveChanges();
                    }
                }
            }
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
                         UNID_ROL = row.UNID_ROL,
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
