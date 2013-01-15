using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class ProcessBachDataMapper : IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }
        /// <summary>
        /// Método que serializa una List<PROCESS_BATCH> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PROCESS_BATCH</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonProcessBach()
        {
            string res = null;
            List< PROCESS_BATCH> listProcess = new List<PROCESS_BATCH>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROCESS_BATCH
                 orderby p.ID_BATCH descending
                 select p).Take(20).ToList().ForEach(row =>
                 {
                     listProcess.Add(new PROCESS_BATCH
                     {
                         ID_BATCH= row.ID_BATCH,
                         IS_DONE= row.IS_DONE,
                         PEND_DATE= row.PEND_DATE,
                         PSTART_DATE= row.PSTART_DATE,
                         PSTATUS= row.PSTATUS,
                         ID_STATUS= row.ID_STATUS
                     });
                 });
                if (listProcess.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listProcess);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<PROCESS_BATCH>
        /// </summary>
        /// <returns>Regresa List<PROCESS_BATCH></returns>
        /// <returns>Si no regresa null</returns>
        public List<PROCESS_BATCH> GetDeserializeProcessBach(string listPocos)
        {
            List<PROCESS_BATCH> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<PROCESS_BATCH>>(listPocos);
            }

            return res;
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
