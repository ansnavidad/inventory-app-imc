using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    class TipoPedimentoDataMapper : IDataMapper
    {
        public object getElements()
        {
            object o = null;
            FixupCollection<TIPO_PEDIMENTO> tp = new FixupCollection<TIPO_PEDIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TIPO_PEDIMENTO
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
                TIPO_PEDIMENTO Eprov = (TIPO_PEDIMENTO)element;
                FixupCollection<TIPO_PEDIMENTO> tp = new FixupCollection<TIPO_PEDIMENTO>();

                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.TIPO_PEDIMENTO
                     where p.UNID_TIPO_PEDIMENTO == Eprov.UNID_TIPO_PEDIMENTO
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
                    TIPO_PEDIMENTO tipoPedimento = (TIPO_PEDIMENTO)element;

                    var modifiedTipoPedimento = entity.TIPO_PEDIMENTO.First(p => p.UNID_TIPO_PEDIMENTO == tipoPedimento.UNID_TIPO_PEDIMENTO);
                    modifiedTipoPedimento.NOTA = tipoPedimento.NOTA;
                    modifiedTipoPedimento.CLAVE = tipoPedimento.CLAVE;
                    modifiedTipoPedimento.REGIMEN = tipoPedimento.REGIMEN;
                    modifiedTipoPedimento.TIPO_PEDIMENTO_NAME = tipoPedimento.TIPO_PEDIMENTO_NAME;

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
                    TIPO_PEDIMENTO tipoPedimento = (TIPO_PEDIMENTO)element;

                    tipoPedimento.UNID_TIPO_PEDIMENTO = UNID.getNewUNID();

                    entity.TIPO_PEDIMENTO.AddObject(tipoPedimento);
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
