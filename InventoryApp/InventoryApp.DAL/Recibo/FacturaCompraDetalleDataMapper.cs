using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL.Recibo
{
    public class FacturaCompraDetalleDataMapper : IDataMapper
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
    }
}
