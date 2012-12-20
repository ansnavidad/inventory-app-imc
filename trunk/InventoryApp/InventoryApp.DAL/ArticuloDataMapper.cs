using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class ArticuloDataMapper : IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                resul = (from articulo in entity.ARTICULOes
                       where articulo.IS_ACTIVE == true
                       where articulo.IS_MODIFIED == false
                       select articulo.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonArticulo(long Last_Modified_Date)
        {
            string res = null;
            List<ARTICULO> listArticulos = new List<ARTICULO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ARTICULOes
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     listArticulos.Add(new ARTICULO
                     {
                         ARTICULO1 = row.ARTICULO1,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         UNID_CATEGORIA = row.UNID_CATEGORIA,
                         UNID_EQUIPO = row.UNID_EQUIPO,
                         UNID_MARCA = row.UNID_MARCA,
                         UNID_MODELO = row.UNID_MODELO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listArticulos.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listArticulos);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                ARTICULO poco = (ARTICULO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.ARTICULOes
                                 where poco.UNID_ARTICULO == cust.UNID_ARTICULO
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
                        insertElementSync((object)poco);
                    }

                    var modifiedArticulo = entity.ARTICULOes.First(p => p.UNID_ARTICULO == poco.UNID_ARTICULO);
                    modifiedArticulo.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
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

        public List<ARTICULO> getElementsByCategoria(CATEGORIA categoria)
        {
            List<ARTICULO> articulos = new List<ARTICULO>();

            if (categoria != null)
            {
                try
                {
                    using (var Entity = new TAE2Entities())
                    {
                        (from p in Entity.ARTICULOes
                         where p.UNID_CATEGORIA == categoria.UNID_CATEGORIA
                            && p.IS_ACTIVE == true
                         select p).ToList<ARTICULO>().ForEach(o => articulos.Add(new ARTICULO()
                         {
                             UNID_ARTICULO = o.UNID_ARTICULO
                             ,
                             ARTICULO1 = o.ARTICULO1
                             ,
                             UNID_CATEGORIA = o.UNID_CATEGORIA
                             ,
                             UNID_EQUIPO = o.UNID_EQUIPO
                             ,
                             UNID_MARCA = o.UNID_MARCA
                             ,
                             UNID_MODELO = o.UNID_MODELO
                             ,
                             EQUIPO = new EQUIPO()
                                 {
                                     EQUIPO_NAME = o.EQUIPO.EQUIPO_NAME
                                     ,
                                     UNID_EQUIPO = o.EQUIPO.UNID_EQUIPO
                                 }
                             ,
                             MARCA = new MARCA()
                             {
                                 UNID_MARCA = o.UNID_MARCA
                                 ,
                                 MARCA_NAME = o.MARCA.MARCA_NAME
                             },
                             MODELO = new MODELO()
                             {
                                 UNID_MODELO = o.MODELO.UNID_MODELO
                                 ,
                                 MODELO_NAME = o.MODELO.MODELO_NAME
                             },
                             CATEGORIA = new CATEGORIA()
                             {
                                 UNID_CATEGORIA=o.CATEGORIA.UNID_CATEGORIA
                                 ,CATEGORIA_NAME=o.CATEGORIA.CATEGORIA_NAME
                             }
                         }));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return articulos;
        }
        
        public object getElements()
        {
            object res = null;
            FixupCollection<ARTICULO> tp = new FixupCollection<ARTICULO>();
            using (var entity = new TAE2Entities())
            {
                res = (from articulo in entity.ARTICULOes
                       where articulo.IS_ACTIVE == true
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
                       select cust).ToList();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="equipo"></param>
        /// <returns>Regresa un string JSON y si no hay datos </returns>
        public FixupCollection<ARTICULO> getElement(CATEGORIA categoria, EQUIPO equipo)
        {
            FixupCollection<ARTICULO> articulos = new FixupCollection<ARTICULO>();


            if (categoria != null && equipo != null)
            {
                using (var entity = new TAE2Entities())
                {

                    try
                    {
                        (from cust in entity.ARTICULOes
                         where cust.UNID_CATEGORIA == categoria.UNID_CATEGORIA
                             && cust.UNID_EQUIPO == equipo.UNID_EQUIPO
                         select cust).ToList<ARTICULO>().ForEach(o => articulos.Add(o));
                    }
                    catch (Exception ex)
                    {
                        ;
                    }

                } 
            }//endif

            return articulos;
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
                    //Sync
                    modifiedItemStatus.IS_MODIFIED = true;
                    modifiedItemStatus.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    ARTICULO art = (ARTICULO)element;

                    var validacion = (from cust in entity.ARTICULOes
                                      where cust.ARTICULO1 == art.ARTICULO1
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        art.UNID_ARTICULO = UNID.getNewUNID();
                        //Sync
                        art.IS_MODIFIED = true;
                        art.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.ARTICULOes.AddObject(art);
                        entity.SaveChanges();
                    }
                }
            }
        }

        public void insertElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    ARTICULO art = (ARTICULO)element;

                    var validacion = (from cust in entity.ARTICULOes
                                      where cust.ARTICULO1 == art.ARTICULO1
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        //Sync
                        
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.ARTICULOes.AddObject(art);
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
                    ARTICULO articulo = (ARTICULO)element;
                    var modifiedArticulo = entity.ARTICULOes.First(p => p.UNID_ARTICULO == articulo.UNID_ARTICULO);
                    modifiedArticulo.IS_ACTIVE = false;
                    //Sync
                    modifiedArticulo.IS_MODIFIED = true;
                    modifiedArticulo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<ARTICULO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de ARTICULO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonArticulo()
        {
            string res = null;
            List<ARTICULO> listArticulos = new List<ARTICULO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ARTICULOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listArticulos.Add(new ARTICULO
                     {
                         ARTICULO1= row.ARTICULO1,
                         UNID_ARTICULO= row.UNID_ARTICULO,
                         UNID_CATEGORIA= row.UNID_CATEGORIA,
                         UNID_EQUIPO=row.UNID_EQUIPO,
                         UNID_MARCA=row.UNID_MARCA,
                         UNID_MODELO=row.UNID_MODELO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listArticulos.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listArticulos);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<ARTICULO> 
        /// </summary>
        /// <returns>Regresa List<ARTICULO></returns>
        /// <returns>Si no regresa null</returns>
        public List<ARTICULO> GetDeserializeArticulo(string listPocos)
        {
            List<ARTICULO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<ARTICULO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetArticulo()
        {
            List<ARTICULO> reset = new List<ARTICULO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.ARTICULOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new ARTICULO
                     {
                         ARTICULO1 = row.ARTICULO1,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         UNID_CATEGORIA = row.UNID_CATEGORIA,
                         UNID_EQUIPO = row.UNID_EQUIPO,
                         UNID_MARCA = row.UNID_MARCA,
                         UNID_MODELO = row.UNID_MODELO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.ARTICULOes.First(p => p.UNID_ARTICULO == item.UNID_ARTICULO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
