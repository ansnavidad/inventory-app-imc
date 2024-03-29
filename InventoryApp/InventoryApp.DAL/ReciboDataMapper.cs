﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class ReciboDataMapper:IDataMapper
    {
        public Dictionary<string, string> GetResponseDictionary(string response)
        {
            Dictionary<string, string> resx = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            return resx;
        }

        public void LimpiarRecibo(List<long> UnidsMovs, long UnidR) {

            using (var Entity = new TAE2Entities())
            {
                List<long> EliminarR_M = new List<long>();
                List<long> EliminarM = new List<long>();

                var UnidsPorEliminar = (from r in Entity.RECIBO_MOVIMIENTO
                                        where r.UNID_RECIBO == UnidR
                                        select r).ToList();

                if (UnidsPorEliminar != null && UnidsMovs != null && UnidsPorEliminar.Count > 0 && UnidsMovs.Count > 0)
                {
                    foreach (long u in UnidsMovs)
                    {
                        bool aux = true;

                        int j = 0;
                        for (j = 0; j < UnidsPorEliminar.Count; j++)
                        {

                            if (u == UnidsPorEliminar[j].UNID_MOVIMIENTO)
                            {
                                aux = false;
                                break;
                            }
                        }

                        if (!aux)
                        {
                            EliminarR_M.Add(UnidsPorEliminar[j].UNID_RECIBO_MOVIMIENTO);
                            EliminarM.Add(u);
                        }
                    }

                    foreach (long d in EliminarR_M)
                    {

                        var delR_M = (from r in Entity.RECIBO_MOVIMIENTO
                                      where r.UNID_RECIBO_MOVIMIENTO == d
                                      select r).First();
                        //Sync
                        delR_M.IS_ACTIVE = false;
                        delR_M.IS_MODIFIED = true;

                        delR_M.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = Entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        Entity.SaveChanges();
                    }

                    foreach (long d in EliminarM)
                    {
                        var delM = (from r in Entity.MOVIMENTOes
                                    where r.UNID_MOVIMIENTO == d
                                    select r).First();
                        delM.IS_ACTIVE = false;
                        delM.IS_MODIFIED = true;

                        //Sync
                        delM.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = Entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        Entity.SaveChanges();

                        var delM_D = (from r in Entity.MOVIMIENTO_DETALLE
                                      where r.UNID_MOVIMIENTO == d
                                      select r).ToList();

                        foreach (MOVIMIENTO_DETALLE m in delM_D)
                        {

                            var delU_M = (from r in Entity.ULTIMO_MOVIMIENTO
                                          where r.UNID_MOVIMIENTO_DETALLE == m.UNID_MOVIMIENTO_DETALLE
                                          select r).First();
                            var delI = (from r in Entity.ITEMs
                                        where r.UNID_ITEM == delU_M.UNID_ITEM
                                        select r).First();
                            var DelM = (from r in Entity.MOVIMIENTO_DETALLE
                                        where r.UNID_MOVIMIENTO_DETALLE == m.UNID_MOVIMIENTO_DETALLE
                                        select r).First();

                            //Sync
                            delU_M.IS_ACTIVE = false;
                            delU_M.IS_MODIFIED = true;
                            delU_M.LAST_MODIFIED_DATE = UNID.getNewUNID();

                            delI.IS_ACTIVE = false;
                            delI.IS_MODIFIED = true;
                            delI.LAST_MODIFIED_DATE = UNID.getNewUNID();

                            DelM.IS_ACTIVE = false;
                            DelM.IS_MODIFIED = true;
                            DelM.LAST_MODIFIED_DATE = UNID.getNewUNID();

                            modifiedSync = Entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                            modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                            Entity.SaveChanges();
                        }
                    }
                }
            }
        }

        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
                var resul0 = (from prov in entity.RECIBOes
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from rec in entity.RECIBOes
                         where rec.IS_ACTIVE == true
                         where rec.IS_MODIFIED == false
                         select rec.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonRecibo(long? lastModifiedDate)
        {
            string res = null;
            List<RECIBO> listRecibo = new List<RECIBO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.RECIBOes
                 where p.LAST_MODIFIED_DATE > lastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listRecibo.Add(new RECIBO
                     {
                         UNID_RECIBO = row.UNID_RECIBO,
                         FECHA_CREACION = row.FECHA_CREACION,
                         FECHA_CIERRE = row.FECHA_CIERRE,
                         UNID_SOLICITANTE = row.UNID_SOLICITANTE,
                         PO = row.PO,
                         TT = row.TT,
                         PEDIMIENTO_IMPO = row.PEDIMIENTO_IMPO,
                         PEDIMENTO_EXPO = row.PEDIMENTO_EXPO,
                         UNID_RECIBO_STATUS = row.UNID_RECIBO_STATUS,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listRecibo.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listRecibo);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                RECIBO poco = (RECIBO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.RECIBOes
                                 where poco.UNID_RECIBO == cust.UNID_RECIBO
                                 select cust).ToList();

                    //Actualización
                    if (query.Count > 0)
                    {
                        var aux = query.First();

                        if (aux.LAST_MODIFIED_DATE < poco.LAST_MODIFIED_DATE)
                            udpateElementSync((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElementSyn((object)poco);
                    }

                    var modifiedMenu = entity.RECIBOes.First(p => p.UNID_RECIBO == poco.UNID_RECIBO);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }
        
        public List<MOVIMENTO> GetListMovimiento(RECIBO recibo)
        {
            List<MOVIMENTO> listMovimiento = new List<MOVIMENTO>();

            try
            {
                using (var Entity = new TAE2Entities())
                {
                    var mvtos = (from o in Entity.RECIBOes.Include("RECIBO_MOVIMIENTO.MOVIMENTO.MOVIMIENTO_DETALLE.ITEM.FACTURA_DETALLE.FACTURA.FACTURA_DETALLE")
                                 where o.UNID_RECIBO == recibo.UNID_RECIBO && o.IS_ACTIVE == true 
                                 select o).FirstOrDefault();

                    mvtos.RECIBO_MOVIMIENTO.ToList().ForEach(rm => 
                    {
                        if (rm.IS_ACTIVE == true)
                        {
                            MOVIMENTO m = new MOVIMENTO()
                            {
                                UNID_MOVIMIENTO = rm.MOVIMENTO.UNID_MOVIMIENTO,
                                ALMACEN = rm.MOVIMENTO.ALMACEN,
                                CLIENTE = rm.MOVIMENTO.CLIENTE,
                                CONTACTO = rm.MOVIMENTO.CONTACTO,
                                PROVEEDOR = rm.MOVIMENTO.PROVEEDOR,
                                FECHA_MOVIMIENTO = rm.MOVIMENTO.FECHA_MOVIMIENTO, 
                                FINISHED = rm.MOVIMENTO.FINISHED
                            };

                            FixupCollection<MOVIMIENTO_DETALLE> mdColl = new FixupCollection<MOVIMIENTO_DETALLE>();

                            rm.MOVIMENTO.MOVIMIENTO_DETALLE.ToList().ForEach(md =>
                            {
                                if (md.IS_ACTIVE == true)
                                {
                                    mdColl.Add(new MOVIMIENTO_DETALLE()
                                    {
                                        UNID_MOVIMIENTO_DETALLE = md.UNID_MOVIMIENTO_DETALLE,
                                        UNID_MOVIMIENTO = md.UNID_MOVIMIENTO,
                                        ITEM = new ITEM()
                                        {
                                            UNID_ITEM = md.ITEM.UNID_ITEM,
                                            COSTO_UNITARIO = md.ITEM.COSTO_UNITARIO,
                                            FACTURA_DETALLE = new FACTURA_DETALLE()
                                            {
                                                FACTURA = new FACTURA()
                                                {
                                                    MONEDA = new MONEDA() { 
                                                        MONEDA_ABR = md.ITEM.FACTURA_DETALLE.FACTURA.MONEDA.MONEDA_ABR  ,
                                                        MONEDA_NAME = md.ITEM.FACTURA_DETALLE.FACTURA.MONEDA.MONEDA_NAME,
                                                        UNID_MONEDA = md.ITEM.FACTURA_DETALLE.FACTURA.MONEDA.UNID_MONEDA
                                                    }, 
                                                    FACTURA_NUMERO = md.ITEM.FACTURA_DETALLE.FACTURA.FACTURA_NUMERO,
                                                    FECHA_FACTURA = md.ITEM.FACTURA_DETALLE.FACTURA.FECHA_FACTURA,
                                                    IVA_POR = md.ITEM.FACTURA_DETALLE.FACTURA.IVA_POR,
                                                    TC = md.ITEM.FACTURA_DETALLE.FACTURA.TC,   
                                                    NUMERO_PEDIMENTO = md.ITEM.FACTURA_DETALLE.FACTURA.NUMERO_PEDIMENTO,
                                                    UNID_FACTURA = md.ITEM.FACTURA_DETALLE.FACTURA.UNID_FACTURA,
                                                    UNID_TIPO_PEDIMENTO = md.ITEM.FACTURA_DETALLE.FACTURA.UNID_TIPO_PEDIMENTO,
                                                    UNID_LOTE = md.ITEM.FACTURA_DETALLE.FACTURA.UNID_LOTE,
                                                    UNID_PROVEEDOR = md.ITEM.FACTURA_DETALLE.FACTURA.UNID_PROVEEDOR,
                                                    UNID_MONEDA = md.ITEM.FACTURA_DETALLE.FACTURA.UNID_MONEDA,    
                                                    FACTURA_DETALLE = md.ITEM.FACTURA_DETALLE.FACTURA.FACTURA_DETALLE
                                                },
                                                //= md.ITEM.FACTURA_DETALLE.FACTURA,
                                                UNID_FACTURA_DETALE = md.ITEM.UNID_FACTURA_DETALE,
                                                CANTIDAD = md.ITEM.FACTURA_DETALLE.CANTIDAD,
                                                DESCRIPCION = md.ITEM.FACTURA_DETALLE.DESCRIPCION,
                                                IMPUESTO_UNITARIO = md.ITEM.FACTURA_DETALLE.IMPUESTO_UNITARIO,
                                                PRECIO_UNITARIO = md.ITEM.FACTURA_DETALLE.PRECIO_UNITARIO,
                                                NUMERO = md.ITEM.FACTURA_DETALLE.NUMERO
                                            },
                                            NUMERO_SERIE = md.ITEM.NUMERO_SERIE,
                                            SKU = md.ITEM.SKU, 
                                            CANTIDAD = md.ITEM.CANTIDAD, 
                                            ARTICULO = new ARTICULO()
                                            {
                                                CATEGORIA = md.ITEM.ARTICULO.CATEGORIA,
                                                MARCA = md.ITEM.ARTICULO.MARCA,
                                                MODELO = md.ITEM.ARTICULO.MODELO,
                                                EQUIPO = md.ITEM.ARTICULO.EQUIPO,
                                                ARTICULO1 = md.ITEM.ARTICULO.ARTICULO1,
                                                UNID_ARTICULO = md.ITEM.ARTICULO.UNID_ARTICULO
                                            },
                                            ITEM_STATUS = new ITEM_STATUS()
                                            {
                                                ITEM_STATUS_NAME = md.ITEM.ITEM_STATUS.ITEM_STATUS_NAME,
                                                UNID_ITEM_STATUS = md.ITEM.ITEM_STATUS.UNID_ITEM_STATUS
                                            }
                                        }
                                    });
                                }
                            });//foreach movto detalle

                            m.MOVIMIENTO_DETALLE = mdColl;

                            listMovimiento.Add(m);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listMovimiento;
        }

        public List<RECIBO> getListElements()
        {
            List<RECIBO> recibos = new List<RECIBO>();

            try
            {
                using (var Entity = new TAE2Entities())
                {
                    (from p in Entity.RECIBOes
                     select p).ToList<RECIBO>().ForEach(o => recibos.Add(new RECIBO()
                     {
                         UNID_RECIBO = o.UNID_RECIBO,
                         FECHA_CREACION = o.FECHA_CREACION,
                         PO = o.PO,
                         TT = o.TT,
                         SOLICITANTE = (o.SOLICITANTE!=null)? new SOLICITANTE()
                         {
                             UNID_SOLICITANTE = o.SOLICITANTE.UNID_SOLICITANTE,
                             SOLICITANTE_NAME = o.SOLICITANTE.SOLICITANTE_NAME,
                             EMPRESA = new EMPRESA()
                             {
                                 UNID_EMPRESA = o.SOLICITANTE.UNID_EMPRESA,
                                 EMPRESA_NAME = o.SOLICITANTE.EMPRESA.EMPRESA_NAME
                             },
                             DEPARTAMENTO = new DEPARTAMENTO()
                             {
                                 UNID_DEPARTAMENTO=o.SOLICITANTE.UNID_DEPARTAMENTO,
                                 DEPARTAMENTO_NAME=o.SOLICITANTE.DEPARTAMENTO.DEPARTAMENTO_NAME
                             }
                         }:null
                     }));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return recibos;
        }

        public object getElements()
        {
            throw new NotImplementedException();
        }

        public object getElement(object element)
        {
            throw new NotImplementedException();
        }

        public void udpateElement(object element, USUARIO u)
        {
            if (element != null && (element as RECIBO) != null)
            {
                RECIBO recibo = element as RECIBO;
                using (var Entity = new TAE2Entities())
                {
                    RECIBO r=Entity.RECIBOes.FirstOrDefault(o => o.UNID_RECIBO == recibo.UNID_RECIBO);
                    if (r != null)
                    {
                        r.UNID_SOLICITANTE = recibo.UNID_SOLICITANTE;
                        r.PO = recibo.PO;
                        //Sync
                        r.IS_MODIFIED = true;
                        r.IS_ACTIVE = true;
                        r.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = Entity.SYNCs.FirstOrDefault(p => p.UNID_SYNC == 20120101000000000);
                        if (modifiedSync != null)
                        {
                            modifiedSync.ACTUAL_DATE = UNID.getNewUNID();                        
                        }
                        
                        //
                        Entity.SaveChanges();
                        UNID.Master(recibo, u, -1, "Modificación");
                    }
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null && (element as RECIBO) != null)
            {
                RECIBO recibo = element as RECIBO;
                using (var Entity = new TAE2Entities())
                {
                    RECIBO r = Entity.RECIBOes.FirstOrDefault(o => o.UNID_RECIBO == recibo.UNID_RECIBO);
                    if (r != null)
                    {
                        r.UNID_SOLICITANTE = recibo.UNID_SOLICITANTE;
                        r.PO = recibo.PO;
                        r.IS_ACTIVE = recibo.IS_ACTIVE;
                        //Sync
                        r.IS_MODIFIED = true;
                        r.IS_ACTIVE = true;
                        r.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = Entity.SYNCs.FirstOrDefault(p => p.UNID_SYNC == 20120101000000000);
                        if (modifiedSync != null)
                        {
                            modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        }

                        //
                        Entity.SaveChanges();
                    }
                }
            }
        }

        public void insertElement(object element, USUARIO u)
        {
            if (element != null && (element as RECIBO) != null)
            {
                using (var entity = new TAE2Entities())
                {
                    RECIBO item = (RECIBO)element;
                    //Sync
                    item.IS_MODIFIED = true;
                    item.IS_ACTIVE = true;
                    item.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.RECIBOes.AddObject(item);
                    entity.SaveChanges();

                    UNID.Master(item, u, -1, "Inserción");
                }
            }
        }

        public void insertElementSyn(object element)
        {
            if (element != null && (element as RECIBO) != null)
            {
                using (var entity = new TAE2Entities())
                {
                    RECIBO item = (RECIBO)element;
                    //Sync
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.RECIBOes.AddObject(item);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element, USUARIO u)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<RECIBO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de RECIBO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonRecibo()
        {
            string res = null;
            List<RECIBO> listRecibo = new List<RECIBO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.RECIBOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listRecibo.Add(new RECIBO
                     {
                         UNID_RECIBO=row.UNID_RECIBO,
                         FECHA_CREACION=row.FECHA_CREACION,
                         FECHA_CIERRE=row.FECHA_CIERRE,
                         UNID_SOLICITANTE=row.UNID_SOLICITANTE,
                         PO=row.PO,
                         TT=row.TT,
                         PEDIMIENTO_IMPO=row.PEDIMIENTO_IMPO,
                         PEDIMENTO_EXPO=row.PEDIMENTO_EXPO,
                         UNID_RECIBO_STATUS=row.UNID_RECIBO_STATUS,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listRecibo.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listRecibo);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<RECIBO>
        /// </summary>
        /// <returns>Regresa List<RECIBO></returns>
        /// <returns>Si no regresa null</returns>
        public List<RECIBO> GetDeserializeRecibo(string listPocos)
        {
            List<RECIBO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<RECIBO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetRecibo()
        {
            List<RECIBO> reset = new List<RECIBO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.RECIBOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new RECIBO
                     {
                         UNID_RECIBO = row.UNID_RECIBO,
                         FECHA_CREACION = row.FECHA_CREACION,
                         FECHA_CIERRE = row.FECHA_CIERRE,
                         UNID_SOLICITANTE = row.UNID_SOLICITANTE,
                         PO = row.PO,
                         TT = row.TT,
                         PEDIMIENTO_IMPO = row.PEDIMIENTO_IMPO,
                         PEDIMENTO_EXPO = row.PEDIMENTO_EXPO,
                         UNID_RECIBO_STATUS = row.UNID_RECIBO_STATUS,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.RECIBOes.First(p => p.UNID_RECIBO == item.UNID_RECIBO);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }


        public void deleteElement(object element)
        {
            throw new NotImplementedException();
        }


        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            throw new NotImplementedException();
        }
    }
}
