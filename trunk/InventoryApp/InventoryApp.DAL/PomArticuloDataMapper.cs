using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class PomArticuloDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from poma in entity.POM_ARTICULO
                         where poma.IS_ACTIVE == true
                         where poma.IS_MODIFIED == false
                         select poma.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonPomArticulo(long? LastModifiedDate)
        {
            string res = null;
            List<POM_ARTICULO> listPomArticulo = new List<POM_ARTICULO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.POM_ARTICULO
                 where p.LAST_MODIFIED_DATE > LastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listPomArticulo.Add(new POM_ARTICULO
                     {
                         UNID_POM = row.UNID_POM,
                         UNID_POM_ARTICULO = row.UNID_POM_ARTICULO,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         CANTIDAD = row.CANTIDAD,
                         COSTO_UNITARIO = row.COSTO_UNITARIO,
                         IVA = row.IVA,
                         TC = row.TC,
                         DESCUENTO = row.DESCUENTO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPomArticulo.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPomArticulo);
                }
                return res;
            }
        }

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
                        var aux = query.First();

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }

                    var modifiedMenu = entity.POM_ARTICULO.First(p => p.UNID_POM_ARTICULO == poco.UNID_POM_ARTICULO);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
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
                    POM_ARTICULO pomA = (POM_ARTICULO)element;
                    var modifiedPomA = entity.POM_ARTICULO.First(p => p.UNID_POM_ARTICULO == pomA.UNID_POM_ARTICULO);
                    modifiedPomA.UNID_POM = pomA.UNID_POM;
                    modifiedPomA.UNID_ARTICULO = pomA.UNID_ARTICULO;
                    modifiedPomA.TC = pomA.TC;
                    modifiedPomA.IVA = pomA.IVA;
                    modifiedPomA.IS_ACTIVE = pomA.IS_ACTIVE;
                    modifiedPomA.DESCUENTO = pomA.DESCUENTO;
                    modifiedPomA.COSTO_UNITARIO = pomA.COSTO_UNITARIO;
                    modifiedPomA.CANTIDAD = pomA.CANTIDAD;                    
                    //Sync
                    modifiedPomA.IS_MODIFIED = true;
                    modifiedPomA.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    POM_ARTICULO pais = (POM_ARTICULO)element;

                    var validacion = (from cust in entity.POM_ARTICULO
                                      where cust.UNID_POM_ARTICULO == pais.UNID_POM_ARTICULO
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        pais.UNID_POM_ARTICULO = UNID.getNewUNID();
                        //Sync
                        pais.IS_MODIFIED = true;
                        pais.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.POM_ARTICULO.AddObject(pais);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método que serializa una List<POM_ARTICULO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de POM_ARTICULO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonPomArticulo()
        {
            string res = null;
            List<POM_ARTICULO> listPomArticulo = new List<POM_ARTICULO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.POM_ARTICULO
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listPomArticulo.Add(new POM_ARTICULO
                     {
                         UNID_POM = row.UNID_POM,
                         UNID_POM_ARTICULO=row.UNID_POM_ARTICULO,
                         UNID_ARTICULO=row.UNID_ARTICULO,
                         CANTIDAD=row.CANTIDAD,
                         COSTO_UNITARIO=row.COSTO_UNITARIO,
                         IVA=row.IVA,
                         TC=row.TC,
                         DESCUENTO=row.DESCUENTO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listPomArticulo.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listPomArticulo);
                }
                return res;
            }
        }
    }
}
