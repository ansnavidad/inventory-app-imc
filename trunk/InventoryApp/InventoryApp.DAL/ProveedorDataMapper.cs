using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class ProveedorDataMapper : IDataMapper
    {
        public object getElements()
        {
            object o = null;            
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.PROVEEDORs
                 select p).ToList();


                foreach (PROVEEDOR trans in ((List<PROVEEDOR>)res))
                {
                    trans.PAI = trans.PAI;
                    trans.CIUDAD = trans.CIUDAD;
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
                    PROVEEDOR proveedor = (PROVEEDOR)element;

                    proveedor.UNID_PROVEEDOR = UNID.getNewUNID();

                    entity.PROVEEDORs.AddObject(proveedor);
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
