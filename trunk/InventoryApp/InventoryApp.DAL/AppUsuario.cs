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
                var resul0 = (from prov in entity.USUARIOs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         FLAG = row.FLAG,
                         ACTIVATION= row.ACTIVATION,
                         FLAG_PASS = row.FLAG_PASS,
                         NUEVO_USUARIO = row.NUEVO_USUARIO,
                         IS_ACTIVE = row.IS_ACTIVE
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
            object o = null;

            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.USUARIOs
                           where p.IS_ACTIVE == true
                           select p).ToList();

                o = res;
            }            
            return o;
        }

        public object getElementsCatalog()
        {
            object o = null;

            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.USUARIOs
                           where p.IS_ACTIVE == true && p.UNID_USUARIO != 1
                           select p).ToList();

                o = res;
            }
            return o;
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                USUARIO user = (USUARIO)element;

                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.USUARIOs
                               where p.USUARIO_MAIL == user.USUARIO_MAIL
                               select p).First();

                    if (res == null)
                        return null;
                    o = res;
                }
            }
            return o;
        }

        public object getElementsRolesFiltrado()
        {
            List<USUARIO> res2 = new List<USUARIO>();

            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.USUARIOs
                           .Include("USUARIO_ROL")
                           where p.IS_ACTIVE == true
                           select p).ToList();               

                foreach (USUARIO u in res) {

                    bool aux = true;
                    foreach (USUARIO_ROL ur in u.USUARIO_ROL) {

                        if (ur.UNID_ROL == 1)
                            aux = false;
                    }

                    if (aux)
                        res2.Add(u);
                }
            }

            return res2;
        }

        public object getElementName(string userName)
        {
            object o = null;
            if (userName != null)
            {
                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.USUARIOs
                               .Include("USUARIO_ROL")
                               .Include("USUARIO_ROL.ROL")
                               .Include("USUARIO_ROL.ROL.ROL_MENU")
                               .Include("USUARIO_ROL.ROL.ROL_MENU.MENU")
                               where p.USUARIO_MAIL == userName
                               select p).First();

                    if (res == null)
                        return null;
                    o = res;

                    
                }
            }
            return o;
        }

        public bool GetElementLogin(object element)
        {
            bool respuesta = false;
            if (element != null)
            {
                USUARIO user = (USUARIO)element;

                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.USUARIOs
                               where p.USUARIO_MAIL == user.USUARIO_MAIL && p.USUARIO_PWD ==user.USUARIO_PWD && p.ACTIVATION ==false
                               select p).First();

                    if (res != null)
                        return true;
                }
            }
            return respuesta;
        }

        public bool GetElementLoginLocal(object element)
        {
            bool respuesta = false;
            if (element != null)
            {
                USUARIO user = (USUARIO)element;

                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.USUARIOs
                               where p.USUARIO_MAIL == user.USUARIO_MAIL && p.USUARIO_PWD == user.USUARIO_PWD && !p.ACTIVATION
                               select p).ToList();

                    if (res.Count != 0)
                        return true; 
                }
            }
            return respuesta;
        }

        public long GetValidarCorreoLocal(object element)
        {
            long respuesta = 0;
            if (element != null)
            {
                USUARIO user = (USUARIO)element;

                using (var Entity = new TAE2Entities())
                {
                    try
                    {
                        var res = (from p in Entity.USUARIOs
                                   where p.USUARIO_MAIL == user.USUARIO_MAIL
                                   select p).First();
                        respuesta = res.UNID_USUARIO;
                    }
                    catch (Exception ex)
                    {
                        return respuesta; 
                        
                    }
                    
                }
            }
            return respuesta;
        }

        public bool GetRecoverPassword(long? unidUser)
        {
            bool res = false;
            using (var Entity = new TAE2Entities())
            {
                try
                {
                    var recoverPass = Entity.USUARIOs.First(p => p.UNID_USUARIO == unidUser);
                    recoverPass.FLAG = true;
                    Entity.SaveChanges();
                    res = true;
                }
                catch (Exception ex)
                {
                    res = false;
                }
            }
            return res;
        }

        public bool GetActivationResponse(long? unidUser)
        {
            bool res = false;
            using (var Entity = new TAE2Entities())
            {
                try
                {
                    var recoverPass = Entity.USUARIOs.First(p => p.UNID_USUARIO == unidUser);
                    recoverPass.ACTIVATION = false;
                    Entity.SaveChanges();
                    res = true;
                }
                catch (Exception ex)
                {
                    res = false;
                }
            }
            return res;
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

        public void insertElementRegistro(object element)
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
                        usuario.IS_MODIFIED = false;
                        usuario.LAST_MODIFIED_DATE = 20120101000000000;                        
                        entity.USUARIOs.AddObject(usuario);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public string insertNewRegistro(object element)
        {
            string respuesta = null;
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    USUARIO usuario = (USUARIO)element;
                   
                        usuario.UNID_USUARIO = UNID.getNewUNID();
                        //Sync
                        usuario.IS_MODIFIED = false;
                        usuario.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        usuario.IS_ACTIVE = true;
                        usuario.NUEVO_USUARIO = true;
                        usuario.ACTIVATION = true;
                        usuario.FLAG = false;
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.USUARIOs.AddObject(usuario);
                        entity.SaveChanges();
                        
                        try
                        {
                            var query = (from p in entity.USUARIOs
                                         where p.UNID_USUARIO == usuario.UNID_USUARIO
                                         select p).First();
                        if (query !=null)
                        {

                            respuesta = GetJsonUsuario((USUARIO)query);

                        }
                        }
                        catch (Exception ex)
                        {

                            respuesta= null;
                        }
                    
                }
            }
            return respuesta;
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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         FLAG = row.FLAG,
                         ACTIVATION = row.ACTIVATION,
                         FLAG_PASS = row.FLAG_PASS,
                         NUEVO_USUARIO = row.NUEVO_USUARIO,
                         IS_ACTIVE = row.IS_ACTIVE
                     });
                 });
                if (listUsuario.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listUsuario);
                }
                return res;
            }
        }

        public string GetJsonUsuarioLogin()
        {
            string res = null;
            List<USUARIO> listUsuario = new List<USUARIO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.USUARIOs
                 where p.IS_ACTIVE == true && p.ACTIVATION==false
                 select p).ToList().ForEach(row =>
                 {
                     listUsuario.Add(new USUARIO
                     {
                         UNID_USUARIO = row.UNID_USUARIO,
                         USUARIO_MAIL = row.USUARIO_MAIL,
                         USUARIO_PWD = row.USUARIO_PWD,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         FLAG = row.FLAG,
                         ACTIVATION = row.ACTIVATION,
                         FLAG_PASS = row.FLAG_PASS,
                         NUEVO_USUARIO = row.NUEVO_USUARIO,
                         IS_ACTIVE = row.IS_ACTIVE
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
        /// Método que serializa UN POCO USUARIO a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de USUARIO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonUsuario(USUARIO usuario)
        { 
            string res = null;

            if (usuario!=null)
                res = SerializerJson.SerializeParametros(usuario);
            return res;
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
        /// Método que regresa True o False<USUARIO>
        /// </summary>
        /// <returns>Regresa List<USUARIO></returns>
        /// <returns>Si no regresa null</returns>
        public bool GetDeserializeUsuarioBool(string listPocos)
        {
            
            if (listPocos == "True")
                return true;
            else
                return false;
            
        }

        /// <summary>
        /// Método que Deserializa JSon a POCO USUARIO
        /// </summary>
        /// <returns>Regresa POCO USUARIO</returns>
        /// <returns>Si no regresa null</returns>
        public USUARIO GetDeserializePocoUsuario(string listPocos)
        {
            USUARIO res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<USUARIO>(listPocos);
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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         FLAG = row.FLAG,
                         ACTIVATION= row.ACTIVATION,
                         FLAG_PASS = row.FLAG_PASS,
                         NUEVO_USUARIO = row.NUEVO_USUARIO,
                         IS_ACTIVE = row.IS_ACTIVE
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
