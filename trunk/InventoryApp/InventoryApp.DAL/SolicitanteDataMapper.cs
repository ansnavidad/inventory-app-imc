using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
     public class SolicitanteDataMapper : IDataMapper 
    {
        public object getElements()
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                res = (from solicitantes in entitie.SOLICITANTEs select solicitantes).ToList();
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                SOLICITANTE solicitante = (SOLICITANTE)element;
                var query = from cust in entitie.SOLICITANTEs
                            where cust.UNID_SOLICITANTE == solicitante.UNID_SOLICITANTE
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
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            if(element != null){
                using (var entitie = new TAE2Entities())
                {
                    SOLICITANTE solicitante = (SOLICITANTE)element;
                    entitie.SOLICITANTEs.AddObject(solicitante);
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
