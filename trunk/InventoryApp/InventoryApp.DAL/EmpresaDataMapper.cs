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
            FixupCollection<EMPRESA> tp = new FixupCollection<EMPRESA>();

            object res = null;

            using (var oAWEntities = new TAE2Entities())
            {
                (from cust in oAWEntities.EMPRESAs
                             select cust).ToList().ForEach(d => { tp.Add(d); });
                if (tp != null)
                {
                    res = tp;
                }
                return res;
            }
        }

        public object getElement(object element)
        {
            FixupCollection<EMPRESA> tp = new FixupCollection<EMPRESA>();

            object res = null;

            if (element != null)
            {
                using (var entitie = new TAE2Entities())
                {
                    EMPRESA empresa = (EMPRESA)element;

                    (from cust in entitie.EMPRESAs
                                where cust.UNID_EMPRESA == empresa.UNID_EMPRESA
                                 select cust).ToList().ForEach(d => { tp.Add(d); });
                    if (tp != null)
                    {
                        res = tp;
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
                using (var oAWEntities = new TAE2Entities())
                {
                    EMPRESA EEmp = (EMPRESA)element;

                    var query = from cust in oAWEntities.EMPRESAs
                                where cust.UNID_EMPRESA == EEmp.UNID_EMPRESA
                                select cust;

                    var Emp = query.First();

                    Emp.EMPRESA_NAME = EEmp.EMPRESA_NAME;

                    Emp.DIRECCION = EEmp.DIRECCION;

                    Emp.RAZON_SOCIAL = EEmp.RAZON_SOCIAL;

                    Emp.RFC = EEmp.RFC;

                    oAWEntities.SaveChanges();

                }
            }
        }

        public void insertElement(object element)
        {
            if (element !=null)
            {
                using (var entitie = new TAE2Entities())
                {
                    EMPRESA EEmp = (EMPRESA)element;

                    EMPRESA empresa = new EMPRESA();

                    empresa.UNID_EMPRESA = UNID.getNewUNID();

                    empresa.EMPRESA_NAME = EEmp.EMPRESA_NAME;

                    empresa.DIRECCION = EEmp.DIRECCION;

                    empresa.RAZON_SOCIAL = EEmp.RAZON_SOCIAL;

                    empresa.RFC = EEmp.RFC;

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
