using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class DepartamentoDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.DEPARTAMENTOes
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return resul;

                resul = (from departamento in entity.DEPARTAMENTOes
                         where departamento.IS_ACTIVE == true
                         where departamento.IS_MODIFIED == false
                         select departamento.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonDepartamento(long? Last_Modified_Date)
        {
            string res = null;
            List<DEPARTAMENTO> listDepartamento = new List<DEPARTAMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.DEPARTAMENTOes
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listDepartamento.Add(new DEPARTAMENTO
                     {
                         DEPARTAMENTO_NAME = row.DEPARTAMENTO_NAME,
                         UNID_DEPARTAMENTO = row.UNID_DEPARTAMENTO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listDepartamento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listDepartamento);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                DEPARTAMENTO poco = (DEPARTAMENTO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.DEPARTAMENTOes
                                 where poco.UNID_DEPARTAMENTO == cust.UNID_DEPARTAMENTO
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

                    var modifiedCotizacion = entity.DEPARTAMENTOes.First(p => p.UNID_DEPARTAMENTO == poco.UNID_DEPARTAMENTO);
                    modifiedCotizacion.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getElements()
        {
            FixupCollection<DEPARTAMENTO> tp = new FixupCollection<DEPARTAMENTO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.DEPARTAMENTOes
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
            FixupCollection<DEPARTAMENTO> tp = new FixupCollection<DEPARTAMENTO>();

            object res = null;
            if (element != null)
            { 
                using (var entitie = new TAE2Entities())
                {
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;

                    var query= (from cust in entitie.DEPARTAMENTOes
                                where cust.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO
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
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;
                    var modifiedDepartamento = entity.DEPARTAMENTOes.First(p => p.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO);
                    modifiedDepartamento.DEPARTAMENTO_NAME = departamento.DEPARTAMENTO_NAME;
                    //Sync
                    modifiedDepartamento.IS_MODIFIED = true;
                    modifiedDepartamento.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;
                    var modifiedDepartamento = entity.DEPARTAMENTOes.First(p => p.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO);
                    modifiedDepartamento.DEPARTAMENTO_NAME = departamento.DEPARTAMENTO_NAME;
                    modifiedDepartamento.IS_ACTIVE = departamento.IS_ACTIVE;
                    //Sync
                    modifiedDepartamento.IS_MODIFIED = true;
                    modifiedDepartamento.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;

                    var validacion = (from cust in entity.DEPARTAMENTOes
                                      where cust.DEPARTAMENTO_NAME == departamento.DEPARTAMENTO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        departamento.UNID_DEPARTAMENTO = UNID.getNewUNID();
                        //Sync
                        departamento.IS_MODIFIED = true;
                        departamento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.DEPARTAMENTOes.AddObject(departamento);
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
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;

                    //Sync
                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.DEPARTAMENTOes.AddObject(departamento);
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
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;
                    var deleteDepartamento = entity.DEPARTAMENTOes.First(p => p.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO);
                    deleteDepartamento.IS_ACTIVE = false;
                    //Sync
                    deleteDepartamento.IS_MODIFIED = true;
                    deleteDepartamento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<DEPARTAMENTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de DEPARTAMENTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonDepartamento()
        {
            string res = null;
            List<DEPARTAMENTO> listDepartamento = new List<DEPARTAMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.DEPARTAMENTOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listDepartamento.Add(new DEPARTAMENTO
                     {
                         DEPARTAMENTO_NAME= row.DEPARTAMENTO_NAME,
                         UNID_DEPARTAMENTO=row.UNID_DEPARTAMENTO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listDepartamento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listDepartamento);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<DEPARTAMENTO>
        /// </summary>
        /// <returns>Regresa List<DEPARTAMENTO></returns>
        /// <returns>Si no regresa null</returns>
        public List<DEPARTAMENTO> GetDeserializeDepartamento(string listPocos)
        {
            List<DEPARTAMENTO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<DEPARTAMENTO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetDepartamento()
        {
            List<DEPARTAMENTO> reset = new List<DEPARTAMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.DEPARTAMENTOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new DEPARTAMENTO
                     {
                         DEPARTAMENTO_NAME = row.DEPARTAMENTO_NAME,
                         UNID_DEPARTAMENTO = row.UNID_DEPARTAMENTO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.DEPARTAMENTOes.First(p => p.UNID_DEPARTAMENTO == item.UNID_DEPARTAMENTO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
