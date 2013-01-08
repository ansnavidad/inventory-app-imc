using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class MaxMinDataMapper : IDataMapper
    {
        public object getElements()
        {
            FixupCollection<MAX_MIN> tp = new FixupCollection<MAX_MIN>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                res = (from cust in entity.MAX_MIN
                             where cust.IS_ACTIVE == true
                             select cust).ToList();

                foreach (MAX_MIN mm in ((List<MAX_MIN>)res))
                {
                    mm.ARTICULO = mm.ARTICULO;
                    mm.ARTICULO.CATEGORIA = mm.ARTICULO.CATEGORIA;
                    mm.ARTICULO.EQUIPO = mm.ARTICULO.EQUIPO;
                    mm.ARTICULO.MODELO = mm.ARTICULO.MODELO;
                    mm.ARTICULO.MARCA = mm.ARTICULO.MARCA;
                    mm.ALMACEN = mm.ALMACEN;
                }
                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;
            using (var entitie = new TAE2Entities())
            {
                MAX_MIN maxMin = (MAX_MIN)element;
                var query = (from cust in entitie.MAX_MIN
                             where cust.UNID_MAX_MIN == maxMin.UNID_MAX_MIN
                             select cust).ToList();
                if (query.Count > 0)
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
                    MAX_MIN maxMin = (MAX_MIN)element;
                    var modifiedMaxMin = entity.MAX_MIN.First(p => p.UNID_MAX_MIN == maxMin.UNID_MAX_MIN);
                    modifiedMaxMin.MAX = maxMin.MAX;
                    modifiedMaxMin.MIN = maxMin.MIN;
                    //Sync
                    modifiedMaxMin.IS_MODIFIED = true;
                    modifiedMaxMin.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    MAX_MIN maxmin = (MAX_MIN)element;

                    
                        maxmin.UNID_MAX_MIN = UNID.getNewUNID();
                        //Sync
                        maxmin.IS_MODIFIED = true;
                        maxmin.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MAX_MIN.AddObject(maxmin);
                        entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MAX_MIN maxMin = (MAX_MIN)element;
                    var modifiedMaxMin = entity.MAX_MIN.First(p => p.UNID_MAX_MIN == maxMin.UNID_MAX_MIN);
                    modifiedMaxMin.IS_ACTIVE = false;
                    //Sync
                    modifiedMaxMin.IS_MODIFIED = true;
                    modifiedMaxMin.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
