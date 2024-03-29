﻿using System;
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
                var resul0 = (from prov in entity.USUARIO_ROL
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         IS_ACTIVE = row.IS_ACTIVE
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

        public object getElementByUserRol(long? unidUser)
        {
            object res = null;
            if (unidUser != null)
            {
                using (var entity = new TAE2Entities())
                {
                    var validacion = (from r in entity.ROLs
                                      join ur in entity.USUARIO_ROL
                                      on r.UNID_ROL equals ur.UNID_ROL
                                      join u in entity.USUARIOs
                                      on  ur.UNID_USUARIO equals u.UNID_USUARIO
                                      where u.UNID_USUARIO == unidUser &&
                                            r.IS_ACTIVE ==true &&
                                            ur.IS_ACTIVE ==true &&
                                            u.IS_ACTIVE ==true
                                      select r).ToList();
                    res = validacion;
                }
            }
            return res;
        }

        public object getElementByRolMenu(long? unidRol)
        {
            object res = null;
            if (unidRol != null)
            {
                using (var entity = new TAE2Entities())
                {
                    var validacion = (from rm in entity.ROL_MENU
                                      join m in entity.MENUs
                                      on rm.UNID_MENU equals m.UNID_MENU
                                      where rm.UNID_ROL == unidRol &&
                                            m.IS_ACTIVE == true &&
                                            rm.IS_ACTIVE == true
                                      select rm).ToList();
                    res = validacion;
                }
            }
            return res;
        }

        public void udpateRolMenu(object element)
        {
            if (element != null)
            {
                ROL_MENU poco = (ROL_MENU)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ROL_MENU
                                 where poco.UNID_ROL == cust.UNID_ROL && 
                                 poco.UNID_MENU == cust.UNID_MENU
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        udpateRM((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertRM((object)poco);
                    }
                }
            }
        }

        public void udpateUsuarioRol(object element)
        {
            if (element != null)
            {
                USUARIO_ROL poco = (USUARIO_ROL)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.USUARIO_ROL
                                 where poco.UNID_ROL == cust.UNID_ROL &&
                                 poco.UNID_USUARIO == cust.UNID_USUARIO
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        udpateUR((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertUR((object)poco);
                    }
                }
            }
        }

        public void udpateRM(object element)
        {
                if (element != null)
                {
                    using (var entity = new TAE2Entities())
                    {
                        ROL_MENU rolUsuario = (ROL_MENU)element;
                        var modifiedRolMenu = entity.ROL_MENU.First(p => p.UNID_MENU == rolUsuario.UNID_MENU
                                                                            && p.UNID_ROL == rolUsuario.UNID_ROL);
                        //Sync
                        modifiedRolMenu.IS_ACTIVE = true;
                        modifiedRolMenu.IS_MODIFIED = true;
                        modifiedRolMenu.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();

                        entity.SaveChanges();
                    }
                }
            
        }

        public void udpateUR(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    USUARIO_ROL UsuarioRol = (USUARIO_ROL)element;
                    var modifiedUsuarioRol = entity.USUARIO_ROL.First(p => p.UNID_USUARIO == UsuarioRol.UNID_USUARIO
                                                                        && p.UNID_ROL == UsuarioRol.UNID_ROL);
                    //Sync
                    modifiedUsuarioRol.IS_ACTIVE = true;
                    modifiedUsuarioRol.IS_MODIFIED = true;
                    modifiedUsuarioRol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();

                    entity.SaveChanges();
                }
            }

        }

        public void insertRM(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ROL_MENU rolMenu = (ROL_MENU)element;
                    
                    //Sync
                    rolMenu.IS_ACTIVE = true;
                    rolMenu.IS_MODIFIED = true;
                    rolMenu.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.ROL_MENU.AddObject(rolMenu);
                    entity.SaveChanges();
                }
            }

        }

        public void insertUR(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    USUARIO_ROL UsuarioRol = (USUARIO_ROL)element;

                    //Sync
                    UsuarioRol.IS_ACTIVE = true;
                    UsuarioRol.IS_MODIFIED = true;
                    UsuarioRol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.USUARIO_ROL.AddObject(UsuarioRol);
                    entity.SaveChanges();
                }
            }

        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    USUARIO_ROL usuario = (USUARIO_ROL)element;

                    var modifiedRol = entity.USUARIO_ROL.First(p => p.UNID_USUARIO == usuario.UNID_USUARIO && p.UNID_ROL == usuario.UNID_ROL);
                    modifiedRol.IS_ACTIVE = usuario.IS_ACTIVE;
                    //Sync                        
                    modifiedRol.IS_MODIFIED = true;
                    modifiedRol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
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
                    USUARIO_ROL usuario = (USUARIO_ROL)element;

                    var validacion = (from cust in entity.USUARIO_ROL
                                      where usuario.UNID_ROL == cust.UNID_ROL && usuario.UNID_USUARIO == cust.UNID_USUARIO
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        usuario.IS_ACTIVE = true;
                        usuario.IS_MODIFIED = true;
                        usuario.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        //Sync                        
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

        public void upsertElement(object element)
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
                        usuario.IS_ACTIVE = true;
                        usuario.IS_MODIFIED = true;
                        usuario.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        //Sync
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.USUARIO_ROL.AddObject(usuario);
                        entity.SaveChanges();
                    }
                    else {

                        var modifiedRol = entity.USUARIO_ROL.First(p => p.UNID_USUARIO == usuario.UNID_USUARIO && p.UNID_ROL == usuario.UNID_ROL);
                        modifiedRol.IS_ACTIVE = true;
                        //Sync                        
                        modifiedRol.IS_MODIFIED = true;
                        modifiedRol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                    }
                }
            }          
        }

        public void deleteElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    USUARIO_ROL usuarioRol = (USUARIO_ROL)element;
                    var modifiedUsuarioRol = entity.USUARIO_ROL.First(p => p.UNID_USUARIO == usuarioRol.UNID_USUARIO 
                                                                        && p.UNID_ROL ==usuarioRol.UNID_ROL);
                    modifiedUsuarioRol.IS_ACTIVE = false;
                    //Sync
                    modifiedUsuarioRol.IS_MODIFIED = true;
                    modifiedUsuarioRol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();

                    entity.SaveChanges();
                }
            }
        }

        public void deleteElementsByRol(long l) {
                        
            using (var entity = new TAE2Entities())
            {
                var lista = (from cust in entity.USUARIO_ROL
                             where cust.UNID_ROL == l
                             select cust).ToList();


                foreach (USUARIO_ROL ur in lista)
                {
                    //Sync
                    var modifiedRolList = (from union in entity.USUARIO_ROL
                                           where union.UNID_ROL == ur.UNID_ROL && union.UNID_USUARIO == ur.UNID_USUARIO
                                           select union).ToList();

                    if(modifiedRolList.Count > 0){
                    
                        var modifiedRol = (from union in entity.USUARIO_ROL
                                           where union.UNID_ROL == ur.UNID_ROL && union.UNID_USUARIO == ur.UNID_USUARIO
                                           select union).First();

                        modifiedRol.IS_ACTIVE = false;
                        //Sync                        
                        modifiedRol.IS_MODIFIED = true;
                        modifiedRol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                    }

                    
                }        
            }            
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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         IS_ACTIVE = row.IS_ACTIVE
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

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetUsuarioRol()
        {
            List<USUARIO_ROL> reset = new List<USUARIO_ROL>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.USUARIO_ROL
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new USUARIO_ROL
                     {
                         UNID_ROL = row.UNID_ROL,
                         UNID_USUARIO = row.UNID_USUARIO,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         IS_ACTIVE = row.IS_ACTIVE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.USUARIO_ROL.First(p => p.UNID_USUARIO == item.UNID_USUARIO && p.UNID_ROL==item.UNID_ROL);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }


        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
