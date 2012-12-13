using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class ServerLastDataMapper :IDataMapper
    {


        public bool updateDumy() {

            try
            {
                using (var entity = new TAE2Entities())
                {
                    var modifiedSync = entity.SERVER_LASTDATA.First(p => p.UNID_SERVER_LASTDATA == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;                
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
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
