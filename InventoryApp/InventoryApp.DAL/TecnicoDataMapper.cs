using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class TecnicoDataMapper : IDataMapper
    {
        public object getTecnicosByAlmancen(ALMACEN a){

            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.TECNICOes
                           join enlace in Entity.ALMACEN_TECNICO on p.UNID_TECNICO equals enlace.UNID_TECNICO
                           join almacen in Entity.ALMACENs on enlace.UNID_ALMACEN equals almacen.UNID_ALMACEN
                           join almacen2 in Entity.ALMACENs on almacen.UNID_ALMACEN equals a.UNID_ALMACEN
                           where (p.IS_ACTIVE == true)
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
    }
}
