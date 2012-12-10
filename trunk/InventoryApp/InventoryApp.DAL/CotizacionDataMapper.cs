using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class CotizacionDataMapper : IDataMapper
    {
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
    }
}
