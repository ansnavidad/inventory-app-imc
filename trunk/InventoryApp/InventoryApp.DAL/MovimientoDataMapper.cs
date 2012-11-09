using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class MovimientoDataMapper : IDataMapper
    {
        public object getElements()
        {            
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           select p).ToList();

                foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                {
                    //Para conservar las prop. de navegación
                }

                return res;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                MOVIMENTO Eprov = (MOVIMENTO)element;

                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.MOVIMENTOes
                               where p.UNID_MOVIMIENTO == Eprov.UNID_MOVIMIENTO
                               select p).ToList();

                    foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                    {
                        //Para conservar las prop. de navegación
                    }

                    o = (object)res;
                }
            }
            return o;
        }

        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MOVIMENTO movimiento = (MOVIMENTO)element;

                    entity.MOVIMENTOes.AddObject(movimiento);
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
