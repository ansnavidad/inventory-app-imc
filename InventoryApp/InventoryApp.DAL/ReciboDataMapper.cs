﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class ReciboDataMapper:IDataMapper
    {
        public List<RECIBO> getListElements()
        {
            List<RECIBO> recibos = new List<RECIBO>();

            try
            {
                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.RECIBOes
                     select p).ToList<RECIBO>().ForEach(o => recibos.Add(o));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return recibos;
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
            if (element != null && (element as RECIBO) != null)
            {
                using (var entity = new TAE2Entities())
                {
                    RECIBO item = (RECIBO)element;

                    entity.RECIBOes.AddObject(item);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
