using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class AppRolMenuDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from rolmenu in entity.ROL_MENU
                         where rolmenu.IS_ACTIVE == true
                         where rolmenu.IS_MODIFIED == false
                         select rolmenu.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonRolMenu(long? Last_Modified_Date)
        {
            string res = null;
            List<ROL_MENU> listRolMenu = new List<ROL_MENU>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ROL_MENU
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listRolMenu.Add(new ROL_MENU
                     {
                         UNID_ROL = row.UNID_ROL,
                         UNID_MENU = row.UNID_MENU,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listRolMenu.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listRolMenu);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {

            if (element != null)
            {
                ROL_MENU poco = (ROL_MENU)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ROL_MENU
                                 where poco.UNID_ROL == cust.UNID_ROL && poco.UNID_MENU == cust.UNID_MENU
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

                    var modifiedRol = entity.ROLs.First(p => p.UNID_ROL == poco.UNID_ROL);
                    modifiedRol.IS_MODIFIED = false;
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
                    ROL_MENU rolMenu = (ROL_MENU)element;

                    var validacion = (from cust in entity.ROL_MENU
                                      where cust.UNID_ROL == rolMenu.UNID_ROL && cust.UNID_MENU == rolMenu.UNID_MENU
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {                        
                        //Sync
                        
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.ROL_MENU.AddObject(rolMenu);
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
        /// Método que serializa una List<ROL_MENU> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de ROL_MENU</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonRolMenu()
        {
            string res = null;
            List<ROL_MENU> listRolMenu = new List<ROL_MENU>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ROL_MENU
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listRolMenu.Add(new ROL_MENU
                     {
                         UNID_ROL = row.UNID_ROL,
                         UNID_MENU = row.UNID_MENU,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listRolMenu.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listRolMenu);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<ROL_MENU>
        /// </summary>
        /// <returns>Regresa List<ROL_MENU></returns>
        /// <returns>Si no regresa null</returns>
        public List<ROL_MENU> GetDeserializeRolMenu(string listPocos)
        {
            List<ROL_MENU> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<ROL_MENU>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetRolMenu()
        {
            List<ROL_MENU> reset = new List<ROL_MENU>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ROL_MENU
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new ROL_MENU
                     {
                         UNID_ROL = row.UNID_ROL,
                         UNID_MENU = row.UNID_MENU,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.ROL_MENU.First(p => p.UNID_MENU==item.UNID_MENU);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
