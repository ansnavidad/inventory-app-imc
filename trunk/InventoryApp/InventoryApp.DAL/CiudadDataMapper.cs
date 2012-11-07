using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class CiudadDataMapper : IDataMapper
    {
        public object getElements()
        {
            object o = null;
            FixupCollection<CIUDAD> tp = new FixupCollection<CIUDAD>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.CIUDADs
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
                CIUDAD Eprov = (CIUDAD)element;
                FixupCollection<CIUDAD> tp = new FixupCollection<CIUDAD>();

                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.CIUDADs
                     where p.UNID_CIUDAD == Eprov.UNID_CIUDAD
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
                    CIUDAD ciudad = (CIUDAD)element;
                    var modifiedCiudad = entity.CIUDADs.First(p => p.UNID_CIUDAD == ciudad.UNID_CIUDAD);                    
                    modifiedCiudad.ISO = ciudad.ISO;
                    modifiedCiudad.CIUDAD1 = ciudad.CIUDAD1;                    

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
                    CIUDAD ciudad = (CIUDAD)element;

                    ciudad.UNID_CIUDAD = UNID.getNewUNID();
                    entity.CIUDADs.AddObject(ciudad);
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

