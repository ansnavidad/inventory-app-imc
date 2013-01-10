using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class AppUsuario : IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }
        
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from appUsuario in entity.USUARIOs
                         where appUsuario.IS_ACTIVE == true
                         where appUsuario.IS_MODIFIED == false
                         select appUsuario.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonUsuario(long? Last_Modified_Date)
        {
            string res = null;
            List<USUARIO> listUsuario = new List<USUARIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.USUARIOs
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listUsuario.Add(new USUARIO
                     {
                         UNID_USUARIO = row.UNID_USUARIO,
                         USUARIO_MAIL = row.USUARIO_MAIL,
                         USUARIO_PWD = row.USUARIO_PWD,
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

        public void loadSync(object element)
        {

            if (element != null)
            {
                USUARIO poco = (USUARIO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.USUARIOs
                                 where poco.UNID_USUARIO == cust.UNID_USUARIO
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        var aux = query.First();

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElementSync((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElementSync((object)poco);
                    }

                    var modifiedUsuario = entity.USUARIOs.First(p => p.UNID_USUARIO == poco.UNID_USUARIO);
                    modifiedUsuario.IS_MODIFIED = false;
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
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    USUARIO usuario = (USUARIO)element;
                    var modifiedUsuario = entity.USUARIOs.First(p => p.UNID_USUARIO == usuario.UNID_USUARIO);
                    modifiedUsuario.USUARIO_MAIL = usuario.USUARIO_MAIL;
                    modifiedUsuario.USUARIO_PWD = usuario.USUARIO_PWD;
                    //Sync
                    modifiedUsuario.IS_MODIFIED = true;
                    modifiedUsuario.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    USUARIO usuario = (USUARIO)element;
                    var modifiedUsuario = entity.USUARIOs.First(p => p.UNID_USUARIO == usuario.UNID_USUARIO);
                    modifiedUsuario.USUARIO_MAIL = usuario.USUARIO_MAIL;
                    modifiedUsuario.USUARIO_PWD = usuario.USUARIO_PWD;
                    modifiedUsuario.IS_ACTIVE = usuario.IS_ACTIVE;
                    //Sync
                    modifiedUsuario.IS_MODIFIED = true;
                    modifiedUsuario.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    USUARIO usuario = (USUARIO)element;

                    var validacion = (from cust in entity.USUARIOs
                                      where cust.UNID_USUARIO == usuario.UNID_USUARIO
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        usuario.UNID_USUARIO = UNID.getNewUNID();
                        //Sync
                        usuario.IS_MODIFIED = true;
                        usuario.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.USUARIOs.AddObject(usuario);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void insertElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    USUARIO usuario = (USUARIO)element;
                                       
                    //Sync
                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.USUARIOs.AddObject(usuario);
                    entity.SaveChanges();                    
                }
            }
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
                         UNID_USUARIO = row.UNID_USUARIO,
                         USUARIO_MAIL = row.USUARIO_MAIL,
                         USUARIO_PWD = row.USUARIO_PWD,
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

        /// <summary>
        /// Método que Deserializa JSon a List<USUARIO>
        /// </summary>
        /// <returns>Regresa List<USUARIO></returns>
        /// <returns>Si no regresa null</returns>
        public List<USUARIO> GetDeserializeUsuario(string listPocos)
        {
            List<USUARIO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<USUARIO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetUsuario()
        {
            List<USUARIO> reset = new List<USUARIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.USUARIOs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new USUARIO
                     {
                         UNID_USUARIO = row.UNID_USUARIO,
                         USUARIO_MAIL = row.USUARIO_MAIL,
                         USUARIO_PWD = row.USUARIO_PWD,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.USUARIOs.First(p => p.UNID_USUARIO == item.UNID_USUARIO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
