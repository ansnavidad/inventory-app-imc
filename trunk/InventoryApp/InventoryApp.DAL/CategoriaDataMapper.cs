using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public class CategoriaDataMapper : IDataMapper
    {

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

                    categoria.UNID_CATEGORIA = UNID.getNewUNID();

                    entity.CATEGORIAs.AddObject(categoria);

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
                    CATEGORIA categoria = (CATEGORIA)element;
                    var deleteCategoria = entity.CATEGORIAs.First(p => p.UNID_CATEGORIA == categoria.UNID_CATEGORIA);
                    deleteCategoria.IS_ACTIVE = false;
                    entity.SaveChanges();
                }
            }
        }
    }
}
