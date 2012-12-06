using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class CategoriaDataMapper : IDataMapper
    {

        public List<CATEGORIA> getElementsByProveedor(PROVEEDOR proveedor)
        {
            List<CATEGORIA> res = new List<CATEGORIA>();

            try
            {
                using (var entity = new TAE2Entities())
                {
                    var query = (from provL in entity.PROVEEDOR_CATEGORIA
                                 join cat in entity.CATEGORIAs on provL.UNID_CATEGORIA equals cat.UNID_CATEGORIA
                                 join prov in entity.PROVEEDORs on provL.UNID_PROVEEDOR equals prov.UNID_PROVEEDOR
                                 where provL.UNID_PROVEEDOR == proveedor.UNID_PROVEEDOR
                                 select cat).ToList<CATEGORIA>();

                    foreach (CATEGORIA c in query)
                    {
                        res.Add(c);
                    }
                }
            }
            catch (Exception)
            {
                ;
            }
            return res;
        }//

        public object getElements()
        {
            object res = null;

            FixupCollection<CATEGORIA> tp = new FixupCollection<CATEGORIA>();

            using (var entity = new TAE2Entities())
            {

                 var query= (from cust in entity.CATEGORIAs
                             where cust.IS_ACTIVE==true
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
            object res = null;

            FixupCollection<CATEGORIA> tp = new FixupCollection<CATEGORIA>();

            if (element!=null)
            {
                using (var entity = new TAE2Entities())
                {
                    CATEGORIA ETipo = (CATEGORIA)element;

                   var query= (from cust in entity.CATEGORIAs
                                 where cust.UNID_CATEGORIA == ETipo.UNID_CATEGORIA
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
                    CATEGORIA ETipo = (CATEGORIA)element;

                    var query = (from cust in entity.CATEGORIAs
                                 where cust.UNID_CATEGORIA==ETipo.UNID_CATEGORIA
                                 select cust).ToList();
                    if (query.Count > 0)
                    {
                        var tipo = query.First();

                        tipo.CATEGORIA_NAME = ETipo.CATEGORIA_NAME;
                        //Sync
                        tipo.IS_MODIFIED = true;
                        tipo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    CATEGORIA categoria = (CATEGORIA)element;

                    var validacion = (from cust in entity.CATEGORIAs
                                      where cust.CATEGORIA_NAME == categoria.CATEGORIA_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        categoria.UNID_CATEGORIA = UNID.getNewUNID();
                        //Sync
                        categoria.IS_MODIFIED = true;
                        categoria.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.CATEGORIAs.AddObject(categoria);
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
                    CATEGORIA categoria = (CATEGORIA)element;
                    var deleteCategoria = entity.CATEGORIAs.First(p => p.UNID_CATEGORIA == categoria.UNID_CATEGORIA);
                    deleteCategoria.IS_ACTIVE = false;
                    //Sync
                    deleteCategoria.IS_MODIFIED = true;
                    deleteCategoria.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
