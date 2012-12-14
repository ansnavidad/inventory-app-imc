﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL.Recibo
{
    public class FacturaCompraDataMapper:IDataMapper
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
                        var aux = query.First();

                        if (UNID.compareUNIDS(aux.LAST_MODIFIED_DATE, poco.LAST_MODIFIED_DATE))
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }

                    var modifiedCotizacion = entity.FACTURAs.First(p => p.UNID_FACTURA == poco.UNID_FACTURA);
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
                resul = (from articulo in entity.FACTURAs
                         where articulo.IS_ACTIVE == true
                         where articulo.IS_MODIFIED == false
                         select articulo.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonFactura(long? LMD)
        {
            string res = null;
            List<FACTURA> listFactura = new List<FACTURA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.FACTURAs
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     listFactura.Add(new FACTURA
                     {
                         UNID_FACTURA = row.UNID_FACTURA,
                         UNID_LOTE = row.UNID_LOTE,
                         FACTURA_NUMERO = row.FACTURA_NUMERO,
                         FECHA_FACTURA = row.FECHA_FACTURA,
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         UNID_MONEDA = row.UNID_MONEDA,
                         NUMERO_PEDIMENTO = row.NUMERO_PEDIMENTO,
                         IVA_POR = row.IVA_POR,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listFactura.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listFactura);
                }
                return res;
            }
        }

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

        /// <summary>
        /// Método que serializa una List<FACTURA> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de FACTURA</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonFactura()
        {
            string res = null;
            List<FACTURA> listFactura = new List<FACTURA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.FACTURAs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listFactura.Add(new FACTURA
                     {
                         UNID_FACTURA = row.UNID_FACTURA,
                         UNID_LOTE=row.UNID_LOTE,
                         FACTURA_NUMERO=row.FACTURA_NUMERO,
                         FECHA_FACTURA=row.FECHA_FACTURA,
                         UNID_PROVEEDOR=row.UNID_PROVEEDOR,
                         UNID_MONEDA=row.UNID_MONEDA,
                         NUMERO_PEDIMENTO=row.NUMERO_PEDIMENTO,
                         IVA_POR=row.IVA_POR,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listFactura.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listFactura);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<FACTURA> 
        /// </summary>
        /// <returns>Regresa List<FACTURA></returns>
        /// <returns>Si no regresa null</returns>
        public List<FACTURA> GetDeserializeFactura(string listPocos)
        {
            List<FACTURA> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<FACTURA>>(listPocos);
            }

            return res;
        }
    }
}
