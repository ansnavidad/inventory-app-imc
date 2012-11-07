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
            FixupCollection<MONEDA> tp = new FixupCollection<MONEDA>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                (from cust in entity.MONEDAs
                 select cust).ToList().ForEach(d => { tp.Add(d); });

                if (tp.Count > 0)
                {
                    res = tp;
                }

                return res;
            } throw new NotImplementedException();
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
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MONEDA moneda = (MONEDA)element;
                    var modifiedMoneda = entity.MONEDAs.First(p => p.UNID_MONEDA == moneda.UNID_MONEDA);
                    modifiedMoneda.MONEDA_NAME = moneda.MONEDA_NAME;
                    modifiedMoneda.MONEDA_ABR = moneda.MONEDA_ABR;
                    entity.SaveChanges();
                }
            }
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MONEDA moneda = (MONEDA)element;
                    moneda.UNID_MONEDA = UNID.getNewUNID();
                    entity.MONEDAs.AddObject(moneda);
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
