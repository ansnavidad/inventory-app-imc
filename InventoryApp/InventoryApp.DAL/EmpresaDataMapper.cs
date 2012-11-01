using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class EmpresaDataMapper : IDataMapper
    {
        public object getElements()
        {
            object res = null;
            using (var oAWEntities = new TAE2Entities())
            {
                var query = from cust in oAWEntities.EMPRESAs
                                        select cust;
                if (query != null)
                {
                    res = query;
                }
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;

            using (var oAWEntities = new TAE2Entities())
            {
                 EMPRESA EEmp = (EMPRESA)element;

                var query = from cust in oAWEntities.EMPRESAs
                            where cust.UNID_EMPRESA == EEmp.UNID_EMPRESA
                            select cust;
                if (query != null)
                {
                    res = query;
                }
                return res;
            }
        }

        public void udpateElement(object element)
        {
            using (var oAWEntities = new TAE2Entities())
            {
                EMPRESA EEmp = (EMPRESA)element;
                
                var query = from cust in oAWEntities.EMPRESAs
                                        where cust.UNID_EMPRESA==EEmp.UNID_EMPRESA
                                        select cust;

                var Emp = query.First();

                Emp.EMPRESA_NAME = EEmp.EMPRESA_NAME;

                Emp.DIRECCION = EEmp.DIRECCION;

                Emp.RAZON_SOCIAL = EEmp.RAZON_SOCIAL;

                Emp.RFC = EEmp.RFC;

                Emp.SOLICITANTEs = EEmp.SOLICITANTEs;

                Emp.POMs = EEmp.POMs;

                oAWEntities.EMPRESAs.AddObject(Emp);

                oAWEntities.SaveChanges();
            }
        }

        public void insertElement(object element)
        {
            if (element !=null)
            {
                using (var entitie = new TAE2Entities())
                {
                    EMPRESA empresa = (EMPRESA)element;
                    entitie.EMPRESAs.AddObject(empresa);
                    entitie.SaveChanges();
                }            
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
