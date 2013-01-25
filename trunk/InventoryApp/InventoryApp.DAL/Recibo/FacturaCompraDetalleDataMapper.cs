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

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElementSny((object)poco);
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
                var resul0 = (from prov in entity.FACTURA_DETALLE
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from facturaDetalle in entity.FACTURA_DETALLE
                         where facturaDetalle.IS_ACTIVE == true
                         where facturaDetalle.IS_MODIFIED == false
                         select facturaDetalle.LAST_MODIFIED_DATE).Max();
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

        public List<FACTURA_DETALLE> GetDetallebyFactura(FACTURA factura)
        {
            List<FACTURA_DETALLE> fDetalles = new List<FACTURA_DETALLE>();

            if (factura != null)
            {
                using (var entity = new TAE2Entities())
                {
                    var res = (from detalles in entity.FACTURA_DETALLE
                               where detalles.UNID_FACTURA == factura.UNID_FACTURA
                               select detalles).ToList();

                    if (res != null)
                    {
                        foreach (FACTURA_DETALLE fd in (List<FACTURA_DETALLE>)res)
                        {
                            fd.ARTICULO = fd.ARTICULO;
                            fDetalles.Add(fd);
                        }
                    }
                }
            }

            return fDetalles;
        }

        public void udpateElements(List<FACTURA_DETALLE> listFd)
        {
            using (var entity = new TAE2Entities())
            {
                //ELIMINAR
                long UnidFactura = listFd[0].UNID_FACTURA;

                var resFacs = (from c in entity.FACTURA_DETALLE
                               where c.UNID_FACTURA == UnidFactura
                               select c).ToList();

                List<long> index = new List<long>();

                for (int i = 0; i < resFacs.Count; i++) {
                    bool aux = true;
                    for (int j = 0; j < listFd.Count; j++) {
                        if (resFacs[i].UNID_FACTURA_DETALE == listFd[j].UNID_FACTURA_DETALE)
                            aux = false;
                    }
                    if (aux)
                        index.Add(resFacs[i].UNID_FACTURA_DETALE);
                }

                foreach (long l in index) {
                    var quiteE = (from c in entity.FACTURA_DETALLE
                                  where c.UNID_FACTURA_DETALE == l
                                  select c).First();

                    quiteE.IS_ACTIVE = false;
                    quiteE.IS_MODIFIED = true;
                    //Sync
                    quiteE.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
             
                    var quiteI = (from c in entity.ITEMs
                                  where c.UNID_FACTURA_DETALE == l
                                  select c).ToList();
                    foreach (ITEM ii in quiteI) {
                        ii.IS_ACTIVE = false;
                        ii.IS_MODIFIED = true;
                        //Sync
                        ii.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSynccc = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                    }
                }

                entity.SaveChanges();

                //UPSERT              
                var FacDCheck = (from c in entity.FACTURA_DETALLE                               
                                 select c).ToList();
                foreach (FACTURA_DETALLE ff in listFd) {
                    bool aux = false;
                    foreach (FACTURA_DETALLE dd in FacDCheck) {
                        if (ff.UNID_FACTURA_DETALE == dd.UNID_FACTURA_DETALE)
                            aux = true;
                    }

                    //UPDATE
                    if (aux)
                    {
                        var FacUp= (from c in entity.FACTURA_DETALLE
                                    where c.UNID_FACTURA_DETALE == ff.UNID_FACTURA_DETALE
                                    select c).First();

                        FacUp.UNID_UNIDAD = ff.UNID_UNIDAD;
                        FacUp.UNID_PEDIMENTO = ff.UNID_PEDIMENTO;
                        FacUp.UNID_FACTURA_DETALE = ff.UNID_FACTURA_DETALE;
                        FacUp.UNID_FACTURA = ff.UNID_FACTURA;
                        FacUp.UNID_ARTICULO = ff.UNID_ARTICULO;
                        FacUp.PRECIO_UNITARIO = ff.PRECIO_UNITARIO;
                        FacUp.NUMERO = ff.NUMERO;
                        FacUp.IMPUESTO_UNITARIO = ff.IMPUESTO_UNITARIO;
                        FacUp.DESCRIPCION = ff.DESCRIPCION;
                        FacUp.CANTIDAD = ff.CANTIDAD;
                        FacUp.IS_ACTIVE = true;
                        FacUp.IS_MODIFIED = true;
                        
                        //Sync
                        FacUp.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                    }
                    //INSERT
                    else {

                        ff.IS_MODIFIED = true;
                        ff.IS_ACTIVE = true;

                        //Sync
                        ff.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();                       
                        entity.FACTURA_DETALLE.AddObject(ff);

                        entity.SaveChanges();
                    }
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
                    //Sync
                    facturaDetalle.IS_MODIFIED = true;
                    facturaDetalle.IS_ACTIVE = true;
                    facturaDetalle.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    entity.FACTURA_DETALLE.AddObject(facturaDetalle);
                    entity.SaveChanges();
                }
            }
        }

        public void insertElementSny(object element)
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
                        //Sync
 
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

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetFacturaDetalle()
        {
            List<FACTURA_DETALLE> reset = new List<FACTURA_DETALLE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.FACTURA_DETALLE
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new FACTURA_DETALLE
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
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.FACTURA_DETALLE.First(p => p.UNID_FACTURA_DETALE == item.UNID_FACTURA_DETALE);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
