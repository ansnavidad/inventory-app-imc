using System;
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
            using (var entity = new TAE2Entities())
            {
                res = (from tipo in entity.TIPO_EMPRESA
                                        select tipo).ToList();
               
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;

            using (var entity = new TAE2Entities())
            {
                TIPO_EMPRESA TEmp = (TIPO_EMPRESA)element;

                res = (from tipo in entity.TIPO_EMPRESA
                            where tipo.UNID_TIPO_EMPRESA == TEmp.UNID_TIPO_EMPRESA  
                            select tipo).ToList();
                
                return res;
            }
        }

        public void udpateElement(object element)
        {
            using (var entity = new TAE2Entities())
            {
                TIPO_EMPRESA TEmp = (TIPO_EMPRESA)element;

                var query = from tipo in entity.TIPO_EMPRESA
                            where tipo.UNID_TIPO_EMPRESA == TEmp.UNID_TIPO_EMPRESA
                            select tipo;

                var tra = query.First();

                tra.TIPO_EMPRESA_NAME = TEmp.TIPO_EMPRESA_NAME;

                entity.TIPO_EMPRESA.AddObject(tra);

                entity.SaveChanges();

            }
            throw new NotImplementedException();
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
