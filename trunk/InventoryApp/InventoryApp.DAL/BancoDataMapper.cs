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
            object res = null;
            using(var entitie = new TAE2Entities()){
                res = (from bancos in entitie.BANCOes select bancos).ToList();
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                BANCO banco = (BANCO)element;

                var query = from cust in entitie.BANCOes
                            where cust.UNID_BANCO == banco.UNID_BANCO
                            select cust;
                if (query != null)
                {
                    res = query;
                }
                return res;
            }
        }

        public void udpateElement(object element)
        {
            using (var entitie = new TAE2Entities())
            {
                BANCO banco = (BANCO)element;
                var query = from cust in entitie.BANCOes
                            where cust.UNID_BANCO == banco.UNID_BANCO
                            select cust;
                var ban = query.First();    
                ban.BANCO_NAME = banco.BANCO_NAME;
                entitie.SaveChanges();
            }
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entitie = new TAE2Entities())
                {
                    BANCO Ebanco = (BANCO)element;
                    Ebanco.UNID_BANCO = UNID.getNewUNID();
                    entitie.BANCOes.AddObject(Ebanco);
                    entitie.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
