using System;
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
            object res = null;
            using (var oAWEntities = new TAE2Entities())
            {
                var query = from marca in oAWEntities.MARCAs
                            select marca;

                if (query != null)
                {
                    res = query;
                }
                return res;
            }
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
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
