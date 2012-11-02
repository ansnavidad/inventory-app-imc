using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class TransporteDataMapper : IDataMapper
    {

        public object getElements()
        {
            object res = null;
            using (var oAWEntities = new TAE2Entities())
            {
                var query = from cust in oAWEntities.TRANSPORTEs
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
                TRANSPORTE Etra = (TRANSPORTE)element;

                var query = from cust in oAWEntities.TRANSPORTEs
                            where cust.UNID_TRANSPORTE == Etra.UNID_TRANSPORTE
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
                TRANSPORTE Etra = (TRANSPORTE)element;

                var query = from cust in oAWEntities.TRANSPORTEs
                            where cust.UNID_TRANSPORTE == Etra.UNID_TRANSPORTE && cust.UNID_TIPO_EMPRESA == Etra.UNID_TIPO_EMPRESA
                            select cust;

                var tra = query.First();

                tra.TRANSPORTE_NAME = Etra.TRANSPORTE_NAME;

                oAWEntities.TRANSPORTEs.AddObject(tra);

                oAWEntities.SaveChanges();

            }

        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var oAWEntities = new TAE2Entities())
                {
                    TRANSPORTE Etra = (TRANSPORTE)element;
                    TRANSPORTE tra = new TRANSPORTE();

                    tra.UNID_TRANSPORTE = Etra.UNID_TRANSPORTE;

                    tra.TRANSPORTE_NAME = Etra.TRANSPORTE_NAME;

                    tra.UNID_TIPO_EMPRESA = Etra.UNID_TIPO_EMPRESA;
                   
                   oAWEntities.TRANSPORTEs.AddObject(tra);

                   oAWEntities.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
