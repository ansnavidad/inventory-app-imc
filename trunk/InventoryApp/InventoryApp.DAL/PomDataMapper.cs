using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class PomDataMapper : IDataMapper
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
        /// Método que serializa una List<POM> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de POM</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonPom()
        {
            string res = null;
            List<POM> listPom = new List<POM>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.POMs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listPom.Add(new POM
                     {
                         UNID_POM=row.UNID_POM,
                         FECHA_POM=row.FECHA_POM,
                         UNID_COTIZACION=row.UNID_COTIZACION,
                         DIAS_ENTREGA=row.DIAS_ENTREGA,
                         FECHA_ENTREGA=row.FECHA_ENTREGA,
                         FECHA_ENTREGA_REAL=row.FECHA_ENTREGA_REAL,
                         UNID_EMPRESA=row.UNID_EMPRESA,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPom.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPom);
                }
                return res;
            }
        }
    }
}
