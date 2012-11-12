using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class Solicitante1DataMapper : IDataMapper
    {
        public object getElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.SOLICITANTE1
                           where p.IS_ACTIVE == true
                           select p                           
                           ).ToList();
                return res;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                SOLICITANTE1 Eprov = (SOLICITANTE1)element;

                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.SOLICITANTE1
                               where p.UNID_SOLICITANTE == Eprov.UNID_SOLICITANTE && p.IS_ACTIVE == true
                               select p).ToList();

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
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
