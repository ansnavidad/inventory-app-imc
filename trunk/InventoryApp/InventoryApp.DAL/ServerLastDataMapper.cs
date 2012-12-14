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
        public bool existDumy() {  
            
            using (var entity = new TAE2Entities())
            {

                var query = (from ser in entity.SERVER_LASTDATA
                             select ser).ToList();

                if (query.Count == 0)
                    return false;
                return true;
            }
        }

        public void insertDummy(){
                        
            using (var entity = new TAE2Entities())
            {
                var query = (from ser in entity.SERVER_LASTDATA
                             select ser).ToList();

                if (query.Count == 0)
                {
                    var modifiedServer = entity.SERVER_LASTDATA.First(p => p.UNID_SERVER_LASTDATA == 20120101000000000);
                    modifiedServer.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                }
            }            
        }

        public void insertElement()
        {
            using (var entity = new TAE2Entities())
            {
                var query = (from ser in entity.SERVER_LASTDATA
                             select ser).ToList();

                if (query.Count > 0)
                {
                    var modifiedServer = entity.SERVER_LASTDATA.First(p => p.UNID_SERVER_LASTDATA == 20120101000000000);
                    modifiedServer.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                }
                else
                {

                    SERVER_LASTDATA server = new SERVER_LASTDATA();
                    server.UNID_SERVER_LASTDATA = 20120101000000000;
                    server.ACTUAL_DATE = UNID.getNewUNID();

                    entity.SERVER_LASTDATA.AddObject(server);
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
