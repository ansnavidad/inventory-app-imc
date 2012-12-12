﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class AppRolMenuDataMapper : IDataMapper
    {
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

                        if (UNID.compareUNIDS(aux.LAST_MODIFIED_DATE, poco.LAST_MODIFIED_DATE))
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }

                    var modifiedRol = entity.ROLs.First(p => p.UNID_ROL == poco.UNID_ROL);
                    modifiedRol.IS_ACTIVE = false;
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
    }
}