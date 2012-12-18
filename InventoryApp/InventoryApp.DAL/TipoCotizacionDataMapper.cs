using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class TipoCotizacionDataMapper : IDataMapper
    {

        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from tipo in entity.TIPO_COTIZACION
                         where tipo.IS_ACTIVE == true
                         where tipo.IS_MODIFIED == false
                         select tipo.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonTipoCotizacion(long? LMD)
        {
            string res = null;
            List<TIPO_COTIZACION> listTipoCotizacion = new List<TIPO_COTIZACION>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_COTIZACION
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     listTipoCotizacion.Add(new TIPO_COTIZACION
                     {
                         UNID_TIPO_COTIZACION = row.UNID_TIPO_COTIZACION,
                         TIPO_COTIZACION_NAME = row.TIPO_COTIZACION_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTipoCotizacion.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTipoCotizacion);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                TIPO_COTIZACION poco = (TIPO_COTIZACION)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.TIPO_COTIZACION
                                 where poco.UNID_TIPO_COTIZACION == cust.UNID_TIPO_COTIZACION
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
                        insertElementSync((object)poco);
                    }

                    var modifiedMenu = entity.TIPO_COTIZACION.First(p => p.UNID_TIPO_COTIZACION == poco.UNID_TIPO_COTIZACION);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

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
                    TIPO_COTIZACION tipo = (TIPO_COTIZACION)element;

                    var validacion = (from cust in entity.TIPO_COTIZACION
                                      where cust.TIPO_COTIZACION_NAME == tipo.TIPO_COTIZACION_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        tipo.UNID_TIPO_COTIZACION = UNID.getNewUNID();
                        //Sync
                        tipo.IS_MODIFIED = true;
                        tipo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.TIPO_COTIZACION.AddObject(tipo);
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
                    TIPO_COTIZACION tipo = (TIPO_COTIZACION)element;

                    var validacion = (from cust in entity.TIPO_COTIZACION
                                      where cust.TIPO_COTIZACION_NAME == tipo.TIPO_COTIZACION_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        //Sync
                        tipo.IS_MODIFIED = true;
                        tipo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
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
                    //Sync
                    deleteTipoCotizacion.IS_MODIFIED = true;
                    deleteTipoCotizacion.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<TIPO_COTIZACION> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de TIPO_COTIZACION</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonTipoCotizacion()
        {
            string res = null;
            List<TIPO_COTIZACION> listTipoCotizacion = new List<TIPO_COTIZACION>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_COTIZACION
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listTipoCotizacion.Add(new TIPO_COTIZACION
                     {
                         UNID_TIPO_COTIZACION=row.UNID_TIPO_COTIZACION,
                         TIPO_COTIZACION_NAME=row.TIPO_COTIZACION_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTipoCotizacion.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTipoCotizacion);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<TIPO_COTIZACION>
        /// </summary>
        /// <returns>Regresa List<TIPO_COTIZACION></returns>
        /// <returns>Si no regresa null</returns>
        public List<TIPO_COTIZACION> GetDeserializeTipoCotizacion(string listPocos)
        {
            List<TIPO_COTIZACION> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<TIPO_COTIZACION>>(listPocos);
            }

            return res;
        }
    }
}
