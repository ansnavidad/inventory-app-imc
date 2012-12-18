using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.Data;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class ProyectoDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from proy in entity.PROYECTOes
                         where proy.IS_ACTIVE == true
                         where proy.IS_MODIFIED == false
                         select proy.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonProyecto(long? LastModifiedDate)
        {
            string res = null;
            List<PROYECTO> listProyecto = new List<PROYECTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROYECTOes
                 where p.LAST_MODIFIED_DATE > LastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listProyecto.Add(new PROYECTO
                     {
                         UNID_PROYECTO = row.UNID_PROYECTO,
                         PROYECTO_NAME = row.PROYECTO_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listProyecto.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listProyecto);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                PROYECTO poco = (PROYECTO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.PROYECTOes
                                 where poco.UNID_PROYECTO == cust.UNID_PROYECTO
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

                    var modifiedMenu = entity.PROYECTOes.First(p => p.UNID_PROYECTO == poco.UNID_PROYECTO);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public object getElements()
        {
            FixupCollection<PROYECTO> tp = new FixupCollection<PROYECTO>();

            object res = null;

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.PROYECTOes
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
            FixupCollection<PROYECTO> tp = new FixupCollection<PROYECTO>();

            object res = null;

            using (var entity = new TAE2Entities())
            {
                PROYECTO EPro = (PROYECTO)element;

                (from cust in entity.PROYECTOes
                            where cust.UNID_PROYECTO==EPro.UNID_PROYECTO
                 select cust).ToList().ForEach(d => { tp.Add(d); });

                if (tp.Count > 0)
                {
                    res = tp;
                }

                return res;
            }
        }

        public void udpateElement(object element)
        {
            using (var entity = new TAE2Entities())
            {
                PROYECTO EPro = (PROYECTO)element;

                var query = (from cust in entity.PROYECTOes
                            where cust.UNID_PROYECTO==EPro.UNID_PROYECTO
                            select cust).ToList();

                if (query.Count>0)
                {
                    var pro = query.First();

                    pro.PROYECTO_NAME = EPro.PROYECTO_NAME;
                    //Sync
                    pro.IS_MODIFIED = true;
                    pro.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    PROYECTO Proyecto = (PROYECTO)element;

                    var validacion = (from cust in entity.PROYECTOes
                                      where cust.PROYECTO_NAME == Proyecto.PROYECTO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        Proyecto.UNID_PROYECTO = UNID.getNewUNID();
                        //Sync
                        Proyecto.IS_MODIFIED = true;
                        Proyecto.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.PROYECTOes.AddObject(Proyecto);
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
                    PROYECTO Proyecto = (PROYECTO)element;

                    var validacion = (from cust in entity.PROYECTOes
                                      where cust.PROYECTO_NAME == Proyecto.PROYECTO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        //Sync
                        Proyecto.IS_MODIFIED = true;
                        Proyecto.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.PROYECTOes.AddObject(Proyecto);
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
                    PROYECTO proyecto = (PROYECTO)element;

                    var deleteProyecto = entity.PROYECTOes.First(p => p.UNID_PROYECTO == proyecto.UNID_PROYECTO);

                    deleteProyecto.IS_ACTIVE = false;
                    //Sync
                    deleteProyecto.IS_MODIFIED = true;
                    deleteProyecto.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<PROYECTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PROYECTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonProyecto()
        {
            string res = null;
            List<PROYECTO> listProyecto = new List<PROYECTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROYECTOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listProyecto.Add(new PROYECTO
                     {
                         UNID_PROYECTO=row.UNID_PROYECTO,
                         PROYECTO_NAME=row.PROYECTO_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listProyecto.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listProyecto);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<PROYECTO>
        /// </summary>
        /// <returns>Regresa List<PROYECTO></returns>
        /// <returns>Si no regresa null</returns>
        public List<PROYECTO> GetDeserializeProyecto(string listPocos)
        {
            List<PROYECTO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<PROYECTO>>(listPocos);
            }

            return res;
        }
    }
}
