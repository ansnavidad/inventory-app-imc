using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

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
    }
}
