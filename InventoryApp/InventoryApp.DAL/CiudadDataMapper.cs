using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class CiudadDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {
            if (element != null)
            {
                CIUDAD poco = (CIUDAD)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.CIUDADs
                                 where poco.UNID_CIUDAD == cust.UNID_CIUDAD
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        var aux = query.First();

                        if (UNID.compareUNIDS(aux.LAST_MODIFIED_DATE, poco.LAST_MODIFIED_DATE))
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }

                    var modifiedCiudad = entity.CIUDADs.First(p => p.UNID_CIUDAD == poco.UNID_CIUDAD);
                    modifiedCiudad.IS_ACTIVE = false;
                    entity.SaveChanges();
                }
            }
        }        
        
        public object getElements()
        {
            object res = null;
            FixupCollection<CIUDAD> tp = new FixupCollection<CIUDAD>();
            using (var Entity = new TAE2Entities())
            {
              var query= (from p in Entity.CIUDADs
                          where p.IS_ACTIVE ==true
                          select p).ToList();

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
            if (element != null)
            {
                CIUDAD Eprov = (CIUDAD)element;
                FixupCollection<CIUDAD> tp = new FixupCollection<CIUDAD>();

                using (var Entity = new TAE2Entities())
                {
                  var query= (from p in Entity.CIUDADs
                     where p.UNID_CIUDAD == Eprov.UNID_CIUDAD
                     select p).ToList();

                  if (query.Count > 0)
                  {
                      res = query;
                  }
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
                    CIUDAD ciudad = (CIUDAD)element;
                    var modifiedCiudad = entity.CIUDADs.First(p => p.UNID_CIUDAD == ciudad.UNID_CIUDAD);                    
                    modifiedCiudad.ISO = ciudad.ISO;
                    modifiedCiudad.CIUDAD1 = ciudad.CIUDAD1;
                    //Sync
                    modifiedCiudad.IS_MODIFIED = true;
                    modifiedCiudad.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    CIUDAD ciudad = (CIUDAD)element;

                    var validacion = (from cust in entity.CIUDADs
                                      where cust.CIUDAD1 == ciudad.CIUDAD1
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        ciudad.UNID_CIUDAD = UNID.getNewUNID();
                        //Sync
                        ciudad.IS_MODIFIED = true;
                        ciudad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.CIUDADs.AddObject(ciudad);
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
                    CIUDAD cuidad = (CIUDAD)element;
                    var deleteCuidad = entity.CIUDADs.First(p => p.UNID_CIUDAD == cuidad.UNID_CIUDAD);
                    deleteCuidad.IS_ACTIVE = false;
                    //Sync
                    deleteCuidad.IS_MODIFIED = true;
                    deleteCuidad.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<CIUDAD> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de CIUDAD</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonCiudad()
        {
            string res = null;
            List<CIUDAD> listCiudad = new List<CIUDAD>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.CIUDADs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listCiudad.Add(new CIUDAD
                     {
                         CIUDAD1= row.CIUDAD1,
                         UNID_CIUDAD=row.UNID_CIUDAD,
                         ISO =row.ISO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listCiudad.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listCiudad);
                }
                return res;
            }
        }
    }
}

