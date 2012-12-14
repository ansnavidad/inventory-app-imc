using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.Data;
using System.Data.Entity;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{

    public class ProveedorDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from prov in entity.PROVEEDORs
                         where prov.IS_ACTIVE == true
                         where prov.IS_MODIFIED == false
                         select prov.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonProveedor(long? LastModifiedDate)
        {
            string res = null;
            List<PROVEEDOR> listProveedor = new List<PROVEEDOR>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROVEEDORs
                 where p.LAST_MODIFIED_DATE > LastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listProveedor.Add(new PROVEEDOR
                     {
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         PROVEEDOR_NAME = row.PROVEEDOR_NAME,
                         CONTACTO = row.CONTACTO,
                         TEL1 = row.TEL1,
                         TEL2 = row.TEL2,
                         MAIL = row.MAIL,
                         CALLE = row.CALLE,
                         UNID_PAIS = row.UNID_PAIS,
                         UNID_CIUDAD = row.UNID_CIUDAD,
                         CODIGO_POSTAL = row.CODIGO_POSTAL,
                         RFC = row.RFC,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listProveedor.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listProveedor);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                PROVEEDOR poco = (PROVEEDOR)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.PROVEEDORs
                                 where poco.UNID_PROVEEDOR == cust.UNID_PROVEEDOR
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

                    var modifiedMenu = entity.PROVEEDORs.First(p => p.UNID_PROVEEDOR == poco.UNID_PROVEEDOR);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
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
                                 where p.IS_ACTIVE == true && p.UNID_PROVEEDOR == element && c.IS_ACTIVE == true && relation.IS_ACTIVE == true
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
                    //INSERTA LAS NUEVAS RELACIONES PROVEEDOR CATEGORIA
                    if (unidCategoria.Count > 0)
                    {
                        foreach (var item in unidCategoria)
                        {
                            var query2 = (from cust in entity.PROVEEDOR_CATEGORIA
                                          where cust.UNID_CATEGORIA == item && cust.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR
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
                                PROVEEDOR_CATEGORIA proveedorCategoria = new PROVEEDOR_CATEGORIA();
                                proveedorCategoria.UNID_CATEGORIA = item;
                                proveedorCategoria.UNID_PROVEEDOR = proveedor.UNID_PROVEEDOR;
                                proveedorCategoria.IS_ACTIVE = true;

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

        /// <summary>
        /// Método que serializa una List<PROVEEDOR> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PROVEEDOR</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonProveedor()
        {
            string res = null;
            List<PROVEEDOR> listProveedor = new List<PROVEEDOR>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROVEEDORs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listProveedor.Add(new PROVEEDOR
                     {
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         PROVEEDOR_NAME= row.PROVEEDOR_NAME,
                         CONTACTO=row.CONTACTO,
                         TEL1=row.TEL1,
                         TEL2=row.TEL2,
                         MAIL=row.MAIL,
                         CALLE=row.CALLE,
                         UNID_PAIS=row.UNID_PAIS,
                         UNID_CIUDAD=row.UNID_CIUDAD,
                         CODIGO_POSTAL=row.CODIGO_POSTAL,
                         RFC=row.RFC,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listProveedor.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listProveedor);
                }
                return res;
            }
        }
    }
}
