using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class SyncDataMapper : IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public bool existDummy() {

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.SYNCs
                             where cust.UNID_SYNC == 20120101000000000
                             select cust).ToList();
                if (query.Count > 0)
                    return true;
                return false;                
            }
        }

        public bool Dummy()
        {

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.SYNCs
                             where cust.ACTUAL_DATE !=0
                             select cust).ToList();
                if (query.Count > 0)
                    return true;
                return false;
            }
        }

        public void ResetDummy()
        {
            using (var Entity = new TAE2Entities())
            {
                var modified = Entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                modified.ACTUAL_DATE =0;
                Entity.SaveChanges();
                
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
