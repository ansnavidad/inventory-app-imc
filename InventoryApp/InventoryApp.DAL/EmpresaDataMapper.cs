﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.Model
{
    public class EmpresaDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.EMPRESAs
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from empresa in entity.EMPRESAs
                         where empresa.IS_ACTIVE == true
                         where empresa.IS_MODIFIED == false
                         select empresa.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonEmpresa(long? Last_Modified_Date)
        {
            string res = null;
            List<EMPRESA> listEmpresa = new List<EMPRESA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.EMPRESAs
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listEmpresa.Add(new EMPRESA
                     {
                         UNID_EMPRESA = row.UNID_EMPRESA,
                         EMPRESA_NAME = row.EMPRESA_NAME,
                         DIRECCION = row.DIRECCION,
                         RAZON_SOCIAL = row.RAZON_SOCIAL,
                         RFC = row.RFC,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listEmpresa.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listEmpresa);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                EMPRESA poco = (EMPRESA)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.EMPRESAs
                                 where poco.UNID_EMPRESA == cust.UNID_EMPRESA
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

                    var modifiedCotizacion = entity.EMPRESAs.First(p => p.UNID_EMPRESA == poco.UNID_EMPRESA);
                    modifiedCotizacion.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getElements()
        {
            FixupCollection<EMPRESA> tp = new FixupCollection<EMPRESA>();

            object res = null;

            using (var oAWEntities = new TAE2Entities())
            {
               var query= (from cust in oAWEntities.EMPRESAs
                           where cust.IS_ACTIVE==true
                           select cust).ToList();
               if (query.Count>0)
               {
                    res = query;
               }
               return res;
            }
        }

        public object getElement(object element)
        {
            FixupCollection<EMPRESA> tp = new FixupCollection<EMPRESA>();

            object res = null;

            if (element != null)
            {
                using (var entitie = new TAE2Entities())
                {
                    EMPRESA empresa = (EMPRESA)element;

                   var query=(from cust in entitie.EMPRESAs
                                where cust.UNID_EMPRESA == empresa.UNID_EMPRESA
                                 select cust).ToList();
                   if (query.Count>0)
                   {
                        res = query;
                   }
                    return res;
                }
            }
            return res;
        }

        public void udpateElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    EMPRESA EEmp = (EMPRESA)element;

                    var query = from cust in entity.EMPRESAs
                                where cust.UNID_EMPRESA == EEmp.UNID_EMPRESA
                                select cust;

                    var Emp = query.First();

                    Emp.EMPRESA_NAME = EEmp.EMPRESA_NAME;
                    Emp.DIRECCION = EEmp.DIRECCION;
                    Emp.RAZON_SOCIAL = EEmp.RAZON_SOCIAL;
                    Emp.RFC = EEmp.RFC;
                    //Sync
                    Emp.IS_MODIFIED = true;
                    Emp.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    UNID.Master(EEmp, u, -1, "Modificación");
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    EMPRESA EEmp = (EMPRESA)element;

                    var query = from cust in entity.EMPRESAs
                                where cust.UNID_EMPRESA == EEmp.UNID_EMPRESA
                                select cust;

                    var Emp = query.First();

                    Emp.EMPRESA_NAME = EEmp.EMPRESA_NAME;
                    Emp.DIRECCION = EEmp.DIRECCION;
                    Emp.RAZON_SOCIAL = EEmp.RAZON_SOCIAL;
                    Emp.RFC = EEmp.RFC;
                    Emp.IS_ACTIVE = EEmp.IS_ACTIVE;
                    //Sync
                    Emp.IS_MODIFIED = true;
                    Emp.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();

                }
            }
        }

        public void insertElement(object element, USUARIO u)
        {
            if (element !=null)
            {
                using (var entity = new TAE2Entities())
                {
                    EMPRESA empresa = (EMPRESA)element;

                    var validacion = (from cust in entity.EMPRESAs
                                      where cust.EMPRESA_NAME == empresa.EMPRESA_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        empresa.UNID_EMPRESA = UNID.getNewUNID();
                        //Sync
                        empresa.IS_MODIFIED = true;
                        empresa.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.EMPRESAs.AddObject(empresa);
                        entity.SaveChanges();

                        UNID.Master(empresa, u, -1, "Inserción");
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
                    EMPRESA empresa = (EMPRESA)element;

                    //Sync
                        
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.EMPRESAs.AddObject(empresa);
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
                    EMPRESA empresa = (EMPRESA)element;
                    var deleteEmpresa = entity.EMPRESAs.First(p => p.UNID_EMPRESA == empresa.UNID_EMPRESA);
                    deleteEmpresa.IS_ACTIVE = false;
                    //Sync
                    deleteEmpresa.IS_MODIFIED = true;
                    deleteEmpresa.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    UNID.Master(empresa, u, -1, "Emininación");
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    EMPRESA empresa = (EMPRESA)element;
                    var deleteEmpresa = entity.EMPRESAs.First(p => p.UNID_EMPRESA == empresa.UNID_EMPRESA);
                    deleteEmpresa.IS_ACTIVE = false;
                    //Sync
                    deleteEmpresa.IS_MODIFIED = true;
                    deleteEmpresa.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<EMPRESA> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de EMPRESA</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonEmpresa()
        {
            string res = null;
            List<EMPRESA> listEmpresa = new List<EMPRESA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.EMPRESAs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listEmpresa.Add(new EMPRESA
                     {
                         UNID_EMPRESA= row.UNID_EMPRESA,
                         EMPRESA_NAME=row.EMPRESA_NAME,
                         DIRECCION=row.DIRECCION,
                         RAZON_SOCIAL=row.RAZON_SOCIAL,
                         RFC=row.RFC,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listEmpresa.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listEmpresa);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<EMPRESA>
        /// </summary>
        /// <returns>Regresa List<EMPRESA></returns>
        /// <returns>Si no regresa null</returns>
        public List<EMPRESA> GetDeserializeEmpresa(string listPocos)
        {
            List<EMPRESA> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<EMPRESA>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetEmpresa()
        {
            List<EMPRESA> reset = new List<EMPRESA>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.EMPRESAs
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new EMPRESA
                     {
                         UNID_EMPRESA = row.UNID_EMPRESA,
                         EMPRESA_NAME = row.EMPRESA_NAME,
                         DIRECCION = row.DIRECCION,
                         RAZON_SOCIAL = row.RAZON_SOCIAL,
                         RFC = row.RFC,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.EMPRESAs.First(p => p.UNID_EMPRESA == item.UNID_EMPRESA);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }


        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
