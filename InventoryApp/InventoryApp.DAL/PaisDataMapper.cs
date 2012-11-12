﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class PaisDataMapper : IDataMapper
    {
        public object getElements()
        {
            object res = null;
            FixupCollection<PAI> tp = new FixupCollection<PAI>();
            using (var Entity = new TAE2Entities())
            {
                var query= (from p in Entity.PAIS
                            where p.IS_ACTIVE ==true
                            select p).ToList();

                if (query.Count > 0)
                {
                    res = query;
                }

                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;
            if (element != null)
            {
                PAI Eprov = (PAI)element;
                FixupCollection<PAI> tp = new FixupCollection<PAI>();
                using (var Entity = new TAE2Entities())
                {
                   var query= (from p in Entity.PAIS
                     where p.UNID_PAIS == Eprov.UNID_PAIS
                     select p).ToList();

                   if (query.Count > 0)
                   {
                        res = query;
                   }
                }
            }
            return res;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PAI pais = (PAI)element;
                    var modifiedPais = entity.PAIS.First(p => p.UNID_PAIS == pais.UNID_PAIS);
                    modifiedPais.PAIS = pais.PAIS;
                    modifiedPais.ISO = pais.ISO;
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
                    PAI pais = (PAI)element;

                    pais.UNID_PAIS = UNID.getNewUNID();
                    entity.PAIS.AddObject(pais);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PAI pais = (PAI)element;

                    var deletePais = entity.PAIS.First(p => p.UNID_PAIS == pais.UNID_PAIS);

                    deletePais.IS_ACTIVE = false;

                    entity.SaveChanges();
                }
            }
        }
    }
}