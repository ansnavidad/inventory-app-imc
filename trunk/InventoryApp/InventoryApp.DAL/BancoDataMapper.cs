using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class BancoDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from banco in entity.BANCOes
                         where banco.IS_ACTIVE == true
                         where banco.IS_MODIFIED == false
                         select banco.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonBanco(long? Last_Modified_Date)
        {
            string res = null;
            List<BANCO> listBanco = new List<BANCO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.BANCOes
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listBanco.Add(new BANCO
                     {
                         BANCO_NAME = row.BANCO_NAME,
                         UNID_BANCO = row.UNID_BANCO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listBanco.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listBanco);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                BANCO poco = (BANCO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.BANCOes
                                 where poco.UNID_BANCO == cust.UNID_BANCO
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        var aux = query.First();

                        if (UNID.compareUNIDS(aux.LAST_MODIFIED_DATE, poco.LAST_MODIFIED_DATE))
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }

                    var modifiedBanco = entity.BANCOes.First(p => p.UNID_BANCO == poco.UNID_BANCO);
                    modifiedBanco.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getElements()
        {
            FixupCollection<BANCO> tp = new FixupCollection<BANCO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.BANCOes
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
                BANCO banco = (BANCO)element;
                var query =( from cust in entitie.BANCOes
                            where cust.UNID_BANCO == banco.UNID_BANCO
                            select cust).ToList();
                if (query.Count >0)
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
                    BANCO banco = (BANCO)element;
                    var modifiedBanco = entity.BANCOes.First(p => p.UNID_BANCO == banco.UNID_BANCO);
                    modifiedBanco.BANCO_NAME = banco.BANCO_NAME;
                    //Sync
                    modifiedBanco.IS_MODIFIED = true;
                    modifiedBanco.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    BANCO banco = (BANCO)element;

                    var validacion = (from cust in entity.BANCOes
                                      where cust.BANCO_NAME == banco.BANCO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        banco.UNID_BANCO = UNID.getNewUNID();
                        //Sync
                        banco.IS_MODIFIED = true;
                        banco.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.BANCOes.AddObject(banco);
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
                    BANCO banco = (BANCO)element;
                    var modifiedBanco = entity.BANCOes.First(p => p.UNID_BANCO == banco.UNID_BANCO);
                    modifiedBanco.IS_ACTIVE = false;
                    //Sync
                    modifiedBanco.IS_MODIFIED = true;
                    modifiedBanco.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<BANCO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de BANCO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonBanco()
        {
            string res = null;
            List<BANCO> listBanco = new List<BANCO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.BANCOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listBanco.Add(new BANCO
                     {
                         BANCO_NAME=row.BANCO_NAME,
                         UNID_BANCO=row.UNID_BANCO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listBanco.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listBanco);
                }
                return res;
            }
        }
    }
}
