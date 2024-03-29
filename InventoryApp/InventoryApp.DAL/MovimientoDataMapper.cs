﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;
using Newtonsoft.Json;

namespace InventoryApp.DAL
{
    public class MovimientoDataMapper : IDataMapper
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
                var resul0 = (from prov in entity.MOVIMENTOes
                              where prov.IS_ACTIVE == true
                              where prov.IS_MODIFIED == false
                              select prov.LAST_MODIFIED_DATE).ToList();

                if (resul0.Count == 0)
                    return 0;

                resul = (from mov in entity.MOVIMENTOes
                         where mov.IS_ACTIVE == true
                         where mov.IS_MODIFIED == false
                         select mov.LAST_MODIFIED_DATE).Max();
                return resul;
            }

        }

        public string GetJsonMovimiento(long? LastModifiedDate)
        {
            string res = null;
            List<MOVIMENTO> listMovimiento = new List<MOVIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MOVIMENTOes
                 where p.LAST_MODIFIED_DATE > LastModifiedDate
                 select p).ToList().ForEach(row =>
                 {
                     listMovimiento.Add(new MOVIMENTO
                     {
                         UNID_MOVIMIENTO = row.UNID_MOVIMIENTO,
                         FECHA_MOVIMIENTO = row.FECHA_MOVIMIENTO,
                         UNID_TIPO_MOVIMIENTO = row.UNID_TIPO_MOVIMIENTO,
                         UNID_ALMACEN_DESTINO = row.UNID_ALMACEN_DESTINO,
                         UNID_PROVEEDOR_DESTINO = row.UNID_PROVEEDOR_DESTINO,
                         UNID_CLIENTE_DESTINO = row.UNID_CLIENTE_DESTINO,
                         UNID_ALMACEN_PROCEDENCIA = row.UNID_ALMACEN_PROCEDENCIA,
                         UNID_CLIENTE_PROCEDENCIA = row.UNID_CLIENTE_PROCEDENCIA,
                         UNID_PROVEEDOR_PROCEDENCIA = row.UNID_PROVEEDOR_PROCEDENCIA,
                         UNID_SERVICIO = row.UNID_SERVICIO, 
                         UNID_TECNICO_TRAS = row.UNID_TECNICO_TRAS,
                         TT = row.TT,
                         CONTACTO = row.CONTACTO,
                         UNID_TRANSPORTE = row.UNID_TRANSPORTE,
                         DIRECCION_ENVIO = row.DIRECCION_ENVIO,
                         SITIO_ENLACE = row.SITIO_ENLACE,
                         NOMBRE_SITIO = row.NOMBRE_SITIO,
                         RECIBE = row.RECIBE,
                         GUIA = row.GUIA,
                         UNID_CLIENTE = row.UNID_CLIENTE,
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         UNID_FACTURA_VENTA = row.UNID_FACTURA_VENTA,
                         PEDIMIENTO_IMPO = row.PEDIMIENTO_IMPO,
                         PEDIMIENTO_EXPO = row.PEDIMIENTO_EXPO,
                         UNID_SOLICITANTE = row.UNID_SOLICITANTE,
                         UNID_TECNICO = row.UNID_TECNICO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,                         
                         UNID_INFRAESTRUCTURA = row.UNID_INFRAESTRUCTURA,
                         FINISHED = row.FINISHED
                     });
                 });
                if (listMovimiento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMovimiento);
                }
                return res;
            }
        }

        public void loadSync(object element)
        {
            if (element != null)
            {
                MOVIMENTO poco = (MOVIMENTO)element;
                using (var entity = new TAE2Entities())
                {
                    var query = (from cust in entity.MOVIMENTOes
                                 where poco.UNID_MOVIMIENTO == cust.UNID_MOVIMIENTO
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
                        insertElementSyn((object)poco);
                    }

                    var modifiedMenu = entity.MOVIMENTOes.First(p => p.UNID_MOVIMIENTO == poco.UNID_MOVIMIENTO);
                    modifiedMenu.IS_MODIFIED = false;
                    entity.SaveChanges();
                }
            }
        }

        public object getElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                               .Include("ALMACEN")
                               .Include("ALMACEN1")
                               .Include("CLIENTE")
                               .Include("CLIENTE1")
                               .Include("CLIENTE2")
                               .Include("FACTURA_VENTA")
                               .Include("PROVEEDOR")
                               .Include("PROVEEDOR1")
                               .Include("PROVEEDOR2")
                               .Include("SERVICIO")
                               .Include("SOLICITANTE")
                               .Include("TIPO_MOVIMIENTO")
                               .Include("TRANSPORTE")
                               .Include("MOVIMIENTO_DETALLE")
                               .Include("TECNICO")
                               .Include("INFRAESTRUCTURA")
                               .Include("TECNICO1")
                               .Include("ALMACEN")
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();
                return res;
            }
        }     

        #region GetElements para los Grids de Movimientos (Salidas/Traspaso/Entradas)

        //Ya no se usa
        public object getEntradasElements()
        {
            using (var Entity = new TAE2Entities())
            {
                Entity.SaveChanges();

                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           .Include("RECIBO_MOVIMIENTO")
                           where p.UNID_TIPO_MOVIMIENTO ==1 || p.UNID_TIPO_MOVIMIENTO==2 || p.UNID_TIPO_MOVIMIENTO==3|| p.UNID_TIPO_MOVIMIENTO==4
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();
                           

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 1 || trans.UNID_TIPO_MOVIMIENTO == 2 || trans.UNID_TIPO_MOVIMIENTO == 3 || trans.UNID_TIPO_MOVIMIENTO == 4)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        trans.RECIBO_MOVIMIENTO = trans.RECIBO_MOVIMIENTO;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;

                return res;
            }
        }
        //Ya no se usa
        public object getSalidasElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           .Include("RECIBO_MOVIMIENTO")
                           where p.UNID_TIPO_MOVIMIENTO > 4 && p.UNID_TIPO_MOVIMIENTO < 16
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO > 4 && trans.UNID_TIPO_MOVIMIENTO < 16)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getEntradaValidacionElements()
        {
            using (var Entity = new TAE2Entities())
            {
                Entity.SaveChanges();

                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO==1
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 1)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        trans.RECIBO_MOVIMIENTO = trans.RECIBO_MOVIMIENTO;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getEntradaPrestamoElements()
        {
            using (var Entity = new TAE2Entities())
            {
                Entity.SaveChanges();

                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO==2
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 2)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        trans.RECIBO_MOVIMIENTO = trans.RECIBO_MOVIMIENTO;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;

                return res;
            }
        }

        public object getEntradaDevolucionElements()
        {
            using (var Entity = new TAE2Entities())
            {
                Entity.SaveChanges();

                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO==3
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 3)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        trans.RECIBO_MOVIMIENTO = trans.RECIBO_MOVIMIENTO;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getEntradaDesinstalacionElements()
        {
            using (var Entity = new TAE2Entities())
            {
                Entity.SaveChanges();

                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO ==4
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 4)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        trans.RECIBO_MOVIMIENTO = trans.RECIBO_MOVIMIENTO;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
                     
            }
        }

        public object getSalidaRentaElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO==5
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 5)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getSalidaDemoElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO==6
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 6)
                //    {

                //        Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getSalidaPrestamoElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO==7
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 7)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;

                return res;
            }
        }

        public object getSalidaVentaElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO==8
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 8)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getSalidaRMAElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO==9
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 9)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getSalidaRevisionElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO == 10
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 10)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getSalidaPruebasElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO == 11
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 11)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getSalidaConfiguracionElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO==12
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 12)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getSalidaObsequioElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO == 13
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 13)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getSalidaCorrectivoElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO ==14
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 14)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getSalidaOfficeElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO == 15
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 15)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getSalidaBajaElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO == 18
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 15)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        public object getTraspasos()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           .Include("ALMACEN")
                           .Include("ALMACEN1")
                           .Include("CLIENTE")
                           .Include("CLIENTE1")
                           .Include("CLIENTE2")
                           .Include("FACTURA_VENTA")
                           .Include("PROVEEDOR")
                           .Include("PROVEEDOR1")
                           .Include("PROVEEDOR2")
                           .Include("SERVICIO")
                           .Include("SOLICITANTE")
                           .Include("TIPO_MOVIMIENTO")
                           .Include("TRANSPORTE")
                           .Include("MOVIMIENTO_DETALLE")
                           .Include("TECNICO")
                           .Include("INFRAESTRUCTURA")
                           .Include("TECNICO1")
                           where p.UNID_TIPO_MOVIMIENTO == 17
                           orderby p.UNID_MOVIMIENTO descending
                           select p).Take(50).ToList();

                //List<MOVIMENTO> final = new List<MOVIMENTO>();

                //foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                //{
                //    if (trans.UNID_TIPO_MOVIMIENTO == 17)
                //    {

                //        //Para conservar las prop. de navegación
                //        trans.ALMACEN = trans.ALMACEN;
                //        trans.ALMACEN1 = trans.ALMACEN1;
                //        trans.CLIENTE = trans.CLIENTE;
                //        trans.CLIENTE1 = trans.CLIENTE1;
                //        trans.CLIENTE2 = trans.CLIENTE2;
                //        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                //        trans.PROVEEDOR = trans.PROVEEDOR;
                //        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                //        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                //        trans.SERVICIO = trans.SERVICIO;
                //        trans.SOLICITANTE = trans.SOLICITANTE;
                //        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                //        trans.TRANSPORTE = trans.TRANSPORTE;
                //        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                //        trans.TECNICO = trans.TECNICO;
                //        trans.INFRAESTRUCTURA = trans.INFRAESTRUCTURA;
                //        trans.TECNICO1 = trans.TECNICO1;
                //        final.Add(trans);
                //    }
                //}

                //return (object)final;
                return res;
            }
        }

        #endregion

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                MOVIMENTO Eprov = (MOVIMENTO)element;

                using (var Entity = new TAE2Entities())
                { 
                    var res = (from p in Entity.MOVIMENTOes
                                   .Include("MOVIMIENTO_DETALLE")
                               where p.UNID_MOVIMIENTO == Eprov.UNID_MOVIMIENTO
                               select p).First();
                    
                    if (res == null)
                        return null;
                    o = res;
                }
            }
            return o;
        }

        public void udpateElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MOVIMENTO movimiento = (MOVIMENTO)element;
                    var modifiedMovimiento = entity.MOVIMENTOes.First(p => p.UNID_MOVIMIENTO == movimiento.UNID_MOVIMIENTO);
                    modifiedMovimiento.CONTACTO = movimiento.CONTACTO;
                    modifiedMovimiento.DIRECCION_ENVIO = movimiento.DIRECCION_ENVIO;
                    modifiedMovimiento.FECHA_MOVIMIENTO = movimiento.FECHA_MOVIMIENTO;
                    modifiedMovimiento.GUIA = movimiento.GUIA;
                    modifiedMovimiento.IS_ACTIVE = movimiento.IS_ACTIVE;
                    modifiedMovimiento.NOMBRE_SITIO = movimiento.NOMBRE_SITIO;
                    modifiedMovimiento.PEDIMIENTO_EXPO = movimiento.PEDIMIENTO_EXPO;
                    modifiedMovimiento.PEDIMIENTO_IMPO = movimiento.PEDIMIENTO_IMPO;
                    modifiedMovimiento.RECIBE = movimiento.RECIBE;
                    modifiedMovimiento.SITIO_ENLACE = movimiento.SITIO_ENLACE;
                    modifiedMovimiento.TT = movimiento.TT;
                    modifiedMovimiento.UNID_ALMACEN_DESTINO = movimiento.UNID_ALMACEN_DESTINO;
                    modifiedMovimiento.UNID_ALMACEN_PROCEDENCIA = movimiento.UNID_ALMACEN_PROCEDENCIA;
                    modifiedMovimiento.UNID_CLIENTE = movimiento.UNID_CLIENTE;
                    modifiedMovimiento.UNID_CLIENTE_DESTINO = movimiento.UNID_CLIENTE_DESTINO;
                    modifiedMovimiento.UNID_CLIENTE_PROCEDENCIA = movimiento.UNID_CLIENTE_PROCEDENCIA;
                    modifiedMovimiento.UNID_FACTURA_VENTA = movimiento.UNID_FACTURA_VENTA;
                    modifiedMovimiento.UNID_PROVEEDOR = movimiento.UNID_PROVEEDOR;
                    modifiedMovimiento.UNID_PROVEEDOR_DESTINO = movimiento.UNID_PROVEEDOR_DESTINO;
                    modifiedMovimiento.UNID_PROVEEDOR_PROCEDENCIA = movimiento.UNID_PROVEEDOR_PROCEDENCIA;
                    modifiedMovimiento.UNID_SERVICIO = movimiento.UNID_SERVICIO;
                    modifiedMovimiento.UNID_SOLICITANTE = movimiento.UNID_SOLICITANTE;
                    modifiedMovimiento.UNID_TECNICO = movimiento.UNID_TECNICO;
                    modifiedMovimiento.UNID_TIPO_MOVIMIENTO = movimiento.UNID_TIPO_MOVIMIENTO;
                    modifiedMovimiento.UNID_TRANSPORTE = movimiento.UNID_TRANSPORTE;
                    modifiedMovimiento.UNID_TECNICO_TRAS = movimiento.UNID_TECNICO_TRAS;
                    //Sync
                    modifiedMovimiento.IS_MODIFIED = true;
                    modifiedMovimiento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    UNID.Master(movimiento, u, -1, "Modificación");
                }
            }
        }

        public void udpateElementRecibo(object element) {

            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    DAL.POCOS.MOVIMENTO Mov = (DAL.POCOS.MOVIMENTO)element;

                    var query = (from p in entity.MOVIMENTOes
                                 where p.UNID_MOVIMIENTO == Mov.UNID_MOVIMIENTO
                                 select p).ToList();

                    if (query.Count == 0)
                    {                        
                        //Sync
                        Mov.IS_MODIFIED = true;
                        Mov.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                        entity.MOVIMENTOes.AddObject(Mov);
                    }
                    else
                    {

                        var modifiedMov = entity.MOVIMENTOes.First(p => p.UNID_MOVIMIENTO == Mov.UNID_MOVIMIENTO);

                        modifiedMov.CONTACTO = Mov.CONTACTO;
                        modifiedMov.DIRECCION_ENVIO = Mov.DIRECCION_ENVIO;
                        modifiedMov.FINISHED = Mov.FINISHED;
                        modifiedMov.GUIA = Mov.GUIA;
                        modifiedMov.NOMBRE_SITIO = Mov.NOMBRE_SITIO;
                        modifiedMov.PEDIMIENTO_EXPO = Mov.PEDIMIENTO_EXPO;
                        modifiedMov.PEDIMIENTO_IMPO = Mov.PEDIMIENTO_IMPO;
                        modifiedMov.RECIBE = Mov.RECIBE;
                        modifiedMov.SITIO_ENLACE = Mov.SITIO_ENLACE;
                        modifiedMov.TT = Mov.TT;
                        modifiedMov.UNID_ALMACEN_DESTINO = Mov.UNID_ALMACEN_DESTINO;
                        modifiedMov.UNID_ALMACEN_PROCEDENCIA = Mov.UNID_ALMACEN_PROCEDENCIA;
                        modifiedMov.UNID_CLIENTE = Mov.UNID_CLIENTE;
                        modifiedMov.UNID_CLIENTE_DESTINO = Mov.UNID_CLIENTE_DESTINO;
                        modifiedMov.UNID_CLIENTE_PROCEDENCIA = Mov.UNID_CLIENTE_PROCEDENCIA;
                        modifiedMov.UNID_FACTURA_VENTA = Mov.UNID_FACTURA_VENTA;
                        modifiedMov.UNID_INFRAESTRUCTURA = Mov.UNID_INFRAESTRUCTURA;                        
                        modifiedMov.UNID_PROVEEDOR = Mov.UNID_PROVEEDOR;
                        modifiedMov.UNID_PROVEEDOR_DESTINO = Mov.UNID_PROVEEDOR_DESTINO;
                        modifiedMov.UNID_PROVEEDOR_PROCEDENCIA = Mov.UNID_PROVEEDOR_PROCEDENCIA;
                        modifiedMov.UNID_SERVICIO = Mov.UNID_SERVICIO;
                        modifiedMov.UNID_SOLICITANTE = Mov.UNID_SOLICITANTE;
                        modifiedMov.UNID_TECNICO = Mov.UNID_TECNICO;
                        modifiedMov.UNID_TECNICO_TRAS = Mov.UNID_TECNICO_TRAS;
                        modifiedMov.UNID_TIPO_MOVIMIENTO = Mov.UNID_TIPO_MOVIMIENTO;
                        modifiedMov.UNID_TRANSPORTE = Mov.UNID_TRANSPORTE;
                        modifiedMov.IS_ACTIVE = Mov.IS_ACTIVE;
                        //Sync
                        modifiedMov.IS_MODIFIED = true;
                        modifiedMov.LAST_MODIFIED_DATE = UNID.getNewUNID();
                        var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                        modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                        entity.SaveChanges();
                        //
                    }

                    entity.SaveChanges();
                }
            }
        }

        public void insertElement(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MOVIMENTO movimiento = (MOVIMENTO)element;
                    //Sync
                    movimiento.IS_MODIFIED = true;
                    movimiento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    movimiento.IS_ACTIVE = true;
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MOVIMENTOes.AddObject(movimiento);
                    entity.SaveChanges();

                    UNID.Master(movimiento, u, -1, "Inserción");
                }
            }
        }

        public void insertElementBaja(object element, USUARIO u)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MOVIMENTO movimiento = (MOVIMENTO)element;
                    //Sync
                    movimiento.IS_MODIFIED = true;
                    movimiento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    movimiento.IS_ACTIVE = false;
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MOVIMENTOes.AddObject(movimiento);
                    entity.SaveChanges();

                    UNID.Master(movimiento, u, -1, "Inserción");
                }
            }
        }

        public void insertElementSyn(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MOVIMENTO movimiento = (MOVIMENTO)element;
                    //Sync
                    
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MOVIMENTOes.AddObject(movimiento);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element, USUARIO u)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que serializa una List<MOVIMENTO> a Json
        /// </summary>
        /// <returns>Regresa un String en formato Json de MOVIMENTO</returns>
        /// <returns>Si no hay datos regresa null</returns>
        public string GetJsonMovimiento()
        {
            string res = null;
            List<MOVIMENTO> listMovimiento = new List<MOVIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MOVIMENTOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     listMovimiento.Add(new MOVIMENTO
                     {
                         UNID_MOVIMIENTO=row.UNID_MOVIMIENTO,
                         FECHA_MOVIMIENTO=row.FECHA_MOVIMIENTO,
                         UNID_TIPO_MOVIMIENTO=row.UNID_TIPO_MOVIMIENTO,
                         UNID_ALMACEN_DESTINO=row.UNID_ALMACEN_DESTINO,
                         UNID_PROVEEDOR_DESTINO=row.UNID_PROVEEDOR_DESTINO,
                         UNID_CLIENTE_DESTINO=row.UNID_CLIENTE_DESTINO,
                         UNID_ALMACEN_PROCEDENCIA=row.UNID_ALMACEN_PROCEDENCIA,
                         UNID_CLIENTE_PROCEDENCIA=row.UNID_CLIENTE_PROCEDENCIA,
                         UNID_PROVEEDOR_PROCEDENCIA=row.UNID_PROVEEDOR_PROCEDENCIA,
                         UNID_SERVICIO=row.UNID_SERVICIO, 
                         TT=row.TT,
                         CONTACTO=row.CONTACTO,
                         UNID_TRANSPORTE=row.UNID_TRANSPORTE,
                         DIRECCION_ENVIO=row.DIRECCION_ENVIO,
                         SITIO_ENLACE=row.SITIO_ENLACE,
                         NOMBRE_SITIO=row.NOMBRE_SITIO,
                         RECIBE=row.RECIBE,
                         GUIA=row.GUIA,
                         UNID_CLIENTE=row.UNID_CLIENTE,
                         UNID_PROVEEDOR=row.UNID_PROVEEDOR,
                         UNID_FACTURA_VENTA=row.UNID_FACTURA_VENTA,
                         PEDIMIENTO_IMPO=row.PEDIMIENTO_IMPO,
                         PEDIMIENTO_EXPO=row.PEDIMIENTO_EXPO,
                         UNID_SOLICITANTE=row.UNID_SOLICITANTE,
                         UNID_TECNICO=row.UNID_TECNICO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,                         
                         UNID_INFRAESTRUCTURA = row.UNID_INFRAESTRUCTURA,
                         UNID_TECNICO_TRAS = row.UNID_TECNICO_TRAS,
                         FINISHED = row.FINISHED
                     });
                 });
                if (listMovimiento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMovimiento);
                }
                return res;
            }
        }

        /// <summary>
        /// Método que Deserializa JSon a List<MOVIMENTO>
        /// </summary>
        /// <returns>Regresa List<MOVIMENTO></returns>
        /// <returns>Si no regresa null</returns>
        public List<MOVIMENTO> GetDeserializeMovimiento(string listPocos)
        {
            List<MOVIMENTO> res = null;

            if (!String.IsNullOrEmpty(listPocos))
            {
                res = JsonConvert.DeserializeObject<List<MOVIMENTO>>(listPocos);
            }

            return res;
        }

        /// <summary>
        /// Método que restaura las IS_MODIFIED a false
        /// </summary>
        /// <returns>Regresa void</returns>
        public void ResetMovimiento()
        {
            List<MOVIMENTO> reset = new List<MOVIMENTO>();
            using (var Entity = new TAE2Entities())
            {
                (from p in Entity.MOVIMENTOes
                 where p.IS_MODIFIED == true
                 select p).ToList().ForEach(row =>
                 {
                     reset.Add(new MOVIMENTO
                     {
                         UNID_MOVIMIENTO = row.UNID_MOVIMIENTO,
                         FECHA_MOVIMIENTO = row.FECHA_MOVIMIENTO,
                         UNID_TIPO_MOVIMIENTO = row.UNID_TIPO_MOVIMIENTO,
                         UNID_ALMACEN_DESTINO = row.UNID_ALMACEN_DESTINO,
                         UNID_PROVEEDOR_DESTINO = row.UNID_PROVEEDOR_DESTINO,
                         UNID_CLIENTE_DESTINO = row.UNID_CLIENTE_DESTINO,
                         UNID_ALMACEN_PROCEDENCIA = row.UNID_ALMACEN_PROCEDENCIA,
                         UNID_CLIENTE_PROCEDENCIA = row.UNID_CLIENTE_PROCEDENCIA,
                         UNID_PROVEEDOR_PROCEDENCIA = row.UNID_PROVEEDOR_PROCEDENCIA,
                         UNID_SERVICIO = row.UNID_SERVICIO, 
                         UNID_TECNICO_TRAS = row.UNID_TECNICO_TRAS,
                         TT = row.TT,
                         CONTACTO = row.CONTACTO,
                         UNID_TRANSPORTE = row.UNID_TRANSPORTE,
                         DIRECCION_ENVIO = row.DIRECCION_ENVIO,
                         SITIO_ENLACE = row.SITIO_ENLACE,
                         NOMBRE_SITIO = row.NOMBRE_SITIO,
                         RECIBE = row.RECIBE,
                         GUIA = row.GUIA,
                         UNID_CLIENTE = row.UNID_CLIENTE,
                         UNID_PROVEEDOR = row.UNID_PROVEEDOR,
                         UNID_FACTURA_VENTA = row.UNID_FACTURA_VENTA,
                         PEDIMIENTO_IMPO = row.PEDIMIENTO_IMPO,
                         PEDIMIENTO_EXPO = row.PEDIMIENTO_EXPO,
                         UNID_SOLICITANTE = row.UNID_SOLICITANTE,
                         UNID_TECNICO = row.UNID_TECNICO,
                         IS_ACTIVE = row.IS_ACTIVE,
                         IS_MODIFIED = row.IS_MODIFIED,
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE,                         
                         UNID_INFRAESTRUCTURA = row.UNID_INFRAESTRUCTURA,
                         FINISHED = row.FINISHED
                     });
                 });
                if (reset.Count > 0)
                {
                    foreach (var item in reset)
                    {
                        var modified = Entity.MOVIMENTOes.First(p => p.UNID_MOVIMIENTO == item.UNID_MOVIMIENTO);
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
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MOVIMENTO movimiento = (MOVIMENTO)element;
                    var modifiedMovimiento = entity.MOVIMENTOes.First(p => p.UNID_MOVIMIENTO == movimiento.UNID_MOVIMIENTO);
                    modifiedMovimiento.CONTACTO = movimiento.CONTACTO;
                    modifiedMovimiento.DIRECCION_ENVIO = movimiento.DIRECCION_ENVIO;
                    modifiedMovimiento.FECHA_MOVIMIENTO = movimiento.FECHA_MOVIMIENTO;
                    modifiedMovimiento.GUIA = movimiento.GUIA;
                    modifiedMovimiento.IS_ACTIVE = movimiento.IS_ACTIVE;
                    modifiedMovimiento.NOMBRE_SITIO = movimiento.NOMBRE_SITIO;
                    modifiedMovimiento.PEDIMIENTO_EXPO = movimiento.PEDIMIENTO_EXPO;
                    modifiedMovimiento.PEDIMIENTO_IMPO = movimiento.PEDIMIENTO_IMPO;
                    modifiedMovimiento.RECIBE = movimiento.RECIBE;
                    modifiedMovimiento.SITIO_ENLACE = movimiento.SITIO_ENLACE;
                    modifiedMovimiento.TT = movimiento.TT;
                    modifiedMovimiento.UNID_ALMACEN_DESTINO = movimiento.UNID_ALMACEN_DESTINO;
                    modifiedMovimiento.UNID_ALMACEN_PROCEDENCIA = movimiento.UNID_ALMACEN_PROCEDENCIA;
                    modifiedMovimiento.UNID_CLIENTE = movimiento.UNID_CLIENTE;
                    modifiedMovimiento.UNID_CLIENTE_DESTINO = movimiento.UNID_CLIENTE_DESTINO;
                    modifiedMovimiento.UNID_CLIENTE_PROCEDENCIA = movimiento.UNID_CLIENTE_PROCEDENCIA;
                    modifiedMovimiento.UNID_FACTURA_VENTA = movimiento.UNID_FACTURA_VENTA;
                    modifiedMovimiento.UNID_PROVEEDOR = movimiento.UNID_PROVEEDOR;
                    modifiedMovimiento.UNID_PROVEEDOR_DESTINO = movimiento.UNID_PROVEEDOR_DESTINO;
                    modifiedMovimiento.UNID_PROVEEDOR_PROCEDENCIA = movimiento.UNID_PROVEEDOR_PROCEDENCIA;
                    modifiedMovimiento.UNID_SERVICIO = movimiento.UNID_SERVICIO;
                    modifiedMovimiento.UNID_SOLICITANTE = movimiento.UNID_SOLICITANTE;
                    modifiedMovimiento.UNID_TECNICO = movimiento.UNID_TECNICO;
                    modifiedMovimiento.UNID_TIPO_MOVIMIENTO = movimiento.UNID_TIPO_MOVIMIENTO;
                    modifiedMovimiento.UNID_TRANSPORTE = movimiento.UNID_TRANSPORTE;
                    modifiedMovimiento.UNID_TECNICO_TRAS = movimiento.UNID_TECNICO_TRAS;
                    //Sync
                    modifiedMovimiento.IS_MODIFIED = true;
                    modifiedMovimiento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                }
            }
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MOVIMENTO movimiento = (MOVIMENTO)element;
                    //Sync
                    movimiento.IS_MODIFIED = true;
                    movimiento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    movimiento.IS_ACTIVE = true;
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MOVIMENTOes.AddObject(movimiento);
                    entity.SaveChanges();
                }
            }
        }
    }
}
