using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class ProgramadoDataMapper : IDataMapper
    {
        public object getElements()
        {
            FixupCollection<PROGRAMADO> tp = new FixupCollection<PROGRAMADO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                res = (from cust in entity.PROGRAMADOes
                       where cust.IS_ACTIVE == true
                       select cust).ToList();

                foreach (PROGRAMADO mm in ((List<PROGRAMADO>)res))
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
                PROGRAMADO maxMin = (PROGRAMADO)element;
                var query = (from cust in entitie.PROGRAMADOes
                             where cust.UNID_PROGRAMADO == maxMin.UNID_PROGRAMADO
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
                    PROGRAMADO prog = (PROGRAMADO)element;
                    var modifiedMaxMin = entity.PROGRAMADOes.First(p => p.UNID_PROGRAMADO == prog.UNID_PROGRAMADO);
                    modifiedMaxMin.PROGRAMADO1 = prog.PROGRAMADO1;
                    
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
                    PROGRAMADO prog = (PROGRAMADO)element;


                    prog.UNID_PROGRAMADO = UNID.getNewUNID();
                    //Sync
                    prog.IS_MODIFIED = true;
                    prog.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.PROGRAMADOes.AddObject(prog);
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
                    PROGRAMADO maxMin = (PROGRAMADO)element;
                    var modifiedMaxMin = entity.PROGRAMADOes.First(p => p.UNID_PROGRAMADO == maxMin.UNID_PROGRAMADO);
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
