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
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.PROVEEDOR_CUENTA
                 select p).ToList();


                foreach (PROVEEDOR_CUENTA trans in ((List<PROVEEDOR_CUENTA>)res))
                {
                    trans.BANCO = trans.BANCO;
                    trans.PROVEEDOR = trans.PROVEEDOR;
                }

                return res;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                PROVEEDOR_CUENTA Eprov = (PROVEEDOR_CUENTA)element;

                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.PROVEEDOR_CUENTA
                               where p.UNID_PROVEEDOR_CUENTA == Eprov.UNID_PROVEEDOR_CUENTA
                               select p).ToList();

                    foreach (PROVEEDOR_CUENTA trans in ((List<PROVEEDOR_CUENTA>)res))
                    {
                        trans.BANCO = trans.BANCO;
                        trans.PROVEEDOR = trans.PROVEEDOR;
                    }

                    o = (object)res;                    
                }
            }
            return o;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROVEEDOR_CUENTA proveedor = (PROVEEDOR_CUENTA)element;

                    var modifiedProveedor = entity.PROVEEDOR_CUENTA.First(p => p.UNID_PROVEEDOR_CUENTA == proveedor.UNID_PROVEEDOR_CUENTA);
                    modifiedProveedor.UNID_PROVEEDOR = proveedor.UNID_PROVEEDOR;
                    modifiedProveedor.UNID_BANCO = proveedor.UNID_BANCO;
                    modifiedProveedor.PROVEEDOR = proveedor.PROVEEDOR;
                    modifiedProveedor.NUMERO_CUENTA = proveedor.NUMERO_CUENTA;
                    modifiedProveedor.CLABE = proveedor.CLABE;
                    modifiedProveedor.BENEFICIARIO = proveedor.BENEFICIARIO;
                    modifiedProveedor.BANCO = proveedor.BANCO;

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
                    PROVEEDOR_CUENTA proveedor = (PROVEEDOR_CUENTA)element;

                    proveedor.UNID_PROVEEDOR_CUENTA = UNID.getNewUNID();

                    entity.PROVEEDOR_CUENTA.AddObject(proveedor);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
