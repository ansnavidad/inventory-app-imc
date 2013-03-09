using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace InventoryApp.DAL.CatalogInventario
{
    public class InventarioDataMapper
    {

        public object getElements()
        {
            List<INVENTARIO> tp = new List<INVENTARIO>();

            using (var entity = new TAE2Entities())
            {              
                var query = entity.INVENTARIOs.Include("ALMACEN").OrderByDescending(p => p.UNID_SEGMENTO).Where(p => p.IS_ACTIVE == true).ToList();

                if (query.Count > 0)
                {
                    foreach (INVENTARIO r in query)
                    {
                        tp.Add(r);
                    }
                }
                return tp;
            }
        }

    }
}
