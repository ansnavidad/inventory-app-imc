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
    public class ProveedorCuentaDataMapper : IDataMapper
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
                resul = (from prov in entity.PROVEEDOR_CUENTA
                         where prov.IS_ACTIVE == true
                         where prov.IS_MODIFIED == false
                         select prov.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonProveedorCuenta(long? LastModifiedDate)
        {
            string res = null;
            List<PROVEEDOR_CUENTA> listProveedorCuenta = new List<PROVEEDOR_CUENTA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROVEEDOR_CUENTA
                 where p.LAST_MODIFIED_DATE > LastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listProveedorCuenta.Add(new PROVEEDOR_CUENTA
                     {
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         UNID_PROVEEDOR_CUENTA = row.UNID_PROVEEDOR_CUENTA,
                         UNID_BANCO = row.UNID_BANCO,
                         NUMERO_CUENTA = row.NUMERO_CUENTA,
                         CLABE = row.CLABE,
                         BENEFICIARIO = row.BENEFICIARIO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listProveedorCuenta.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listProveedorCuenta);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                PROVEEDOR_CUENTA poco = (PROVEEDOR_CUENTA)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.PROVEEDOR_CUENTA
                                 where poco.UNID_PROVEEDOR_CUENTA == cust.UNID_PROVEEDOR_CUENTA
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

                    var modifiedMenu = entity.PROVEEDOR_CUENTA.First(p => p.UNID_PROVEEDOR_CUENTA == poco.UNID_PROVEEDOR_CUENTA);
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
                var query = (from p in Entity.PROVEEDOR_CUENTA 
                             where p.IS_ACTIVE == true
                             select p).ToList();
                foreach (PROVEEDOR_CUENTA sol in ((List<PROVEEDOR_CUENTA>)query))
                {
                    sol.BANCO = sol.BANCO;
                    sol.PROVEEDOR = sol.PROVEEDOR;
                }

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
            if (element != null)
            {
                PROVEEDOR_CUENTA Eprov = (PROVEEDOR_CUENTA)element;

                using (var Entity = new TAE2Entities())
                {
                    var query = (from p in Entity.PROVEEDOR_CUENTA
                               where p.UNID_PROVEEDOR_CUENTA == Eprov.UNID_PROVEEDOR_CUENTA
                               select p).ToList();

                    if (query.Count>0)
                    {
                        res = query;
                    }

                    return res;
                }
            }
            return res;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROVEEDOR_CUENTA proveedorCuenta = (PROVEEDOR_CUENTA)element;

                    var modifiedProveedor = entity.PROVEEDOR_CUENTA.First(p => p.UNID_PROVEEDOR_CUENTA == proveedorCuenta.UNID_PROVEEDOR_CUENTA);
                    
                    modifiedProveedor.UNID_BANCO = proveedorCuenta.UNID_BANCO;
                    modifiedProveedor.NUMERO_CUENTA = proveedorCuenta.NUMERO_CUENTA;
                    modifiedProveedor.CLABE = proveedorCuenta.CLABE;
                    modifiedProveedor.BENEFICIARIO = proveedorCuenta.BENEFICIARIO;
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

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROVEEDOR_CUENTA proveedorCuenta = (PROVEEDOR_CUENTA)element;

                    var modifiedProveedor = entity.PROVEEDOR_CUENTA.First(p => p.UNID_PROVEEDOR_CUENTA == proveedorCuenta.UNID_PROVEEDOR_CUENTA);

                    modifiedProveedor.UNID_BANCO = proveedorCuenta.UNID_BANCO;
                    modifiedProveedor.NUMERO_CUENTA = proveedorCuenta.NUMERO_CUENTA;
                    modifiedProveedor.CLABE = proveedorCuenta.CLABE;
                    modifiedProveedor.BENEFICIARIO = proveedorCuenta.BENEFICIARIO;
                    modifiedProveedor.IS_ACTIVE = proveedorCuenta.IS_ACTIVE;
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

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROVEEDOR_CUENTA proveedorCuenta = (PROVEEDOR_CUENTA)element;

                    var validacion = (from cust in entity.PROVEEDOR_CUENTA
                                      where cust.NUMERO_CUENTA == proveedorCuenta.NUMERO_CUENTA
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        proveedorCuenta.UNID_PROVEEDOR_CUENTA = UNID.getNewUNID();
                        //Sync
                        proveedorCuenta.IS_MODIFIED = true;
                        proveedorCuenta.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.PROVEEDOR_CUENTA.AddObject(proveedorCuenta);
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
                    PROVEEDOR_CUENTA proveedorCuenta = (PROVEEDOR_CUENTA)element;

                    //Sync
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.PROVEEDOR_CUENTA.AddObject(proveedorCuenta);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROVEEDOR_CUENTA proveedorCuenta = (PROVEEDOR_CUENTA)element;

                    var deleteProveedorCuenta = entity.PROVEEDOR_CUENTA.First(p => p.UNID_PROVEEDOR_CUENTA == proveedorCuenta.UNID_PROVEEDOR_CUENTA);

                    deleteProveedorCuenta.IS_ACTIVE = false;
                    //Sync
                    deleteProveedorCuenta.IS_MODIFIED = true;
                    deleteProveedorCuenta.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<PROVEEDOR_CUENTA> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PROVEEDOR_CUENTA</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonProveedorCuenta()
        {
            string res = null;
            List<PROVEEDOR_CUENTA> listProveedorCuenta = new List<PROVEEDOR_CUENTA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROVEEDOR_CUENTA
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listProveedorCuenta.Add(new PROVEEDOR_CUENTA
                     {
                         UNID_PROVEEDOR= row.UNID_PROVEEDOR,
                         UNID_PROVEEDOR_CUENTA=row.UNID_PROVEEDOR_CUENTA,
                         UNID_BANCO=row.UNID_BANCO,
                         NUMERO_CUENTA=row.NUMERO_CUENTA,
                         CLABE=row.CLABE,
                         BENEFICIARIO=row.BENEFICIARIO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listProveedorCuenta.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listProveedorCuenta);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<PROVEEDOR_CUENTA>
        /// </summary>
        /// <returns>Regresa List<PROVEEDOR_CUENTA></returns>
        /// <returns>Si no regresa null</returns>
        public List<PROVEEDOR_CUENTA> GetdeserializeProveedorCuenta(string listPocos)
        {
            List<PROVEEDOR_CUENTA> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<PROVEEDOR_CUENTA>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetProveedorCuenta()
        {
            List<PROVEEDOR_CUENTA> reset = new List<PROVEEDOR_CUENTA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROVEEDOR_CUENTA
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new PROVEEDOR_CUENTA
                     {
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         UNID_PROVEEDOR_CUENTA = row.UNID_PROVEEDOR_CUENTA,
                         UNID_BANCO = row.UNID_BANCO,
                         NUMERO_CUENTA = row.NUMERO_CUENTA,
                         CLABE = row.CLABE,
                         BENEFICIARIO = row.BENEFICIARIO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.PROVEEDOR_CUENTA.First(p => p.UNID_PROVEEDOR_CUENTA == item.UNID_PROVEEDOR_CUENTA);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
