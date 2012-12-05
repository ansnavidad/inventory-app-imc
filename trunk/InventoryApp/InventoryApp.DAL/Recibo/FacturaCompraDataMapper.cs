using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL.Recibo
{
    public class FacturaCompraDataMapper:IDataMapper
    {
        public List<FACTURA> GetFacturaList(RECIBO recibo)
        {
            List<FACTURA> listFactura = new List<FACTURA>();

            using (var entity = new TAE2Entities())
            {
                //entity.ContextOptions.ProxyCreationEnabled = false;
                RECIBO selRecibo = (from o in entity.RECIBOes
                                    where o.UNID_RECIBO == recibo.UNID_RECIBO
                                    select o).FirstOrDefault();

                //var res = selRecibo.RECIBO_MOVIMIENTO.cl
                selRecibo.RECIBO_MOVIMIENTO.ToList().ForEach(rm =>
                {
                    //Generar las facturas detalles
                    FixupCollection<FACTURA_DETALLE> facturasDetalle = new FixupCollection<FACTURA_DETALLE>();
                    rm.FACTURA.FACTURA_DETALLE.ToList().ForEach(fd =>
                    {
                        facturasDetalle.Add(new FACTURA_DETALLE()
                        {
                            UNID_FACTURA_DETALE = fd.UNID_FACTURA_DETALE,
                            UNID_FACTURA = fd.UNID_FACTURA,
                            ARTICULO = new ARTICULO()
                            {
                                UNID_ARTICULO=fd.ARTICULO.UNID_ARTICULO,
                                ARTICULO1=fd.ARTICULO.ARTICULO1,
                                CATEGORIA=fd.ARTICULO.CATEGORIA,
                                EQUIPO=fd.ARTICULO.EQUIPO,
                                MARCA=fd.ARTICULO.MARCA,
                                MODELO=fd.ARTICULO.MODELO
                            },
                            CANTIDAD=fd.CANTIDAD,
                            DESCRIPCION=fd.DESCRIPCION,
                            IMPUESTO_UNITARIO=fd.IMPUESTO_UNITARIO,
                            IS_ACTIVE=fd.IS_ACTIVE,
                            NUMERO=fd.NUMERO,
                            PEDIMENTO=fd.PEDIMENTO,
                            PRECIO_UNITARIO=fd.PRECIO_UNITARIO,
                            UNIDAD=fd.UNIDAD
                        });
                    });

                    listFactura.Add(new FACTURA() 
                    {
                        UNID_FACTURA=rm.FACTURA.UNID_FACTURA,
                        FACTURA_DETALLE=facturasDetalle,
                        FACTURA_NUMERO=rm.FACTURA.FACTURA_NUMERO,
                        FECHA_FACTURA=rm.FACTURA.FECHA_FACTURA,
                        IS_ACTIVE=rm.FACTURA.IS_ACTIVE,
                        MONEDA=rm.FACTURA.MONEDA,
                        PROVEEDOR=rm.FACTURA.PROVEEDOR,
                        IVA_POR=rm.FACTURA.IVA_POR,
                        NUMERO_PEDIMENTO=rm.FACTURA.NUMERO_PEDIMENTO
                    });
                });
            }

            return listFactura;
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
            if (element != null && (element as FACTURA) != null)
            {
                FACTURA fac=element as FACTURA;

                using (var entity = new TAE2Entities())
                {
                    var resFac = entity.FACTURAs.First(o => o.UNID_FACTURA == fac.UNID_FACTURA);
                    resFac.FACTURA_NUMERO = fac.FACTURA_NUMERO;
                    resFac.FECHA_FACTURA = fac.FECHA_FACTURA;
                    resFac.IS_ACTIVE = fac.IS_ACTIVE;
                    resFac.IVA_POR = fac.IVA_POR;
                    resFac.UNID_MONEDA = fac.UNID_MONEDA;
                    resFac.NUMERO_PEDIMENTO = fac.NUMERO_PEDIMENTO;
                    resFac.UNID_PROVEEDOR = fac.UNID_PROVEEDOR;
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
                    FACTURA factura = (FACTURA)element;
                    entity.FACTURAs.AddObject(factura);
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
