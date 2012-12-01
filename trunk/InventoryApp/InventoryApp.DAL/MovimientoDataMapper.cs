﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.DAL
{
    public class MovimientoDataMapper : IDataMapper
    {
        public object getElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           select p).ToList();

                foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                {
                    //Para conservar las prop. de navegación
                    trans.ALMACEN = trans.ALMACEN;
                    trans.ALMACEN1 = trans.ALMACEN1;
                    trans.CLIENTE = trans.CLIENTE;
                    trans.CLIENTE1 = trans.CLIENTE1;
                    trans.CLIENTE2 = trans.CLIENTE2;
                    trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                    trans.PROVEEDOR = trans.PROVEEDOR;
                    trans.PROVEEDOR1 = trans.PROVEEDOR1;
                    trans.PROVEEDOR2 = trans.PROVEEDOR2;
                    trans.SERVICIO = trans.SERVICIO;
                    trans.SOLICITANTE = trans.SOLICITANTE;
                    trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                    trans.TRANSPORTE = trans.TRANSPORTE;
                    trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                    trans.TECNICO = trans.TECNICO;  
                }

                return (object)res;
            }
        }

        public object getEntradasElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           select p).ToList();

                List<MOVIMENTO> final = new List<MOVIMENTO>();

                foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                {
                    if (trans.UNID_TIPO_MOVIMIENTO == 1 || trans.UNID_TIPO_MOVIMIENTO == 2 || trans.UNID_TIPO_MOVIMIENTO == 3 || trans.UNID_TIPO_MOVIMIENTO == 4)
                    {

                        //Para conservar las prop. de navegación
                        trans.ALMACEN = trans.ALMACEN;
                        trans.ALMACEN1 = trans.ALMACEN1;
                        trans.CLIENTE = trans.CLIENTE;
                        trans.CLIENTE1 = trans.CLIENTE1;
                        trans.CLIENTE2 = trans.CLIENTE2;
                        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                        trans.PROVEEDOR = trans.PROVEEDOR;
                        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                        trans.SERVICIO = trans.SERVICIO;
                        trans.SOLICITANTE = trans.SOLICITANTE;
                        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                        trans.TRANSPORTE = trans.TRANSPORTE;
                        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                        trans.TECNICO = trans.TECNICO;
                        final.Add(trans);
                    }
                }

                return (object)final;
            }
        }

        public object getSalidasElements()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           select p).ToList();

                List<MOVIMENTO> final = new List<MOVIMENTO>();

                foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                {
                    if (trans.UNID_TIPO_MOVIMIENTO > 4 && trans.UNID_TIPO_MOVIMIENTO < 16)
                    {

                        //Para conservar las prop. de navegación
                        trans.ALMACEN = trans.ALMACEN;
                        trans.ALMACEN1 = trans.ALMACEN1;
                        trans.CLIENTE = trans.CLIENTE;
                        trans.CLIENTE1 = trans.CLIENTE1;
                        trans.CLIENTE2 = trans.CLIENTE2;
                        trans.FACTURA_VENTA = trans.FACTURA_VENTA;
                        trans.PROVEEDOR = trans.PROVEEDOR;
                        trans.PROVEEDOR1 = trans.PROVEEDOR1;
                        trans.PROVEEDOR2 = trans.PROVEEDOR2;
                        trans.SERVICIO = trans.SERVICIO;
                        trans.SOLICITANTE = trans.SOLICITANTE;
                        trans.TIPO_MOVIMIENTO = trans.TIPO_MOVIMIENTO;
                        trans.TRANSPORTE = trans.TRANSPORTE;
                        trans.MOVIMIENTO_DETALLE = trans.MOVIMIENTO_DETALLE;
                        trans.TECNICO = trans.TECNICO;
                        final.Add(trans);
                    }
                }

                return (object)final;
            }
        }

        public object getElement(object element)
        {
            object o = null;
            if (element != null)
            {
                MOVIMENTO Eprov = (MOVIMENTO)element;

                using (var Entity = new TAE2Entities())
                {
                    var res = (from p in Entity.MOVIMENTOes
                               where p.UNID_MOVIMIENTO == Eprov.UNID_MOVIMIENTO
                               select p).ToList();

                    foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                    {
                        //Para conservar las prop. de navegación
                    }

                    o = (object)res;
                }
            }
            return o;
        }

        public void udpateElement(object element)
        {
            throw new NotImplementedException();
        }

        public void insertElement(object element)
        {
            if (element != null)
            {
                using (var entity = new TAE2Entities())
                {
                    MOVIMENTO movimiento = (MOVIMENTO)element;

                    entity.MOVIMENTOes.AddObject(movimiento);
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
