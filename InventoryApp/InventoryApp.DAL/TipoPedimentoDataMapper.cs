using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class TipoPedimentoDataMapper : IDataMapper
    {
        public object getElements()
        {
            object o = null;
            FixupCollection<TIPO_PEDIMENTO> tp = new FixupCollection<TIPO_PEDIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                var query = (from p in Entity.TIPO_PEDIMENTO
                             where p.IS_ACTIVE ==true
                             select p).ToList();

                if (query.Count > 0)
                {
                    o = query;
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
                   var query= (from p in Entity.TIPO_PEDIMENTO
                                where p.UNID_TIPO_PEDIMENTO == Eprov.UNID_TIPO_PEDIMENTO
                                select p).ToList();

                   if (query.Count > 0)
                    {
                        o = query;
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
                    modifiedTipoPedimento.TIPO_PEDIMENTO_NAME = tipoPedimento.TIPO_PEDIMENTO_NAME;
                    modifiedTipoPedimento.REGIMEN = tipoPedimento.REGIMEN;
                    modifiedTipoPedimento.NOTA = tipoPedimento.NOTA;
                    modifiedTipoPedimento.CLAVE = tipoPedimento.CLAVE;

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
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TIPO_PEDIMENTO tipoPedimento = (TIPO_PEDIMENTO)element;

                    var deleteTipoPedimento = entity.TIPO_PEDIMENTO.First(p => p.UNID_TIPO_PEDIMENTO == tipoPedimento.UNID_TIPO_PEDIMENTO);

                    deleteTipoPedimento.IS_ACTIVE = false;

                    entity.SaveChanges();
                }
            }
        }
    }
}
