﻿using System;
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
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                res = (from monedas in entitie.DEPARTAMENTOes select monedas).ToList();
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                DEPARTAMENTO departamento = (DEPARTAMENTO)element;
                var query = from cust in entitie.DEPARTAMENTOes
                            where cust.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO
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
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entitie = new TAE2Entities())
                {
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;
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