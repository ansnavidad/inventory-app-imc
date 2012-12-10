using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class PedimentoDataMapper : IDataMapper
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
        /// Método que serializa una List<PEDIMENTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PEDIMENTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonPedimento()
        {
            string res = null;
            List<PEDIMENTO> listPedimento = new List<PEDIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PEDIMENTOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listPedimento.Add(new PEDIMENTO
                     { 
                         UNID_PEDIMENTO=row.UNID_PEDIMENTO,
                         UNID_LOTE = row.UNID_LOTE,
                         UNID_TIPO_PEDIMENTO=row.UNID_TIPO_PEDIMENTO,
                         PEDIMENTO_NUMERO=row.PEDIMENTO_NUMERO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPedimento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPedimento);
                }
                return res;
            }
        }
    }
}
