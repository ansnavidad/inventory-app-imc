using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class AppUsuarioRol : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from usuarioRol in entity.USUARIO_ROL
                         where usuarioRol.IS_ACTIVE == true
                         where usuarioRol.IS_MODIFIED == false
                         select usuarioRol.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }
        public string GetJsonUsuarioRol(long? Last_Modified_Date)
        {
            string res = null;
            List<USUARIO_ROL> listUsuarioRol = new List<USUARIO_ROL>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.USUARIO_ROL
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
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
                        var aux = query.First();

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }

                    var modifiedUsuarioRol = entity.USUARIO_ROL.First(p => p.UNID_ROL == poco.UNID_ROL && p.UNID_USUARIO == poco.UNID_USUARIO);
                    modifiedUsuarioRol.IS_MODIFIED = false;
                    entity.SaveChanges();
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

        /// <summary>
        /// Método que Deserializa JSon a List<USUARIO_ROL> 
        /// </summary>
        /// <returns>Regresa List<USUARIO_ROL></returns>
        /// <returns>Si no regresa null</returns>
        public List<USUARIO_ROL> GetDeserializeUsuarioRol(string listPocos)
        {
            List<USUARIO_ROL> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<USUARIO_ROL>>(listPocos);
            }

            return res;
        }
    }
}
