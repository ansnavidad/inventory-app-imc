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
                (from cust in entity.TIPO_COTIZACION
                             select cust).ToList().ToList().ForEach(d => { tp.Add(d); });

                if (tp.Count > 0)
                {
                    res = tp;
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

                    (from cust in entity.TIPO_COTIZACION
                                 where cust.UNID_TIPO_COTIZACION == ETipo.UNID_TIPO_COTIZACION
                                 select cust).ToList().ForEach(d => { tp.Add(d); });

                    if (tp.Count > 0)
                    {
                        res = tp;
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
                    TIPO_COTIZACION ETipo = (TIPO_COTIZACION)element;

                    TIPO_COTIZACION tipo = new TIPO_COTIZACION();

                    tipo.UNID_TIPO_COTIZACION = UNID.getNewUNID();

                    tipo.TIPO_COTIZACION_NAME = ETipo.TIPO_COTIZACION_NAME;

                    entity.TIPO_COTIZACION.AddObject(tipo);

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
