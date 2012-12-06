using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.Data;
using System.Data.Entity;

namespace InventoryApp.DAL
{
    public class ProveedorDataMapper : IDataMapper
    {
        public object getElements()
        {
            object res = null;
           
            using (var Entity = new TAE2Entities())
            {
                var query = (from p in Entity.PROVEEDORs
                             where p.IS_ACTIVE == true
                             select p).ToList();
                foreach (PROVEEDOR trans in ((List<PROVEEDOR>)query))
                {
                    trans.PAI = trans.PAI;
                    trans.CIUDAD = trans.CIUDAD;
                }

                if (query.Count>0)
                {
                    res = query;
                }
                return res;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                PROVEEDOR Eprov = (PROVEEDOR)element;
                using (var Entity = new TAE2Entities())
                {

                    var res = (from p in Entity.PROVEEDORs
                               where p.UNID_PROVEEDOR == Eprov.UNID_PROVEEDOR
                               select p).ToList();

                    foreach (PROVEEDOR trans in ((List<PROVEEDOR>)res))
                    {
                        trans.CIUDAD = trans.CIUDAD;
                        trans.PAI = trans.PAI;
                    }

                    o = (object)res;                    

                }
            }
            return o;
        }

        public object getElementProveedorCategoria(long element)
        {
            object o = null;
                using (var Entity = new TAE2Entities())
                {
                    var query = (from p in Entity.PROVEEDORs
                                 join relation in Entity.PROVEEDOR_CATEGORIA
                                 on p.UNID_PROVEEDOR equals relation.UNID_PROVEEDOR
                                 join c in Entity.CATEGORIAs
                                 on relation.UNID_CATEGORIA equals c.UNID_CATEGORIA
                                 where p.IS_ACTIVE == true && p.UNID_PROVEEDOR == element
                                 select c).ToList();
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
                    PROVEEDOR proveedor = (PROVEEDOR)element;

                    var modifiedProveedor = entity.PROVEEDORs.First(p => p.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR);
                    modifiedProveedor.CALLE = proveedor.CALLE;
                    modifiedProveedor.CODIGO_POSTAL = proveedor.CODIGO_POSTAL;
                    modifiedProveedor.CONTACTO = proveedor.CONTACTO;
                    modifiedProveedor.MAIL = proveedor.MAIL;
                    modifiedProveedor.RFC = proveedor.RFC;
                    modifiedProveedor.UNID_CIUDAD = proveedor.UNID_CIUDAD;
                    modifiedProveedor.UNID_PAIS = proveedor.UNID_PAIS;
                    modifiedProveedor.PAI = proveedor.PAI;
                    modifiedProveedor.CIUDAD = proveedor.CIUDAD;
                    modifiedProveedor.TEL1 = proveedor.TEL1;
                    modifiedProveedor.TEL2 = proveedor.TEL2;
                    modifiedProveedor.PROVEEDOR_NAME = proveedor.PROVEEDOR_NAME;
                    //Sync
                    modifiedProveedor.IS_MODIFIED = true;
                    modifiedProveedor.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public void updateRelacion(object element, List<long> unidCategoria, List<long> auxUnidCategoria)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROVEEDOR proveedor = (PROVEEDOR)element;

                    var modifiedProveedor = entity.PROVEEDORs.First(p => p.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR);
                    modifiedProveedor.CALLE = proveedor.CALLE;
                    modifiedProveedor.CODIGO_POSTAL = proveedor.CODIGO_POSTAL;
                    modifiedProveedor.CONTACTO = proveedor.CONTACTO;
                    modifiedProveedor.MAIL = proveedor.MAIL;
                    modifiedProveedor.RFC = proveedor.RFC;
                    modifiedProveedor.UNID_CIUDAD = proveedor.UNID_CIUDAD;
                    modifiedProveedor.UNID_PAIS = proveedor.UNID_PAIS;
                    modifiedProveedor.PAI = proveedor.PAI;
                    modifiedProveedor.CIUDAD = proveedor.CIUDAD;
                    modifiedProveedor.TEL1 = proveedor.TEL1;
                    modifiedProveedor.TEL2 = proveedor.TEL2;
                    modifiedProveedor.PROVEEDOR_NAME = proveedor.PROVEEDOR_NAME;
                    //Sync
                    modifiedProveedor.IS_MODIFIED = true;
                    modifiedProveedor.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                    //ELIMINA TODAS LAS RELACIONES QUE EXISTEN
                    if (auxUnidCategoria.Count > 0)
                    {
                        foreach (var e in auxUnidCategoria)
                        {
                            PROVEEDOR_CATEGORIA proveedorCategoria = new PROVEEDOR_CATEGORIA();
                            var query = (from p in entity.PROVEEDORs
                                         join relation in entity.PROVEEDOR_CATEGORIA
                                         on p.UNID_PROVEEDOR equals relation.UNID_PROVEEDOR
                                         join c in entity.CATEGORIAs
                                         on relation.UNID_CATEGORIA equals c.UNID_CATEGORIA
                                         where p.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR && c.UNID_CATEGORIA == e
                                         select relation).ToList().First();
                            entity.PROVEEDOR_CATEGORIA.DeleteObject((PROVEEDOR_CATEGORIA)query);
                            entity.SaveChanges();
                        }
                    }
                    //INSERTA LAS NUEVAS RELACIONES PROVEEDOR CATEGORIA
                    if (unidCategoria.Count > 0)
                    {
                        foreach (var item in unidCategoria)
                        {
                            PROVEEDOR_CATEGORIA proveedorCategoria = new PROVEEDOR_CATEGORIA();
                            proveedorCategoria.UNID_CATEGORIA = item;
                            proveedorCategoria.UNID_PROVEEDOR = proveedor.UNID_PROVEEDOR;
                            //Sync
                            proveedorCategoria.IS_MODIFIED = true;
                            proveedorCategoria.LAST_MODIFIED_DATE = UNID.getNewUNID();

                            modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                            modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                            entity.SaveChanges();
                            //
                            entity.PROVEEDOR_CATEGORIA.AddObject(proveedorCategoria);
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
                    PROVEEDOR proveedor = (PROVEEDOR)element;

                    var validacion = (from cust in entity.PROVEEDORs
                                      where cust.PROVEEDOR_NAME == proveedor.PROVEEDOR_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        proveedor.UNID_PROVEEDOR = UNID.getNewUNID();
                        //Sync
                        proveedor.IS_MODIFIED = true;
                        proveedor.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.PROVEEDORs.AddObject(proveedor);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void insertRelacion(object element, List<long> unidCategoria)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROVEEDOR proveedor = (PROVEEDOR)element;
                    proveedor.UNID_PROVEEDOR = UNID.getNewUNID();
                    entity.PROVEEDORs.AddObject(proveedor);
                    entity.SaveChanges();

                    if (unidCategoria.Count > 0)
                    {
                        foreach (var item in unidCategoria)
                        {
                            PROVEEDOR_CATEGORIA proveedorCategoria = new PROVEEDOR_CATEGORIA();
                            proveedorCategoria.UNID_CATEGORIA = item;
                            proveedorCategoria.UNID_PROVEEDOR = proveedor.UNID_PROVEEDOR;
                            //Sync
                            proveedorCategoria.IS_MODIFIED = true;
                            proveedorCategoria.LAST_MODIFIED_DATE = UNID.getNewUNID();
                            var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                            modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                            entity.SaveChanges();
                            //
                            entity.PROVEEDOR_CATEGORIA.AddObject(proveedorCategoria);
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
                    PROVEEDOR proveedor = (PROVEEDOR)element;

                    var deleteProveedor = entity.PROVEEDORs.First(p => p.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR);

                    deleteProveedor.IS_ACTIVE = false;
                    //Sync
                    deleteProveedor.IS_MODIFIED = true;
                    deleteProveedor.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    //
                    entity.SaveChanges();
                }
            }
        }
    }
}
