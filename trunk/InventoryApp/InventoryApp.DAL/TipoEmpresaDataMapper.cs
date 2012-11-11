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

            FixupCollection<TIPO_EMPRESA> tp = new FixupCollection<TIPO_EMPRESA>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query=(from cust in entity.TIPO_EMPRESA
                           where cust.IS_ACTIVE==true
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
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)element;
                    var modifiedItemStatus = entity.TIPO_EMPRESA.First(p => p.UNID_TIPO_EMPRESA == tipoEmpresa.UNID_TIPO_EMPRESA);
                    modifiedItemStatus.TIPO_EMPRESA_NAME = tipoEmpresa.TIPO_EMPRESA_NAME;
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
                    TIPO_EMPRESA tra = (TIPO_EMPRESA)element;
                    tra.UNID_TIPO_EMPRESA = UNID.getNewUNID();

                    entity.TIPO_EMPRESA.AddObject(tra);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)element;

                    var deleteTipoEmpresa = entity.TIPO_EMPRESA.First(p => p.UNID_TIPO_EMPRESA == tipoEmpresa.UNID_TIPO_EMPRESA);

                    deleteTipoEmpresa.IS_ACTIVE = false;

                    entity.SaveChanges();
                }
            }
        }
    }
}
