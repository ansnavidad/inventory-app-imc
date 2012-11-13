using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class ClienteDataMapper : IDataMapper
    {
        public object getElements()
        {
            FixupCollection<CLIENTE> tp = new FixupCollection<CLIENTE>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.CLIENTEs
                             where cust.IS_ACTIVE == true
                             select cust).ToList();
                if (query.Count > 0)
                {
                    res = query;
                }
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                CLIENTE cliente = (CLIENTE)element;
                var query = (from cust in entitie.CLIENTEs
                             where cust.UNID_CLIENTE == cliente.UNID_CLIENTE
                             select cust).ToList();
                if (query.Count > 0)
                {
                    res = query;
                }
                return res;
            }
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    CLIENTE cliente = (CLIENTE)element;
                    var modifiedCliente = entity.CLIENTEs.First(p => p.UNID_CLIENTE == cliente.UNID_CLIENTE);
                    modifiedCliente.CLIENTE1 = cliente.CLIENTE1;
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
                    CLIENTE cliente = (CLIENTE)element;
                    cliente.UNID_CLIENTE = UNID.getNewUNID();
                    entity.CLIENTEs.AddObject(cliente);
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
                    CLIENTE cliente = (CLIENTE)element;
                    var modifiedCliente = entity.CLIENTEs.First(p => p.UNID_CLIENTE == cliente.UNID_CLIENTE);
                    modifiedCliente.IS_ACTIVE = false;
                    entity.SaveChanges();
                }
            }
        }
    }
}
