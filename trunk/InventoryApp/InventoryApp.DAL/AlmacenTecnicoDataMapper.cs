using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class AlmacenTecnicoDataMapper : IDataMapper
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
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<ALMACEN_TECNICO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de ALMACEN_TECNICO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonAlmacenTecnico()
        {
            string res = null;
            List<ALMACEN_TECNICO> listAlmacenTecnico = new List<ALMACEN_TECNICO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ALMACEN_TECNICO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listAlmacenTecnico.Add(new ALMACEN_TECNICO
                     {
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_TECNICO = row.UNID_TECNICO,
                         IS_MODIFIED = row.IS_MODIFIED,
                         IS_ACTIVE = row.IS_ACTIVE,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listAlmacenTecnico.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listAlmacenTecnico);
                }
                return res;
            }
        }

        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                var resul0 = (from prov in entity.ALMACEN_TECNICO
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from almacentecnico in entity.ALMACEN_TECNICO
                         where almacentecnico.IS_ACTIVE == true
                         where almacentecnico.IS_MODIFIED == false
                         select almacentecnico.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonAlmacenTecnico(long? Last_Modified_Date)
        {
            string res = null;
            List<ALMACEN_TECNICO> listAlmacenTecnico = new List<ALMACEN_TECNICO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ALMACEN_TECNICO
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listAlmacenTecnico.Add(new ALMACEN_TECNICO
                     {
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_TECNICO = row.UNID_TECNICO,
                         IS_MODIFIED = row.IS_MODIFIED,
                         IS_ACTIVE = row.IS_ACTIVE,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listAlmacenTecnico.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listAlmacenTecnico);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<ALMACEN_TECNICO>
        /// </summary>
        /// <returns>Regresa List<ALMACEN_TECNICO></returns>
        /// <returns>Si regresa null</returns>
        public List<ALMACEN_TECNICO> GetDeserializeAlmacenTecnico(string listPocos)
        {
            List<ALMACEN_TECNICO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<ALMACEN_TECNICO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetAlmacenTecnico()
        {
            List<ALMACEN_TECNICO> reset = new List<ALMACEN_TECNICO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ALMACEN_TECNICO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new ALMACEN_TECNICO
                     {
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_TECNICO = row.UNID_TECNICO,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.ALMACEN_TECNICO.First(p => p.UNID_ALMACEN== item.UNID_ALMACEN && p.UNID_TECNICO==item.UNID_TECNICO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
