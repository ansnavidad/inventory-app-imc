using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class PedimentoDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {
            if (element != null)
            {
                PEDIMENTO poco = (PEDIMENTO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.PEDIMENTOes
                                 where poco.UNID_PEDIMENTO == cust.UNID_PEDIMENTO
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }
                }
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
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    PEDIMENTO pedimento = (PEDIMENTO)element;
                    var modifiedPedimento = entity.PEDIMENTOes.First(p => p.UNID_PEDIMENTO == pedimento.UNID_PEDIMENTO);
                    modifiedPedimento.UNID_TIPO_PEDIMENTO = pedimento.UNID_TIPO_PEDIMENTO;
                    modifiedPedimento.UNID_LOTE = pedimento.UNID_LOTE;
                    modifiedPedimento.PEDIMENTO_NUMERO = pedimento.PEDIMENTO_NUMERO;
                    modifiedPedimento.IS_ACTIVE = pedimento.IS_ACTIVE;                    
                    //Sync
                    modifiedPedimento.IS_MODIFIED = true;
                    modifiedPedimento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
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
                    PEDIMENTO pedimento = (PEDIMENTO)element;

                    var validacion = (from cust in entity.PEDIMENTOes
                                      where cust.UNID_PEDIMENTO == pedimento.UNID_PEDIMENTO
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        pedimento.UNID_PEDIMENTO = UNID.getNewUNID();
                        //Sync
                        pedimento.IS_MODIFIED = true;
                        pedimento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.PEDIMENTOes.AddObject(pedimento);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
