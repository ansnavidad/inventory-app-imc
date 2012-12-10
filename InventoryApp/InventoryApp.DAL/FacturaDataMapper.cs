using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class FacturaDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {

            if (element != null)
            {
                FACTURA poco = (FACTURA)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.FACTURAs
                                 where poco.UNID_FACTURA == cust.UNID_FACTURA
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }
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
                    FACTURA factura = (FACTURA)element;
                    var modifiedFactura = entity.FACTURAs.First(p => p.UNID_FACTURA == factura.UNID_FACTURA);
                    modifiedFactura.FACTURA_NUMERO = factura.FACTURA_NUMERO;
                    modifiedFactura.FECHA_FACTURA = factura.FECHA_FACTURA;
                    modifiedFactura.IS_ACTIVE = factura.IS_ACTIVE;
                    modifiedFactura.IVA_POR = factura.IVA_POR;
                    modifiedFactura.NUMERO_PEDIMENTO = factura.NUMERO_PEDIMENTO;
                    modifiedFactura.UNID_LOTE = factura.UNID_LOTE;
                    modifiedFactura.UNID_MONEDA = factura.UNID_MONEDA;
                    modifiedFactura.UNID_PROVEEDOR = factura.UNID_PROVEEDOR;
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
                    FACTURA factura = (FACTURA)element;

                    var validacion = (from cust in entity.FACTURAs
                                      where cust.UNID_FACTURA == factura.UNID_FACTURA
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        factura.UNID_FACTURA = UNID.getNewUNID();
                        //Sync
                        factura.IS_MODIFIED = true;
                        factura.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.FACTURAs.AddObject(factura);
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