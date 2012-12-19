using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class ReciboMovimientoDataMapper:IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from rm in entity.RECIBO_MOVIMIENTO
                         where rm.IS_ACTIVE == true
                         where rm.IS_MODIFIED == false
                         select rm.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonReciboMovimiento(long? lastModifiedDate)
        {
            string res = null;
            List<RECIBO_MOVIMIENTO> listReciboMovimiento = new List<RECIBO_MOVIMIENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.RECIBO_MOVIMIENTO
                 where p.LAST_MODIFIED_DATE > lastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listReciboMovimiento.Add(new RECIBO_MOVIMIENTO
                     {
                         UNID_RECIBO = row.UNID_RECIBO,
                         UNID_RECIBO_MOVIMIENTO = row.UNID_RECIBO_MOVIMIENTO,
                         UNID_MOVIMIENTO = row.UNID_MOVIMIENTO,
                         UNID_FACTURA = row.UNID_FACTURA,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listReciboMovimiento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listReciboMovimiento);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {

            if (element != null)
            {
                RECIBO_MOVIMIENTO poco = (RECIBO_MOVIMIENTO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.RECIBO_MOVIMIENTO
                                 where poco.UNID_RECIBO_MOVIMIENTO == cust.UNID_RECIBO_MOVIMIENTO
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
                        insertElementSyn((object)poco);
                    }

                    var modifiedMenu = entity.RECIBO_MOVIMIENTO.First(p => p.UNID_RECIBO_MOVIMIENTO == poco.UNID_RECIBO_MOVIMIENTO);
                    modifiedMenu.IS_MODIFIED = false;
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
                    RECIBO_MOVIMIENTO reciboM = (RECIBO_MOVIMIENTO)element;
                    var modifiedReciboM = entity.RECIBO_MOVIMIENTO.First(p => p.UNID_RECIBO_MOVIMIENTO == reciboM.UNID_RECIBO_MOVIMIENTO);
                    modifiedReciboM.UNID_RECIBO = reciboM.UNID_RECIBO;
                    modifiedReciboM.UNID_MOVIMIENTO = reciboM.UNID_MOVIMIENTO;
                    modifiedReciboM.UNID_FACTURA = reciboM.UNID_FACTURA;                    
                    //Sync
                    modifiedReciboM.IS_MODIFIED = true;
                    modifiedReciboM.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
            if (element != null && (element as RECIBO_MOVIMIENTO) != null)
            {
                using (var entity = new TAE2Entities())
                {
                    RECIBO_MOVIMIENTO item = (RECIBO_MOVIMIENTO)element;
                    //Sync
                    item.IS_MODIFIED = true;
                    item.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.RECIBO_MOVIMIENTO.AddObject(item);
                    entity.SaveChanges();
                }
            }
        }

        public void insertElementSyn(object element)
        {
            if (element != null && (element as RECIBO_MOVIMIENTO) != null)
            {
                using (var entity = new TAE2Entities())
                {
                    RECIBO_MOVIMIENTO item = (RECIBO_MOVIMIENTO)element;
                    //Sync
                    
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.RECIBO_MOVIMIENTO.AddObject(item);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<RECIBO_MOVIMIENTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de RECIBO_MOVIMIENTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonReciboMovimiento()
        {
            string res = null;
            List<RECIBO_MOVIMIENTO> listReciboMovimiento = new List<RECIBO_MOVIMIENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.RECIBO_MOVIMIENTO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listReciboMovimiento.Add(new RECIBO_MOVIMIENTO
                     {
                         UNID_RECIBO = row.UNID_RECIBO,
                         UNID_RECIBO_MOVIMIENTO=row.UNID_RECIBO_MOVIMIENTO,
                         UNID_MOVIMIENTO=row.UNID_MOVIMIENTO,
                         UNID_FACTURA=row.UNID_FACTURA,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listReciboMovimiento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listReciboMovimiento);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<RECIBO_MOVIMIENTO>
        /// </summary>
        /// <returns>Regresa List<RECIBO_MOVIMIENTO></returns>
        /// <returns>Si no regresa null</returns>
        public List<RECIBO_MOVIMIENTO> GetDeserializeReciboMovimiento(string listPocos)
        {
            List<RECIBO_MOVIMIENTO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<RECIBO_MOVIMIENTO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetReciboMovimiento()
        {
            List<RECIBO_MOVIMIENTO> reset = new List<RECIBO_MOVIMIENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.RECIBO_MOVIMIENTO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new RECIBO_MOVIMIENTO
                     {
                         UNID_RECIBO = row.UNID_RECIBO,
                         UNID_RECIBO_MOVIMIENTO = row.UNID_RECIBO_MOVIMIENTO,
                         UNID_MOVIMIENTO = row.UNID_MOVIMIENTO,
                         UNID_FACTURA = row.UNID_FACTURA,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.RECIBO_MOVIMIENTO.First(p => p.UNID_RECIBO_MOVIMIENTO == item.UNID_RECIBO_MOVIMIENTO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
