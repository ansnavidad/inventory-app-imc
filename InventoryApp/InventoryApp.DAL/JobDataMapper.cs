using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using System.Data;
using System.Data.Objects;

namespace InventoryApp.DAL
{
    public class JobDataMapper
    {
        public void executeJob()
        {
            using (TAE2Entities entity = new TAE2Entities())
            {
                entity.GetJob();
                //var query = entity.SP_TAE2_JOB();    
            }
        }
        
    }
}

