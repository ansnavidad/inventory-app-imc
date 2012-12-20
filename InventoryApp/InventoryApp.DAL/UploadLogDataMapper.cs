using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class UploadLogDataMapper : IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
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
        public void InsertUploadLogLocal(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    UPLOAD_LOG uploadLog = (UPLOAD_LOG)element;
                    entity.UPLOAD_LOG.AddObject(uploadLog);
                    entity.SaveChanges();
                }
            }
        }
        public string InsertUploadLog(object element)
        {
            string res = null;

            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    UPLOAD_LOG uploadLog = (UPLOAD_LOG)element;
                    uploadLog.UNID_UPLOAD_LOG = UNID.getNewUNID();
                    entity.UPLOAD_LOG.AddObject(uploadLog);
                    entity.SaveChanges();
                    res = GetJsonUpLoadLog(uploadLog);
                }
            }
            return res;
        }

        public string InsertUploadLogNew(object element)
        {
            string res = null;

            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    UPLOAD_LOG uploadLog = (UPLOAD_LOG)element;
                    entity.UPLOAD_LOG.AddObject(uploadLog);
                    entity.SaveChanges();
                    res = GetJsonUpLoadLog(uploadLog);
                }
            }
            return res;
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa un POCO UPLOAD_LOG a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de UPLOAD_LOG</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonUpLoadLog(UPLOAD_LOG upLoadLog)
        {
            string res = null;
            res = SerializerJson.SerializeParametros(upLoadLog);
            return res;
            
        }

        /// <summary>
        /// Método que Deserializa JSon a List<UPLOAD_LOG>
        /// </summary>
        /// <returns>Regresa List<UPLOAD_LOG></returns>
        /// <returns>Si no regresa null</returns>
        public List<UPLOAD_LOG> GetDeserializeUpLoadLog1(string upLoadLog)
        {
            List<UPLOAD_LOG> listUpLoadLog = null;
            UPLOAD_LOG up;

            if (!String.IsNullOrEmpty(upLoadLog))
            {
                up = JsonConvert.DeserializeObject<UPLOAD_LOG>(upLoadLog);
                listUpLoadLog = new List<UPLOAD_LOG>();
                listUpLoadLog.Add(up);
            }
            return listUpLoadLog;
        }

        /// <summary>
        /// Método que Deserializa JSon a List<UPLOAD_LOG>
        /// </summary>
        /// <returns>Regresa UPLOAD_LOG</returns>
        /// <returns>Si no regresa null</returns>
        public UPLOAD_LOG GetDeserializeUpLoadLog(string upLoadLog)
        {
            //UPLOAD_LOG listUpLoadLog = null;
            UPLOAD_LOG up=null;

            if (!String.IsNullOrEmpty(upLoadLog))
            {
                up = JsonConvert.DeserializeObject<UPLOAD_LOG>(upLoadLog);
                
            }
            return up;
        }

        /// <summary>
        /// Método que Deserializa JSon a UPLOAD_LOG
        /// </summary>
        /// <returns>Regresa Dictionary<string,object></returns>
        /// <returns>Si no regresa null</returns>
        public bool GetDeserializeUpLoad(string upLoadLog)
        {
            bool val = false;
            Dictionary<string, object> res = new Dictionary<string, object>();
            List<UPLOAD_LOG> listUpLoadLog = new List<UPLOAD_LOG>();
            UPLOAD_LOG up = new UPLOAD_LOG();
            try
            {
                if (!String.IsNullOrEmpty(upLoadLog))
                {
                    //Prueba
                    res = (Dictionary<string, object>)JsonConvert.DeserializeObject(upLoadLog, typeof(Dictionary<string, object>));
                    foreach (var item in res)
                    {
                        up = JsonConvert.DeserializeObject<UPLOAD_LOG>(item.Value.ToString());
                    }
                    //inserta a la base local
                    InsertUploadLogLocal(up);
                    val = true;
                }
            }
            catch (Exception)
            {

                val = false;
            }

            return val;
        }
    }
}
