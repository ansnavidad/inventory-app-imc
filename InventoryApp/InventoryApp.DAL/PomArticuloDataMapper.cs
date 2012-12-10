using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class PomArticuloDataMapper : IDataMapper
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
        /// Método que serializa una List<POM_ARTICULO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de POM_ARTICULO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonPomArticulo()
        {
            string res = null;
            List<POM_ARTICULO> listPomArticulo = new List<POM_ARTICULO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.POM_ARTICULO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listPomArticulo.Add(new POM_ARTICULO
                     {
                         UNID_POM = row.UNID_POM,
                         UNID_POM_ARTICULO=row.UNID_POM_ARTICULO,
                         UNID_ARTICULO=row.UNID_ARTICULO,
                         CANTIDAD=row.CANTIDAD,
                         COSTO_UNITARIO=row.COSTO_UNITARIO,
                         IVA=row.IVA,
                         TC=row.TC,
                         DESCUENTO=row.DESCUENTO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPomArticulo.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPomArticulo);
                }
                return res;
            }
        }
    }
}
