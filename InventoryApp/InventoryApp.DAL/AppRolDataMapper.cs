using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace InventoryApp.DAL
{
    public class AppRolDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.ROLs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from rol in entity.ROLs
                         where rol.IS_ACTIVE == true
                         where rol.IS_MODIFIED == false
                         select rol.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonRol(long? Last_Modified_Date)
        {
            string res = null;
            List<ROL> listRol = new List<ROL>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ROLs
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listRol.Add(new ROL
                     {
                         UNID_ROL = row.UNID_ROL,
                         ROL_NAME = row.ROL_NAME,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_SYSTEM_ROOL = row.IS_SYSTEM_ROOL,
                         RECIBIR_MAILS = row.RECIBIR_MAILS
                     });
                 });
                if (listRol.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listRol);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {

            if (element != null)
            {
                ROL poco = (ROL)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ROLs
                                 where poco.UNID_ROL == cust.UNID_ROL
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

                    var modifiedRol = entity.ROLs.First(p => p.UNID_ROL == poco.UNID_ROL);
                    modifiedRol.IS_MODIFIED = false;
                    entity.SaveChanges(); 
                }
            }
        }

        public object getElements()
        {
            ObservableCollection<ROL> tp = new ObservableCollection<ROL>();           

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.ROLs
                             where cust.IS_ACTIVE == true && cust.UNID_ROL != 1
                             select cust).ToList();

                if (query.Count > 0)
                {
                    foreach (ROL r in query) {

                        tp.Add(r);
                    }
                }
                return tp;
            }
        }

        public object getElementsRol()
        {
            
            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.ROLs
                             where cust.IS_ACTIVE == true
                             select cust).ToList();

                return query;
            }
        }

        public object getElementsSecurity(bool b)
        {
            if (b)
            {
                ObservableCollection<ROL> tp = new ObservableCollection<ROL>();

                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ROLs
                                 .Include("ROL_MENU")
                                 .Include("USUARIO_ROL")
                                 .Include("ROL_MENU.MENU")
                                 .Include("USUARIO_ROL.USUARIO")
                                 where cust.IS_ACTIVE == true && cust.UNID_ROL != 1
                                 select cust).ToList();

                    if (query.Count > 0)
                    {
                        foreach (ROL r in query)
                        {
                            tp.Add(r);
                        }
                    }
                    return tp;
                }
            }
            else {

                ObservableCollection<ROL> tp = new ObservableCollection<ROL>();

                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ROLs
                                 .Include("ROL_MENU")
                                 .Include("USUARIO_ROL")
                                 .Include("ROL_MENU.MENU")
                                 .Include("USUARIO_ROL.USUARIO")
                                 where cust.IS_ACTIVE == true && cust.UNID_ROL != 1 && !cust.IS_SYSTEM_ROOL
                                 select cust).ToList();

                    if (query.Count > 0)
                    {
                        foreach (ROL r in query)
                        {
                            tp.Add(r);
                        }
                    }
                    return tp;
                }
            }
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                ROL Rol = (ROL)element;
                var query = (from cust in entitie.ROLs
                             where cust.UNID_ROL == Rol.UNID_ROL
                             select cust).ToList();
                if (query.Count > 0)
                {
                    res = query;
                }
                return res;
            }
        }

        public object getElementRoles(long unidUsuario)
        {
            object res = null;
            if (unidUsuario != null)
            {
                
                using (var entitie = new TAE2Entities())
                {

                    var query = (from cust in entitie.ROLs
                                 join ur in entitie.USUARIO_ROL
                                 on  cust.UNID_ROL equals ur.UNID_ROL
                                 join u in entitie.USUARIOs
                                 on ur.UNID_USUARIO equals u.UNID_USUARIO
                                 where cust.IS_ACTIVE == true &&
                                 u.UNID_USUARIO == unidUsuario &&
                                 ur.IS_ACTIVE ==true
                                 select cust).ToList();
                    if (query.Count != 0)
                        res = query;
                    else
                        return null;
                }
            }
            return res;
        }

        public void deleteElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ROL rol = (ROL)element;
                    var modifiedRol = entity.ROLs.First(p => p.UNID_ROL == rol.UNID_ROL);                    
                    modifiedRol.IS_ACTIVE = false;
                    //Sync
                    modifiedRol.IS_MODIFIED = true;
                    modifiedRol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    //
                    entity.SaveChanges();

                    UNID.Master(rol, u, -1, "Emininación");
                }
            }
        }

        public void udpateElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ROL rol = (ROL)element;
                    var modifiedRol = entity.ROLs.First(p => p.UNID_ROL == rol.UNID_ROL);
                    modifiedRol.ROL_NAME = rol.ROL_NAME;
                    modifiedRol.RECIBIR_MAILS = rol.RECIBIR_MAILS;
                    modifiedRol.IS_SYSTEM_ROOL = rol.IS_SYSTEM_ROOL;                    
                    //Sync
                    modifiedRol.IS_MODIFIED = true;
                    modifiedRol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    //
                    entity.SaveChanges();

                    UNID.Master(rol, u, -1, "Modificación");
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ROL rol = (ROL)element;
                    var modifiedRol = entity.ROLs.First(p => p.UNID_ROL == rol.UNID_ROL);
                    modifiedRol.ROL_NAME = rol.ROL_NAME;
                    modifiedRol.IS_ACTIVE = rol.IS_ACTIVE;
                    modifiedRol.RECIBIR_MAILS = rol.RECIBIR_MAILS;
                    modifiedRol.IS_SYSTEM_ROOL = rol.IS_SYSTEM_ROOL;  
                    //Sync
                    modifiedRol.IS_MODIFIED = true;
                    modifiedRol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public void insertElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ROL rol = (ROL)element;

                    var validacion = (from cust in entity.ROLs
                                      where cust.ROL_NAME == rol.ROL_NAME && cust.IS_ACTIVE
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        //rol.UNID_ROL = UNID.getNewUNID();
                        //Sync
                        rol.IS_MODIFIED = true;
                        rol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.ROLs.AddObject(rol);
                        entity.SaveChanges();
                        UNID.Master(rol, u, -1, "Inserción");
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
                    ROL rol = (ROL)element;

                    //Sync

                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.ROLs.AddObject(rol);
                    entity.SaveChanges();                   
                }
            }
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
                         UNID_ROL = row.UNID_ROL,
                         ROL_NAME = row.ROL_NAME,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         IS_ACTIVE= row.IS_ACTIVE,
                         IS_SYSTEM_ROOL= row.IS_SYSTEM_ROOL,
                         RECIBIR_MAILS = row.RECIBIR_MAILS

                     });
                 });
                if (listRol.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listRol);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<ROL>
        /// </summary>
        /// <returns>Regresa List<ROL></returns>
        /// <returns>Si no regresa null</returns>
        public List<ROL> GetDeserializeRol(string listPocos)
        {
            List<ROL> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<ROL>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetRol()
        {
            List<ROL> reset = new List<ROL>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ROLs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new ROL
                     {
                         UNID_ROL = row.UNID_ROL,
                         ROL_NAME = row.ROL_NAME,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_SYSTEM_ROOL = row.IS_SYSTEM_ROOL,
                         RECIBIR_MAILS = row.RECIBIR_MAILS
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.ROLs.First(p => p.UNID_ROL == item.UNID_ROL);
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


        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
