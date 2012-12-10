using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class ClienteDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {
            if (element != null)
            {
                CLIENTE poco = (CLIENTE)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.CLIENTEs
                                 where poco.UNID_CLIENTE == cust.UNID_CLIENTE
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
                    //Sync
                    modifiedCliente.IS_MODIFIED = true;
                    modifiedCliente.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    CLIENTE cliente = (CLIENTE)element;

                    var validacion = (from cust in entity.CLIENTEs
                                      where cust.CLIENTE1 == cliente.CLIENTE1
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        cliente.UNID_CLIENTE = UNID.getNewUNID();
                        //Sync
                        cliente.IS_MODIFIED = true;
                        cliente.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.CLIENTEs.AddObject(cliente);
                        entity.SaveChanges();
                    }
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
                    //Sync
                    modifiedCliente.IS_MODIFIED = true;
                    modifiedCliente.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<CLIENTE> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de CLIENTE</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonCliente()
        {
            string res = null;
            List<CLIENTE> listCliente = new List<CLIENTE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.CLIENTEs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listCliente.Add(new CLIENTE
                     {
                         CLIENTE1=row.CLIENTE1,
                         UNID_CLIENTE=row.UNID_CLIENTE,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listCliente.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listCliente);
                }
                return res;
            }
        }
    }
}
