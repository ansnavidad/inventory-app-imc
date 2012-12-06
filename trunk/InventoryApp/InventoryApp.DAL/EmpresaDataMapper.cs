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
                using (var entity = new TAE2Entities())
                {
                    EMPRESA EEmp = (EMPRESA)element;

                    var query = from cust in entity.EMPRESAs
                                where cust.UNID_EMPRESA == EEmp.UNID_EMPRESA
                                select cust;

                    var Emp = query.First();

                    Emp.EMPRESA_NAME = EEmp.EMPRESA_NAME;

                    Emp.DIRECCION = EEmp.DIRECCION;

                    Emp.RAZON_SOCIAL = EEmp.RAZON_SOCIAL;

                    Emp.RFC = EEmp.RFC;
                    //Sync
                    Emp.IS_MODIFIED = true;
                    Emp.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
            if (element !=null)
            {
                using (var entity = new TAE2Entities())
                {
                    EMPRESA empresa = (EMPRESA)element;

                    var validacion = (from cust in entity.EMPRESAs
                                      where cust.EMPRESA_NAME == empresa.EMPRESA_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        empresa.UNID_EMPRESA = UNID.getNewUNID();
                        //Sync
                        empresa.IS_MODIFIED = true;
                        empresa.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.EMPRESAs.AddObject(empresa);
                        entity.SaveChanges();
                    }
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
                    //Sync
                    deleteEmpresa.IS_MODIFIED = true;
                    deleteEmpresa.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }
    }
}
