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
                       where transporte.IS_ACTIVE==true
                       select transporte).ToList();

                foreach (TRANSPORTE trans in ((List<TRANSPORTE>)res))
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
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TRANSPORTE transporte = (TRANSPORTE)element;
                    var modifiedItemStatus = entity.TRANSPORTEs.First(p => p.UNID_TRANSPORTE == transporte.UNID_TRANSPORTE);
                    modifiedItemStatus.TRANSPORTE_NAME = transporte.TRANSPORTE_NAME;
                    modifiedItemStatus.UNID_TIPO_EMPRESA = transporte.UNID_TIPO_EMPRESA;
                    entity.SaveChanges();
                }
            }


        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var oAWEntities = new TAE2Entities())
                {
                    TRANSPORTE tra = (TRANSPORTE)element;
                    tra.UNID_TRANSPORTE = UNID.getNewUNID();

                    oAWEntities.TRANSPORTEs.AddObject(tra);
                    oAWEntities.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TRANSPORTE transporte = (TRANSPORTE)element;

                    var deleteTransporte = entity.TRANSPORTEs.First(p => p.UNID_TRANSPORTE == transporte.UNID_TRANSPORTE);

                    deleteTransporte.IS_ACTIVE = false;

                    entity.SaveChanges();
                }
            }
        }
    }
}
