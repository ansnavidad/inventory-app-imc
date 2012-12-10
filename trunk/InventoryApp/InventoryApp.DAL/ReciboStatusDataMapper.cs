﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class ReciboStatusDataMapper : IDataMapper
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
        /// Método que serializa una List<RECIBO_STATUS> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de RECIBO_STATUS</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonReciboStatus()
        {
            string res = null;
            List<RECIBO_STATUS> listReciboStatus = new List<RECIBO_STATUS>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.RECIBO_STATUS
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listReciboStatus.Add(new RECIBO_STATUS
                     {
                         UNID_RECIBO_STATUS=row.UNID_RECIBO_STATUS,
                         RECIBO_STATUS_NAME=row.RECIBO_STATUS_NAME,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listReciboStatus.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listReciboStatus);
                }
                return res;
            }
        }
    }
}
