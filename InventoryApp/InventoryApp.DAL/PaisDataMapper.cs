using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class PaisDataMapper : IDataMapper
    {
        public object getElements()
        {
            object res = null;
            FixupCollection<PAI> tp = new FixupCollection<PAI>();
            using (var Entity = new TAE2Entities())
            {
                var query= (from p in Entity.PAIS
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
                PAI Eprov = (PAI)element;
                FixupCollection<PAI> tp = new FixupCollection<PAI>();
                using (var Entity = new TAE2Entities())
                {
                   var query= (from p in Entity.PAIS
                     where p.UNID_PAIS == Eprov.UNID_PAIS
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
                    PAI pais = (PAI)element;
                    var modifiedPais = entity.PAIS.First(p => p.UNID_PAIS == pais.UNID_PAIS);
                    modifiedPais.PAIS = pais.PAIS;
                    modifiedPais.ISO = pais.ISO;
                    //Sync
                    modifiedPais.IS_MODIFIED = true;
                    modifiedPais.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    PAI pais = (PAI)element;

                    var validacion = (from cust in entity.PAIS
                                      where cust.PAIS == pais.PAIS
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        pais.UNID_PAIS = UNID.getNewUNID();
                        //Sync
                        pais.IS_MODIFIED = true;
                        pais.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.PAIS.AddObject(pais);
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
                    PAI pais = (PAI)element;

                    var deletePais = entity.PAIS.First(p => p.UNID_PAIS == pais.UNID_PAIS);

                    deletePais.IS_ACTIVE = false;
                    //Sync
                    deletePais.IS_MODIFIED = true;
                    deletePais.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<PAIS> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PAIS</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonPais()
        {
            string res = null;
            List<PAI> listPais = new List<PAI>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PAIS
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listPais.Add(new PAI
                     {
                         UNID_PAIS=row.UNID_PAIS,
                         PAIS=row.PAIS,
                         ISO=row.ISO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPais.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPais);
                }
                return res;
            }
        }
    }
}
