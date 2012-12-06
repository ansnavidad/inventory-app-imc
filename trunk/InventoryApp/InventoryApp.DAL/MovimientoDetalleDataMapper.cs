using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class MovimientoDetalleDataMapper : IDataMapper
    {
        //
        /// <summary>
        /// Obtiene todos los elementos en la tabla transporte
        /// </summary>
        /// <returns></returns>
        public object getElements()
        {
            object res = null;
            using (var entity = new TAE2Entities())
            {
                res = (from movimiento_detalle in entity.MOVIMIENTO_DETALLE
                       select movimiento_detalle).ToList();

                foreach (MOVIMIENTO_DETALLE detalle in ((List<MOVIMIENTO_DETALLE>)res))
                {
                    detalle.ITEM = detalle.ITEM;
                    detalle.MOVIMENTO = detalle.MOVIMENTO;
                }


                return res;
            }
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
                using (var entity = new TAE2Entities())
                {
                    MOVIMIENTO_DETALLE mov = (MOVIMIENTO_DETALLE)element;
                    //Sync
                    mov.IS_MODIFIED = true;
                    mov.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MOVIMIENTO_DETALLE.AddObject(mov);
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
