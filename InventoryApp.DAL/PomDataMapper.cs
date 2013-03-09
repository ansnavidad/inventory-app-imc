using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class PomDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.POMs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from pom in entity.POMs
                         where pom.IS_ACTIVE == true
                         where pom.IS_MODIFIED == false
                         select pom.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonPom(long? LastModifiedDate)
        {
            string res = null;
            List<POM> listPom = new List<POM>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.POMs
                 where p.LAST_MODIFIED_DATE > LastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listPom.Add(new POM
                     {
                         UNID_POM = row.UNID_POM,
                         FECHA_POM = row.FECHA_POM,
                         UNID_COTIZACION = row.UNID_COTIZACION,
                         DIAS_ENTREGA = row.DIAS_ENTREGA,
                         FECHA_ENTREGA = row.FECHA_ENTREGA,
                         FECHA_ENTREGA_REAL = row.FECHA_ENTREGA_REAL,
                         UNID_EMPRESA = row.UNID_EMPRESA,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPom.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPom);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                POM poco = (POM)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.POMs
                                 where poco.UNID_POM == cust.UNID_POM
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

                    var modifiedMenu = entity.POMs.First(p => p.UNID_POM == poco.UNID_POM);
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
                    POM pom = (POM)element;
                    var modifiedPom = entity.POMs.First(p => p.UNID_POM == pom.UNID_POM);
                    modifiedPom.UNID_EMPRESA = pom.UNID_EMPRESA;
                    modifiedPom.UNID_COTIZACION = pom.UNID_COTIZACION;
                    modifiedPom.IS_ACTIVE = pom.IS_ACTIVE;
                    modifiedPom.FECHA_POM = pom.FECHA_POM;
                    modifiedPom.FECHA_ENTREGA_REAL = pom.FECHA_ENTREGA_REAL;
                    modifiedPom.FECHA_ENTREGA = pom.FECHA_ENTREGA;
                    modifiedPom.DIAS_ENTREGA = pom.DIAS_ENTREGA;
                    //Sync
                    modifiedPom.IS_MODIFIED = true;
                    modifiedPom.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    POM pom = (POM)element;

                    var validacion = (from cust in entity.POMs
                                      where cust.UNID_POM == pom.UNID_POM
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        pom.UNID_POM = UNID.getNewUNID();
                        //Sync
                        pom.IS_MODIFIED = true;
                        pom.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.POMs.AddObject(pom);
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
                    POM pom = (POM)element;

                    //Sync
                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.POMs.AddObject(pom);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element, USUARIO u)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<POM> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de POM</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonPom()
        {
            string res = null;
            List<POM> listPom = new List<POM>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.POMs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listPom.Add(new POM
                     {
                         UNID_POM=row.UNID_POM,
                         FECHA_POM=row.FECHA_POM,
                         UNID_COTIZACION=row.UNID_COTIZACION,
                         DIAS_ENTREGA=row.DIAS_ENTREGA,
                         FECHA_ENTREGA=row.FECHA_ENTREGA,
                         FECHA_ENTREGA_REAL=row.FECHA_ENTREGA_REAL,
                         UNID_EMPRESA=row.UNID_EMPRESA,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPom.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPom);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<POM>
        /// </summary>
        /// <returns>Regresa List<POM></returns>
        /// <returns>Si no regresa null</returns>
        public List<POM> GetDeserializePom(string listPocos)
        {
            List<POM> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<POM>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetPom()
        {
            List<POM> reset = new List<POM>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.POMs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new POM
                     {
                         UNID_POM = row.UNID_POM,
                         FECHA_POM = row.FECHA_POM,
                         UNID_COTIZACION = row.UNID_COTIZACION,
                         DIAS_ENTREGA = row.DIAS_ENTREGA,
                         FECHA_ENTREGA = row.FECHA_ENTREGA,
                         FECHA_ENTREGA_REAL = row.FECHA_ENTREGA_REAL,
                         UNID_EMPRESA = row.UNID_EMPRESA,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.POMs.First(p => p.UNID_POM == item.UNID_POM);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }


        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
