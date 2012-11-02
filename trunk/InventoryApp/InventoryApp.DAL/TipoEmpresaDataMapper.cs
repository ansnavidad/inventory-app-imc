﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class TipoEmpresaDataMapper : IDataMapper
    {

        public object getElements()
        {
            object res = null;
            using (var oAWEntities = new TAE2Entities())
            {
                var query = from cust in oAWEntities.TIPO_EMPRESA
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
                TIPO_EMPRESA TEmp = (TIPO_EMPRESA)element;

                var query = from cust in oAWEntities.TIPO_EMPRESA
                            where cust.UNID_TIPO_EMPRESA == TEmp.UNID_TIPO_EMPRESA  
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
                TIPO_EMPRESA TEEmp = (TIPO_EMPRESA)element;

                var query = from cust in oAWEntities.TIPO_EMPRESA
                            where cust.UNID_TIPO_EMPRESA == TEEmp.UNID_TIPO_EMPRESA
                            select cust;

                var tEmp = query.First();

                tEmp.TIPO_EMPRESA_NAME = TEEmp.TIPO_EMPRESA_NAME;

                oAWEntities.TIPO_EMPRESA.AddObject(tEmp);

                oAWEntities.SaveChanges();

            }
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
