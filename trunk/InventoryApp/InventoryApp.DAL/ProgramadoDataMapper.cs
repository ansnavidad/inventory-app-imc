using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using Newtonsoft.Json;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class ProgramadoDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.PROGRAMADOes
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from medio in entity.PROGRAMADOes
                         where medio.IS_ACTIVE == true
                         where medio.IS_MODIFIED == false
                         select medio.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonProgramado(long? Last_Modified_Date)
        {
            string res = null;
            List<PROGRAMADO> listProgramado = new List<PROGRAMADO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROGRAMADOes
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listProgramado.Add(new PROGRAMADO
                     {
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         UNID_PROGRAMADO= row.UNID_PROGRAMADO,
                         PROGRAMADO1= row.PROGRAMADO1,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listProgramado.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listProgramado);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                PROGRAMADO poco = (PROGRAMADO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.PROGRAMADOes
                                 where poco.UNID_PROGRAMADO == cust.UNID_PROGRAMADO
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

                    var modifiedCotizacion = entity.PROGRAMADOes.First(p => p.UNID_PROGRAMADO == poco.UNID_PROGRAMADO);
                    modifiedCotizacion.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public object getElements()
        {
            FixupCollection<PROGRAMADO> tp = new FixupCollection<PROGRAMADO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                res = (from cust in entity.PROGRAMADOes
                       .Include("ARTICULO")
                       .Include("ARTICULO.CATEGORIA")
                       .Include("ARTICULO.EQUIPO")
                       .Include("ARTICULO.MODELO")
                       .Include("ARTICULO.MARCA")
                       .Include("ALMACEN")
                       where cust.IS_ACTIVE == true
                       select cust).ToList();
                return res;
            }
        }

        public object getElementArticulos(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                PROGRAMADO programado = (PROGRAMADO)element;
                res = (from cust in entitie.PROGRAMADOes
                       .Include("ARTICULO")
                       .Include("ARTICULO.CATEGORIA")
                       .Include("ARTICULO.EQUIPO")
                       .Include("ARTICULO.MODELO")
                       .Include("ARTICULO.MARCA")
                       .Include("ALMACEN")
                       where cust.UNID_ALMACEN == programado.UNID_ALMACEN && cust.IS_ACTIVE == true
                       select cust).ToList();
                
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                PROGRAMADO maxMin = (PROGRAMADO)element;
                var query = (from cust in entitie.PROGRAMADOes
                             where cust.UNID_PROGRAMADO == maxMin.UNID_PROGRAMADO
                             select cust).ToList();
                if (query.Count > 0)
                {
                    res = query;
                }
                return res;
            }
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROGRAMADO prog = (PROGRAMADO)element;
                    var modifiedMaxMin = entity.PROGRAMADOes.First(p => p.UNID_PROGRAMADO == prog.UNID_PROGRAMADO);
                    modifiedMaxMin.PROGRAMADO1 = prog.PROGRAMADO1;
                    //Sync
                    modifiedMaxMin.IS_MODIFIED = true;
                    modifiedMaxMin.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    PROGRAMADO programado = (PROGRAMADO)element;

                    var modifiedProgramado = entity.PROGRAMADOes.First(p => p.UNID_PROGRAMADO == programado.UNID_PROGRAMADO);
                    modifiedProgramado.PROGRAMADO1 = programado.PROGRAMADO1;
                    modifiedProgramado.IS_ACTIVE = programado.IS_ACTIVE;
                    //Sync
                    modifiedProgramado.IS_MODIFIED = true;
                    modifiedProgramado.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    PROGRAMADO prog = (PROGRAMADO)element;


                    prog.UNID_PROGRAMADO = UNID.getNewUNID();
                    //Sync
                    prog.IS_MODIFIED = true;
                    prog.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.PROGRAMADOes.AddObject(prog);
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
                    PROGRAMADO programado = (PROGRAMADO)element;
                    //Sync
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.PROGRAMADOes.AddObject(programado);
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
                    PROGRAMADO maxMin = (PROGRAMADO)element;
                    var modifiedMaxMin = entity.PROGRAMADOes.First(p => p.UNID_PROGRAMADO == maxMin.UNID_PROGRAMADO);
                    modifiedMaxMin.IS_ACTIVE = false;
                    //Sync
                    modifiedMaxMin.IS_MODIFIED = true;
                    modifiedMaxMin.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<PROGRAMADO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PROGRAMADO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonProgramado()
        {
            string res = null;
            List<PROGRAMADO> listProgramado = new List<PROGRAMADO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROGRAMADOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listProgramado.Add(new PROGRAMADO
                     {
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         UNID_PROGRAMADO = row.UNID_PROGRAMADO,
                         PROGRAMADO1 = row.PROGRAMADO1,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listProgramado.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listProgramado);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<PROGRAMADO>
        /// </summary>
        /// <returns>Regresa List<PROGRAMADO></returns>
        /// <returns>Si no regresa null</returns>
        public List<PROGRAMADO> GetDeserializeProgramado(string listPocos)
        {
            List<PROGRAMADO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<PROGRAMADO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetProgramado()
        {
            List<PROGRAMADO> reset = new List<PROGRAMADO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROGRAMADOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new PROGRAMADO
                     {
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         UNID_PROGRAMADO = row.UNID_PROGRAMADO,
                         PROGRAMADO1 = row.PROGRAMADO1,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.PROGRAMADOes.First(p => p.UNID_PROGRAMADO == item.UNID_PROGRAMADO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
