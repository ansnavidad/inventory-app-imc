using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class AlmacenDataMapper : IDataMapper
    {
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
                ALMACEN almacen = (ALMACEN)element;
                var query = (from cust in entitie.ALMACENs
                             where cust.UNID_ALMACEN == almacen.UNID_ALMACEN
                             select cust).ToList();
                if (query.Count > 0)
                {
                    res = query;
                }
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
                             where a.IS_ACTIVE == true && a.UNID_ALMACEN == element
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


                    //var sync = (from sy in entity.SYNCs
                    //            select sy).ToList().First();
                    //entity.SYNCs.DeleteObject(sync);
                    //SYNC syncN = new SYNC();                    
                    //syncN.UNID_SYNC = UNID.getNewUNID();                    
                    
                    //entity.SYNCs.AddObject(syncN);
                    
                    

                    entity.SaveChanges();
                }
            }
        }

        public void updateRelacion(object element, List<long> unidTecnico, List<long> auxUnidTecnico)
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
                            entity.ALMACEN_TECNICO.DeleteObject((ALMACEN_TECNICO)query);
                            entity.SaveChanges();
                        }
                    }
                    //INSERTA LAS NUEVAS RELACIONES ALMACEN TECNICO
                    if (unidTecnico.Count > 0)
                    {
                        foreach (var item in unidTecnico)
                        {
                            ALMACEN_TECNICO almacenTecnico = new ALMACEN_TECNICO();
                            almacenTecnico.UNID_ALMACEN = almacen.UNID_ALMACEN;
                            almacenTecnico.UNID_TECNICO =item;
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

        public void insertRelacion(object element, List<long> unidTecnico)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ALMACEN almacen = (ALMACEN)element;
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
    }
}
