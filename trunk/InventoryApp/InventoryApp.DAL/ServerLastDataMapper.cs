using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class ServerLastDataMapper :IDataMapper
    {
        public long GetDeserializeServerLast(string listPocos)
        {
            long res = 0;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<long>(listPocos);
            }

            return res;
        }

        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }
        
        public long GetServerLastFecha()
        {
            long res = 0;
            try
            {
                using (var entity = new TAE2Entities())
                {
                    var serverFecha = entity.SERVER_LASTDATA.First(p => p.UNID_SERVER_LASTDATA == 20120101000000000);
                    res = serverFecha.ACTUAL_DATE;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public void updateDumy() {

            try
            {
                using (var entity = new TAE2Entities())
                {
                    var modifiedSync = entity.SERVER_LASTDATA.First(p => p.UNID_SERVER_LASTDATA == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }

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
            using (var entity = new TAE2Entities())
            {
                long aux = (long)element;

                var modifiedServer = entity.SERVER_LASTDATA.First(p => p.UNID_SERVER_LASTDATA == 20120101000000000);
                modifiedServer.ACTUAL_DATE = aux;
                entity.SaveChanges();
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
