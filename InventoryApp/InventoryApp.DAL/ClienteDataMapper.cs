using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class ClienteDataMapper : IDataMapper
    {
        public List<CLIENTE> getClienteList()
        {
            List<CLIENTE> clientes = new List<CLIENTE>();

            try
            {
                using (var entitie = new TAE2Entities())
                {
                    (from ctes in entitie.CLIENTEs
                     select ctes).ToList<CLIENTE>().ForEach(o => clientes.Add(new CLIENTE()
                     {
                         UNID_CLIENTE = o.UNID_CLIENTE
                         ,
                         CLIENTE1 = o.CLIENTE1
                         ,
                         IS_ACTIVE = o.IS_ACTIVE
                     }));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return clientes;
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
