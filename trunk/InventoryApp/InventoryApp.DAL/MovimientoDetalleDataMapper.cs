﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class MovimientoDetalleDataMapper : IDataMapper
    {
        //
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
            object res = null;
            using (var entity = new TAE2Entities())
            {
                res = (from movimiento_detalle in entity.MOVIMIENTO_DETALLE
                       select movimiento_detalle).ToList();

                foreach (MOVIMIENTO_DETALLE detalle in ((List<MOVIMIENTO_DETALLE>)res))
                {
                    detalle.ITEM = detalle.ITEM;
                    detalle.MOVIMENTO = detalle.MOVIMENTO;
                }


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

        public void deleteElement(object element)
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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMovimientoDetalle.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMovimientoDetalle);
                }
                return res;
            }
        }
    }
}
