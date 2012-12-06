using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class DepartamentoDataMapper : IDataMapper
    {
        public object getElements()
        {
            FixupCollection<DEPARTAMENTO> tp = new FixupCollection<DEPARTAMENTO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.DEPARTAMENTOes
                           where cust.IS_ACTIVE ==true
                           select cust).ToList();

               if (query.Count > 0)
               {
                    res = query;
               }

                return res;
            }
        }

        public object getElement(object element)
        {
            FixupCollection<DEPARTAMENTO> tp = new FixupCollection<DEPARTAMENTO>();

            object res = null;
            if (element != null)
            { 
                using (var entitie = new TAE2Entities())
                {
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;

                    var query= (from cust in entitie.DEPARTAMENTOes
                                where cust.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO
                                select cust).ToList();
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
                using (var entity = new TAE2Entities())
                {
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;
                    var modifiedDepartamento = entity.DEPARTAMENTOes.First(p => p.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO);
                    modifiedDepartamento.DEPARTAMENTO_NAME = departamento.DEPARTAMENTO_NAME;
                    //Sync
                    modifiedDepartamento.IS_MODIFIED = true;
                    modifiedDepartamento.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;

                    var validacion = (from cust in entity.DEPARTAMENTOes
                                      where cust.DEPARTAMENTO_NAME == departamento.DEPARTAMENTO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        departamento.UNID_DEPARTAMENTO = UNID.getNewUNID();
                        //Sync
                        departamento.IS_MODIFIED = true;
                        departamento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.DEPARTAMENTOes.AddObject(departamento);
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
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;
                    var deleteDepartamento = entity.DEPARTAMENTOes.First(p => p.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO);
                    deleteDepartamento.IS_ACTIVE = false;
                    //Sync
                    deleteDepartamento.IS_MODIFIED = true;
                    deleteDepartamento.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
