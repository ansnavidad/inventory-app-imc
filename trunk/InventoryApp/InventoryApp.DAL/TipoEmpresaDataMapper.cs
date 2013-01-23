using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class TipoEmpresaDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.TIPO_EMPRESA
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from tipo in entity.TIPO_EMPRESA
                         where tipo.IS_ACTIVE == true
                         where tipo.IS_MODIFIED == false
                         select tipo.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonTipoEmpresa(long? LMD)
        {
            string res = null;
            List<TIPO_EMPRESA> listTipoEmpresa = new List<TIPO_EMPRESA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_EMPRESA
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     listTipoEmpresa.Add(new TIPO_EMPRESA
                     {
                         UNID_TIPO_EMPRESA = row.UNID_TIPO_EMPRESA,
                         TIPO_EMPRESA_NAME = row.TIPO_EMPRESA_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTipoEmpresa.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTipoEmpresa);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                TIPO_EMPRESA poco = (TIPO_EMPRESA)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.TIPO_EMPRESA
                                 where poco.UNID_TIPO_EMPRESA == cust.UNID_TIPO_EMPRESA
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

                    var modifiedMenu = entity.TIPO_EMPRESA.First(p => p.UNID_TIPO_EMPRESA == poco.UNID_TIPO_EMPRESA);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public object getElements()
        {

            FixupCollection<TIPO_EMPRESA> tp = new FixupCollection<TIPO_EMPRESA>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query=(from cust in entity.TIPO_EMPRESA
                           where cust.IS_ACTIVE==true
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

            using (var entity = new TAE2Entities())
            {
                TIPO_EMPRESA TEmp = (TIPO_EMPRESA)element;

                res = (from tipo in entity.TIPO_EMPRESA
                       where tipo.UNID_TIPO_EMPRESA == TEmp.UNID_TIPO_EMPRESA
                       select tipo).ToList();

                return res;
            }
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)element;
                    var modifiedItemStatus = entity.TIPO_EMPRESA.First(p => p.UNID_TIPO_EMPRESA == tipoEmpresa.UNID_TIPO_EMPRESA);
                    modifiedItemStatus.TIPO_EMPRESA_NAME = tipoEmpresa.TIPO_EMPRESA_NAME;
                    //Sync
                    modifiedItemStatus.IS_MODIFIED = true;
                    modifiedItemStatus.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)element;
                    var modifiedItemStatus = entity.TIPO_EMPRESA.First(p => p.UNID_TIPO_EMPRESA == tipoEmpresa.UNID_TIPO_EMPRESA);
                    modifiedItemStatus.TIPO_EMPRESA_NAME = tipoEmpresa.TIPO_EMPRESA_NAME;
                    modifiedItemStatus.IS_ACTIVE = tipoEmpresa.IS_ACTIVE;
                    //Sync
                    modifiedItemStatus.IS_MODIFIED = true;
                    modifiedItemStatus.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    TIPO_EMPRESA tra = (TIPO_EMPRESA)element;

                    var validacion = (from cust in entity.TIPO_EMPRESA
                                      where cust.TIPO_EMPRESA_NAME == tra.TIPO_EMPRESA_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        tra.UNID_TIPO_EMPRESA = UNID.getNewUNID();
                        //Sync
                        tra.IS_MODIFIED = true;
                        tra.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.TIPO_EMPRESA.AddObject(tra);
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
                    TIPO_EMPRESA tipoEmpresa = (TIPO_EMPRESA)element;

                    var deleteTipoEmpresa = entity.TIPO_EMPRESA.First(p => p.UNID_TIPO_EMPRESA == tipoEmpresa.UNID_TIPO_EMPRESA);

                    deleteTipoEmpresa.IS_ACTIVE = false;
                    //Sync
                    deleteTipoEmpresa.IS_MODIFIED = true;
                    deleteTipoEmpresa.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
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
                    TIPO_EMPRESA tra = (TIPO_EMPRESA)element;

                    //Sync                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.TIPO_EMPRESA.AddObject(tra);
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<TIPO_EMPRESA> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de TIPO_EMPRESA</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonTipoEmpresa()
        {
            string res = null;
            List<TIPO_EMPRESA> listTipoEmpresa = new List<TIPO_EMPRESA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_EMPRESA
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listTipoEmpresa.Add(new TIPO_EMPRESA
                     {
                         UNID_TIPO_EMPRESA=row.UNID_TIPO_EMPRESA,
                         TIPO_EMPRESA_NAME=row.TIPO_EMPRESA_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTipoEmpresa.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTipoEmpresa);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<TIPO_EMPRESA>
        /// </summary>
        /// <returns>Regresa List<TIPO_EMPRESA></returns>
        /// <returns>Si no hay regresa null</returns>
        public List<TIPO_EMPRESA> GetDeserializeTipoEmpresa(string listPocos)
        {
            List<TIPO_EMPRESA> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<TIPO_EMPRESA>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetTipoEmpresa()
        {
            List<TIPO_EMPRESA> reset = new List<TIPO_EMPRESA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_EMPRESA
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new TIPO_EMPRESA
                     {
                         UNID_TIPO_EMPRESA = row.UNID_TIPO_EMPRESA,
                         TIPO_EMPRESA_NAME = row.TIPO_EMPRESA_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.TIPO_EMPRESA.First(p => p.UNID_TIPO_EMPRESA == item.UNID_TIPO_EMPRESA);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
