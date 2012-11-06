using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
     public class ModeloDataMapper : IDataMapper
    {
        public object getElements()
        {

            FixupCollection<MODELO> tp = new FixupCollection<MODELO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                (from cust in entity.MODELOes
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
            object res = null;

            using (var entity = new TAE2Entities())
            {
                MODELO Tmp = (MODELO)element;

                res = (from tipo in entity.MODELOes
                       where tipo.UNID_MODELO == Tmp.UNID_MODELO
                       select tipo).ToList();

                return res;
            }
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MODELO modelo = (MODELO)element;
                    var modifiedItemStatus = entity.MODELOes.First(p => p.UNID_MODELO == modelo.UNID_MODELO);
                    modifiedItemStatus.MODELO_NAME = modelo.MODELO_NAME;
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
                    MODELO modelo = (MODELO)element;
                    modelo.UNID_MODELO = UNID.getNewUNID();

                    entity.MODELOes.AddObject(modelo);
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
