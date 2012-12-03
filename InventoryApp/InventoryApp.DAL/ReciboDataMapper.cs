using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class ReciboDataMapper:IDataMapper
    {
        public List<MOVIMENTO> GetListMovimiento(RECIBO recibo)
        {
            List<MOVIMENTO> listMovimiento = new List<MOVIMENTO>();

            try
            {
                using (var Entity = new TAE2Entities())
                {
                    var mvtos = (from o in Entity.RECIBOes
                                 where o.UNID_RECIBO == recibo.UNID_RECIBO
                                 select o).FirstOrDefault();

                    mvtos.RECIBO_MOVIMIENTO.ToList().ForEach(rm => 
                    {
                        MOVIMENTO m=new MOVIMENTO()
                        {
                            UNID_MOVIMIENTO=rm.MOVIMENTO.UNID_MOVIMIENTO,
                            ALMACEN=rm.MOVIMENTO.ALMACEN,
                            CLIENTE=rm.MOVIMENTO.CLIENTE,
                            CONTACTO=rm.MOVIMENTO.CONTACTO,
                            PROVEEDOR=rm.MOVIMENTO.PROVEEDOR,
                            FECHA_MOVIMIENTO=rm.MOVIMENTO.FECHA_MOVIMIENTO
                        };

                        FixupCollection<MOVIMIENTO_DETALLE> mdColl=new FixupCollection<MOVIMIENTO_DETALLE>();

                        rm.MOVIMENTO.MOVIMIENTO_DETALLE.ToList().ForEach(md =>
                        {
                            mdColl.Add(new MOVIMIENTO_DETALLE()
                            {
                                UNID_MOVIMIENTO_DETALLE = md.UNID_MOVIMIENTO_DETALLE,
                                UNID_MOVIMIENTO = md.UNID_MOVIMIENTO,
                                ITEM = new ITEM()
                                {
                                    UNID_ITEM = md.ITEM.UNID_ITEM,
                                    COSTO_UNITARIO = md.ITEM.COSTO_UNITARIO,
                                    FACTURA_DETALLE = new FACTURA_DETALLE()
                                    {
                                        FACTURA=md.ITEM.FACTURA_DETALLE.FACTURA,
                                        UNID_FACTURA_DETALE=md.ITEM.UNID_FACTURA_DETALE,
                                        CANTIDAD=md.ITEM.FACTURA_DETALLE.CANTIDAD,
                                        DESCRIPCION=md.ITEM.FACTURA_DETALLE.DESCRIPCION,
                                        IMPUESTO_UNITARIO=md.ITEM.FACTURA_DETALLE.IMPUESTO_UNITARIO,
                                        NUMERO=md.ITEM.FACTURA_DETALLE.NUMERO
                                    },
                                    NUMERO_SERIE = md.ITEM.NUMERO_SERIE,
                                    SKU = md.ITEM.SKU,
                                    ARTICULO = new ARTICULO()
                                    {
                                        CATEGORIA=md.ITEM.ARTICULO.CATEGORIA,
                                        MARCA=md.ITEM.ARTICULO.MARCA,
                                        MODELO=md.ITEM.ARTICULO.MODELO,
                                        EQUIPO=md.ITEM.ARTICULO.EQUIPO,
                                        ARTICULO1=md.ITEM.ARTICULO.ARTICULO1,
                                        UNID_ARTICULO=md.ITEM.ARTICULO.UNID_ARTICULO
                                    }
                                }
                            });
                        });//foreach movto detalle

                        m.MOVIMIENTO_DETALLE = mdColl;

                        listMovimiento.Add(m);
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listMovimiento;
        }

        public List<RECIBO> getListElements()
        {
            List<RECIBO> recibos = new List<RECIBO>();

            try
            {
                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.RECIBOes
                     select p).ToList<RECIBO>().ForEach(o => recibos.Add(new RECIBO()
                     {
                         UNID_RECIBO = o.UNID_RECIBO,
                         FECHA_CREACION = o.FECHA_CREACION,
                         PO = o.PO,
                         TT = o.TT,
                         SOLICITANTE = (o.SOLICITANTE!=null)? new SOLICITANTE()
                         {
                             UNID_SOLICITANTE = o.SOLICITANTE.UNID_SOLICITANTE,
                             SOLICITANTE_NAME = o.SOLICITANTE.SOLICITANTE_NAME,
                             EMPRESA = new EMPRESA()
                             {
                                 UNID_EMPRESA = o.SOLICITANTE.UNID_EMPRESA,
                                 EMPRESA_NAME = o.SOLICITANTE.EMPRESA.EMPRESA_NAME
                             },
                             DEPARTAMENTO = new DEPARTAMENTO()
                             {
                                 UNID_DEPARTAMENTO=o.SOLICITANTE.UNID_DEPARTAMENTO,
                                 DEPARTAMENTO_NAME=o.SOLICITANTE.DEPARTAMENTO.DEPARTAMENTO_NAME
                             }
                         }:null
                     }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return recibos;
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
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            if (element != null && (element as RECIBO) != null)
            {
                using (var entity = new TAE2Entities())
                {
                    RECIBO item = (RECIBO)element;

                    entity.RECIBOes.AddObject(item);
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
