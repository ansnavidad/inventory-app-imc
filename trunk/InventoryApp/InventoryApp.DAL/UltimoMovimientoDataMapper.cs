using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class UltimoMovimientoDataMapper : IDataMapper
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
            if (element != null)
            {   
                using (var entity = new TAE2Entities())
                {
                    ULTIMO_MOVIMIENTO ultimoMov = (ULTIMO_MOVIMIENTO)element;

                    var query = (from p in entity.ULTIMO_MOVIMIENTO
                                 where p.UNID_ITEM == ultimoMov.UNID_ITEM
                                 select p).ToList();

                    if (query.Count == 0)
                    {
                        //Sync
                        ultimoMov.IS_MODIFIED = true;
                        ultimoMov.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.ULTIMO_MOVIMIENTO.AddObject(ultimoMov);                       
                    }
                    else {

                        var modifiedMov = entity.ULTIMO_MOVIMIENTO.First(p => p.UNID_ITEM == ultimoMov.UNID_ITEM);

                        modifiedMov.UNID_ALMACEN = ultimoMov.UNID_ALMACEN;
                        modifiedMov.UNID_CLIENTE = ultimoMov.UNID_CLIENTE;
                        modifiedMov.UNID_MOVIMIENTO_DETALLE = ultimoMov.UNID_MOVIMIENTO_DETALLE;
                        modifiedMov.UNID_PROVEEDOR = ultimoMov.UNID_PROVEEDOR;
                        //Sync
                        modifiedMov.IS_MODIFIED = true;
                        modifiedMov.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
