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
            throw new NotImplementedException();
        }

        public object getElement(object element)
        {
            throw new NotImplementedException();
        }

        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entitie = new TAE2Entities())
                {
                    DEPARTAMENTO Edepartamento = (DEPARTAMENTO)element;
                    DEPARTAMENTO departamento = new DEPARTAMENTO();
                    departamento.UNID_DEPARTAMENTO = departamento.UNID_DEPARTAMENTO;
                    departamento.DEPARTAMENTO_NAME = departamento.DEPARTAMENTO_NAME;
                    entitie.DEPARTAMENTOes.AddObject(departamento);
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
