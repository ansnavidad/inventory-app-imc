using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class TipoPedimentoDataMapper : IDataMapper
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
                resul = (from tipo in entity.TIPO_PEDIMENTO
                         where tipo.IS_ACTIVE == true
                         where tipo.IS_MODIFIED == false
                         select tipo.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonTipoPedimento(long? LMD)
        {
            string res = null;
            List<TIPO_PEDIMENTO> listTipoPedimento = new List<TIPO_PEDIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_PEDIMENTO
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     listTipoPedimento.Add(new TIPO_PEDIMENTO
                     {
                         UNID_TIPO_PEDIMENTO = row.UNID_TIPO_PEDIMENTO,
                         TIPO_PEDIMENTO_NAME = row.TIPO_PEDIMENTO_NAME,
                         CLAVE = row.CLAVE,
                         REGIMEN = row.REGIMEN,
                         NOTA = row.NOTA,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTipoPedimento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTipoPedimento);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                TIPO_PEDIMENTO poco = (TIPO_PEDIMENTO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.TIPO_PEDIMENTO
                                 where poco.UNID_TIPO_PEDIMENTO == cust.UNID_TIPO_PEDIMENTO
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

                    var modifiedMenu = entity.TIPO_PEDIMENTO.First(p => p.UNID_TIPO_PEDIMENTO == poco.UNID_TIPO_PEDIMENTO);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public object getElements()
        {
            object o = null;
            FixupCollection<TIPO_PEDIMENTO> tp = new FixupCollection<TIPO_PEDIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                var query = (from p in Entity.TIPO_PEDIMENTO
                             where p.IS_ACTIVE == true
                             select p).ToList();

                if (query.Count > 0)
                {
                    o = query;
                }

                return o;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                TIPO_PEDIMENTO Eprov = (TIPO_PEDIMENTO)element;
                FixupCollection<TIPO_PEDIMENTO> tp = new FixupCollection<TIPO_PEDIMENTO>();

                using (var Entity = new TAE2Entities())
                {
                    var query = (from p in Entity.TIPO_PEDIMENTO
                                 where p.UNID_TIPO_PEDIMENTO == Eprov.UNID_TIPO_PEDIMENTO
                                 select p).ToList();

                    if (query.Count > 0)
                    {
                        o = query;
                    }
                }
            }
            return o;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_PEDIMENTO tipoPedimento = (TIPO_PEDIMENTO)element;
                    var modifiedTipoPedimento = entity.TIPO_PEDIMENTO.First(p => p.UNID_TIPO_PEDIMENTO == tipoPedimento.UNID_TIPO_PEDIMENTO);
                    modifiedTipoPedimento.TIPO_PEDIMENTO_NAME = tipoPedimento.TIPO_PEDIMENTO_NAME;
                    modifiedTipoPedimento.REGIMEN = tipoPedimento.REGIMEN;
                    modifiedTipoPedimento.NOTA = tipoPedimento.NOTA;
                    modifiedTipoPedimento.CLAVE = tipoPedimento.CLAVE;
                    //Sync
                    modifiedTipoPedimento.IS_MODIFIED = true;
                    modifiedTipoPedimento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
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
                    TIPO_PEDIMENTO tipoPedimento = (TIPO_PEDIMENTO)element;
                    var modifiedTipoPedimento = entity.TIPO_PEDIMENTO.First(p => p.UNID_TIPO_PEDIMENTO == tipoPedimento.UNID_TIPO_PEDIMENTO);
                    modifiedTipoPedimento.TIPO_PEDIMENTO_NAME = tipoPedimento.TIPO_PEDIMENTO_NAME;
                    modifiedTipoPedimento.REGIMEN = tipoPedimento.REGIMEN;
                    modifiedTipoPedimento.NOTA = tipoPedimento.NOTA;
                    modifiedTipoPedimento.CLAVE = tipoPedimento.CLAVE;
                    modifiedTipoPedimento.IS_ACTIVE = tipoPedimento.IS_ACTIVE;
                    //Sync
                    modifiedTipoPedimento.IS_MODIFIED = true;
                    modifiedTipoPedimento.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    TIPO_PEDIMENTO tipoPedimento = (TIPO_PEDIMENTO)element;

                    var validacion = (from cust in entity.TIPO_PEDIMENTO
                                      where cust.TIPO_PEDIMENTO_NAME == tipoPedimento.TIPO_PEDIMENTO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        tipoPedimento.UNID_TIPO_PEDIMENTO = UNID.getNewUNID();
                        //Sync
                        tipoPedimento.IS_MODIFIED = true;
                        tipoPedimento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.TIPO_PEDIMENTO.AddObject(tipoPedimento);
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
                    TIPO_PEDIMENTO tipoPedimento = (TIPO_PEDIMENTO)element;

                    var validacion = (from cust in entity.TIPO_PEDIMENTO
                                      where cust.TIPO_PEDIMENTO_NAME == tipoPedimento.TIPO_PEDIMENTO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        //Sync
                        
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.TIPO_PEDIMENTO.AddObject(tipoPedimento);
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
                    TIPO_PEDIMENTO tipoPedimento = (TIPO_PEDIMENTO)element;

                    var deleteTipoPedimento = entity.TIPO_PEDIMENTO.First(p => p.UNID_TIPO_PEDIMENTO == tipoPedimento.UNID_TIPO_PEDIMENTO);

                    deleteTipoPedimento.IS_ACTIVE = false;
                    //Sync
                    deleteTipoPedimento.IS_MODIFIED = true;
                    deleteTipoPedimento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public List<TIPO_PEDIMENTO> getListElements()
        {
            List<TIPO_PEDIMENTO> tipoPedimentos = new List<TIPO_PEDIMENTO>();

            try
            {
                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.TIPO_PEDIMENTO
                     select p).ToList<TIPO_PEDIMENTO>().ForEach(o => tipoPedimentos.Add(o));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tipoPedimentos;
        }

        /// <summary>
        /// Método que serializa una List<TIPO_PEDIMENTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de TIPO_PEDIMENTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonTipoPedimento()
        {
            string res = null;
            List<TIPO_PEDIMENTO> listTipoPedimento = new List<TIPO_PEDIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_PEDIMENTO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listTipoPedimento.Add(new TIPO_PEDIMENTO
                     {
                         UNID_TIPO_PEDIMENTO=row.UNID_TIPO_PEDIMENTO,
                         TIPO_PEDIMENTO_NAME=row.TIPO_PEDIMENTO_NAME,
                         CLAVE=row.CLAVE,
                         REGIMEN=row.REGIMEN,
                         NOTA=row.NOTA,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTipoPedimento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTipoPedimento);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<TIPO_PEDIMENTO>
        /// </summary>
        /// <returns>Regresa List<TIPO_PEDIMENTO></returns>
        /// <returns>Si no regresa null</returns>
        public List<TIPO_PEDIMENTO> GetDeserializeTipoPedimento(string listPocos)
        {
            List<TIPO_PEDIMENTO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<TIPO_PEDIMENTO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetTipoPedimento()
        {
            List<TIPO_PEDIMENTO> reset = new List<TIPO_PEDIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_PEDIMENTO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new TIPO_PEDIMENTO
                     {
                         UNID_TIPO_PEDIMENTO = row.UNID_TIPO_PEDIMENTO,
                         TIPO_PEDIMENTO_NAME = row.TIPO_PEDIMENTO_NAME,
                         CLAVE = row.CLAVE,
                         REGIMEN = row.REGIMEN,
                         NOTA = row.NOTA,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.TIPO_PEDIMENTO.First(p => p.UNID_TIPO_PEDIMENTO == item.UNID_TIPO_PEDIMENTO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
