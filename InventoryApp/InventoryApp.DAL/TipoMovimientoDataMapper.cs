using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class TipoMovimientoDataMapper : IDataMapper
    {

        public object getElements()
        {
            object res = null;

            FixupCollection<TIPO_MOVIMIENTO> tp = new FixupCollection<TIPO_MOVIMIENTO>();

            using (var entity = new TAE2Entities())
            {
                var query=(from cust in entity.TIPO_MOVIMIENTO
                           where cust.IS_ACTIVE ==true
                           select cust).ToList();

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

            FixupCollection<TIPO_MOVIMIENTO> tp = new FixupCollection<TIPO_MOVIMIENTO>();

            if (element!=null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_MOVIMIENTO ETipo = (TIPO_MOVIMIENTO)element;

                    var query=(from cust in entity.TIPO_MOVIMIENTO
                                 where cust.UNID_TIPO_MOVIMIENTO == ETipo.UNID_TIPO_MOVIMIENTO
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
                    TIPO_MOVIMIENTO ETipo = (TIPO_MOVIMIENTO)element;

                    var query = (from cust in entity.TIPO_MOVIMIENTO
                                 where cust.UNID_TIPO_MOVIMIENTO == ETipo.UNID_TIPO_MOVIMIENTO
                                 select cust).ToList();
                    if (query.Count > 0)
                    {
                        var tipo = query.First();

                        tipo.SIGNO_MOVIMIENTO = ETipo.SIGNO_MOVIMIENTO;

                        tipo.TIPO_MOVIMIENTO_NAME = ETipo.TIPO_MOVIMIENTO_NAME;

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
                    TIPO_MOVIMIENTO tipo = (TIPO_MOVIMIENTO)element;

                    tipo.UNID_TIPO_MOVIMIENTO = UNID.getNewUNID();

                    entity.TIPO_MOVIMIENTO.AddObject(tipo);

                    entity.SaveChanges();
                    
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_MOVIMIENTO tipoMovimiento = (TIPO_MOVIMIENTO)element;

                    var deleteTipoMovimiento = entity.TIPO_MOVIMIENTO.First(p => p.UNID_TIPO_MOVIMIENTO == tipoMovimiento.UNID_TIPO_MOVIMIENTO);

                    deleteTipoMovimiento.IS_ACTIVE = false;

                    entity.SaveChanges();
                }
            }
        }
    }
}
