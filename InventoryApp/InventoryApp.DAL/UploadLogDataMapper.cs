using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class UploadLogDataMapper : IDataMapper
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
                    UPLOAD_LOG uploadLog = (UPLOAD_LOG)element;                    
                    uploadLog.UNID_UPLOAD_LOG = UNID.getNewUNID();                        
                    entity.SaveChanges();                    
                }
            }
        }

        public object returnPoco(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    UPLOAD_LOG uploadLog = (UPLOAD_LOG)element;
                    uploadLog.UNID_UPLOAD_LOG = UNID.getNewUNID();
                    entity.SaveChanges();

                    return (object)uploadLog;
                }
            }

            return new object();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
