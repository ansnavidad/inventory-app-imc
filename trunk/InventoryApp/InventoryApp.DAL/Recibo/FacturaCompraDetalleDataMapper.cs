using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL.Recibo
{
    public class FacturaCompraDetalleDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {

            if (element != null)
            {
                FACTURA_DETALLE poco = (FACTURA_DETALLE)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.FACTURA_DETALLE
                                 where poco.UNID_FACTURA_DETALE == cust.UNID_FACTURA_DETALE
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        var aux = query.First();

                        if (UNID.compareUNIDS(aux.LAST_MODIFIED_DATE, poco.LAST_MODIFIED_DATE))
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }

                    var modifiedCotizacion = entity.FACTURA_DETALLE.First(p => p.UNID_FACTURA_DETALE == poco.UNID_FACTURA_DETALE);
                    modifiedCotizacion.IS_ACTIVE = false;
                    entity.SaveChanges();
                }
            }
        }

        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from articulo in entity.FACTURA_DETALLE
                         where articulo.IS_ACTIVE == true
                         where articulo.IS_MODIFIED == false
                         select articulo.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonFacturaDetalle(long? LMD)
        {
            string res = null;
            List<FACTURA_DETALLE> listFacturaDetalle = new List<FACTURA_DETALLE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.FACTURA_DETALLE
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     listFacturaDetalle.Add(new FACTURA_DETALLE
                     {
                         UNID_FACTURA_DETALE = row.UNID_FACTURA_DETALE,
                         UNID_FACTURA = row.UNID_FACTURA,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         CANTIDAD = row.CANTIDAD,
                         PRECIO_UNITARIO = row.PRECIO_UNITARIO,
                         IMPUESTO_UNITARIO = row.IMPUESTO_UNITARIO,
                         NUMERO = row.NUMERO,
                         DESCRIPCION = row.DESCRIPCION,
                         UNID_UNIDAD = row.UNID_UNIDAD,
                         UNID_PEDIMENTO = row.UNID_PEDIMENTO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listFacturaDetalle.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listFacturaDetalle);
                }
                return res;
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
            if (element != null && (element as FACTURA_DETALLE) != null)
            {
                FACTURA_DETALLE fd=element as FACTURA_DETALLE;

                using (var entity = new TAE2Entities())
                {
                    //var resFd=entity.FACTURA_DETALLE.First(o=>o.UNID_FACTURA_DETALE==fd.UNID_FACTURA_DETALE);
                    //resFd.
                }
            }
        }

        public void udpateElements(List<FACTURA_DETALLE> listFd)
        {
            using (var entity = new TAE2Entities())
            {
                foreach (FACTURA_DETALLE fd in listFd)
                {
                    var res = entity.FACTURA_DETALLE.First(o => o.UNID_FACTURA_DETALE == fd.UNID_FACTURA_DETALE);
                    res.UNID_UNIDAD=fd.UNID_UNIDAD;
                    res.UNID_ARTICULO=fd.UNID_ARTICULO;
                    res.UNID_FACTURA=fd.UNID_FACTURA;
                    res.PRECIO_UNITARIO=fd.PRECIO_UNITARIO;
                    res.NUMERO=fd.NUMERO;
                    res.IMPUESTO_UNITARIO = fd.IMPUESTO_UNITARIO;
                    res.CANTIDAD = fd.CANTIDAD;
                    res.IS_ACTIVE = fd.IS_ACTIVE;

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
                    FACTURA_DETALLE facturaDetalle = (FACTURA_DETALLE)element;
                    entity.FACTURA_DETALLE.AddObject(facturaDetalle);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<FACTURA_DETALLE> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de FACTURA_DETALLE</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonFacturaDetalle()
        {
            string res = null;
            List<FACTURA_DETALLE> listFacturaDetalle = new List<FACTURA_DETALLE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.FACTURA_DETALLE
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listFacturaDetalle.Add(new FACTURA_DETALLE
                     {
                         UNID_FACTURA_DETALE=row.UNID_FACTURA_DETALE,
                         UNID_FACTURA=row.UNID_FACTURA,
                         UNID_ARTICULO=row.UNID_ARTICULO,
                         CANTIDAD=row.CANTIDAD,
                         PRECIO_UNITARIO=row.PRECIO_UNITARIO,
                         IMPUESTO_UNITARIO=row.IMPUESTO_UNITARIO,
                         NUMERO=row.NUMERO,
                         DESCRIPCION=row.DESCRIPCION,
                         UNID_UNIDAD=row.UNID_UNIDAD,
                         UNID_PEDIMENTO=row.UNID_PEDIMENTO,
                         IS_ACTIVE=row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listFacturaDetalle.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listFacturaDetalle);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<FACTURA_DETALLE>
        /// </summary>
        /// <returns>Regresa List<FACTURA_DETALLE></returns>
        /// <returns>Si no regresa null</returns>
        public List<FACTURA_DETALLE> GetDeserializeFacturaDetalle(string listPocos)
        {
            List<FACTURA_DETALLE> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<FACTURA_DETALLE>>(listPocos);
            }

            return res;
        }
    }
}
