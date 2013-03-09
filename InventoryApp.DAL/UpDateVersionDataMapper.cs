using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class UpDateVersionDataMapper
    {

        /// <summary>
        /// Método que serializa una List<UPDATE> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de UPDATE</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonVersion()
        {
            string res = null;
            List<UPDATE> listUpdate = new List<UPDATE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.UPDATEs
                 select p).ToList().ForEach(row =>
                 {
                     listUpdate.Add(new UPDATE
                     {
                      UNID_UPDATE= row.UNID_UPDATE,
                      VERSION= row.VERSION 
                     });
                 });
                if (listUpdate.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listUpdate);
                }
                return res;
            }
        }

        public List<UPDATE> GetDeserializeUpDate(string listPocos)
        {
            List<UPDATE> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<UPDATE>>(listPocos);
            }

            return res;
        }

        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }
    }
}
