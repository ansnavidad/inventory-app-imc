using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class FacturaDetalleDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from facturaDetalle in entity.FACTURA_DETALLE
                         where facturaDetalle.IS_ACTIVE == true
                         where facturaDetalle.IS_MODIFIED == false
                         select facturaDetalle.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }


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
                    FACTURA_DETALLE factura = (FACTURA_DETALLE)element;
                    var modifiedFactura = entity.FACTURA_DETALLE.First(p => p.UNID_FACTURA_DETALE == factura.UNID_FACTURA_DETALE);
                    modifiedFactura.CANTIDAD = factura.CANTIDAD;
                    modifiedFactura.DESCRIPCION = factura.DESCRIPCION;
                    modifiedFactura.IS_ACTIVE = factura.IS_ACTIVE;
                    modifiedFactura.IMPUESTO_UNITARIO = factura.IMPUESTO_UNITARIO;
                    modifiedFactura.NUMERO = factura.NUMERO;
                    modifiedFactura.PRECIO_UNITARIO = factura.PRECIO_UNITARIO;
                    modifiedFactura.UNID_ARTICULO = factura.UNID_ARTICULO;
                    modifiedFactura.UNID_FACTURA = factura.UNID_FACTURA;
                    modifiedFactura.UNID_PEDIMENTO = factura.UNID_PEDIMENTO;
                    modifiedFactura.UNID_UNIDAD = factura.UNID_UNIDAD;
                    //Sync
                    modifiedFactura.IS_MODIFIED = true;
                    modifiedFactura.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    FACTURA_DETALLE factura = (FACTURA_DETALLE)element;

                    var validacion = (from cust in entity.FACTURA_DETALLE
                                      where cust.UNID_FACTURA_DETALE == factura.UNID_FACTURA_DETALE
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        factura.UNID_FACTURA_DETALE = UNID.getNewUNID();
                        //Sync
                        factura.IS_MODIFIED = true;
                        factura.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.FACTURA_DETALLE.AddObject(factura);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
