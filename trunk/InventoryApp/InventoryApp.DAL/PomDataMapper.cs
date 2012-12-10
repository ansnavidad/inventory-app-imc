using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class PomDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {

            if (element != null)
            {
                POM poco = (POM)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.POMs
                                 where poco.UNID_POM == cust.UNID_POM
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
            throw new NotImplementedException();
        }

        public object getElement(object element)
        {
            throw new NotImplementedException();
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    POM pom = (POM)element;
                    var modifiedPom = entity.POMs.First(p => p.UNID_POM == pom.UNID_POM);
                    modifiedPom.UNID_EMPRESA = pom.UNID_EMPRESA;
                    modifiedPom.UNID_COTIZACION = pom.UNID_COTIZACION;
                    modifiedPom.IS_ACTIVE = pom.IS_ACTIVE;
                    modifiedPom.FECHA_POM = pom.FECHA_POM;
                    modifiedPom.FECHA_ENTREGA_REAL = pom.FECHA_ENTREGA_REAL;
                    modifiedPom.FECHA_ENTREGA = pom.FECHA_ENTREGA;
                    modifiedPom.DIAS_ENTREGA = pom.DIAS_ENTREGA;                    
                    //Sync
                    //modifiedPom.IS_MODIFIED = true;
                    modifiedPom.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    POM pom = (POM)element;

                    var validacion = (from cust in entity.MARCAs
                                      where cust.UNID_MARCA == pom.UNID_POM
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        pom.UNID_POM = UNID.getNewUNID();
                        //Sync
                        //pom.IS_MODIFIED = true;
                        pom.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.POMs.AddObject(pom);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
