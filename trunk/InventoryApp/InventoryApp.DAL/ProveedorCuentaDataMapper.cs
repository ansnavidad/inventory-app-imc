using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class ProveedorCuentaDataMapper : IDataMapper
    {
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
                    modifiedProveedor.UNID_PROVEEDOR = proveedorCuenta.UNID_PROVEEDOR;
                    modifiedProveedor.UNID_BANCO = proveedorCuenta.UNID_BANCO;
                    modifiedProveedor.PROVEEDOR = proveedorCuenta.PROVEEDOR;
                    modifiedProveedor.NUMERO_CUENTA = proveedorCuenta.NUMERO_CUENTA;
                    modifiedProveedor.CLABE = proveedorCuenta.CLABE;
                    modifiedProveedor.BENEFICIARIO = proveedorCuenta.BENEFICIARIO;
                    modifiedProveedor.BANCO = proveedorCuenta.BANCO;

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

                    proveedorCuenta.UNID_PROVEEDOR_CUENTA = UNID.getNewUNID();
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

                    entity.SaveChanges();
                }
            }
        }
    }
}
