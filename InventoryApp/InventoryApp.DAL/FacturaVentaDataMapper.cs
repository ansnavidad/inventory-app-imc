using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class FacturaVentaDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from factura in entity.FACTURA_VENTA
                         where factura.IS_ACTIVE == true
                         where factura.IS_MODIFIED == false
                         select factura.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                FACTURA_VENTA poco = (FACTURA_VENTA)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.FACTURA_VENTA
                                 where poco.UNID_FACTURA_VENTA == cust.UNID_FACTURA_VENTA
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        var aux = query.First();

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }

                    var modifiedCotizacion = entity.FACTURA_VENTA.First(p => p.UNID_FACTURA_VENTA == poco.UNID_FACTURA_VENTA);
                    modifiedCotizacion.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

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
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    FACTURA_VENTA factura = (FACTURA_VENTA)element;
                    var modifiedMoneda = entity.FACTURA_VENTA.First(p => p.UNID_FACTURA_VENTA == factura.UNID_FACTURA_VENTA);
                    modifiedMoneda.FOLIO = factura.FOLIO;
                    modifiedMoneda.IMPORTE_FACTURA = factura.IMPORTE_FACTURA;
                    modifiedMoneda.IVA_PESOS = factura.IVA_PESOS;
                    modifiedMoneda.POR_IVA = factura.POR_IVA;
                    modifiedMoneda.TIPO_CAMBIO = factura.TIPO_CAMBIO;
                    modifiedMoneda.TOTAL_DESC_FACTURA = factura.TOTAL_DESC_FACTURA;
                    modifiedMoneda.TOTAL_FACTURA = factura.TOTAL_FACTURA;
                    modifiedMoneda.UNID_MONEDA = factura.UNID_MONEDA;                    
                    //Sync
                    modifiedMoneda.IS_MODIFIED = true;
                    modifiedMoneda.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
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

        /// <summary>
        /// Método que Deserializa JSon a List<FACTURA_VENTA>
        /// </summary>
        /// <returns>Regresa List<FACTURA_VENTA></returns>
        /// <returns>Si no regresa null</returns>
        public List<FACTURA_VENTA> GetDeserializeFacturaVenta(string listPocos)
        {
            List<FACTURA_VENTA> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<FACTURA_VENTA>>(listPocos);
            }

            return res;
        }
    }
}
