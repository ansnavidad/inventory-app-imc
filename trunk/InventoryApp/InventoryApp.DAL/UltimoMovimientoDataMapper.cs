using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class UltimoMovimientoDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {
            if (element != null)
            {
                ULTIMO_MOVIMIENTO poco = (ULTIMO_MOVIMIENTO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ULTIMO_MOVIMIENTO
                                 where poco.UNID_ITEM == cust.UNID_ITEM
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

                    var modifiedMenu = entity.ULTIMO_MOVIMIENTO.First(p => p.UNID_ITEM == poco.UNID_ITEM);
                    modifiedMenu.IS_ACTIVE = false;
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

        /// <summary>
        /// Método que serializa una List<ULTIMO_MOVIMIENTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de ULTIMO_MOVIMIENTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonUltimoMovimiento()
        {
            string res = null;
            List<ULTIMO_MOVIMIENTO> listUltimoMovimiento = new List<ULTIMO_MOVIMIENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ULTIMO_MOVIMIENTO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listUltimoMovimiento.Add(new ULTIMO_MOVIMIENTO
                     {
                         UNID_ITEM=row.UNID_ITEM,
                         UNID_ALMACEN=row.UNID_ALMACEN,
                         UNID_CLIENTE=row.UNID_CLIENTE,
                         UNID_PROVEEDOR=row.UNID_PROVEEDOR,
                         UNID_MOVIMIENTO_DETALLE=row.UNID_MOVIMIENTO_DETALLE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listUltimoMovimiento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listUltimoMovimiento);
                }
                return res;
            }
        }
    }
}
