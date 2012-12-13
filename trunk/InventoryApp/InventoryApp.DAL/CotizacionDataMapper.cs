using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class CotizacionDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from cotizacion in entity.COTIZACIONs
                         where cotizacion.IS_ACTIVE == true
                         where cotizacion.IS_MODIFIED == false
                         select cotizacion.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonCotizacion(long? Last_Modified_Date)
        {
            string res = null;
            List<COTIZACION> listCotizacion = new List<COTIZACION>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.COTIZACIONs
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listCotizacion.Add(new COTIZACION
                     {
                         UNID_COTIZACION = row.UNID_COTIZACION,
                         ID_EMPRESA = row.ID_EMPRESA,
                         ID_USER = row.ID_USER,
                         ID_STATUS = row.ID_STATUS,
                         ID_CATEGORIA = row.ID_CATEGORIA,
                         ID_TIPO_COTIZACION = row.ID_TIPO_COTIZACION,
                         ID_PROYECTO = row.ID_PROYECTO,
                         FECHA_SOLICITUD = row.FECHA_SOLICITUD,
                         FECHA_REQUERIMENTO = row.FECHA_REQUERIMENTO,
                         FECHA_COTIZACION = row.FECHA_COTIZACION,
                         OBSERVACIONES_COMPRAS = row.OBSERVACIONES_COMPRAS,
                         DIAS_VIGENCIA = row.DIAS_VIGENCIA,
                         MOTIVO_CANCELACION = row.MOTIVO_CANCELACION,
                         ID_ROL = row.ID_ROL,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listCotizacion.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listCotizacion);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                COTIZACION poco = (COTIZACION)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.COTIZACIONs
                                 where poco.UNID_COTIZACION == cust.UNID_COTIZACION
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

                    var modifiedCotizacion = entity.COTIZACIONs.First(p => p.UNID_COTIZACION == poco.UNID_COTIZACION);
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
                    COTIZACION cotizacion = (COTIZACION)element;
                    var modifiedCotizacion = entity.COTIZACIONs.First(p => p.UNID_COTIZACION == cotizacion.UNID_COTIZACION);
                    modifiedCotizacion.OBSERVACIONES_COMPRAS = cotizacion.OBSERVACIONES_COMPRAS;
                    modifiedCotizacion.MOTIVO_CANCELACION = cotizacion.MOTIVO_CANCELACION;
                    modifiedCotizacion.IS_ACTIVE = cotizacion.IS_ACTIVE;
                    modifiedCotizacion.ID_USER = cotizacion.ID_USER;
                    modifiedCotizacion.ID_TIPO_COTIZACION = cotizacion.ID_TIPO_COTIZACION;
                    modifiedCotizacion.ID_STATUS = cotizacion.ID_STATUS;
                    modifiedCotizacion.ID_ROL = cotizacion.ID_ROL;
                    modifiedCotizacion.ID_PROYECTO = cotizacion.ID_PROYECTO;
                    modifiedCotizacion.ID_EMPRESA = cotizacion.ID_EMPRESA;
                    modifiedCotizacion.ID_CATEGORIA = cotizacion.ID_CATEGORIA;
                    modifiedCotizacion.FECHA_SOLICITUD = cotizacion.FECHA_SOLICITUD;
                    modifiedCotizacion.FECHA_REQUERIMENTO = cotizacion.FECHA_REQUERIMENTO;
                    modifiedCotizacion.FECHA_COTIZACION = cotizacion.FECHA_COTIZACION;
                    modifiedCotizacion.DIAS_VIGENCIA = cotizacion.DIAS_VIGENCIA;
                    //Sync
                    modifiedCotizacion.IS_MODIFIED = true;
                    modifiedCotizacion.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
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
                    COTIZACION cotizacion = (COTIZACION)element;

                    var validacion = (from cust in entity.COTIZACIONs
                                      where cust.UNID_COTIZACION == cotizacion.UNID_COTIZACION
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        cotizacion.UNID_COTIZACION = UNID.getNewUNID();
                        //Sync
                        cotizacion.IS_MODIFIED = true;
                        cotizacion.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.COTIZACIONs.AddObject(cotizacion);
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
        /// Método que serializa una List<COTIZACION> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de COTIZACION</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonCotizacion()
        {
            string res = null;
            List<COTIZACION> listCotizacion = new List<COTIZACION>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.COTIZACIONs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listCotizacion.Add(new COTIZACION
                     {
                         UNID_COTIZACION=row.UNID_COTIZACION,
                         ID_EMPRESA=row.ID_EMPRESA,
                         ID_USER=row.ID_USER,
                         ID_STATUS=row.ID_STATUS,
                         ID_CATEGORIA=row.ID_CATEGORIA,
                         ID_TIPO_COTIZACION=row.ID_TIPO_COTIZACION,
                         ID_PROYECTO=row.ID_PROYECTO,
                         FECHA_SOLICITUD=row.FECHA_SOLICITUD,
                         FECHA_REQUERIMENTO=row.FECHA_REQUERIMENTO,
                         FECHA_COTIZACION=row.FECHA_COTIZACION,
                         OBSERVACIONES_COMPRAS=row.OBSERVACIONES_COMPRAS,
                         DIAS_VIGENCIA=row.DIAS_VIGENCIA,
                         MOTIVO_CANCELACION=row.MOTIVO_CANCELACION,
                         ID_ROL=row.ID_ROL,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listCotizacion.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listCotizacion);
                }
                return res;
            }
        }
    }
}
