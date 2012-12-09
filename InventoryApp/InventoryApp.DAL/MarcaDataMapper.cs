using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class MarcaDataMapper : IDataMapper
    {

        public object getElements()
        {

            FixupCollection<MARCA> modelos = new FixupCollection<MARCA>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.MARCAs
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

            using (var entity = new TAE2Entities())
            {
                MARCA Tmp = (MARCA)element;

                res = (from tipo in entity.MARCAs
                       where tipo.UNID_MARCA == Tmp.UNID_MARCA
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
                    MARCA marca = (MARCA)element;
                    var modifiedItemStatus = entity.MARCAs.First(p => p.UNID_MARCA == marca.UNID_MARCA);
                    modifiedItemStatus.MARCA_NAME = marca.MARCA_NAME;
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
                    MARCA marca = (MARCA)element;

                    var validacion = (from cust in entity.MARCAs
                                      where cust.MARCA_NAME == marca.MARCA_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        marca.UNID_MARCA = UNID.getNewUNID();
                        //Sync
                        marca.IS_MODIFIED = true;
                        marca.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MARCAs.AddObject(marca);
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
                    MARCA marca = (MARCA)element;

                    var deleteMarca = entity.MARCAs.First(p => p.UNID_MARCA == marca.UNID_MARCA);

                    deleteMarca.IS_ACTIVE = false;
                    //Sync
                    deleteMarca.IS_MODIFIED = true;
                    deleteMarca.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<MARCA> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de MARCA</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonMarca()
        {
            string res = null;
            List<MARCA> listMarca = new List<MARCA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MARCAs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listMarca.Add(new MARCA
                     {
                         UNID_MARCA=row.UNID_MARCA,
                         MARCA_NAME=row.MARCA_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMarca.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMarca);
                }
                return res;
            }
        }
    }
}
