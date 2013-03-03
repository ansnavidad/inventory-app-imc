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
    }
}
