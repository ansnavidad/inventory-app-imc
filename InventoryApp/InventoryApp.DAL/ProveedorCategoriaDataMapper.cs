using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class ProveedorCategoriaDataMapper : IDataMapper
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
        /// Método que serializa una List<PROVEEDOR_CATEGORIA> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PROVEEDOR_CATEGORIA</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonProveedorCategoria()
        {
            string res = null;
            List<PROVEEDOR_CATEGORIA> listProveedorCategoria = new List<PROVEEDOR_CATEGORIA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PROVEEDOR_CATEGORIA
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listProveedorCategoria.Add(new PROVEEDOR_CATEGORIA
                     {
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         UNID_CATEGORIA= row.UNID_CATEGORIA,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listProveedorCategoria.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listProveedorCategoria);
                }
                return res;
            }
        }
    }
}
