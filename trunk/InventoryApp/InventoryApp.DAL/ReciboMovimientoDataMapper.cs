using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class ReciboMovimientoDataMapper:IDataMapper
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
            if (element != null && (element as RECIBO_MOVIMIENTO) != null)
            {
                using (var entity = new TAE2Entities())
                {
                    RECIBO_MOVIMIENTO item = (RECIBO_MOVIMIENTO)element;
                    //Sync
                    item.IS_MODIFIED = true;
                    item.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.RECIBO_MOVIMIENTO.AddObject(item);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<RECIBO_MOVIMIENTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de RECIBO_MOVIMIENTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonReciboMovimiento()
        {
            string res = null;
            List<RECIBO_MOVIMIENTO> listReciboMovimiento = new List<RECIBO_MOVIMIENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.RECIBO_MOVIMIENTO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listReciboMovimiento.Add(new RECIBO_MOVIMIENTO
                     {
                         UNID_RECIBO = row.UNID_RECIBO,
                         UNID_RECIBO_MOVIMIENTO=row.UNID_RECIBO_MOVIMIENTO,
                         UNID_MOVIMIENTO=row.UNID_MOVIMIENTO,
                         UNID_FACTURA=row.UNID_FACTURA,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listReciboMovimiento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listReciboMovimiento);
                }
                return res;
            }
        }
    }
}
