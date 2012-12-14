using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class TecnicoDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from tecnico in entity.TECNICOes
                         where tecnico.IS_ACTIVE == true
                         where tecnico.IS_MODIFIED == false
                         select tecnico.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonTecnico(long? LMD)
        {
            string res = null;
            List<TECNICO> listTecnico = new List<TECNICO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TECNICOes
                 where p.LAST_MODIFIED_DATE > LMD
                 select p).ToList().ForEach(row =>
                 {
                     listTecnico.Add(new TECNICO
                     {
                         UNID_TECNICO = row.UNID_TECNICO,
                         TECNICO_NAME = row.TECNICO_NAME,
                         MAIL = row.MAIL,
                         UNID_CIUDAD = row.UNID_CIUDAD,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTecnico.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTecnico);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                TECNICO poco = (TECNICO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.TECNICOes
                                 where poco.UNID_TECNICO == cust.UNID_TECNICO
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

                    var modifiedTecnico = entity.TECNICOes.First(p => p.UNID_TECNICO == poco.UNID_TECNICO);
                    modifiedTecnico.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public object getTecnicosByAlmancen(ALMACEN a){

            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.TECNICOes
                           join enlace in Entity.ALMACEN_TECNICO on p.UNID_TECNICO equals enlace.UNID_TECNICO
                           join almacen in Entity.ALMACENs on enlace.UNID_ALMACEN equals almacen.UNID_ALMACEN
                           where (p.IS_ACTIVE == true && a.UNID_ALMACEN == almacen.UNID_ALMACEN)
                           select p).ToList();

                return res;
            }
        }

        public object getTecnicosByAlmancenFirst(ALMACEN a)
        {

            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.TECNICOes
                           join enlace in Entity.ALMACEN_TECNICO on p.UNID_TECNICO equals enlace.UNID_TECNICO
                           join almacen in Entity.ALMACENs on enlace.UNID_ALMACEN equals almacen.UNID_ALMACEN
                           join almacen2 in Entity.ALMACENs on almacen.UNID_ALMACEN equals a.UNID_ALMACEN
                           where (p.IS_ACTIVE == true)
                           select p).ToList().First();

                return res;
            }
        }

        public object getElements()
        {
            object o = null;
            FixupCollection<TECNICO> tp = new FixupCollection<TECNICO>();
            using (var Entity = new TAE2Entities())
            {
                var query = (from p in Entity.TECNICOes
                             where p.IS_ACTIVE == true
                             select p).ToList();
                foreach (TECNICO tec in ((List<TECNICO>)query))
                {
                    tec.CIUDAD = tec.CIUDAD;
                }
                
                if (query.Count > 0)
                {
                    foreach (TECNICO t in query) {

                        t.CIUDAD = t.CIUDAD;
                    }
                    o = query;
                }

                return o;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                TECNICO Eprov = (TECNICO)element;
                FixupCollection<TECNICO> tp = new FixupCollection<TECNICO>();

                using (var Entity = new TAE2Entities())
                {
                    var query = (from p in Entity.TECNICOes
                                 where p.UNID_TECNICO == Eprov.UNID_TECNICO
                                 select p).ToList();

                    if (query.Count > 0)
                    {
                        o = query;
                    }
                }
            }
            return o;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    TECNICO tecnico = (TECNICO)element;
                    var modifiedTecnico = entity.TECNICOes.First(p => p.UNID_TECNICO == tecnico.UNID_TECNICO);
                    modifiedTecnico.UNID_CIUDAD = tecnico.UNID_CIUDAD;
                    modifiedTecnico.TECNICO_NAME = tecnico.TECNICO_NAME;
                    modifiedTecnico.MAIL = tecnico.MAIL;
                    modifiedTecnico.IS_ACTIVE = tecnico.IS_ACTIVE;
                    modifiedTecnico.CIUDAD = tecnico.CIUDAD;
                    //Sync
                    modifiedTecnico.IS_MODIFIED = true;
                    modifiedTecnico.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    TECNICO tecnico = (TECNICO)element;

                    var validacion = (from cust in entity.TECNICOes
                                      where cust.TECNICO_NAME == tecnico.TECNICO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        tecnico.UNID_TECNICO = UNID.getNewUNID();
                        //Sync
                        tecnico.IS_MODIFIED = true;
                        tecnico.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                        entity.SaveChanges();
                        //
                        entity.TECNICOes.AddObject(tecnico);
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
                    TECNICO tecnico = (TECNICO)element;

                    var deleteTecnico = entity.TECNICOes.First(p => p.UNID_TECNICO == tecnico.UNID_TECNICO);

                    deleteTecnico.IS_ACTIVE = false;
                    //Sync
                    deleteTecnico.IS_MODIFIED = true;
                    deleteTecnico.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID(); 
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        public List<TECNICO> getListElements()
        {
            List<TECNICO> tecnico = new List<TECNICO>();

            try
            {
                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.TECNICOes
                     select p).ToList<TECNICO>().ForEach(o => tecnico.Add(o));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tecnico;
        }

        /// <summary>
        /// Método que serializa una List<TECNICO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de TECNICO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonTecnico()
        {
            string res = null;
            List<TECNICO> listTecnico = new List<TECNICO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.TECNICOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listTecnico.Add(new TECNICO
                     {
                         UNID_TECNICO=row.UNID_TECNICO,
                         TECNICO_NAME=row.TECNICO_NAME,
                         MAIL=row.MAIL,
                         UNID_CIUDAD=row.UNID_CIUDAD,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listTecnico.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listTecnico);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<TECNICO>
        /// </summary>
        /// <returns>Regresa List<TECNICO></returns>
        /// <returns>Si no regresa null</returns>
        public List<TECNICO> GetDeserializeTecnico(string listPocos)
        {
            List<TECNICO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<TECNICO>>(listPocos);
            }

            return res;
        }
    }
}
