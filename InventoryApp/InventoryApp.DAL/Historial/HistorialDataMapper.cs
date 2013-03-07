using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace InventoryApp.DAL.Historial
{
    public class HistorialDataMapper
    {
        public List<MASTER_INVENTARIOS> GetElementsByUNID(long unid, string s)
        {
            //UNID_USUARIO_MOD	    USUARIO2
            //UNID_USUARIO_CREADOR	USUARIO1
            switch (s)
            {
                case "BANCO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS      
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_BANCO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "MAXMIN":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_MAX_MIN == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "ROL":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_ROL == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }
                case "FACTURA":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_FACTURA == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }
                case "RECIBO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_RECIBO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }
                case "ITEM":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_ITEM == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "PROGRAMADO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_PROGRAMADO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }
                case "CATEGORIA":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_CATEGORIA == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "ARTICULO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_ARTICULO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "MODELO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_MODELO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "MARCA":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_MARCA == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "EQUIPO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_EQUIPO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "CIUDAD":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_CIUDAD == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "CLIENTE":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_CLIENTE == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "DEPARTAMENTO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_DEPARTAMENTO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "EMPRESA":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_EMPRESA == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "INFRAESTRUCTURA":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_INFRAESTRUCTURA == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "ITEMSTATUS":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_ITEM_STATUS == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "MEDIOENVIO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_MEDIO_ENVIO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "MONEDA":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_MONEDA == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "PAIS":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_PAIS == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "PROPIEDAD":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_PROPIEDAD == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "PROYECTO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_PROYECTO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "SERVICIO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_SERVICIO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "SOLICITANTE":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_SOLICITANTE == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "TERMINOENVIO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_TERMINO_ENVIO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "TIPOCOTIZACION":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_TIPO_COTIZACION == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "TIPOEMPRESA":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_TIPO_EMPRESA == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "TIPOPEDIMENTO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_TIPO_PEDIMENTO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "TRANSPORTE":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_TRANSPORTE == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "UNIDAD":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_UNIDAD == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "ALMACEN":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_ALMACEN == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "PROVEEDOR":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_PROVEEDOR == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }

                case "MOVIMENTO":

                    using (var Entity = new TAE2Entities())
                    {
                        var res = (from p in Entity.MASTER_INVENTARIOS
                                   .Include("USUARIO")
                                   .Include("USUARIO1")
                                   .Include("USUARIO2")
                                   where p.IS_ACTIVE == true && p.UNID_MOVIMENTO == unid
                                   select p).OrderBy(p => p.UNID_MASTER_INVENTARIOS).ToList();
                        return res;
                    }
            }
            return null;
        }

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
                var resul0 = (from prov in entity.MASTER_INVENTARIOS
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from banco in entity.MASTER_INVENTARIOS
                         where banco.IS_ACTIVE == true
                         where banco.IS_MODIFIED == false
                         select banco.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonMasterInventarios(long? Last_Modified_Date)
        {
            string res = null;
            List<MASTER_INVENTARIOS> list = new List<MASTER_INVENTARIOS>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MASTER_INVENTARIOS
                 where p.LAST_MODIFIED_DATE > Last_Modified_Date
                 select p).ToList().ForEach(row =>
                 {
                     list.Add(new MASTER_INVENTARIOS
                     {
                         UNID_MASTER_INVENTARIOS = row.UNID_MASTER_INVENTARIOS,
                         UNID_USUARIO_MOD = row.UNID_USUARIO_MOD,
                         FECHA = row.FECHA,
                         ROLES = row.ROLES,
                         UNID_USUARIO_CREADOR=row.UNID_USUARIO_CREADOR,
                         MODIFICACIONES = row.MODIFICACIONES,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         UNID_MENU = row.UNID_MENU,
                         UNID_ROL=row.UNID_ROL,
                         UNID_ROL_MENU=row.UNID_ROL_MENU,
                         UNID_USUARIO = row.UNID_USUARIO,
                         UNID_USUARIO_ROL=row.UNID_USUARIO_ROL,
                         UNID_ARTICULO=row.UNID_ARTICULO,
                         UNID_CATEGORIA=row.UNID_CATEGORIA,
                         UNID_EQUIPO=row.UNID_EQUIPO,
                         UNID_MARCA=row.UNID_MARCA,
                         UNID_MODELO=row.UNID_MODELO,
                         UNID_TEST=row.UNID_TEST,
                         UNID_BANCO=row.UNID_BANCO,
                         UNID_DEPARTAMENTO=row.UNID_DEPARTAMENTO,
                         UNID_EMPRESA=row.UNID_EMPRESA,
                         UNID_INFRAESTRUCTURA=row.UNID_INFRAESTRUCTURA,
                         UNID_MEDIO_ENVIO=row.UNID_MEDIO_ENVIO,
                         UNID_MONEDA=row.UNID_MONEDA,
                         UNID_PROCESS_BATCH=row.UNID_PROCESS_BATCH,
                         UNID_PROVEEDOR= row.UNID_PROVEEDOR,
                         UNID_PROVEEDOR_CATEGORIA=row.UNID_PROVEEDOR_CATEGORIA,
                         UNID_PROVEEDOR_CUENTA=row.UNID_PROVEEDOR_CUENTA,
                         UNID_PROYECTO=row.UNID_PROYECTO,
                         UNID_SERVER_LASTDATA= row.UNID_SERVER_LASTDATA,
                         UNID_SOLICITANTE= row.UNID_SOLICITANTE,
                         UNID_SYNC= row.UNID_SYNC,
                         UNID_TERMINO_ENVIO=row.UNID_TERMINO_ENVIO,
                         UNID_TIPO_COTIZACION=row.UNID_TIPO_COTIZACION,
                         UNID_TIPO_EMPRESA=row.UNID_TIPO_EMPRESA,
                         UNID_TIPO_PEDIMENTO=row.UNID_TIPO_PEDIMENTO,
                         UNID_TRANSPORTE=row.UNID_TRANSPORTE,
                         UNID_UPLOAD_LOG=row.UNID_UPLOAD_LOG,
                         UNID_ALMACEN= row.UNID_ALMACEN,
                         UNID_ALMACEN_TECNICO=row.UNID_ALMACEN_TECNICO,
                         UNID_CLIENTE=row.UNID_CLIENTE,
                         UNID_ITEM_STATUS=row.UNID_ITEM_STATUS,
                         UNID_MAX_MIN=row.UNID_MAX_MIN,
                         UNID_PROGRAMADO=row.UNID_PROGRAMADO,
                         UNID_PROPIEDAD=row.UNID_PROPIEDAD,
                         UNID_SERVICIO=row.UNID_SERVICIO,
                         UNID_TECNICO=row.UNID_TECNICO,
                         UNID_TIPO_MOVIMIENTO= row.UNID_TIPO_MOVIMIENTO,
                         UNID_UNIDAD= row.UNID_UNIDAD,
                         UNID_COTIZACION=row.UNID_COTIZACION,
                         UNID_CIUDAD=row.UNID_CIUDAD,
                         UNID_PAIS=row.UNID_PAIS,
                         UNID_FACTURA_VENTA=row.UNID_FACTURA_VENTA,
                         UNID_ITEM=row.UNID_ITEM,
                         UNID_MOVIMENTO=row.UNID_MOVIMENTO,
                         UNID_MOVIMIENTO_DETALLE=row.UNID_MOVIMIENTO_DETALLE,
                         UNID_RECIBO=row.UNID_RECIBO,
                         UNID_RECIBO_MOVIMIENTO=row.UNID_RECIBO_MOVIMIENTO,
                         UNID_RECIBO_STATUS=row.UNID_RECIBO_STATUS,
                         UNID_ULTIMO_MOVIMIENTO=row.UNID_ULTIMO_MOVIMIENTO,
                         UNID_FACTURA=row.UNID_FACTURA,
                         UNID_FACTURA_DETALLE=row.UNID_FACTURA_DETALLE,
                         UNID_LOTE=row.UNID_LOTE,
                         UNID_PEDIMENTO=row.UNID_PEDIMENTO,
                         UNID_POM=row.UNID_POM,
                         UNID_POM_ARTICULO=row.UNID_POM_ARTICULO,
                         UNID_FACTURA_MOD=row.UNID_FACTURA_MOD,
                         UNID_FACTURA_DETALLE_MOD=row.UNID_FACTURA_DETALLE_MOD,
                         UNID_ITEM_MOD=row.UNID_ITEM_MOD,
                         UNID_MOVIMIENTO_DETALLE_MOD=row.UNID_MOVIMIENTO_DETALLE_MOD,
                         UNID_ULTIMO_MOVIMIENTO_MOD=row.UNID_ULTIMO_MOVIMIENTO_MOD,
                         UNID_INVENTARIO=row.UNID_INVENTARIO,
                         UNID_BATCH_LOAD=row.UNID_BATCH_LOAD,
                         UNID_LOAD=row.UNID_LOAD
                         
                     });
                 });
                if (list.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(list);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                MASTER_INVENTARIOS poco = (MASTER_INVENTARIOS)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.MASTER_INVENTARIOS
                                 where poco.UNID_MASTER_INVENTARIOS == cust.UNID_MASTER_INVENTARIOS
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
                        insertElementSync((object)poco);
                    }

                    var modified = entity.MASTER_INVENTARIOS.First(p => p.UNID_MASTER_INVENTARIOS == poco.UNID_MASTER_INVENTARIOS);
                    modified.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public void udpateElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MASTER_INVENTARIOS inven = (MASTER_INVENTARIOS)element;
                    var modifiedInve = entity.MASTER_INVENTARIOS.First(p => p.UNID_MASTER_INVENTARIOS == inven.UNID_MASTER_INVENTARIOS);
                         
                         modifiedInve.UNID_USUARIO_MOD = inven.UNID_USUARIO_MOD;
                         modifiedInve.FECHA = inven.FECHA;
                         modifiedInve.ROLES = inven.ROLES;
                         modifiedInve.UNID_USUARIO_CREADOR=inven.UNID_USUARIO_CREADOR;
                         modifiedInve.MODIFICACIONES = inven.MODIFICACIONES;
                         modifiedInve.IS_ACTIVE = inven.IS_ACTIVE;
                         modifiedInve.UNID_MENU = inven.UNID_MENU;
                         modifiedInve.UNID_ROL=inven.UNID_ROL;
                         modifiedInve.UNID_ROL_MENU=inven.UNID_ROL_MENU;
                         modifiedInve.UNID_USUARIO = inven.UNID_USUARIO;
                         modifiedInve.UNID_USUARIO_ROL=inven.UNID_USUARIO_ROL;
                         modifiedInve.UNID_ARTICULO=inven.UNID_ARTICULO;
                         modifiedInve.UNID_CATEGORIA=inven.UNID_CATEGORIA;
                         modifiedInve.UNID_EQUIPO=inven.UNID_EQUIPO;
                         modifiedInve.UNID_MARCA=inven.UNID_MARCA;
                         modifiedInve.UNID_MODELO=inven.UNID_MODELO;
                         modifiedInve.UNID_TEST=inven.UNID_TEST;
                         modifiedInve.UNID_BANCO=inven.UNID_BANCO;
                         modifiedInve.UNID_DEPARTAMENTO=inven.UNID_DEPARTAMENTO;
                         modifiedInve.UNID_EMPRESA=inven.UNID_EMPRESA;
                         modifiedInve.UNID_INFRAESTRUCTURA=inven.UNID_INFRAESTRUCTURA;
                         modifiedInve.UNID_MEDIO_ENVIO=inven.UNID_MEDIO_ENVIO;
                         modifiedInve.UNID_MONEDA=inven.UNID_MONEDA;
                         modifiedInve.UNID_PROCESS_BATCH=inven.UNID_PROCESS_BATCH;
                         modifiedInve.UNID_PROVEEDOR= inven.UNID_PROVEEDOR;
                         modifiedInve.UNID_PROVEEDOR_CATEGORIA=inven.UNID_PROVEEDOR_CATEGORIA;
                         modifiedInve.UNID_PROVEEDOR_CUENTA=inven.UNID_PROVEEDOR_CUENTA;
                         modifiedInve.UNID_PROYECTO=inven.UNID_PROYECTO;
                         modifiedInve.UNID_SERVER_LASTDATA= inven.UNID_SERVER_LASTDATA;
                         modifiedInve.UNID_SOLICITANTE= inven.UNID_SOLICITANTE;
                         modifiedInve.UNID_SYNC= inven.UNID_SYNC;
                         modifiedInve.UNID_TERMINO_ENVIO=inven.UNID_TERMINO_ENVIO;
                         modifiedInve.UNID_TIPO_COTIZACION=inven.UNID_TIPO_COTIZACION;
                         modifiedInve.UNID_TIPO_EMPRESA=inven.UNID_TIPO_EMPRESA;
                         modifiedInve.UNID_TIPO_PEDIMENTO=inven.UNID_TIPO_PEDIMENTO;
                         modifiedInve.UNID_TRANSPORTE=inven.UNID_TRANSPORTE;
                         modifiedInve.UNID_UPLOAD_LOG=inven.UNID_UPLOAD_LOG;
                         modifiedInve.UNID_ALMACEN= inven.UNID_ALMACEN;
                         modifiedInve.UNID_ALMACEN_TECNICO=inven.UNID_ALMACEN_TECNICO;
                         modifiedInve.UNID_CLIENTE=inven.UNID_CLIENTE;
                         modifiedInve.UNID_ITEM_STATUS=inven.UNID_ITEM_STATUS;
                         modifiedInve.UNID_MAX_MIN=inven.UNID_MAX_MIN;
                         modifiedInve.UNID_PROGRAMADO=inven.UNID_PROGRAMADO;
                         modifiedInve.UNID_PROPIEDAD=inven.UNID_PROPIEDAD;
                         modifiedInve.UNID_SERVICIO=inven.UNID_SERVICIO;
                         modifiedInve.UNID_TECNICO=inven.UNID_TECNICO;
                         modifiedInve.UNID_TIPO_MOVIMIENTO= inven.UNID_TIPO_MOVIMIENTO;
                         modifiedInve.UNIDAD= inven.UNIDAD;
                         modifiedInve.UNID_COTIZACION=inven.UNID_COTIZACION;
                         modifiedInve.UNID_CIUDAD=inven.UNID_CIUDAD;
                         modifiedInve.UNID_PAIS=inven.UNID_PAIS;
                         modifiedInve.UNID_FACTURA_VENTA=inven.UNID_FACTURA_VENTA;
                         modifiedInve.UNID_ITEM=inven.UNID_ITEM;
                         modifiedInve.UNID_MOVIMENTO=inven.UNID_MOVIMENTO;
                         modifiedInve.UNID_MOVIMIENTO_DETALLE=inven.UNID_MOVIMIENTO_DETALLE;
                         modifiedInve.UNID_RECIBO=inven.UNID_RECIBO;
                         modifiedInve.UNID_RECIBO_MOVIMIENTO=inven.UNID_RECIBO_MOVIMIENTO;
                         modifiedInve.UNID_RECIBO_STATUS=inven.UNID_RECIBO_STATUS;
                         modifiedInve.UNID_ULTIMO_MOVIMIENTO=inven.UNID_ULTIMO_MOVIMIENTO;
                         modifiedInve.UNID_FACTURA=inven.UNID_FACTURA;
                         modifiedInve.UNID_FACTURA_DETALLE=inven.UNID_FACTURA_DETALLE;
                         modifiedInve.UNID_LOTE=inven.UNID_LOTE;
                         modifiedInve.UNID_PEDIMENTO=inven.UNID_PEDIMENTO;
                         modifiedInve.UNID_POM=inven.UNID_POM;
                         modifiedInve.UNID_POM_ARTICULO=inven.UNID_POM_ARTICULO;
                         modifiedInve.UNID_FACTURA_MOD=inven.UNID_FACTURA_MOD;
                         modifiedInve.UNID_FACTURA_DETALLE_MOD=inven.UNID_FACTURA_DETALLE_MOD;
                         modifiedInve.UNID_ITEM_MOD=inven.UNID_ITEM_MOD;
                         modifiedInve.UNID_MOVIMIENTO_DETALLE_MOD=inven.UNID_MOVIMIENTO_DETALLE_MOD;
                         modifiedInve.UNID_ULTIMO_MOVIMIENTO_MOD=inven.UNID_ULTIMO_MOVIMIENTO_MOD;
                         modifiedInve.UNID_INVENTARIO=inven.UNID_INVENTARIO;
                         modifiedInve.UNID_BATCH_LOAD=inven.UNID_BATCH_LOAD;
                         modifiedInve.UNID_LOAD = inven.UNID_LOAD;

                    //Sync
                    modifiedInve.IS_MODIFIED = true;
                    modifiedInve.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //                   
                    entity.SaveChanges();
                }
            }
        }

        public void insertElementSync(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MASTER_INVENTARIOS inve = (MASTER_INVENTARIOS)element;

                    //Sync

                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MASTER_INVENTARIOS.AddObject(inve);
                    entity.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Método que serializa una List<MASTER_INVENTARIOS> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de MASTER_INVENTARIOS</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonMasterInventarios()
        {
            string res = null;
            List<MASTER_INVENTARIOS> list = new List<MASTER_INVENTARIOS>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MASTER_INVENTARIOS
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     list.Add(new MASTER_INVENTARIOS
                     {
                         UNID_MASTER_INVENTARIOS = row.UNID_MASTER_INVENTARIOS,
                         UNID_USUARIO_MOD = row.UNID_USUARIO_MOD,
                         FECHA = row.FECHA,
                         ROLES = row.ROLES,
                         UNID_USUARIO_CREADOR = row.UNID_USUARIO_CREADOR,
                         MODIFICACIONES = row.MODIFICACIONES,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         UNID_MENU = row.UNID_MENU,
                         UNID_ROL = row.UNID_ROL,
                         UNID_ROL_MENU = row.UNID_ROL_MENU,
                         UNID_USUARIO = row.UNID_USUARIO,
                         UNID_USUARIO_ROL = row.UNID_USUARIO_ROL,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         UNID_CATEGORIA = row.UNID_CATEGORIA,
                         UNID_EQUIPO = row.UNID_EQUIPO,
                         UNID_MARCA = row.UNID_MARCA,
                         UNID_MODELO = row.UNID_MODELO,
                         UNID_TEST = row.UNID_TEST,
                         UNID_BANCO = row.UNID_BANCO,
                         UNID_DEPARTAMENTO = row.UNID_DEPARTAMENTO,
                         UNID_EMPRESA = row.UNID_EMPRESA,
                         UNID_INFRAESTRUCTURA = row.UNID_INFRAESTRUCTURA,
                         UNID_MEDIO_ENVIO = row.UNID_MEDIO_ENVIO,
                         UNID_MONEDA = row.UNID_MONEDA,
                         UNID_PROCESS_BATCH = row.UNID_PROCESS_BATCH,
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         UNID_PROVEEDOR_CATEGORIA = row.UNID_PROVEEDOR_CATEGORIA,
                         UNID_PROVEEDOR_CUENTA = row.UNID_PROVEEDOR_CUENTA,
                         UNID_PROYECTO = row.UNID_PROYECTO,
                         UNID_SERVER_LASTDATA = row.UNID_SERVER_LASTDATA,
                         UNID_SOLICITANTE = row.UNID_SOLICITANTE,
                         UNID_SYNC = row.UNID_SYNC,
                         UNID_TERMINO_ENVIO = row.UNID_TERMINO_ENVIO,
                         UNID_TIPO_COTIZACION = row.UNID_TIPO_COTIZACION,
                         UNID_TIPO_EMPRESA = row.UNID_TIPO_EMPRESA,
                         UNID_TIPO_PEDIMENTO = row.UNID_TIPO_PEDIMENTO,
                         UNID_TRANSPORTE = row.UNID_TRANSPORTE,
                         UNID_UPLOAD_LOG = row.UNID_UPLOAD_LOG,
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_ALMACEN_TECNICO = row.UNID_ALMACEN_TECNICO,
                         UNID_CLIENTE = row.UNID_CLIENTE,
                         UNID_ITEM_STATUS = row.UNID_ITEM_STATUS,
                         UNID_MAX_MIN = row.UNID_MAX_MIN,
                         UNID_PROGRAMADO = row.UNID_PROGRAMADO,
                         UNID_PROPIEDAD = row.UNID_PROPIEDAD,
                         UNID_SERVICIO = row.UNID_SERVICIO,
                         UNID_TECNICO = row.UNID_TECNICO,
                         UNID_TIPO_MOVIMIENTO = row.UNID_TIPO_MOVIMIENTO,
                         UNID_UNIDAD = row.UNID_UNIDAD,
                         UNID_COTIZACION = row.UNID_COTIZACION,
                         UNID_CIUDAD = row.UNID_CIUDAD,
                         UNID_PAIS = row.UNID_PAIS,
                         UNID_FACTURA_VENTA = row.UNID_FACTURA_VENTA,
                         UNID_ITEM = row.UNID_ITEM,
                         UNID_MOVIMENTO = row.UNID_MOVIMENTO,
                         UNID_MOVIMIENTO_DETALLE = row.UNID_MOVIMIENTO_DETALLE,
                         UNID_RECIBO = row.UNID_RECIBO,
                         UNID_RECIBO_MOVIMIENTO = row.UNID_RECIBO_MOVIMIENTO,
                         UNID_RECIBO_STATUS = row.UNID_RECIBO_STATUS,
                         UNID_ULTIMO_MOVIMIENTO = row.UNID_ULTIMO_MOVIMIENTO,
                         UNID_FACTURA = row.UNID_FACTURA,
                         UNID_FACTURA_DETALLE = row.UNID_FACTURA_DETALLE,
                         UNID_LOTE = row.UNID_LOTE,
                         UNID_PEDIMENTO = row.UNID_PEDIMENTO,
                         UNID_POM = row.UNID_POM,
                         UNID_POM_ARTICULO = row.UNID_POM_ARTICULO,
                         UNID_FACTURA_MOD = row.UNID_FACTURA_MOD,
                         UNID_FACTURA_DETALLE_MOD = row.UNID_FACTURA_DETALLE_MOD,
                         UNID_ITEM_MOD = row.UNID_ITEM_MOD,
                         UNID_MOVIMIENTO_DETALLE_MOD = row.UNID_MOVIMIENTO_DETALLE_MOD,
                         UNID_ULTIMO_MOVIMIENTO_MOD = row.UNID_ULTIMO_MOVIMIENTO_MOD,
                         UNID_INVENTARIO = row.UNID_INVENTARIO,
                         UNID_BATCH_LOAD = row.UNID_BATCH_LOAD,
                         UNID_LOAD = row.UNID_LOAD
                     });
                 });
                if (list.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(list);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<MASTER_INVENTARIOS> 
        /// </summary>
        /// <returns>Regresa List<MASTER_INVENTARIOS></returns>
        /// <returns>Si regresa null</returns>
        public List<MASTER_INVENTARIOS> GetDeserializeMasterInventarios(string listPocos)
        {
            List<MASTER_INVENTARIOS> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<MASTER_INVENTARIOS>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetMasterInventarios()
        {
            List<MASTER_INVENTARIOS> reset = new List<MASTER_INVENTARIOS>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MASTER_INVENTARIOS
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new MASTER_INVENTARIOS
                     {
                         UNID_MASTER_INVENTARIOS = row.UNID_MASTER_INVENTARIOS,
                         UNID_USUARIO_MOD = row.UNID_USUARIO_MOD,
                         FECHA = row.FECHA,
                         ROLES = row.ROLES,
                         UNID_USUARIO_CREADOR = row.UNID_USUARIO_CREADOR,
                         MODIFICACIONES = row.MODIFICACIONES,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,
                         UNID_MENU = row.UNID_MENU,
                         UNID_ROL = row.UNID_ROL,
                         UNID_ROL_MENU = row.UNID_ROL_MENU,
                         UNID_USUARIO = row.UNID_USUARIO,
                         UNID_USUARIO_ROL = row.UNID_USUARIO_ROL,
                         UNID_ARTICULO = row.UNID_ARTICULO,
                         UNID_CATEGORIA = row.UNID_CATEGORIA,
                         UNID_EQUIPO = row.UNID_EQUIPO,
                         UNID_MARCA = row.UNID_MARCA,
                         UNID_MODELO = row.UNID_MODELO,
                         UNID_TEST = row.UNID_TEST,
                         UNID_BANCO = row.UNID_BANCO,
                         UNID_DEPARTAMENTO = row.UNID_DEPARTAMENTO,
                         UNID_EMPRESA = row.UNID_EMPRESA,
                         UNID_INFRAESTRUCTURA = row.UNID_INFRAESTRUCTURA,
                         UNID_MEDIO_ENVIO = row.UNID_MEDIO_ENVIO,
                         UNID_MONEDA = row.UNID_MONEDA,
                         UNID_PROCESS_BATCH = row.UNID_PROCESS_BATCH,
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         UNID_PROVEEDOR_CATEGORIA = row.UNID_PROVEEDOR_CATEGORIA,
                         UNID_PROVEEDOR_CUENTA = row.UNID_PROVEEDOR_CUENTA,
                         UNID_PROYECTO = row.UNID_PROYECTO,
                         UNID_SERVER_LASTDATA = row.UNID_SERVER_LASTDATA,
                         UNID_SOLICITANTE = row.UNID_SOLICITANTE,
                         UNID_SYNC = row.UNID_SYNC,
                         UNID_TERMINO_ENVIO = row.UNID_TERMINO_ENVIO,
                         UNID_TIPO_COTIZACION = row.UNID_TIPO_COTIZACION,
                         UNID_TIPO_EMPRESA = row.UNID_TIPO_EMPRESA,
                         UNID_TIPO_PEDIMENTO = row.UNID_TIPO_PEDIMENTO,
                         UNID_TRANSPORTE = row.UNID_TRANSPORTE,
                         UNID_UPLOAD_LOG = row.UNID_UPLOAD_LOG,
                         UNID_ALMACEN = row.UNID_ALMACEN,
                         UNID_ALMACEN_TECNICO = row.UNID_ALMACEN_TECNICO,
                         UNID_CLIENTE = row.UNID_CLIENTE,
                         UNID_ITEM_STATUS = row.UNID_ITEM_STATUS,
                         UNID_MAX_MIN = row.UNID_MAX_MIN,
                         UNID_PROGRAMADO = row.UNID_PROGRAMADO,
                         UNID_PROPIEDAD = row.UNID_PROPIEDAD,
                         UNID_SERVICIO = row.UNID_SERVICIO,
                         UNID_TECNICO = row.UNID_TECNICO,
                         UNID_TIPO_MOVIMIENTO = row.UNID_TIPO_MOVIMIENTO,
                         UNID_UNIDAD = row.UNID_UNIDAD,
                         UNID_COTIZACION = row.UNID_COTIZACION,
                         UNID_CIUDAD = row.UNID_CIUDAD,
                         UNID_PAIS = row.UNID_PAIS,
                         UNID_FACTURA_VENTA = row.UNID_FACTURA_VENTA,
                         UNID_ITEM = row.UNID_ITEM,
                         UNID_MOVIMENTO = row.UNID_MOVIMENTO,
                         UNID_MOVIMIENTO_DETALLE = row.UNID_MOVIMIENTO_DETALLE,
                         UNID_RECIBO = row.UNID_RECIBO,
                         UNID_RECIBO_MOVIMIENTO = row.UNID_RECIBO_MOVIMIENTO,
                         UNID_RECIBO_STATUS = row.UNID_RECIBO_STATUS,
                         UNID_ULTIMO_MOVIMIENTO = row.UNID_ULTIMO_MOVIMIENTO,
                         UNID_FACTURA = row.UNID_FACTURA,
                         UNID_FACTURA_DETALLE = row.UNID_FACTURA_DETALLE,
                         UNID_LOTE = row.UNID_LOTE,
                         UNID_PEDIMENTO = row.UNID_PEDIMENTO,
                         UNID_POM = row.UNID_POM,
                         UNID_POM_ARTICULO = row.UNID_POM_ARTICULO,
                         UNID_FACTURA_MOD = row.UNID_FACTURA_MOD,
                         UNID_FACTURA_DETALLE_MOD = row.UNID_FACTURA_DETALLE_MOD,
                         UNID_ITEM_MOD = row.UNID_ITEM_MOD,
                         UNID_MOVIMIENTO_DETALLE_MOD = row.UNID_MOVIMIENTO_DETALLE_MOD,
                         UNID_ULTIMO_MOVIMIENTO_MOD = row.UNID_ULTIMO_MOVIMIENTO_MOD,
                         UNID_INVENTARIO = row.UNID_INVENTARIO,
                         UNID_BATCH_LOAD = row.UNID_BATCH_LOAD,
                         UNID_LOAD = row.UNID_LOAD
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.MASTER_INVENTARIOS.First(p => p.UNID_MASTER_INVENTARIOS == item.UNID_MASTER_INVENTARIOS);
                        modified.IS_MODIFIED = false;
                        Entity.SaveChanges();
                    }
                }
            }
        }
    }
}
