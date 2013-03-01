using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.Data;
using System.Data.Entity;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{

    public class ProveedorDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.PROVEEDORs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

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

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElementSync((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElementSync((object)poco);
                    }

                    var modifiedMenu = entity.PROVEEDORs.First(p => p.UNID_PROVEEDOR == poco.UNID_PROVEEDOR);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public void loadSyncRelation(object element)
        {
            if (element != null)
            {
                PROVEEDOR_CATEGORIA poco = (PROVEEDOR_CATEGORIA)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.PROVEEDOR_CATEGORIA
                                 where poco.UNID_PROVEEDOR == cust.UNID_PROVEEDOR && poco.UNID_CATEGORIA == cust.UNID_CATEGORIA
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
                        insertElementRelationSyn((object)poco);
                    }

                    var modifiedRelation = entity.PROVEEDOR_CATEGORIA.First(p => p.UNID_PROVEEDOR == poco.UNID_PROVEEDOR && p.UNID_CATEGORIA == poco.UNID_CATEGORIA);
                    modifiedRelation.IS_MODIFIED = false;
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
                               select p).First();

                    
                        res.CIUDAD = res.CIUDAD;
                        res.PAI = res.PAI;
                    

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

        public object getElementProveedorCuenta(long element)
        {
            object o = null;
                using (var Entity = new TAE2Entities())
                {
                    var query = (from p in Entity.PROVEEDORs
                                 join c in Entity.PROVEEDOR_CUENTA
                                 on p.UNID_PROVEEDOR equals c.UNID_PROVEEDOR
                                 where c.IS_ACTIVE == true && p.UNID_PROVEEDOR == element && p.IS_ACTIVE == true
                                 select c).ToList();
                    foreach (PROVEEDOR_CUENTA a in query) {

                        a.PROVEEDOR = a.PROVEEDOR;
                        a.BANCO = a.BANCO;
                    }

                    o = query;                    
                }

            return o;
        }
        
        public List<PROVEEDOR> getElementsByCategoria(CATEGORIA categoria)
        {
            List<PROVEEDOR> res = new List<PROVEEDOR>();

            try
            {
                using (var entity = new TAE2Entities())
                {

                    var o = (from provL in entity.PROVEEDOR_CATEGORIA
                             join prov in entity.PROVEEDORs on provL.UNID_PROVEEDOR equals prov.UNID_PROVEEDOR
                             join cat in entity.CATEGORIAs on provL.UNID_CATEGORIA equals cat.UNID_CATEGORIA
                             where provL.UNID_CATEGORIA == categoria.UNID_CATEGORIA
                             select prov).ToList<PROVEEDOR>();

  
                    foreach (PROVEEDOR c in o)
                    {
                        res.Add(c);
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
            return res;
        }//

        public void udpateElement(object element, USUARIO u)
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
                    UNID.Master(proveedor, u, -1, "Modificación");
                }
            }
        }

        public void udpateElementSync(object element)
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
                    modifiedProveedor.IS_ACTIVE = proveedor.IS_ACTIVE;
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

        public void updateRelacion(object element, List<long> unidCategoria, List<long> auxUnidCategoria, List<long> unidCuenta, List<long> auxUnidCuenta, List<PROVEEDOR_CUENTA> listF, USUARIO u)
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
                    UNID.Master(proveedor, u, -1, "Modificación");
                    
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

                    //ELIMINA TODAS LAS RELACIONES DE PROVEEDOR_CUENTA
                    if (auxUnidCuenta.Count > 0)
                    {
                        foreach (var e in auxUnidCuenta)
                        {
                            PROVEEDOR_CUENTA proveedorCuenta = new PROVEEDOR_CUENTA();
                            var query = (from p in entity.PROVEEDORs
                                         join relation in entity.PROVEEDOR_CUENTA
                                         on p.UNID_PROVEEDOR equals relation.UNID_PROVEEDOR
                                         where p.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR && relation.UNID_PROVEEDOR_CUENTA == e
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
                    //INSERTA LAS NUEVAS RELACIONES PROVEEDOR CUENTA
                    if (unidCuenta.Count > 0)
                    {
                        foreach (var item in unidCuenta)
                        {
                            var query2 = (from cust in entity.PROVEEDOR_CUENTA
                                          where cust.UNID_PROVEEDOR_CUENTA == item && cust.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR
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
                                PROVEEDOR_CUENTA proveedorCuenta = new PROVEEDOR_CUENTA();

                                proveedorCuenta = listF.First(p => p.UNID_PROVEEDOR_CUENTA == item);

                                proveedorCuenta.UNID_PROVEEDOR_CUENTA = item;
                                proveedorCuenta.UNID_PROVEEDOR = proveedor.UNID_PROVEEDOR;
                                proveedorCuenta.IS_ACTIVE = true;

                                //Sync
                                proveedorCuenta.IS_MODIFIED = true;
                                proveedorCuenta.LAST_MODIFIED_DATE = UNID.getNewUNID();
                                modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                                modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                                entity.SaveChanges();
                                //

                                entity.PROVEEDOR_CUENTA.AddObject(proveedorCuenta);
                                entity.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        public void udpateElementRelation(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROVEEDOR_CATEGORIA relation = (PROVEEDOR_CATEGORIA)element;
                    var modifiedRelation = entity.PROVEEDOR_CATEGORIA.First(p => p.UNID_PROVEEDOR == relation.UNID_PROVEEDOR && p.UNID_CATEGORIA == relation.UNID_CATEGORIA);                    
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
                    PROVEEDOR_CATEGORIA relation = (PROVEEDOR_CATEGORIA)element;
                    var modifiedRelation = entity.PROVEEDOR_CATEGORIA.First(p => p.UNID_PROVEEDOR == relation.UNID_PROVEEDOR && p.UNID_CATEGORIA == relation.UNID_CATEGORIA);
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

        public void insertElement(object element, USUARIO u)
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

                        UNID.Master(proveedor, u, -1, "Inserción");
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
                    PROVEEDOR proveedor = (PROVEEDOR)element;

                    //Sync
                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.PROVEEDORs.AddObject(proveedor);
                    entity.SaveChanges();
                }
            }
        }

        public void insertRelacion(object element, List<long> unidCategoria, USUARIO u)
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
                        //proveedor.UNID_PROVEEDOR = UNID.getNewUNID();
                        //Sync
                        proveedor.IS_MODIFIED = true;
                        proveedor.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.PROVEEDORs.AddObject(proveedor);
                        entity.SaveChanges();
                        
                        UNID.Master(proveedor, u, -1, "Inserción");                        
                    }

                    if (unidCategoria.Count > 0)
                    {
                        foreach (var item in unidCategoria)
                        {
                            PROVEEDOR_CATEGORIA proveedorCategoria = new PROVEEDOR_CATEGORIA();
                            proveedorCategoria.UNID_CATEGORIA = item;
                            proveedorCategoria.UNID_PROVEEDOR = proveedor.UNID_PROVEEDOR;
                            proveedorCategoria.IS_ACTIVE = true;
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

        public void insertElementRelationSyn(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROVEEDOR_CATEGORIA relation = (PROVEEDOR_CATEGORIA)element;

                    //Sync
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();

                    entity.PROVEEDOR_CATEGORIA.AddObject(relation);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element, USUARIO u)
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

                    UNID.Master(proveedor, u, -1, "Emininación");
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

        /// <summary>
        /// Método que Deserializa JSon a List<PROVEEDOR>
        /// </summary>
        /// <returns>Regresa List<PROVEEDOR></returns>
        /// <returns>Si no regresa null</returns>
        public List<PROVEEDOR> GetDeserializeProveedor(string listPocos)
        {
            List<PROVEEDOR> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<PROVEEDOR>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetProveedor()
        {
            List<PROVEEDOR> reset = new List<PROVEEDOR>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROVEEDORs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new PROVEEDOR
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
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.PROVEEDORs.First(p => p.UNID_PROVEEDOR == item.UNID_PROVEEDOR);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
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
