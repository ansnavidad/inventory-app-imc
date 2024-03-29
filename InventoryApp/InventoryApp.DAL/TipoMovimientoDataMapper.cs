﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class TipoMovimientoDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.TIPO_MOVIMIENTO
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from tipo in entity.TIPO_MOVIMIENTO
                         where tipo.IS_ACTIVE == true
                         where tipo.IS_MODIFIED == false
                         select tipo.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonTipoMovimiento(long? LMD)
        {
            string res = null;
            List<TIPO_MOVIMIENTO> listTipoMovimiento = new List<TIPO_MOVIMIENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_MOVIMIENTO
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     listTipoMovimiento.Add(new TIPO_MOVIMIENTO
                     {
                         UNID_TIPO_MOVIMIENTO = row.UNID_TIPO_MOVIMIENTO,
                         TIPO_MOVIMIENTO_NAME = row.TIPO_MOVIMIENTO_NAME,
                         SIGNO_MOVIMIENTO = row.SIGNO_MOVIMIENTO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTipoMovimiento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTipoMovimiento);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                TIPO_MOVIMIENTO poco = (TIPO_MOVIMIENTO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.TIPO_MOVIMIENTO
                                 where poco.UNID_TIPO_MOVIMIENTO == cust.UNID_TIPO_MOVIMIENTO
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

                    var modifiedMenu = entity.TIPO_MOVIMIENTO.First(p => p.UNID_TIPO_MOVIMIENTO == poco.UNID_TIPO_MOVIMIENTO);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public object getElements()
        {
            object res = null;

            FixupCollection<TIPO_MOVIMIENTO> tp = new FixupCollection<TIPO_MOVIMIENTO>();

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.TIPO_MOVIMIENTO
                             where cust.IS_ACTIVE == true
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

            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_MOVIMIENTO ETipo = (TIPO_MOVIMIENTO)element;

                    var query = (from cust in entity.TIPO_MOVIMIENTO
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
            if (element != null)
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
                        //Sync
                        tipo.IS_MODIFIED = true;
                        tipo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
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
                        tipo.IS_ACTIVE = ETipo.IS_ACTIVE;
                        //Sync
                        tipo.IS_MODIFIED = true;
                        tipo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
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
                    //Sync
                    tipo.IS_MODIFIED = true;
                    tipo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.TIPO_MOVIMIENTO.AddObject(tipo);
                    
                    entity.SaveChanges();

                }
            }
        }

        public void insertElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_MOVIMIENTO tipo = (TIPO_MOVIMIENTO)element;

                    //Sync
                    
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.TIPO_MOVIMIENTO.AddObject(tipo);

                    entity.SaveChanges();

                }
            }
        }

        public void deleteElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_MOVIMIENTO tipoMovimiento = (TIPO_MOVIMIENTO)element;

                    var deleteTipoMovimiento = entity.TIPO_MOVIMIENTO.First(p => p.UNID_TIPO_MOVIMIENTO == tipoMovimiento.UNID_TIPO_MOVIMIENTO);

                    deleteTipoMovimiento.IS_ACTIVE = false;
                    //Sync
                    deleteTipoMovimiento.IS_MODIFIED = true;
                    deleteTipoMovimiento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
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
                    //Sync
                    deleteTipoMovimiento.IS_MODIFIED = true;
                    deleteTipoMovimiento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public List<TIPO_MOVIMIENTO> getListElements()
        {
            List<TIPO_MOVIMIENTO> elements = new List<TIPO_MOVIMIENTO>();

            try
            {
                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.TIPO_MOVIMIENTO
                     select p).ToList<TIPO_MOVIMIENTO>().ForEach(o => elements.Add(o));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return elements;
        }

        /// <summary>
        /// Método que serializa una List<TIPO_MOVIMIENTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de TIPO_MOVIMIENTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonTipoMovimiento()
        {
            string res = null;
            List<TIPO_MOVIMIENTO> listTipoMovimiento = new List<TIPO_MOVIMIENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_MOVIMIENTO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listTipoMovimiento.Add(new TIPO_MOVIMIENTO
                     {
                         UNID_TIPO_MOVIMIENTO=row.UNID_TIPO_MOVIMIENTO,
                         TIPO_MOVIMIENTO_NAME=row.TIPO_MOVIMIENTO_NAME,
                         SIGNO_MOVIMIENTO=row.SIGNO_MOVIMIENTO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTipoMovimiento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTipoMovimiento);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<TIPO_MOVIMIENTO>
        /// </summary>
        /// <returns>Regresa List<TIPO_MOVIMIENTO></returns>
        /// <returns>Si no regresa null</returns>
        public List<TIPO_MOVIMIENTO> GetDeserializeTipoMovimiento(string listPocos)
        {
            List<TIPO_MOVIMIENTO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<TIPO_MOVIMIENTO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetTipoMovimiento()
        {
            List<TIPO_MOVIMIENTO> reset = new List<TIPO_MOVIMIENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_MOVIMIENTO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new TIPO_MOVIMIENTO
                     {
                         UNID_TIPO_MOVIMIENTO = row.UNID_TIPO_MOVIMIENTO,
                         TIPO_MOVIMIENTO_NAME = row.TIPO_MOVIMIENTO_NAME,
                         SIGNO_MOVIMIENTO = row.SIGNO_MOVIMIENTO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.TIPO_MOVIMIENTO.First(p => p.UNID_TIPO_MOVIMIENTO == item.UNID_TIPO_MOVIMIENTO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
