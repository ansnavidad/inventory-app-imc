using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class BatchLoadDataMapper : IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        /// <summary>
        /// Método que serializa una List<BATCH_LOAD_CARGAMOV> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de BATCH_LOAD_CARGAMOV</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonBachLoad()
        {
            string res = null;
            List<BATCH_LOAD_CARGAMOV> listBatch = new List<BATCH_LOAD_CARGAMOV>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.BATCH_LOAD_CARGAMOV
                 orderby p.ID_BATCH descending
                 select p).Take(20).ToList().ForEach(row =>
                 {
                     listBatch.Add(new BATCH_LOAD_CARGAMOV
                     {
                         ID_BATCH = row.ID_BATCH,
                         START_EXEC_DATE = row.START_EXEC_DATE,
                         END_EXEC_DATE = row.END_EXEC_DATE
                     });
                 });
                if (listBatch.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listBatch);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que serializa una List<LOG_LOAD_CARGAMOV> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de BATCH_LOAD_CARGAMOV</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonLogLoad(int? idBatch)
        {
            string res = null;
            List<LOG_LOAD_CARGAMOV> listLogLoad = new List<LOG_LOAD_CARGAMOV>();
            if (idBatch!=null)
            {
                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.LOG_LOAD_CARGAMOV
                     where p.ID_BATCH ==idBatch
                     select p).ToList().ForEach(row =>
                     {
                         listLogLoad.Add(new LOG_LOAD_CARGAMOV
                         {
                             ID = row.ID,
                             ID_BATCH = row.ID_BATCH,
                             EXEC_DATE = row.EXEC_DATE,
                             GROUP_MSG = row.GROUP_MSG,
                             MSG = row.MSG
                         });
                     });
                    if (listLogLoad.Count > 0)
                    {
                       res = SerializerJson.SerializeParametros(listLogLoad);
                    }
                }    
            }
            return res;
        }

        /// <summary>
        /// Método que valida si hay proceso corriendo en la tabla BATCH_LOAD_CARGAMOV
        /// </summary>
        /// <returns>Regresa un true o false</returns>
        public bool GetBatchProcessRunning()
        {
            bool res = true;
            
            using (var Entity = new TAE2Entities())
            {
                var query = (from p in Entity.BATCH_LOAD_CARGAMOV
                             where p.END_EXEC_DATE == null
                             select p).ToList();

                if (query.Count != 0)
                    return false;
                
                return res;
            }
        }

        /// <summary>
        /// Método que manda a ejecutar un store procedure y asu vez un job
        /// </summary>
        /// <returns></returns>
        public void GetExecuteJob()
        {
            using (TAE2Entities entity = new TAE2Entities())
            {
                entity.GetJob();
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a FixupCollection<BATCH_LOAD_CARGAMOV>
        /// </summary>
        /// <returns>Regresa FixupCollection<BATCH_LOAD_CARGAMOV></returns>
        /// <returns>Si no regresa null</returns>
        public FixupCollection<BATCH_LOAD_CARGAMOV> GetDeserializeBatchLoad(string listPocos)
        {
            FixupCollection<BATCH_LOAD_CARGAMOV> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<FixupCollection<BATCH_LOAD_CARGAMOV>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que Deserializa JSon a FixupCollection<LOG_LOAD_CARGAMOV>
        /// </summary>
        /// <returns>Regresa FixupCollection<LOG_LOAD_CARGAMOV></returns>
        /// <returns>Si no regresa null</returns>
        public FixupCollection<LOG_LOAD_CARGAMOV> GetDeserializeLogLoad(string listPocos)
        {
            FixupCollection<LOG_LOAD_CARGAMOV> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<FixupCollection<LOG_LOAD_CARGAMOV>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que regresa True o False<BATCH_LOAD_CARGAMOV>
        /// </summary>
        /// <returns>Regresa List<BATCH_LOAD_CARGAMOV></returns>
        /// <returns>Si no regresa null</returns>
        public bool GetDeserializeBatchLoadBool(string listPocos)
        {

            if (listPocos == "True")
                return true;
            else
                return false;

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

        public void deleteElement(object element, POCOS.USUARIO u)
        {
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
