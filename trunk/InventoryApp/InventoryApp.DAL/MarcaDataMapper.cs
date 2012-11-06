﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class MarcaDataMapper : IDataMapper
    {

        public object getElements()
        {

            FixupCollection<MARCA> modelos = new FixupCollection<MARCA>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                (from cust in entity.MARCAs
                 select cust).ToList().ForEach(d => { modelos.Add(d); });

                if (modelos.Count > 0)
                {
                    res = modelos;
                }

                return res;
            }

        }

        public object getElement(object element)
        {
            object res = null;

            using (var entity = new TAE2Entities())
            {
                MARCA Tmp = (MARCA)element;

                res = (from tipo in entity.MARCAs
                       where tipo.UNID_MARCA == Tmp.UNID_MARCA
                       select tipo).ToList();

                return res;
            }
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MARCA marca = (MARCA)element;
                    var modifiedItemStatus = entity.MARCAs.First(p => p.UNID_MARCA == marca.UNID_MARCA);
                    modifiedItemStatus.MARCA_NAME = marca.MARCA_NAME;
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
                    MARCA marca = (MARCA)element;
                    marca.UNID_MARCA = UNID.getNewUNID();

                    entity.MARCAs.AddObject(marca);
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
