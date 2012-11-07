using System;
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
            object o = null;
            FixupCollection<PAI> tp = new FixupCollection<PAI>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PAIS
                 select p).ToList().ForEach(d => { tp.Add(d); });

                if (tp.Count > 0)
                {
                    o = (object)tp;
                }

                return o;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                PAI Eprov = (PAI)element;
                FixupCollection<PAI> tp = new FixupCollection<PAI>();

                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.PAIS
                     where p.UNID_PAIS == Eprov.UNID_PAIS
                     select p).ToList().ForEach(d => { tp.Add(d); });

                    if (tp.Count > 0)
                    {
                        o = (object)tp;
                    }
                }
            }
            return o;
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
            throw new NotImplementedException();
        }
    }
}
