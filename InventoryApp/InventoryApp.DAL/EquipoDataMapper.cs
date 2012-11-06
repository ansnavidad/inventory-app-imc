using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class EquipoDataMapper : IDataMapper
    {
        public object getElements()
        {

            FixupCollection<EQUIPO> equipos = new FixupCollection<EQUIPO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                (from cust in entity.EQUIPOes
                 select cust).ToList().ForEach(d => { equipos.Add(d); });

                if (equipos.Count > 0)
                {
                    res = equipos;
                }

                return res;
            }

        }

        public object getElement(object element)
        {
            object res = null;

            using (var entity = new TAE2Entities())
            {
                EQUIPO Tmp = (EQUIPO)element;

                res = (from tipo in entity.EQUIPOes
                       where tipo.UNID_EQUIPO == Tmp.UNID_EQUIPO
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
                    EQUIPO equipo = (EQUIPO)element;
                    var modifiedItemStatus = entity.EQUIPOes.First(p => p.UNID_EQUIPO == equipo.UNID_EQUIPO);
                    modifiedItemStatus.EQUIPO_NAME = equipo.EQUIPO_NAME;
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
                    EQUIPO equipo = (EQUIPO)element;
                    equipo.UNID_EQUIPO = UNID.getNewUNID();

                    entity.EQUIPOes.AddObject(equipo);
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
