using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.Data;

namespace InventoryApp.DAL
{
    public class ProyectoDataMapper : IDataMapper
    {

        public object getElements()
        {
            FixupCollection<PROYECTO> tp = new FixupCollection<PROYECTO>();

            object res = null;

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.PROYECTOes
                           where cust.IS_ACTIVE ==true
                           select cust).ToList();

               if (query.Count > 0)
                {
                    res = query;
                }
                return res;
            }
        }

        public object getElement(object element)
        {
            FixupCollection<PROYECTO> tp = new FixupCollection<PROYECTO>();

            object res = null;

            using (var entity = new TAE2Entities())
            {
                PROYECTO EPro = (PROYECTO)element;

                (from cust in entity.PROYECTOes
                            where cust.UNID_PROYECTO==EPro.UNID_PROYECTO
                 select cust).ToList().ForEach(d => { tp.Add(d); });

                if (tp.Count > 0)
                {
                    res = tp;
                }

                return res;
            }
        }

        public void udpateElement(object element)
        {
            using (var entity = new TAE2Entities())
            {
                PROYECTO EPro = (PROYECTO)element;

                var query = (from cust in entity.PROYECTOes
                            where cust.UNID_PROYECTO==EPro.UNID_PROYECTO
                            select cust).ToList();

                if (query.Count>0)
                {
                    var pro = query.First();

                    pro.PROYECTO_NAME = EPro.PROYECTO_NAME;

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
                    PROYECTO Proyecto = (PROYECTO)element;

                    var validacion = (from cust in entity.PROYECTOes
                                      where cust.PROYECTO_NAME == Proyecto.PROYECTO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        Proyecto.UNID_PROYECTO = UNID.getNewUNID();
                        entity.PROYECTOes.AddObject(Proyecto);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PROYECTO proyecto = (PROYECTO)element;

                    var deleteProyecto = entity.PROYECTOes.First(p => p.UNID_PROYECTO == proyecto.UNID_PROYECTO);

                    deleteProyecto.IS_ACTIVE = false;

                    entity.SaveChanges();
                }
            }
        }
    }
}
