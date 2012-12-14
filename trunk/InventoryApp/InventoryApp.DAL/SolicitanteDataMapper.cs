using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
     public class SolicitanteDataMapper : IDataMapper 
    {
         public long? LastModifiedDate()
         {
             long? resul = null;
             using (var entity = new TAE2Entities())
             {
                 resul = (from sol in entity.SOLICITANTEs
                          where sol.IS_ACTIVE == true
                          where sol.IS_MODIFIED == false
                          select sol.LAST_MODIFIED_DATE).Max();
                 return resul;
             }

         }

         public string GetJsonSolicitante(long? LMD)
         {
             string res = null;
             List<SOLICITANTE> listSolicitante = new List<SOLICITANTE>();
             using (var Entity = new TAE2Entities())
             {
                 (from p in Entity.SOLICITANTEs
                  where p.LAST_MODIFIED_DATE > LMD
                  select p).ToList().ForEach(row =>
                  {
                      listSolicitante.Add(new SOLICITANTE
                      {
                          UNID_SOLICITANTE = row.UNID_SOLICITANTE,
                          SOLICITANTE_NAME = row.SOLICITANTE_NAME,
                          UNID_EMPRESA = row.UNID_EMPRESA,
                          UNID_DEPARTAMENTO = row.UNID_DEPARTAMENTO,
                          EMAIL = row.EMAIL,
                          VALIDADOR = row.VALIDADOR,
                          IS_ACTIVE = row.IS_ACTIVE,
                          IS_MODIFIED = row.IS_MODIFIED,
                          LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                      });
                  });
                 if (listSolicitante.Count > 0)
                 {
                     res = SerializerJson.SerializeParametros(listSolicitante);
                 }
                 return res;
             }
         }

         public void loadSync(object element)
         {
             if (element != null)
             {
                 SOLICITANTE poco = (SOLICITANTE)element;
                 using (var entity = new TAE2Entities())
                 {
                     var query = (from cust in entity.SOLICITANTEs
                                  where poco.UNID_SOLICITANTE == cust.UNID_SOLICITANTE
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

                     var modifiedMenu = entity.SOLICITANTEs.First(p => p.UNID_SOLICITANTE == poco.UNID_SOLICITANTE);
                     modifiedMenu.IS_MODIFIED = false;
                     entity.SaveChanges();
                 }
             }
         }

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
                using (var entity = new TAE2Entities())
                {
                    SOLICITANTE ESol = (SOLICITANTE)element;

                    var Sol = entity.SOLICITANTEs.First(p => p.UNID_SOLICITANTE == ESol.UNID_SOLICITANTE);
                    
                    Sol.SOLICITANTE_NAME = ESol.SOLICITANTE_NAME;

                    Sol.EMAIL = ESol.EMAIL;

                    Sol.VALIDADOR = ESol.VALIDADOR;

                    Sol.UNID_EMPRESA = ESol.UNID_EMPRESA;

                    Sol.EMPRESA = ESol.EMPRESA;

                    Sol.UNID_DEPARTAMENTO = ESol.UNID_DEPARTAMENTO;

                    Sol.DEPARTAMENTO = ESol.DEPARTAMENTO;
                    //Sync
                    Sol.IS_MODIFIED = true;
                    Sol.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
            if(element != null){
                using (var entity = new TAE2Entities())
                {
                    SOLICITANTE Sol = (SOLICITANTE)element;

                    var validacion = (from cust in entity.SOLICITANTEs
                                      where cust.SOLICITANTE_NAME == Sol.SOLICITANTE_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        Sol.UNID_SOLICITANTE = UNID.getNewUNID();
                        //Sync
                        Sol.IS_MODIFIED = true;
                        Sol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.SOLICITANTEs.AddObject(Sol);
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
                    SOLICITANTE sol = (SOLICITANTE)element;

                    var deleteSol = entity.SOLICITANTEs.First(p => p.UNID_SOLICITANTE == sol.UNID_SOLICITANTE);

                    deleteSol.IS_ACTIVE = false;
                    //Sync
                    deleteSol.IS_MODIFIED = true;
                    deleteSol.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<SOLICITANTE> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de SOLICITANTE</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonSolicitante()
        {
            string res = null;
            List<SOLICITANTE> listSolicitante = new List<SOLICITANTE>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.SOLICITANTEs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listSolicitante.Add(new SOLICITANTE
                     {
                         UNID_SOLICITANTE=row.UNID_SOLICITANTE,
                         SOLICITANTE_NAME=row.SOLICITANTE_NAME,
                         UNID_EMPRESA=row.UNID_EMPRESA,
                         UNID_DEPARTAMENTO=row.UNID_DEPARTAMENTO,
                         EMAIL=row.EMAIL,
                         VALIDADOR=row.VALIDADOR,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listSolicitante.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listSolicitante);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<SOLICITANTE>
        /// </summary>
        /// <returns>Regresa List<SOLICITANTE></returns>
        /// <returns>Si no regresa null</returns>
        public List<SOLICITANTE> GetDeserializeSolicitante(string listPocos)
        {
            List<SOLICITANTE> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<SOLICITANTE>>(listPocos);
            }

            return res;
        }
    }
}
