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
                (from cust in entity.DEPARTAMENTOes
                 select cust).ToList().ForEach(d => { tp.Add(d); });

                if (tp.Count > 0)
                {
                    res = tp;
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
                     (from cust in entitie.DEPARTAMENTOes
                                where cust.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO
                      select cust).ToList().ForEach(d => { tp.Add(d); });
                     if (tp.Count>0)
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
                using (var entity = new TAE2Entities())
                {
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;
                    var modifiedDepartamento = entity.DEPARTAMENTOes.First(p => p.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO);
                    modifiedDepartamento.DEPARTAMENTO_NAME = departamento.DEPARTAMENTO_NAME;
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
                    departamento.UNID_DEPARTAMENTO = UNID.getNewUNID();
                    entity.DEPARTAMENTOes.AddObject(departamento);
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
