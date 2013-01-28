using System;
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

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElementSyn((object)poco);
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
                var resul0 = (from prov in entity.FACTURAs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from facturaDetalle in entity.FACTURAs
                         where facturaDetalle.IS_ACTIVE == true
                         where facturaDetalle.IS_MODIFIED == false
                         select facturaDetalle.LAST_MODIFIED_DATE).Max();
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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         TC= row.TC
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
                                    where o.UNID_RECIBO == recibo.UNID_RECIBO && o.IS_ACTIVE == true
                                    select o).FirstOrDefault();

                //var res = selRecibo.RECIBO_MOVIMIENTO.cl
                selRecibo.RECIBO_MOVIMIENTO.ToList().ForEach(rm =>
                {

                    if (rm.IS_ACTIVE == true)
                    {
                        //Generar las facturas detalles
                        FixupCollection<FACTURA_DETALLE> facturasDetalle = new FixupCollection<FACTURA_DETALLE>();
                        rm.FACTURA.FACTURA_DETALLE.ToList().ForEach(fd =>
                        {
                            if (fd.IS_ACTIVE == true)
                            {
                                facturasDetalle.Add(new FACTURA_DETALLE()
                                {
                                    UNID_FACTURA_DETALE = fd.UNID_FACTURA_DETALE,
                                    UNID_FACTURA = fd.UNID_FACTURA,
                                    ARTICULO = new ARTICULO()
                                    {
                                        UNID_ARTICULO = fd.ARTICULO.UNID_ARTICULO,
                                        ARTICULO1 = fd.ARTICULO.ARTICULO1,
                                        CATEGORIA = fd.ARTICULO.CATEGORIA,
                                        EQUIPO = fd.ARTICULO.EQUIPO,
                                        MARCA = fd.ARTICULO.MARCA,
                                        MODELO = fd.ARTICULO.MODELO
                                    },
                                    CANTIDAD = fd.CANTIDAD,
                                    DESCRIPCION = fd.DESCRIPCION,
                                    IMPUESTO_UNITARIO = fd.IMPUESTO_UNITARIO,
                                    IS_ACTIVE = fd.IS_ACTIVE,
                                    NUMERO = fd.NUMERO,
                                    PEDIMENTO = fd.PEDIMENTO,
                                    PRECIO_UNITARIO = fd.PRECIO_UNITARIO,
                                    UNIDAD = fd.UNIDAD
                                });
                            }
                        });

                        listFactura.Add(new FACTURA()
                        {
                            UNID_FACTURA = rm.FACTURA.UNID_FACTURA,
                            FACTURA_DETALLE = facturasDetalle,
                            FACTURA_NUMERO = rm.FACTURA.FACTURA_NUMERO,
                            FECHA_FACTURA = rm.FACTURA.FECHA_FACTURA,
                            IS_ACTIVE = rm.FACTURA.IS_ACTIVE,
                            MONEDA = rm.FACTURA.MONEDA,
                            PROVEEDOR = rm.FACTURA.PROVEEDOR,
                            IVA_POR = rm.FACTURA.IVA_POR,
                            NUMERO_PEDIMENTO = rm.FACTURA.NUMERO_PEDIMENTO,
                            TC = rm.FACTURA.TC,
                            TIPO_PEDIMENTO = new TIPO_PEDIMENTO()
                            {
                                TIPO_PEDIMENTO_NAME = rm.FACTURA.TIPO_PEDIMENTO.TIPO_PEDIMENTO_NAME,
                                UNID_TIPO_PEDIMENTO = rm.FACTURA.TIPO_PEDIMENTO.UNID_TIPO_PEDIMENTO,
                                REGIMEN = rm.FACTURA.TIPO_PEDIMENTO.REGIMEN,
                                CLAVE = rm.FACTURA.TIPO_PEDIMENTO.CLAVE,
                                NOTA = rm.FACTURA.TIPO_PEDIMENTO.NOTA
                            }
                        });
                    }
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
                    modifiedFactura.TC = factura.TC;
                    //Sync
                    modifiedFactura.IS_MODIFIED = true;
                    modifiedFactura.IS_ACTIVE = true;
                    modifiedFactura.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public void deleteFacturas(List<long> FacturasEliminadas) {

            using (var entity = new TAE2Entities())
            {
                List<long> FacturasAeliminar = new List<long>();

                var res = (from fact in entity.FACTURAs
                           select fact.UNID_FACTURA).ToList();
                for (int i = 0; i < FacturasEliminadas.Count; i++) {

                    for (int j = 0; j < res.Count; j++) {

                        if (FacturasEliminadas[i] == res[j]) {

                            FacturasAeliminar.Add(FacturasEliminadas[i]);
                        }
                    }
                }

                foreach (long l in FacturasAeliminar) {

                    var del = (from f in entity.FACTURAs
                               where f.UNID_FACTURA == l
                               select f).First();

                    del.IS_ACTIVE = false;
                    del.IS_MODIFIED = true;
                    
                    //Sync
                    del.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();

                    var delF_D = (from f in entity.FACTURA_DETALLE
                                  where f.UNID_FACTURA == l
                                  select f).ToList();
                    
                    if(delF_D != null && delF_D.Count > 0){

                        foreach (FACTURA_DETALLE ff in delF_D)
                        {
                            var D = entity.FACTURA_DETALLE.First(p => p.UNID_FACTURA_DETALE == ff.UNID_FACTURA_DETALE);
                            
                            //Sync
                            D.IS_ACTIVE = false;
                            D.IS_MODIFIED = true;
                            D.LAST_MODIFIED_DATE = UNID.getNewUNID();
                            modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                            modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                            entity.SaveChanges();
                        }
                    }
                }
            }
        }

        public List<FACTURA> getFacturabyProveedores(List<PROVEEDOR> proveedores)
        {
            List<FACTURA> respuesta = new List<FACTURA>();
            if (proveedores != null)
            {
                using (var entity = new TAE2Entities())
                {
                   
                    foreach (PROVEEDOR prov in proveedores)
                    {
                        var res = (from fact in entity.FACTURAs
                                   where fact.UNID_PROVEEDOR == prov.UNID_PROVEEDOR
                                   select fact).ToList<FACTURA>();

                        foreach (FACTURA r in res)
                        {
                            r.PROVEEDOR = r.PROVEEDOR;
                            r.MONEDA = r.MONEDA;
                            r.IVA_POR = r.IVA_POR;
                            respuesta.Add(r);
                        }
                    }
                }
            }

            return respuesta;
        }

        public FACTURA getFacturabyDetalle(FACTURA_DETALLE detalle)
        {
            FACTURA respuesta = new FACTURA();
            if (detalle != null)
            {
                using (var entity = new TAE2Entities())
                {

                    
                        var res = (from fact in entity.FACTURAs
                                   where fact.UNID_FACTURA == detalle.UNID_FACTURA
                                   select fact).First<FACTURA>();

                        res.PROVEEDOR = res.PROVEEDOR;
                        respuesta = res;
                }
            }

            return respuesta;
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    FACTURA factura = (FACTURA)element;
                    //Sync
                    factura.IS_MODIFIED = true;
                    factura.IS_ACTIVE = true;
                    factura.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    entity.FACTURAs.AddObject(factura);
                    entity.SaveChanges();
                }
            }
        }

        public void insertElementSyn(object element)
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
                        //Sync
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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         TC= row.TC
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

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetFactura()
        {
            List<FACTURA> reset = new List<FACTURA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.FACTURAs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new FACTURA
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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         TC =row.TC
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.FACTURAs.First(p => p.UNID_FACTURA == item.UNID_FACTURA);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
