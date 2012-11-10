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
               var query= (from cust in oAWEntities.EMPRESAs
                           where cust.IS_ACTIVE==true
                           select cust).ToList();
               if (query.Count>0)
               {
                    res = query;
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

                   var query=(from cust in entitie.EMPRESAs
                                where cust.UNID_EMPRESA == empresa.UNID_EMPRESA
                                 select cust).ToList();
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
                    EMPRESA empresa = (EMPRESA)element;

                    empresa.UNID_EMPRESA = UNID.getNewUNID();

                    entitie.EMPRESAs.AddObject(empresa);

                    entitie.SaveChanges();
                }            
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    EMPRESA empresa = (EMPRESA)element;
                    var deleteEmpresa = entity.EMPRESAs.First(p => p.UNID_EMPRESA == empresa.UNID_EMPRESA);
                    deleteEmpresa.IS_ACTIVE = false;
                    entity.SaveChanges();
                }
            }
        }
    }
}
