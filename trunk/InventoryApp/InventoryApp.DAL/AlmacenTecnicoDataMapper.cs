using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class AlmacenTecnicoDataMapper : IDataMapper
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
                         UNID_TECNICO=row.UNID_TECNICO,
                         IS_MODIFIED = row.IS_MODIFIED,
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
    }
}
