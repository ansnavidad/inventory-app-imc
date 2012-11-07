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
