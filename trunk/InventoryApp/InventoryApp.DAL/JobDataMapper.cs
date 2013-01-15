using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.Data;
using System.Data.Objects;

namespace InventoryApp.DAL
{
    public class JobDataMapper: IDataMapper
    {
        public void executeJob()
        {
            using (TAE2Entities entity = new TAE2Entities())
            {
                entity.GetJob();
            }
        }


        public object getElements()
        {
            throw new NotImplementedException();
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

