using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class BancoDataMapper : IDataMapper
    {
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
    }
}
