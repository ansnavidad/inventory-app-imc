﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class BancoDataMapper : IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                var resul0 = (from prov in entity.BANCOes
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

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

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElementSync((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElementSync((object)poco);
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

        public void udpateElement(object element, USUARIO u)
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
                    //Master
                    UNID.Master(banco, u, -1, "Modificación");
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    BANCO banco = (BANCO)element;
                    var modifiedBanco = entity.BANCOes.First(p => p.UNID_BANCO == banco.UNID_BANCO);
                    modifiedBanco.BANCO_NAME = banco.BANCO_NAME;
                    modifiedBanco.IS_ACTIVE = banco.IS_ACTIVE;
                    //Sync
                    modifiedBanco.IS_MODIFIED = true;
                    modifiedBanco.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //                    
                }
            }
        }

        public void insertElement(object element, USUARIO u)
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
                        //Master
                        UNID.Master(banco, u, -1, "Inserción");
                    }
                }
            }
        }

        public void insertElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    BANCO banco = (BANCO)element;

                    //Sync
                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.BANCOes.AddObject(banco);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element, USUARIO u)
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
                    //Master
                    UNID.Master(banco, u, -1, "Emininación");
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
       
        public List<BANCO> GetDeserializeBanco(string listPocos)
        {
            List<BANCO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<BANCO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetBanco()
        {
            List<BANCO> reset = new List<BANCO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.BANCOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new BANCO
                     {
                         BANCO_NAME = row.BANCO_NAME,
                         UNID_BANCO = row.UNID_BANCO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.BANCOes.First(p => p.UNID_BANCO == item.UNID_BANCO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }
        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }
        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
