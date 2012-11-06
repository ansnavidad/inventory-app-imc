﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class MonedaDataMapper : IDataMapper
    {
        public object getElements()
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                res = (from monedas in entitie.MONEDAs select monedas).ToList();
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                MONEDA moneda = (MONEDA)element;

                var query = from cust in entitie.MONEDAs
                            where cust.UNID_MONEDA == moneda.UNID_MONEDA
                            select cust;
                if (query != null)
                {
                    res = query;
                }
                return res;
            }
        }

        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entitie = new TAE2Entities())
                {
                    MONEDA moneda = (MONEDA)element;
                    entitie.MONEDAs.AddObject(moneda);
                    entitie.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}