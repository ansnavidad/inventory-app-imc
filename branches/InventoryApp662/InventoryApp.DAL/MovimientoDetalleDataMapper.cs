using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class MovimientoDetalleDataMapper : IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                var resul0 = (from prov in entity.MOVIMIENTO_DETALLE
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from movd in entity.MOVIMIENTO_DETALLE
                         where movd.IS_ACTIVE == true
                         where movd.IS_MODIFIED == false
                         select movd.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonMovimientoDetalle(long? lastModifiedDate)
        {
            string res = null;
            List<MOVIMIENTO_DETALLE> listMovimientoDetalle = new List<MOVIMIENTO_DETALLE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MOVIMIENTO_DETALLE
                 where p.LAST_MODIFIED_DATE > lastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listMovimientoDetalle.Add(new MOVIMIENTO_DETALLE
                     {
                         UNID_MOVIMIENTO_DETALLE = row.UNID_MOVIMIENTO_DETALLE,
                         UNID_ITEM = row.UNID_ITEM,
                         UNID_MOVIMIENTO = row.UNID_MOVIMIENTO,
                         OBSERVACIONES = row.OBSERVACIONES,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         CANTIDAD=row.CANTIDAD,
                         UNID_ITEM_STATUS= row.UNID_ITEM_STATUS
                     });
                 });
                if (listMovimientoDetalle.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMovimientoDetalle);
                }
                return res;
            }
        }

        /// <summary>
        /// Obtiene todos los elementos en la tabla transporte
        /// </summary>
        /// <returns></returns>
        public void loadSync(object element)
        {

            if (element != null)
            {
                MOVIMIENTO_DETALLE poco = (MOVIMIENTO_DETALLE)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.MOVIMIENTO_DETALLE
                                 where poco.UNID_MOVIMIENTO_DETALLE == cust.UNID_MOVIMIENTO_DETALLE
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

                    var modifiedMenu = entity.MOVIMIENTO_DETALLE.First(p => p.UNID_MOVIMIENTO_DETALLE == poco.UNID_MOVIMIENTO_DETALLE);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getElements()
        {
            object res = null;
            using (var entity = new TAE2Entities())
            {
                res = (from movimiento_detalle in entity.MOVIMIENTO_DETALLE
                       select movimiento_detalle).ToList();

                foreach (MOVIMIENTO_DETALLE detalle in ((List<MOVIMIENTO_DETALLE>)res))
                {
                    detalle.ITEM = detalle.ITEM;
                    detalle.MOVIMENTO = detalle.MOVIMENTO;
                    detalle.ITEM_STATUS = detalle.ITEM_STATUS;
                }


                return res;
            }
        }

        public object getElementLectura(object element)
        {
            object res = null;
            MOVIMIENTO_DETALLE it = (MOVIMIENTO_DETALLE)element;
            List<MOVIMIENTO_DETALLE> reset = new List<MOVIMIENTO_DETALLE>();
            using (var entity = new TAE2Entities())
            {
                res = (from items in entity.MOVIMIENTO_DETALLE
                       where items.UNID_MOVIMIENTO == it.UNID_MOVIMIENTO && items.UNID_ITEM == it.UNID_ITEM && items.UNID_MOVIMIENTO_DETALLE == it.UNID_MOVIMIENTO_DETALLE
                       select items).ToList();

                foreach (MOVIMIENTO_DETALLE mov in ((List<MOVIMIENTO_DETALLE>)res))
                {
                    mov.ITEM = mov.ITEM;
                    mov.ITEM.ARTICULO = mov.ITEM.ARTICULO;
                    mov.ITEM.ARTICULO.CATEGORIA = mov.ITEM.ARTICULO.CATEGORIA;
                    mov.ITEM.ARTICULO.MARCA = mov.ITEM.ARTICULO.MARCA;
                    mov.ITEM.ARTICULO.MODELO = mov.ITEM.ARTICULO.MODELO;
                    mov.ITEM.ARTICULO.EQUIPO = mov.ITEM.ARTICULO.EQUIPO;
                    mov.ITEM_STATUS= mov.ITEM_STATUS;
                    mov.ULTIMO_MOVIMIENTO = mov.ULTIMO_MOVIMIENTO;
                    mov.MOVIMENTO = mov.MOVIMENTO;
                }

                //return res=reset;
                return res;
            }
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
                    MOVIMIENTO_DETALLE MovimientoD = (MOVIMIENTO_DETALLE)element;
                    var modifiedMovimientoD = entity.MOVIMIENTO_DETALLE.First(p => p.UNID_MOVIMIENTO_DETALLE == MovimientoD.UNID_MOVIMIENTO_DETALLE);
                    modifiedMovimientoD.IS_ACTIVE = MovimientoD.IS_ACTIVE;
                    modifiedMovimientoD.OBSERVACIONES = MovimientoD.OBSERVACIONES;
                    modifiedMovimientoD.UNID_ITEM = MovimientoD.UNID_ITEM;
                    modifiedMovimientoD.UNID_MOVIMIENTO = MovimientoD.UNID_MOVIMIENTO;
                    modifiedMovimientoD.UNID_ITEM_STATUS = MovimientoD.UNID_ITEM_STATUS;
                    //Sync
                    modifiedMovimientoD.IS_MODIFIED = true;
                    modifiedMovimientoD.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public void udpateElementRecibo(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MOVIMIENTO_DETALLE movDet = (MOVIMIENTO_DETALLE)element;

                    var query = (from p in entity.MOVIMIENTO_DETALLE
                                 where p.UNID_MOVIMIENTO_DETALLE == movDet.UNID_MOVIMIENTO_DETALLE
                                 select p).ToList();

                    if (query.Count == 0)
                    {
                        //Sync
                        movDet.IS_MODIFIED = true;
                        movDet.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MOVIMIENTO_DETALLE.AddObject(movDet);
                    }
                    else
                    {
                        var modifiedM_D = entity.MOVIMIENTO_DETALLE.First(p => p.UNID_ITEM == movDet.UNID_ITEM);

                        modifiedM_D.IS_ACTIVE = movDet.IS_ACTIVE;
                        modifiedM_D.OBSERVACIONES = movDet.OBSERVACIONES;
                        modifiedM_D.UNID_ITEM = movDet.UNID_ITEM;
                        modifiedM_D.UNID_MOVIMIENTO = movDet.UNID_MOVIMIENTO;
                        modifiedM_D.UNID_ITEM_STATUS = movDet.UNID_ITEM_STATUS;
                        modifiedM_D.CANTIDAD = movDet.CANTIDAD;
                        modifiedM_D.UNID_ITEM_STATUS = movDet.UNID_ITEM_STATUS;

                        //Sync
                        modifiedM_D.IS_MODIFIED = true;
                        modifiedM_D.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                    }

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
                    MOVIMIENTO_DETALLE mov = (MOVIMIENTO_DETALLE)element;
                    //Sync
                    mov.IS_MODIFIED = true;
                    mov.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MOVIMIENTO_DETALLE.AddObject(mov);
                    entity.SaveChanges();
                }
            }
        }

        public void insertElementBaja(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MOVIMIENTO_DETALLE mov = (MOVIMIENTO_DETALLE)element;
                    //Sync
                    mov.IS_MODIFIED = true;
                    mov.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MOVIMIENTO_DETALLE.AddObject(mov);
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
                    MOVIMIENTO_DETALLE mov = (MOVIMIENTO_DETALLE)element;
                    //Sync
                    
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MOVIMIENTO_DETALLE.AddObject(mov);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element, USUARIO u)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<MOVIMIENTO_DETALLE> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de MOVIMIENTO_DETALLE</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonMovimientoDetalle()
        {
            string res = null;
            List<MOVIMIENTO_DETALLE> listMovimientoDetalle = new List<MOVIMIENTO_DETALLE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MOVIMIENTO_DETALLE
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listMovimientoDetalle.Add(new MOVIMIENTO_DETALLE
                     {
                         UNID_MOVIMIENTO_DETALLE= row.UNID_MOVIMIENTO_DETALLE,
                         UNID_ITEM=row.UNID_ITEM,
                         UNID_MOVIMIENTO=row.UNID_MOVIMIENTO,
                         OBSERVACIONES=row.OBSERVACIONES,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         CANTIDAD = row.CANTIDAD,
                         UNID_ITEM_STATUS= row.UNID_ITEM_STATUS
                     });
                 });
                if (listMovimientoDetalle.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMovimientoDetalle);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<MOVIMIENTO_DETALLE>
        /// </summary>
        /// <returns>Regresa List<MOVIMIENTO_DETALLE></returns>
        /// <returns>Si no regresa null</returns>
        public List<MOVIMIENTO_DETALLE> GetDeserializeMovimientoDetalle(string listPocos)
        {
            List<MOVIMIENTO_DETALLE> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<MOVIMIENTO_DETALLE>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetMovimientoDetalle()
        {
            List<MOVIMIENTO_DETALLE> reset = new List<MOVIMIENTO_DETALLE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MOVIMIENTO_DETALLE
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new MOVIMIENTO_DETALLE
                     {
                         UNID_MOVIMIENTO_DETALLE = row.UNID_MOVIMIENTO_DETALLE,
                         UNID_ITEM = row.UNID_ITEM,
                         UNID_MOVIMIENTO = row.UNID_MOVIMIENTO,
                         OBSERVACIONES = row.OBSERVACIONES,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         CANTIDAD = row.CANTIDAD,
                         UNID_ITEM_STATUS= row.UNID_ITEM_STATUS
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.MOVIMIENTO_DETALLE.First(p => p.UNID_MOVIMIENTO_DETALLE == item.UNID_MOVIMIENTO_DETALLE);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
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
