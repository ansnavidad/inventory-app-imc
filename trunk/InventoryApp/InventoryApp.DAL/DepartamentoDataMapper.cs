using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class DepartamentoDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {
            if (element != null)
            {
                DEPARTAMENTO poco = (DEPARTAMENTO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.DEPARTAMENTOes
                                 where poco.UNID_DEPARTAMENTO == cust.UNID_DEPARTAMENTO
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
        
        public object getElements()
        {
            FixupCollection<DEPARTAMENTO> tp = new FixupCollection<DEPARTAMENTO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
               var query= (from cust in entity.DEPARTAMENTOes
                           where cust.IS_ACTIVE ==true
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
            FixupCollection<DEPARTAMENTO> tp = new FixupCollection<DEPARTAMENTO>();

            object res = null;
            if (element != null)
            { 
                using (var entitie = new TAE2Entities())
                {
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;

                    var query= (from cust in entitie.DEPARTAMENTOes
                                where cust.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO
                                select cust).ToList();
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
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;
                    var modifiedDepartamento = entity.DEPARTAMENTOes.First(p => p.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO);
                    modifiedDepartamento.DEPARTAMENTO_NAME = departamento.DEPARTAMENTO_NAME;
                    //Sync
                    modifiedDepartamento.IS_MODIFIED = true;
                    modifiedDepartamento.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;

                    var validacion = (from cust in entity.DEPARTAMENTOes
                                      where cust.DEPARTAMENTO_NAME == departamento.DEPARTAMENTO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        departamento.UNID_DEPARTAMENTO = UNID.getNewUNID();
                        //Sync
                        departamento.IS_MODIFIED = true;
                        departamento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.DEPARTAMENTOes.AddObject(departamento);
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
                    DEPARTAMENTO departamento = (DEPARTAMENTO)element;
                    var deleteDepartamento = entity.DEPARTAMENTOes.First(p => p.UNID_DEPARTAMENTO == departamento.UNID_DEPARTAMENTO);
                    deleteDepartamento.IS_ACTIVE = false;
                    //Sync
                    deleteDepartamento.IS_MODIFIED = true;
                    deleteDepartamento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<DEPARTAMENTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de DEPARTAMENTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonDepartamento()
        {
            string res = null;
            List<DEPARTAMENTO> listDepartamento = new List<DEPARTAMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.DEPARTAMENTOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listDepartamento.Add(new DEPARTAMENTO
                     {
                         DEPARTAMENTO_NAME= row.DEPARTAMENTO_NAME,
                         UNID_DEPARTAMENTO=row.UNID_DEPARTAMENTO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listDepartamento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listDepartamento);
                }
                return res;
            }
        }
    }
}
