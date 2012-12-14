using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class PedimentoDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from ped in entity.PEDIMENTOes
                         where ped.IS_ACTIVE == true
                         where ped.IS_MODIFIED == false
                         select ped.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonPedimento(long? LastModifiedDate)
        {
            string res = null;
            List<PEDIMENTO> listPedimento = new List<PEDIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PEDIMENTOes
                 where p.LAST_MODIFIED_DATE > LastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listPedimento.Add(new PEDIMENTO
                     {
                         UNID_PEDIMENTO = row.UNID_PEDIMENTO,
                         UNID_LOTE = row.UNID_LOTE,
                         UNID_TIPO_PEDIMENTO = row.UNID_TIPO_PEDIMENTO,
                         PEDIMENTO_NUMERO = row.PEDIMENTO_NUMERO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPedimento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPedimento);
                }
                return res;
            }
        }

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
                        var aux = query.First();

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }

                    var modifiedMenu = entity.PEDIMENTOes.First(p => p.UNID_PEDIMENTO == poco.UNID_PEDIMENTO);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
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
        /// <summary>
        /// Método que serializa una List<PEDIMENTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de PEDIMENTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonPedimento()
        {
            string res = null;
            List<PEDIMENTO> listPedimento = new List<PEDIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.PEDIMENTOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listPedimento.Add(new PEDIMENTO
                     { 
                         UNID_PEDIMENTO=row.UNID_PEDIMENTO,
                         UNID_LOTE = row.UNID_LOTE,
                         UNID_TIPO_PEDIMENTO=row.UNID_TIPO_PEDIMENTO,
                         PEDIMENTO_NUMERO=row.PEDIMENTO_NUMERO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPedimento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPedimento);
                }
                return res;
            }
        }
    }
}
