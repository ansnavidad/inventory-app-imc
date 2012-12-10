using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class EquipoDataMapper : IDataMapper
    {
        public void loadSync(object element)
        {

            if (element != null)
            {
                EQUIPO poco = (EQUIPO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.EQUIPOes
                                 where poco.UNID_EQUIPO == cust.UNID_EQUIPO
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

            FixupCollection<EQUIPO> equipos = new FixupCollection<EQUIPO>();
            object res = null;

            using (var entity = new TAE2Entities())
            {
                var query = (from cust in entity.EQUIPOes
                             where cust.IS_ACTIVE ==true
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

            using (var entity = new TAE2Entities())
            {
                EQUIPO Tmp = (EQUIPO)element;

                res = (from tipo in entity.EQUIPOes
                       where tipo.UNID_EQUIPO == Tmp.UNID_EQUIPO
                       select tipo).ToList();

                return res;
            }
        }

        public FixupCollection<EQUIPO> GetArticuloEquipoByCategoria(CATEGORIA categoria)
        {
            FixupCollection<EQUIPO> equipos = new FixupCollection<EQUIPO>();

            if (categoria != null)
            {
                using (var entity = new TAE2Entities())
                {
                    try
                    {
                        (from art in entity.ARTICULOes
                         join catt in entity.CATEGORIAs
                            on art.UNID_CATEGORIA equals catt.UNID_CATEGORIA
                         join equ in entity.EQUIPOes
                            on art.UNID_EQUIPO equals equ.UNID_EQUIPO
                         where catt.UNID_CATEGORIA == categoria.UNID_CATEGORIA
                         select equ
                                            ).Distinct().ToList<EQUIPO>().ForEach(o => equipos.Add(new EQUIPO() { UNID_EQUIPO = o.UNID_EQUIPO, EQUIPO_NAME = o.EQUIPO_NAME }));
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }

            return equipos;
        }

        public void udpateElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    EQUIPO equipo = (EQUIPO)element;
                    var modifiedItemStatus = entity.EQUIPOes.First(p => p.UNID_EQUIPO == equipo.UNID_EQUIPO);
                    modifiedItemStatus.EQUIPO_NAME = equipo.EQUIPO_NAME;
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
                    EQUIPO equipo = (EQUIPO)element;

                    var validacion = (from cust in entity.EQUIPOes
                                      where cust.EQUIPO_NAME == equipo.EQUIPO_NAME
                                      select cust).ToList();

                    if (validacion.Count == 0)
                    {
                        equipo.UNID_EQUIPO = UNID.getNewUNID();
                        //Sync
                        equipo.IS_MODIFIED = true;
                        equipo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.EQUIPOes.AddObject(equipo);
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
                    EQUIPO equipo = (EQUIPO)element;
                    var deleteEquipo = entity.EQUIPOes.First(p => p.UNID_EQUIPO == equipo.UNID_EQUIPO);
                    deleteEquipo.IS_ACTIVE = false;
                    //Sync
                    deleteEquipo.IS_MODIFIED = true;
                    deleteEquipo.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<EQUIPO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de EQUIPO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonEquipo()
        {
            string res = null;
            List<EQUIPO> listEquipo = new List<EQUIPO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.EQUIPOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listEquipo.Add(new EQUIPO
                     {
                         UNID_EQUIPO=row.UNID_EQUIPO,
                         EQUIPO_NAME=row.EQUIPO_NAME,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listEquipo.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listEquipo);
                }
                return res;
            }
        }
    }
}
