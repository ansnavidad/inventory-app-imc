using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class AppRolDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
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
                            udpateElement((object)poco);
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
            FixupCollection<ROL> tp = new FixupCollection<ROL>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.ROLs
                             select cust).ToList();
                if (query.Count > 0)
                {
                    res = query;
                }
                return res;
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

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ROL rol = (ROL)element;
                    var modifiedRol = entity.ROLs.First(p => p.UNID_ROL == rol.UNID_ROL);
                    modifiedRol.ROL_NAME = rol.ROL_NAME;
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

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ROL rol = (ROL)element;

                    var validacion = (from cust in entity.ROLs
                                      where cust.ROL_NAME == rol.ROL_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        rol.UNID_ROL = UNID.getNewUNID();
                        //Sync
                        rol.IS_MODIFIED = true;
                        rol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.ROLs.AddObject(rol);
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
                    ROL rol = (ROL)element;

                    var validacion = (from cust in entity.ROLs
                                      where cust.ROL_NAME == rol.ROL_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {                        
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
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ROL rol = (ROL)element;
                    var modifiedRol = entity.ROLs.First(p => p.UNID_ROL == rol.UNID_ROL);
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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
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
    }
}
