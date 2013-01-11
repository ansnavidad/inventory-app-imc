using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class ReciboStatusDataMapper : IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                var resul0 = (from prov in entity.RECIBO_STATUS
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return resul;

                resul = (from rs in entity.RECIBO_STATUS
                         where rs.IS_ACTIVE == true
                         where rs.IS_MODIFIED == false
                         select rs.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonReciboStatus(long? LastModifiedDate)
        {
            string res = null;
            List<RECIBO_STATUS> listReciboStatus = new List<RECIBO_STATUS>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.RECIBO_STATUS
                 where p.LAST_MODIFIED_DATE > LastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listReciboStatus.Add(new RECIBO_STATUS
                     {
                         UNID_RECIBO_STATUS = row.UNID_RECIBO_STATUS,
                         RECIBO_STATUS_NAME = row.RECIBO_STATUS_NAME,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listReciboStatus.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listReciboStatus);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                RECIBO_STATUS poco = (RECIBO_STATUS)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.RECIBO_STATUS
                                 where poco.UNID_RECIBO_STATUS == cust.UNID_RECIBO_STATUS
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        var aux = query.First();

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElementSync((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElementSync((object)poco);
                    }

                    var modifiedMenu = entity.RECIBO_STATUS.First(p => p.UNID_RECIBO_STATUS == poco.UNID_RECIBO_STATUS);
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
                    RECIBO_STATUS reciboS = (RECIBO_STATUS)element;
                    var modifiedReciboS = entity.RECIBO_STATUS.First(p => p.UNID_RECIBO_STATUS == reciboS.UNID_RECIBO_STATUS);
                    modifiedReciboS.RECIBO_STATUS_NAME = reciboS.RECIBO_STATUS_NAME;
                    //Sync
                    modifiedReciboS.IS_MODIFIED = true;
                    modifiedReciboS.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    RECIBO_STATUS reciboS = (RECIBO_STATUS)element;
                    var modifiedReciboS = entity.RECIBO_STATUS.First(p => p.UNID_RECIBO_STATUS == reciboS.UNID_RECIBO_STATUS);
                    modifiedReciboS.RECIBO_STATUS_NAME = reciboS.RECIBO_STATUS_NAME;
                    modifiedReciboS.IS_ACTIVE = reciboS.IS_ACTIVE;
                    //Sync
                    modifiedReciboS.IS_MODIFIED = true;
                    modifiedReciboS.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    RECIBO_STATUS reciboS = (RECIBO_STATUS)element;

                    var validacion = (from cust in entity.RECIBO_STATUS
                                      where cust.RECIBO_STATUS_NAME == reciboS.RECIBO_STATUS_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        reciboS.UNID_RECIBO_STATUS = UNID.getNewUNID();
                        //Sync
                        reciboS.IS_MODIFIED = true;
                        reciboS.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.RECIBO_STATUS.AddObject(reciboS);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void insertElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    RECIBO_STATUS reciboS = (RECIBO_STATUS)element;

                    //Sync                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.RECIBO_STATUS.AddObject(reciboS);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<RECIBO_STATUS> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de RECIBO_STATUS</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonReciboStatus()
        {
            string res = null;
            List<RECIBO_STATUS> listReciboStatus = new List<RECIBO_STATUS>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.RECIBO_STATUS
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listReciboStatus.Add(new RECIBO_STATUS
                     {
                         UNID_RECIBO_STATUS=row.UNID_RECIBO_STATUS,
                         RECIBO_STATUS_NAME=row.RECIBO_STATUS_NAME,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listReciboStatus.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listReciboStatus);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<RECIBO_STATUS>
        /// </summary>
        /// <returns>Regresa List<RECIBO_STATUS></returns>
        /// <returns>Si no regresa null</returns>
        public List<RECIBO_STATUS> GetDeserializeReciboStatus(string listPocos)
        {
            List<RECIBO_STATUS> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<RECIBO_STATUS>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetReciboStatus()
        {
            List<RECIBO_STATUS> reset = new List<RECIBO_STATUS>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.RECIBO_STATUS
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new RECIBO_STATUS
                     {
                         UNID_RECIBO_STATUS = row.UNID_RECIBO_STATUS,
                         RECIBO_STATUS_NAME = row.RECIBO_STATUS_NAME,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.RECIBO_STATUS.First(p => p.UNID_RECIBO_STATUS == item.UNID_RECIBO_STATUS);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
