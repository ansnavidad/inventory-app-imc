using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class FacturaVentaDataMapper : IDataMapper
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
                using (var entity = new TAE2Entities())
                {
                    FACTURA_VENTA tra = (FACTURA_VENTA)element;

                    //Sync
                    tra.IS_MODIFIED = true;
                    tra.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.FACTURA_VENTA.AddObject(tra);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método que serializa una List<FACTURA_VENTA> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de FACTURA_VENTA</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonFacturaVenta()
        {
            string res = null;
            List<FACTURA_VENTA> listFacturaVenta = new List<FACTURA_VENTA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.FACTURA_VENTA
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listFacturaVenta.Add(new FACTURA_VENTA
                     {
                         UNID_FACTURA_VENTA=row.UNID_FACTURA_VENTA,
                         FOLIO = row.FOLIO,
                         TOTAL_DESC_FACTURA=row.TOTAL_DESC_FACTURA,
                         TOTAL_FACTURA=row.TOTAL_FACTURA,
                         POR_IVA=row.POR_IVA,
                         IVA_PESOS=row.IVA_PESOS,
                         IMPORTE_FACTURA=row.IMPORTE_FACTURA,
                         UNID_MONEDA=row.UNID_MONEDA,
                         TIPO_CAMBIO=row.TIPO_CAMBIO,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listFacturaVenta.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listFacturaVenta);
                }
                return res;
            }
        }
    }
}
