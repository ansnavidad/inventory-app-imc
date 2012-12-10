using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class ReciboStatusDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {

            if (element != null)
            {
                RECIBO_STATUS poco = (RECIBO_STATUS)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.RECIBO_STATUS
                                 where poco.UNID_RECIBO_STATUS == cust.UNID_RECIBO_STATUS
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }
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
                    RECIBO_STATUS reciboS = (RECIBO_STATUS)element;
                    var modifiedReciboS = entity.RECIBO_STATUS.First(p => p.UNID_RECIBO_STATUS == reciboS.UNID_RECIBO_STATUS);
                    modifiedReciboS.RECIBO_STATUS_NAME = reciboS.RECIBO_STATUS_NAME;                    
                    //Sync
                    modifiedReciboS.IS_MODIFIED = true;
                    modifiedReciboS.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    RECIBO_STATUS reciboS = (RECIBO_STATUS)element;

                    var validacion = (from cust in entity.RECIBO_STATUS
                                      where cust.UNID_RECIBO_STATUS == reciboS.UNID_RECIBO_STATUS
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        reciboS.UNID_RECIBO_STATUS = UNID.getNewUNID();
                        //Sync
                        reciboS.IS_MODIFIED = true;
                        reciboS.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.RECIBO_STATUS.AddObject(reciboS);
                        entity.SaveChanges();
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
