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
        /// <summary>
        /// Obtiene todos los elementos en la tabla transporte
        /// </summary>
        /// <returns></returns>
        public object getElements()
        {
            object res = null;
            using (var entity = new TAE2Entities())
            {
                res = (from transporte in entity.TRANSPORTEs
                                        select transporte).ToList();

              foreach (TRANSPORTE trans in  ((List<TRANSPORTE>)res))
              {
                  trans.TIPO_EMPRESA = trans.TIPO_EMPRESA;
              }
                

                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;

            using (var entity = new TAE2Entities())
            {
                TRANSPORTE Etra = (TRANSPORTE)element;

                res = (from cust in entity.TRANSPORTEs
                            where cust.UNID_TRANSPORTE == Etra.UNID_TRANSPORTE
                            select cust).ToList<TRANSPORTE>();
                
                foreach (TRANSPORTE trans in ((List<TRANSPORTE>)res))
                {
                    trans.TIPO_EMPRESA = trans.TIPO_EMPRESA;
                }

                return res;
            }
        }

        public void udpateElement(object element)
        {
            using (var entity = new TAE2Entities())
            {
                TRANSPORTE Etra = (TRANSPORTE)element;

                var query = from transporte in entity.TRANSPORTEs
                            where transporte.UNID_TRANSPORTE == Etra.UNID_TRANSPORTE && transporte.UNID_TIPO_EMPRESA == Etra.UNID_TIPO_EMPRESA
                            select transporte;

                var tra = query.First();
                tra.TRANSPORTE_NAME = Etra.TRANSPORTE_NAME;
                entity.SaveChanges();

            }

        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var oAWEntities = new TAE2Entities())
                {
                    TRANSPORTE tra = (TRANSPORTE)element;
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
