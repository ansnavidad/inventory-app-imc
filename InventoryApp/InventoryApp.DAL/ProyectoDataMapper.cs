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
                (from cust in entity.PROYECTOes
                             select cust).ToList().ForEach(d => { tp.Add(d); });

                if (tp.Count > 0)
                {
                    res = tp;
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
                    PROYECTO EPro = (PROYECTO)element;

                    PROYECTO pro = new PROYECTO();

                    pro.UNID_PROYECTO = UNID.getNewUNID();

                    pro.PROYECTO_NAME = EPro.PROYECTO_NAME;

                    entity.PROYECTOes.AddObject(pro);

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
