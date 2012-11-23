using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class TipoCotizacionDataMapper : IDataMapper
    {

        public object getElements()
        {
            object res = null;

            FixupCollection<TIPO_COTIZACION> tp = new FixupCollection<TIPO_COTIZACION>();

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.TIPO_COTIZACION
                           where cust.IS_ACTIVE ==true
                           select cust).ToList().ToList();

               if (query.Count > 0)
                {
                    res = query;
                }
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;

            FixupCollection<TIPO_COTIZACION> tp = new FixupCollection<TIPO_COTIZACION>();

            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_COTIZACION ETipo = (TIPO_COTIZACION)element;

                    var query = (from cust in entity.TIPO_COTIZACION
                                 where cust.UNID_TIPO_COTIZACION == ETipo.UNID_TIPO_COTIZACION
                                 select cust).ToList();

                    if (query.Count > 0)
                    {
                        res = query;
                    }
                    return res;
                }
            }
            return res;
        }

        public void udpateElement(object element)
        {
            if (element!=null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_COTIZACION ETipo = (TIPO_COTIZACION)element;

                    var query = (from cust in entity.TIPO_COTIZACION
                                 where cust.UNID_TIPO_COTIZACION == ETipo.UNID_TIPO_COTIZACION
                                 select cust).ToList();
                    if (query.Count > 0)
                    {
                        var tipo = query.First();

                        tipo.TIPO_COTIZACION_NAME = ETipo.TIPO_COTIZACION_NAME;

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
                    TIPO_COTIZACION tipo = (TIPO_COTIZACION)element;

                    var validacion = (from cust in entity.TIPO_COTIZACION
                                      where cust.TIPO_COTIZACION_NAME == tipo.TIPO_COTIZACION_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        tipo.UNID_TIPO_COTIZACION = UNID.getNewUNID();
                        entity.TIPO_COTIZACION.AddObject(tipo);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_COTIZACION tipoCotizacion = (TIPO_COTIZACION)element;

                    var deleteTipoCotizacion = entity.TIPO_COTIZACION.First(p => p.UNID_TIPO_COTIZACION == tipoCotizacion.UNID_TIPO_COTIZACION);

                    deleteTipoCotizacion.IS_ACTIVE = false;

                    entity.SaveChanges();
                }
            }
        }
    }
}
