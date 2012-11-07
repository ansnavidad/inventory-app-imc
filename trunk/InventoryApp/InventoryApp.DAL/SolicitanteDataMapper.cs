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
            //FixupCollection<SOLICITANTE> tp = new FixupCollection<SOLICITANTE>();
            //List<SOLICITANTE> ts = new List<SOLICITANTE>();

            //object res = null;

            using (var entitie = new TAE2Entities())
            {
                //(from cust in entitie.SOLICITANTEs
                // select cust).ToList().ForEach(d => { tp.Add(d); });
                    var query= (from solicitantes in entitie.SOLICITANTEs 
                     select solicitantes).ToList();

                    foreach (SOLICITANTE sol in ((List<SOLICITANTE>)query))
                    {
                        //ts.Add(sol);
                        sol.DEPARTAMENTO = sol.DEPARTAMENTO;
                        sol.EMPRESA = sol.EMPRESA;
                    }
                    
                    //if (tp != null)
                    //{
                    //    res = tp;
                    //}
                    return query;
            }
        }

        public object getElement(object element)
        {
            //FixupCollection<SOLICITANTE> tp = new FixupCollection<SOLICITANTE>();

            //List<SOLICITANTE> ts = new List<SOLICITANTE>();

            object res = null;

            if (element != null)
            {
                using (var entitie = new TAE2Entities())
                {
                    SOLICITANTE solicitante = (SOLICITANTE)element;
                     //(from cust in entitie.SOLICITANTEs
                     //           where cust.UNID_SOLICITANTE == solicitante.UNID_SOLICITANTE
                     //            select cust).ToList().ForEach(d => { tp.Add(d); });

                    var query= (from cust in entitie.SOLICITANTEs
                     where cust.UNID_SOLICITANTE == solicitante.UNID_SOLICITANTE
                     select cust).ToList();

                    foreach (SOLICITANTE sol  in ((List<SOLICITANTE>)query))
                    {
                        sol.DEPARTAMENTO = sol.DEPARTAMENTO;
                        sol.EMPRESA = sol.EMPRESA;
                    }

                    if (query.Count > 0)
                    {
                        res = query;
                    }
                     //if (tp.Count>0)
                     //{
                     //    res = tp;
                     //}
                     return res;
                }
             }
            return res;
        }
        

        public void udpateElement(object element)
           {
            if (element != null)
            {
                using (var oAWEntities = new TAE2Entities())
                {
                    SOLICITANTE ESol = (SOLICITANTE)element;

                    var Sol = oAWEntities.SOLICITANTEs.First(p => p.UNID_SOLICITANTE == ESol.UNID_SOLICITANTE);
                    //var query = from cust in oAWEntities.SOLICITANTEs
                    //            where cust.UNID_SOLICITANTE == ESol.UNID_SOLICITANTE
                    //            select cust;

                    //var Sol = query.First();

                    Sol.SOLICITANTE_NAME = ESol.SOLICITANTE_NAME;

                    Sol.EMAIL = ESol.EMAIL;

                    Sol.VALIDADOR = ESol.VALIDADOR;

                    Sol.UNID_EMPRESA = ESol.UNID_EMPRESA;

                    Sol.EMPRESA = ESol.EMPRESA;

                    Sol.UNID_DEPARTAMENTO = ESol.UNID_DEPARTAMENTO;

                    Sol.DEPARTAMENTO = ESol.DEPARTAMENTO;

                    oAWEntities.SaveChanges();

                }
            }
        }

        public void insertElement(object element)
        {
            if(element != null){
                using (var entitie = new TAE2Entities())
                {
                    SOLICITANTE ESol = (SOLICITANTE)element;

                    //SOLICITANTE Sol = new SOLICITANTE();

                    ESol.UNID_SOLICITANTE = UNID.getNewUNID();

                    //Sol.SOLICITANTE_NAME = ESol.SOLICITANTE_NAME;

                    //Sol.EMAIL = ESol.EMAIL;

                    //Sol.VALIDADOR = ESol.VALIDADOR;

                    //Sol.UNID_EMPRESA = ESol.UNID_EMPRESA;

                    //Sol.UNID_DEPARTAMENTO = ESol.UNID_SOLICITANTE;

                    entitie.SOLICITANTEs.AddObject(ESol);

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
