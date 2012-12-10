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
            throw new NotImplementedException();
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
