using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class UnidadDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from articulo in entity.UNIDADs
                         where articulo.IS_ACTIVE == true
                         where articulo.IS_MODIFIED == false
                         select articulo.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonUnidad(long? LMD)
        {
            string res = null;
            List<UNIDAD> listUnidad = new List<UNIDAD>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.UNIDADs
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     listUnidad.Add(new UNIDAD
                     {
                         UNID_UNIDAD = row.UNID_UNIDAD,
                         UNIDAD1 = row.UNIDAD1,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listUnidad.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listUnidad);
                }
                return res;
            }
        }
        public void loadSync(object element)
        {
            if (element != null)
            {
                UNIDAD poco = (UNIDAD)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.UNIDADs
                                 where poco.UNID_UNIDAD == cust.UNID_UNIDAD
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
                        insertElement((object)poco);
                    }

                    var modifiedMenu = entity.UNIDADs.First(p => p.UNID_UNIDAD == poco.UNID_UNIDAD);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }        
        
        public object getElements()
        {
            FixupCollection<UNIDAD> tp = new FixupCollection<UNIDAD>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.UNIDADs
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
            using (var entitie = new TAE2Entities())
            {
                UNIDAD unidad = (UNIDAD)element;
                var query = (from cust in entitie.UNIDADs
                             where cust.UNID_UNIDAD == unidad.UNID_UNIDAD
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
                    UNIDAD unidad = (UNIDAD)element;
                    var modifiedUnidad = entity.UNIDADs.First(p => p.UNID_UNIDAD == unidad.UNID_UNIDAD);
                    modifiedUnidad.UNIDAD1 = unidad.UNIDAD1;
                    //Sync
                    modifiedUnidad.IS_MODIFIED = true;
                    modifiedUnidad.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    UNIDAD unidad = (UNIDAD)element;

                    var validacion = (from cust in entity.UNIDADs
                                      where cust.UNIDAD1 == unidad.UNIDAD1
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        unidad.UNID_UNIDAD = UNID.getNewUNID();
                        //Sync
                        unidad.IS_MODIFIED = true;
                        unidad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.UNIDADs.AddObject(unidad);
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
                    UNIDAD unidad = (UNIDAD)element;
                    var modifiedUnidad = entity.UNIDADs.First(p => p.UNID_UNIDAD == unidad.UNID_UNIDAD);
                    modifiedUnidad.IS_ACTIVE = false;
                    //Sync
                    modifiedUnidad.IS_MODIFIED = true;
                    modifiedUnidad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public List<UNIDAD> getListElements()
        {
            List<UNIDAD> unidades = new List<UNIDAD>();

            try
            {
                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.UNIDADs
                     select p).ToList<UNIDAD>().ForEach(o => unidades.Add(o));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return unidades;
        }

        /// <summary>
        /// Método que serializa una List<UNIDAD> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de UNIDAD</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonUnidad()
        {
            string res = null;
            List<UNIDAD> listUnidad = new List<UNIDAD>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.UNIDADs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listUnidad.Add(new UNIDAD
                     {
                         UNID_UNIDAD=row.UNID_UNIDAD,
                         UNIDAD1=row.UNIDAD1,
                         IS_ACTIVE=row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listUnidad.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listUnidad);
                }
                return res;
            }
        }
    }
}
