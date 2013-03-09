using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class EquipoDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.EQUIPOes
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from equipo in entity.EQUIPOes
                         where equipo.IS_ACTIVE == true
                         where equipo.IS_MODIFIED == false
                         select equipo.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonEquipo(long? Last_Modified_Date)
        {
            string res = null;
            List<EQUIPO> listEquipo = new List<EQUIPO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.EQUIPOes
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listEquipo.Add(new EQUIPO
                     {
                         UNID_EQUIPO = row.UNID_EQUIPO,
                         EQUIPO_NAME = row.EQUIPO_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listEquipo.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listEquipo);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {

            if (element != null)
            {
                EQUIPO poco = (EQUIPO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.EQUIPOes
                                 where poco.UNID_EQUIPO == cust.UNID_EQUIPO
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

                    var modifiedCotizacion = entity.EQUIPOes.First(p => p.UNID_EQUIPO == poco.UNID_EQUIPO);
                    modifiedCotizacion.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public object getElements()
        {

            FixupCollection<EQUIPO> equipos = new FixupCollection<EQUIPO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.EQUIPOes
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
            object res = null;

            using (var entity = new TAE2Entities())
            {
                EQUIPO Tmp = (EQUIPO)element;

                res = (from tipo in entity.EQUIPOes
                       where tipo.UNID_EQUIPO == Tmp.UNID_EQUIPO
                       select tipo).ToList();

                return res;
            }
        }

        public FixupCollection<EQUIPO> GetArticuloEquipoByCategoria(CATEGORIA categoria)
        {
            FixupCollection<EQUIPO> equipos = new FixupCollection<EQUIPO>();

            if (categoria != null)
            {
                using (var entity = new TAE2Entities())
                {
                    try
                    {
                        (from art in entity.ARTICULOes
                         join catt in entity.CATEGORIAs
                            on art.UNID_CATEGORIA equals catt.UNID_CATEGORIA
                         join equ in entity.EQUIPOes
                            on art.UNID_EQUIPO equals equ.UNID_EQUIPO
                         where catt.UNID_CATEGORIA == categoria.UNID_CATEGORIA
                         select equ
                                            ).Distinct().ToList<EQUIPO>().ForEach(o => equipos.Add(new EQUIPO() { UNID_EQUIPO = o.UNID_EQUIPO, EQUIPO_NAME = o.EQUIPO_NAME }));
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            return equipos;
        }

        public void udpateElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    EQUIPO equipo = (EQUIPO)element;
                    var modifiedItemStatus = entity.EQUIPOes.First(p => p.UNID_EQUIPO == equipo.UNID_EQUIPO);
                    modifiedItemStatus.EQUIPO_NAME = equipo.EQUIPO_NAME;
                    //Sync
                    modifiedItemStatus.IS_MODIFIED = true;
                    modifiedItemStatus.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    UNID.Master(equipo, u, -1, "Modificación");
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    EQUIPO equipo = (EQUIPO)element;
                    var modifiedItemStatus = entity.EQUIPOes.First(p => p.UNID_EQUIPO == equipo.UNID_EQUIPO);
                    modifiedItemStatus.EQUIPO_NAME = equipo.EQUIPO_NAME;
                    modifiedItemStatus.IS_ACTIVE = equipo.IS_ACTIVE;
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

        public void insertElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    EQUIPO equipo = (EQUIPO)element;

                    var validacion = (from cust in entity.EQUIPOes
                                      where cust.EQUIPO_NAME == equipo.EQUIPO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        equipo.UNID_EQUIPO = UNID.getNewUNID();
                        //Sync
                        equipo.IS_MODIFIED = true;
                        equipo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.EQUIPOes.AddObject(equipo);
                        entity.SaveChanges();

                        UNID.Master(equipo, u, -1, "Inserción");
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
                    EQUIPO equipo = (EQUIPO)element;

                    //Sync
                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.EQUIPOes.AddObject(equipo);
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
                    EQUIPO equipo = (EQUIPO)element;
                    var deleteEquipo = entity.EQUIPOes.First(p => p.UNID_EQUIPO == equipo.UNID_EQUIPO);
                    deleteEquipo.IS_ACTIVE = false;
                    //Sync
                    deleteEquipo.IS_MODIFIED = true;
                    deleteEquipo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    UNID.Master(equipo, u, -1, "Emininación");
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    EQUIPO equipo = (EQUIPO)element;
                    var deleteEquipo = entity.EQUIPOes.First(p => p.UNID_EQUIPO == equipo.UNID_EQUIPO);
                    deleteEquipo.IS_ACTIVE = false;
                    //Sync
                    deleteEquipo.IS_MODIFIED = true;
                    deleteEquipo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<EQUIPO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de EQUIPO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonEquipo()
        {
            string res = null;
            List<EQUIPO> listEquipo = new List<EQUIPO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.EQUIPOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listEquipo.Add(new EQUIPO
                     {
                         UNID_EQUIPO=row.UNID_EQUIPO,
                         EQUIPO_NAME=row.EQUIPO_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listEquipo.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listEquipo);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<EQUIPO>
        /// </summary>
        /// <returns>Regresa List<EQUIPO></returns>
        /// <returns>Si no regresa null</returns>
        public List<EQUIPO> GetDeserializeEquipo(string listPocos)
        {
            List<EQUIPO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<EQUIPO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetEquipo()
        {
            List<EQUIPO> reset = new List<EQUIPO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.EQUIPOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new EQUIPO
                     {
                         UNID_EQUIPO = row.UNID_EQUIPO,
                         EQUIPO_NAME = row.EQUIPO_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.EQUIPOes.First(p => p.UNID_EQUIPO == item.UNID_EQUIPO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }


        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
