using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class ArticuloDataMapper : IDataMapper
    {
        /// <summary>
        /// Obtiene todos los elementos en la tabla transporte
        /// </summary>
        /// <returns></returns>
        public object getElements_EntradasSalidas(object element)
        {
            object o = null;
            if (element != null)
            {
                CATEGORIA Eprov = (CATEGORIA)element;

                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.ARTICULOes
                               where p.UNID_CATEGORIA == Eprov.UNID_CATEGORIA
                               select p).ToList();

                    foreach (ARTICULO trans in ((List<ARTICULO>)res))
                    {
                        trans.MODELO = trans.MODELO;
                        trans.MARCA = trans.MARCA;
                        trans.EQUIPO = trans.EQUIPO;
                        trans.CATEGORIA = trans.CATEGORIA;                        
                    }

                    o = (object)res;
                }
            }
            return o;
        }
        
        public object getElements()
        {
            object res = null;
            using (var entity = new TAE2Entities())
            {
                res = (from articulo in entity.ARTICULOes
                       select articulo).ToList();

                foreach (ARTICULO art in ((List<ARTICULO>)res))
                {
                    art.CATEGORIA = art.CATEGORIA;
                    art.MARCA = art.MARCA;
                    art.MODELO = art.MODELO;
                    art.EQUIPO = art.EQUIPO;
                }

                return res;
            }
        }

        public object getElement(object element)
        {
            object res = null;

            using (var entity = new TAE2Entities())
            {
                ARTICULO Etra = (ARTICULO)element;

                res = (from cust in entity.ARTICULOes
                       where cust.UNID_ARTICULO == Etra.UNID_ARTICULO
                       select cust).ToList<ARTICULO>();

                foreach (ARTICULO art in ((List<ARTICULO>)res))
                {
                    art.CATEGORIA = art.CATEGORIA;
                    art.MARCA = art.MARCA;
                    art.MODELO = art.MODELO;
                    art.EQUIPO = art.EQUIPO;
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
                    ARTICULO articulo = (ARTICULO)element;
                    var modifiedItemStatus = entity.ARTICULOes.First(p => p.UNID_ARTICULO == articulo.UNID_ARTICULO);
                    modifiedItemStatus.ARTICULO1 = articulo.ARTICULO1;
                    modifiedItemStatus.UNID_CATEGORIA = articulo.UNID_CATEGORIA;
                    modifiedItemStatus.UNID_EQUIPO = articulo.UNID_EQUIPO;
                    modifiedItemStatus.UNID_MARCA = articulo.UNID_MARCA;
                    modifiedItemStatus.UNID_MODELO = articulo.UNID_MODELO;

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
                    ARTICULO art = (ARTICULO)element;
                    art.UNID_ARTICULO = UNID.getNewUNID();

                    entity.ARTICULOes.AddObject(art);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
