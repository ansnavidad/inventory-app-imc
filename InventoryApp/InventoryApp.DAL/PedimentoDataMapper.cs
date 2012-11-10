using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    class PedimentoDataMapper
    {
        public object getElements()
        {
            object res = null;
            using (var oAWEntities = new TAE2Entities())
            {
                var query = (from p in oAWEntities.PEDIMENTOes
                            where p.IS_ACTIVE ==true
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
            if (element != null)
            {
                PROVEEDOR Eprov = (PROVEEDOR)element;

                using (var oAWEntities = new TAE2Entities())
                {
                    var query = (from p in oAWEntities.PEDIMENTOes
                                where p.UNID_PEDIMENTO == Eprov.UNID_PROVEEDOR
                                select p).ToList();

                    if (query.Count > 0)
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

                PROVEEDOR Eprov = (PROVEEDOR)element;

                using (var oAWEntities = new TAE2Entities())
                {
                    var query = from p in oAWEntities.PROVEEDORs
                                where p.UNID_PROVEEDOR == Eprov.UNID_PROVEEDOR
                                select p;

                    var prov = query.First();

                    prov.PROVEEDOR_NAME = Eprov.PROVEEDOR_NAME;
                    prov.CONTACTO = Eprov.CONTACTO;
                    prov.TEL1 = Eprov.TEL1;
                    prov.TEL2 = Eprov.TEL2;
                    prov.MAIL = Eprov.MAIL;
                    prov.CALLE = Eprov.CALLE;
                    prov.UNID_PAIS = Eprov.UNID_PAIS;
                    prov.UNID_CIUDAD = Eprov.UNID_CIUDAD;
                    prov.CODIGO_POSTAL = Eprov.CODIGO_POSTAL;
                    prov.RFC = Eprov.RFC;

                    oAWEntities.SaveChanges();
                }
            }
        }

        public void insertElement(object element)
        {
            if (element != null)
            {

                using (var oAWEntities = new TAE2Entities())
                {

                    PROVEEDOR Eprov = (PROVEEDOR)element;
                    PROVEEDOR prov = new PROVEEDOR();

                    prov.UNID_PROVEEDOR = UNID.getNewUNID();
                    prov.PROVEEDOR_NAME = Eprov.PROVEEDOR_NAME;
                    prov.CONTACTO = Eprov.CONTACTO;
                    prov.TEL1 = Eprov.TEL1;
                    prov.TEL2 = Eprov.TEL2;
                    prov.MAIL = Eprov.MAIL;
                    prov.CALLE = Eprov.CALLE;
                    prov.UNID_PAIS = Eprov.UNID_PAIS;
                    prov.UNID_CIUDAD = Eprov.UNID_CIUDAD;
                    prov.CODIGO_POSTAL = Eprov.CODIGO_POSTAL;
                    prov.RFC = Eprov.RFC;

                    oAWEntities.PROVEEDORs.AddObject(prov);

                    oAWEntities.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var oAWEntities = new TAE2Entities())
                {

                    PROVEEDOR Eprov = (PROVEEDOR)element;

                    var query = from p in oAWEntities.PROVEEDORs
                                where p.UNID_PROVEEDOR == Eprov.UNID_PROVEEDOR
                                select p;

                    var provs = query.First();

                    if (provs != null)
                    {

                        PROVEEDOR aux = (PROVEEDOR)provs;

                        oAWEntities.PROVEEDORs.DeleteObject(aux);
                    }
                }
            }
        }
    }
}
