using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class LoteDataMapper : IDataMapper
    {
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
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    LOTE lote = (LOTE)element;
                    //lote.UNID_LOTE = lote.UNID_LOTE
                    //Sync
                    lote.IS_MODIFIED = true;
                    lote.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.LOTEs.AddObject(lote);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<LOTE> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de LOTE</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonLote()
        {
            string res = null;
            List<LOTE> listLote = new List<LOTE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.LOTEs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listLote.Add(new LOTE
                     {
                         UNID_LOTE=row.UNID_LOTE,
                         UNID_POM=row.UNID_POM,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listLote.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listLote);
                }
                return res;
            }
        }
    }
}
