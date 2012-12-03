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
         
                var query= (from solicitantes in entitie.SOLICITANTEs 
                            where solicitantes.IS_ACTIVE ==true
                            select solicitantes).ToList();

                foreach (SOLICITANTE sol in ((List<SOLICITANTE>)query))
                {
                    sol.DEPARTAMENTO = sol.DEPARTAMENTO;
                    sol.EMPRESA = sol.EMPRESA;
                }
                if (query.Count>0)
                {
                    res = query;    
                }
                return query;
            }
        }

        public List<SOLICITANTE> GetSolicitanteList(EMPRESA empresa)
        {
            List<SOLICITANTE> listSolicitantes = new List<SOLICITANTE>();
            using (var entitie = new TAE2Entities())
            {

                EMPRESA emp = (from e in entitie.EMPRESAs
                             where e.UNID_EMPRESA == empresa.UNID_EMPRESA
                             select e).First<EMPRESA>();

                emp.SOLICITANTEs.ToList().ForEach(s => 
                {
                    listSolicitantes.Add(new SOLICITANTE()
                    {
                        UNID_SOLICITANTE = s.UNID_SOLICITANTE,
                        SOLICITANTE_NAME = s.SOLICITANTE_NAME,
                        DEPARTAMENTO = new DEPARTAMENTO()
                        {
                            UNID_DEPARTAMENTO = s.DEPARTAMENTO.UNID_DEPARTAMENTO,
                            DEPARTAMENTO_NAME = s.DEPARTAMENTO.DEPARTAMENTO_NAME
                        },
                        EMPRESA = new EMPRESA()
                        {
                            UNID_EMPRESA=s.EMPRESA.UNID_EMPRESA,
                            EMPRESA_NAME=s.EMPRESA.EMPRESA_NAME
                        }
                    });
                });
            }

            return listSolicitantes;
        }

         

        public object getElement(object element)
        {
            

            object res = null;

            if (element != null)
            {
                using (var entitie = new TAE2Entities())
                {
                    SOLICITANTE solicitante = (SOLICITANTE)element;
                     
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
                    SOLICITANTE Sol = (SOLICITANTE)element;

                    var validacion = (from cust in entitie.SOLICITANTEs
                                      where cust.SOLICITANTE_NAME == Sol.SOLICITANTE_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        Sol.UNID_SOLICITANTE = UNID.getNewUNID();
                        entitie.SOLICITANTEs.AddObject(Sol);
                        entitie.SaveChanges();
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
                    SOLICITANTE sol = (SOLICITANTE)element;

                    var deleteSol = entity.SOLICITANTEs.First(p => p.UNID_SOLICITANTE == sol.UNID_SOLICITANTE);

                    deleteSol.IS_ACTIVE = false;

                    entity.SaveChanges();
                }
            }
        }
    }
}
