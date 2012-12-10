using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class PomArticuloDataMapper: IDataMapper
    {
        public void loadSync(object element)
        {

            if (element != null)
            {
                POM_ARTICULO poco = (POM_ARTICULO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.POM_ARTICULO
                                 where poco.UNID_POM_ARTICULO == cust.UNID_POM_ARTICULO
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
                    POM_ARTICULO pomArt = (POM_ARTICULO)element;
                    var modifiedPomArt = entity.POM_ARTICULO.First(p => p.UNID_POM_ARTICULO == pomArt.UNID_POM_ARTICULO);
                    modifiedPomArt.UNID_POM = pomArt.UNID_POM;
                    modifiedPomArt.UNID_ARTICULO = pomArt.UNID_ARTICULO;
                    modifiedPomArt.TC = pomArt.TC;
                    modifiedPomArt.IVA = pomArt.IVA;
                    modifiedPomArt.IS_ACTIVE = pomArt.IS_ACTIVE;
                    modifiedPomArt.DESCUENTO = pomArt.DESCUENTO;
                    modifiedPomArt.COSTO_UNITARIO = pomArt.COSTO_UNITARIO;
                    modifiedPomArt.CANTIDAD = pomArt.CANTIDAD;                    
                    //Sync
                    modifiedPomArt.IS_MODIFIED = true;
                    modifiedPomArt.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
            throw new NotImplementedException();
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
