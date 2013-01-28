using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class AlmacenDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.ALMACENs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from almacen in entity.ALMACENs
                         where almacen.IS_ACTIVE == true
                         where almacen.IS_MODIFIED == false
                         select almacen.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonAlmacen(long? Last_Modified_Date)
        {
            string res = null;
            List<ALMACEN> listAlmacen = new List<ALMACEN>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ALMACENs
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listAlmacen.Add(new ALMACEN
                     {
                         ALMACEN_NAME = row.ALMACEN_NAME,
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         CONTACTO = row.CONTACTO,
                         DIRECCION = row.DIRECCION,
                         MAIL = row.MAIL,
                         MAIL_DEFAULT = row.MAIL_DEFAULT,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listAlmacen.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listAlmacen);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                ALMACEN poco = (ALMACEN)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ALMACENs
                                 where poco.UNID_ALMACEN == cust.UNID_ALMACEN
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

                    var modifiedAlmacen = entity.ALMACENs.First(p => p.UNID_ALMACEN == poco.UNID_ALMACEN);
                    modifiedAlmacen.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public void loadSyncRelation(ALMACEN_TECNICO element)
        {
            if (element != null)
            {
                ALMACEN_TECNICO poco = (ALMACEN_TECNICO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ALMACEN_TECNICO
                                 where poco.UNID_ALMACEN == cust.UNID_ALMACEN && poco.UNID_TECNICO == cust.UNID_TECNICO
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        var aux = query.First();

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElementRelationSync((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElementRelation((object)poco);
                    }

                    var modifiedRelation = entity.ALMACEN_TECNICO.First(p => p.UNID_ALMACEN == poco.UNID_ALMACEN && p.UNID_TECNICO == poco.UNID_TECNICO);
                    modifiedRelation.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public void UpsertMixRelation(ALMACEN_TECNICO element)
        {
            if (element != null)
            {
                ALMACEN_TECNICO poco = (ALMACEN_TECNICO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ALMACEN_TECNICO
                                 where poco.UNID_ALMACEN == cust.UNID_ALMACEN && poco.UNID_TECNICO == cust.UNID_TECNICO
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        var query2 = (from cust in entity.ALMACEN_TECNICO
                                     where poco.UNID_ALMACEN == cust.UNID_ALMACEN && poco.UNID_TECNICO == cust.UNID_TECNICO
                                     select cust).ToList().First();
                        poco.IS_ACTIVE = true;
                        //Sync
                        query2.IS_MODIFIED = true;
                        query2.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                    }
                    //Inserción
                    else
                    {
                        poco.IS_ACTIVE = true;
                        //Sync
                        poco.IS_MODIFIED = true;
                        poco.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();

                        entity.ALMACEN_TECNICO.AddObject(poco);
                        entity.SaveChanges();  
                    }
                }
            }
        }

        public object getElements()
        {
            object res = null;

            using (var Entity = new TAE2Entities())
            {
                var query = (from p in Entity.ALMACENs
                           where p.IS_ACTIVE == true
                           select p).ToList();
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
                //ALMACEN almacen = (ALMACEN)element;
                //var query = (from cust in entitie.ALMACENs
                //             where cust.UNID_ALMACEN == almacen.UNID_ALMACEN
                //             select cust).ToList();
                //if (query.Count > 0)
                //{
                //    res = query;
                //}
                //return res;
                ALMACEN almacen = (ALMACEN)element;
                var query = (from cust in entitie.ALMACENs
                             where cust.UNID_ALMACEN == almacen.UNID_ALMACEN
                             select cust).First();

                res = query;
                return res;
            }
        }

        public object getElementAlmacenTecnico(long element)
        {
            object o = null;
            using (var Entity = new TAE2Entities())
            {
                var query = (from a in Entity.ALMACENs
                             join relation in Entity.ALMACEN_TECNICO
                             on a.UNID_ALMACEN equals relation.UNID_ALMACEN
                             join t in Entity.TECNICOes
                             on relation.UNID_TECNICO equals t.UNID_TECNICO
                             where a.IS_ACTIVE == true && a.UNID_ALMACEN == element && t.IS_ACTIVE == true && relation.IS_ACTIVE == true
                             select t).ToList();
                o = query;
            }
            return o;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ALMACEN almacen = (ALMACEN)element;
                    var modifiedAlmacen = entity.ALMACENs.First(p => p.UNID_ALMACEN == almacen.UNID_ALMACEN);
                    modifiedAlmacen.ALMACEN_NAME = almacen.ALMACEN_NAME;
                    modifiedAlmacen.CONTACTO = almacen.CONTACTO;
                    modifiedAlmacen.MAIL = almacen.MAIL;
                    modifiedAlmacen.DIRECCION = almacen.DIRECCION;
                    modifiedAlmacen.MAIL_DEFAULT = almacen.MAIL_DEFAULT;
                    //Sync
                    modifiedAlmacen.IS_MODIFIED = true;
                    modifiedAlmacen.LAST_MODIFIED_DATE = UNID.getNewUNID();

                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    
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
                    ALMACEN almacen = (ALMACEN)element;
                    var modifiedAlmacen = entity.ALMACENs.First(p => p.UNID_ALMACEN == almacen.UNID_ALMACEN);
                    modifiedAlmacen.ALMACEN_NAME = almacen.ALMACEN_NAME;
                    modifiedAlmacen.CONTACTO = almacen.CONTACTO;
                    modifiedAlmacen.MAIL = almacen.MAIL;
                    modifiedAlmacen.DIRECCION = almacen.DIRECCION;
                    modifiedAlmacen.MAIL_DEFAULT = almacen.MAIL_DEFAULT;
                    modifiedAlmacen.IS_ACTIVE = almacen.IS_ACTIVE;
                    //Sync
                    modifiedAlmacen.IS_MODIFIED = true;
                    modifiedAlmacen.LAST_MODIFIED_DATE = UNID.getNewUNID();

                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();

                    entity.SaveChanges();
                }
            }
        }

        public void udpateElementRelation(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ALMACEN_TECNICO relation = (ALMACEN_TECNICO)element;
                    var modifiedRelation = entity.ALMACEN_TECNICO.First(p => p.UNID_ALMACEN == relation.UNID_ALMACEN && p.UNID_TECNICO == relation.UNID_TECNICO);                    
                    //Sync
                    modifiedRelation.IS_MODIFIED = true;
                    modifiedRelation.LAST_MODIFIED_DATE = UNID.getNewUNID();

                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();

                    entity.SaveChanges();
                }
            }
        }

        public void udpateElementRelationSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ALMACEN_TECNICO relation = (ALMACEN_TECNICO)element;
                    var modifiedRelation = entity.ALMACEN_TECNICO.First(p => p.UNID_ALMACEN == relation.UNID_ALMACEN && p.UNID_TECNICO == relation.UNID_TECNICO);
                    modifiedRelation.IS_ACTIVE = relation.IS_ACTIVE;
                    //Sync
                    modifiedRelation.IS_MODIFIED = true;
                    modifiedRelation.LAST_MODIFIED_DATE = UNID.getNewUNID();

                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();

                    entity.SaveChanges();
                }
            }
        }

        public void updateRelacion(object element, List<long> unidTecnico, List<long> auxUnidTecnico)
        {
            try
            {
                if (element != null && ((ALMACEN)element).UNID_ALMACEN != 0)
                {
                    using (var entity = new TAE2Entities())
                    {
                        ALMACEN almacen = (ALMACEN)element;
                        var modifiedAlmacen = entity.ALMACENs.First(p => p.UNID_ALMACEN == almacen.UNID_ALMACEN);
                        modifiedAlmacen.ALMACEN_NAME = almacen.ALMACEN_NAME;
                        modifiedAlmacen.CONTACTO = almacen.CONTACTO;
                        modifiedAlmacen.MAIL = almacen.MAIL;
                        modifiedAlmacen.DIRECCION = almacen.DIRECCION;
                        modifiedAlmacen.MAIL_DEFAULT = almacen.MAIL_DEFAULT;
                        //Sync
                        modifiedAlmacen.IS_MODIFIED = true;
                        modifiedAlmacen.LAST_MODIFIED_DATE = UNID.getNewUNID();

                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();

                        entity.SaveChanges();
                        //ELIMINA TODAS LAS RELACIONES QUE EXISTEN
                        if (auxUnidTecnico.Count > 0)
                        {
                            foreach (var e in auxUnidTecnico)
                            {
                                //ALMACEN_TECNICO almacenTecnico = new ALMACEN_TECNICO();
                                //var query = (from a in entity.ALMACENs
                                //             join relation in entity.ALMACEN_TECNICO
                                //             on a.UNID_ALMACEN equals relation.UNID_ALMACEN
                                //             join t in entity.TECNICOes
                                //             on relation.UNID_TECNICO equals t.UNID_TECNICO
                                //             where a.UNID_ALMACEN == almacen.UNID_ALMACEN && t.UNID_TECNICO == e
                                //             select relation).ToList().First();
                                //entity.ALMACEN_TECNICO.DeleteObject((ALMACEN_TECNICO)query);
                                //entity.SaveChanges();

                                ALMACEN_TECNICO almacenTecnico = new ALMACEN_TECNICO();
                                var query = (from a in entity.ALMACENs
                                             join relation in entity.ALMACEN_TECNICO
                                             on a.UNID_ALMACEN equals relation.UNID_ALMACEN
                                             join t in entity.TECNICOes
                                             on relation.UNID_TECNICO equals t.UNID_TECNICO
                                             where a.UNID_ALMACEN == almacen.UNID_ALMACEN && t.UNID_TECNICO == e
                                             select relation).ToList().First();
                                query.IS_ACTIVE = false;
                                //Sync
                                query.IS_MODIFIED = true;
                                query.LAST_MODIFIED_DATE = UNID.getNewUNID();
                                modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                                modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                                entity.SaveChanges();
                                //
                            }
                        }
                        //INSERTA LAS NUEVAS RELACIONES ALMACEN TECNICO
                        if (unidTecnico.Count > 0)
                        {
                            foreach (var item in unidTecnico)
                            {
                                var query2 = (from cust in entity.ALMACEN_TECNICO
                                              where cust.UNID_ALMACEN == almacen.UNID_ALMACEN && cust.UNID_TECNICO == item
                                              select cust).ToList();

                                if (query2.Count > 0)
                                {
                                    var query3 = query2.First();

                                    //Sync
                                    query3.IS_ACTIVE = true;
                                    query3.IS_MODIFIED = true;
                                    query3.LAST_MODIFIED_DATE = UNID.getNewUNID();
                                    modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                                    entity.SaveChanges();
                                    //
                                }
                                else
                                {

                                    ALMACEN_TECNICO almacenTecnico = new ALMACEN_TECNICO();
                                    almacenTecnico.UNID_ALMACEN = almacen.UNID_ALMACEN;
                                    almacenTecnico.UNID_TECNICO = item;

                                    //Sync
                                    almacenTecnico.IS_MODIFIED = true;
                                    almacenTecnico.IS_ACTIVE = true;
                                    almacenTecnico.LAST_MODIFIED_DATE = UNID.getNewUNID();
                                    modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                                    entity.SaveChanges();
                                    //

                                    entity.ALMACEN_TECNICO.AddObject(almacenTecnico);
                                    entity.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                                
            }
        }

        public void updateRelacion(object element, List<long> unidTecnico, List<long> auxUnidTecnico, string s)
        {
            try
            {
                if (element != null && ((ALMACEN)element).UNID_ALMACEN != 0)
                {
                    using (var entity = new TAE2Entities())
                    {
                        ALMACEN almacen = (ALMACEN)element;
                        
                        //ELIMINA TODAS LAS RELACIONES QUE EXISTEN
                        if (auxUnidTecnico.Count > 0)
                        {
                            foreach (var e in auxUnidTecnico)
                            {
                                ALMACEN_TECNICO almacenTecnico = new ALMACEN_TECNICO();
                                var query = (from a in entity.ALMACENs
                                             join relation in entity.ALMACEN_TECNICO
                                             on a.UNID_ALMACEN equals relation.UNID_ALMACEN
                                             join t in entity.TECNICOes
                                             on relation.UNID_TECNICO equals t.UNID_TECNICO
                                             where a.UNID_ALMACEN == almacen.UNID_ALMACEN && t.UNID_TECNICO == e
                                             select relation).ToList().First();
                                query.IS_ACTIVE = false;
                                //Sync
                                query.IS_MODIFIED = true;
                                query.LAST_MODIFIED_DATE = UNID.getNewUNID();
                                var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                                modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                                entity.SaveChanges();
                                //
                            }
                        }
                        //INSERTA LAS NUEVAS RELACIONES ALMACEN TECNICO
                        if (unidTecnico.Count > 0)
                        {
                            foreach (var item in unidTecnico)
                            {
                                var query2 = (from cust in entity.ALMACEN_TECNICO
                                              where cust.UNID_ALMACEN == almacen.UNID_ALMACEN && cust.UNID_TECNICO == item
                                              select cust).ToList();

                                if (query2.Count > 0)
                                {
                                    var query3 = query2.First();

                                    //Sync
                                    query3.IS_ACTIVE = true;
                                    query3.IS_MODIFIED = true;
                                    query3.LAST_MODIFIED_DATE = UNID.getNewUNID();
                                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                                    entity.SaveChanges();
                                    //
                                }
                                else
                                {

                                    ALMACEN_TECNICO almacenTecnico = new ALMACEN_TECNICO();
                                    almacenTecnico.UNID_ALMACEN = almacen.UNID_ALMACEN;
                                    almacenTecnico.UNID_TECNICO = item;

                                    //Sync
                                    almacenTecnico.IS_MODIFIED = true;
                                    almacenTecnico.IS_ACTIVE = true;
                                    almacenTecnico.LAST_MODIFIED_DATE = UNID.getNewUNID();
                                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                                    entity.SaveChanges();
                                    //

                                    entity.ALMACEN_TECNICO.AddObject(almacenTecnico);
                                    entity.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ALMACEN almacen = (ALMACEN)element;

                    var validacion = (from cust in entity.ALMACENs
                                      where cust.ALMACEN_NAME == almacen.ALMACEN_NAME
                                      select cust).ToList();
                    
                    if (validacion.Count == 0)
                    {
                        almacen.UNID_ALMACEN = UNID.getNewUNID();
                        //Sync
                        almacen.IS_MODIFIED = true;
                        almacen.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();

                        entity.ALMACENs.AddObject(almacen);
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
                    ALMACEN almacen = (ALMACEN)element;
                                          
                    //Sync
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();

                    entity.ALMACENs.AddObject(almacen);
                    entity.SaveChanges();
                }
            }
        }

        public void insertElementRelation(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ALMACEN_TECNICO relation = (ALMACEN_TECNICO)element;                                        
  
                    //Sync
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();

                    entity.ALMACEN_TECNICO.AddObject(relation);
                    entity.SaveChanges();                    
                }
            }
        }

        public void insertRelacion(object element, List<long> unidTecnico)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ALMACEN almacen = (ALMACEN)element;
                    if (almacen.UNID_ALMACEN == 0)
                        almacen.UNID_ALMACEN = UNID.getNewUNID();
                    //Sync
                    almacen.IS_MODIFIED = true;
                    almacen.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();

                    entity.ALMACENs.AddObject(almacen);
                    entity.SaveChanges();

                    if (unidTecnico.Count > 0)
                    {
                        foreach (var item in unidTecnico)
                        {
                            ALMACEN_TECNICO almacenTecnico = new ALMACEN_TECNICO();
                            almacenTecnico.UNID_ALMACEN =almacen.UNID_ALMACEN;
                            almacenTecnico.UNID_TECNICO = item;
                            //Sync
                            almacenTecnico.IS_MODIFIED = true;
                            almacenTecnico.LAST_MODIFIED_DATE = UNID.getNewUNID();
                            modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                            modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                            entity.SaveChanges();

                            entity.ALMACEN_TECNICO.AddObject(almacenTecnico);
                            entity.SaveChanges();
                        }
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
                    ALMACEN almacen = (ALMACEN)element;
                    var modifiedAlamacen = entity.ALMACENs.First(p => p.UNID_ALMACEN == almacen.UNID_ALMACEN);
                    modifiedAlamacen.IS_ACTIVE = false;
                    //Sync
                    modifiedAlamacen.IS_MODIFIED = true;
                    modifiedAlamacen.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();

                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<ALMACEN> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de ALMACEN</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonAlmacen()
        {
            string res = null;
            List<ALMACEN> listAlmacen = new List<ALMACEN>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ALMACENs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                                    {
                                        listAlmacen.Add(new ALMACEN
                                        {
                                            ALMACEN_NAME = row.ALMACEN_NAME,
                                            UNID_ALMACEN = row.UNID_ALMACEN,
                                            CONTACTO = row.CONTACTO,
                                            DIRECCION = row.DIRECCION,
                                            MAIL= row.MAIL,
                                            MAIL_DEFAULT=row.MAIL_DEFAULT,
                                            IS_ACTIVE= row.IS_ACTIVE,
                                            IS_MODIFIED=row.IS_MODIFIED,
                                            LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                                        });
                                    });
                if (listAlmacen.Count>0)
                {
                    res = SerializerJson.SerializeParametros(listAlmacen);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<ALMACEN>
        /// </summary>
        /// <returns>Regresa List<ALMACEN></returns>
        /// <returns>Si no regresa null</returns>
        public List<ALMACEN> GetDeserializeAlmacen(string listPocos)
        {
            List<ALMACEN> res =null;
            
            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<ALMACEN>>(listPocos);    
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetAlmacen()
        {
            List<ALMACEN> reset = new List<ALMACEN>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ALMACENs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new ALMACEN
                     {
                         ALMACEN_NAME = row.ALMACEN_NAME,
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         CONTACTO = row.CONTACTO,
                         DIRECCION = row.DIRECCION,
                         MAIL = row.MAIL,
                         MAIL_DEFAULT = row.MAIL_DEFAULT,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.ALMACENs.First(p => p.UNID_ALMACEN == item.UNID_ALMACEN);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }

    }
}
