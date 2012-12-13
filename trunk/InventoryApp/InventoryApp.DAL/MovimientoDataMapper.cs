using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using InventoryApp.DAL.JSON;

namespace InventoryApp.DAL
{
    public class MovimientoDataMapper : IDataMapper
    {
        public long? LastModifiedDate()
        {
            long? resul = null;
            using (var entity = new TAE2Entities())
            {
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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
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

                        if (UNID.compareUNIDS(aux.LAST_MODIFIED_DATE, poco.LAST_MODIFIED_DATE))
                            udpateElement((object)poco);
                    }
                    //Inserción
                    else
                    {
                        insertElement((object)poco);
                    }

                    var modifiedMenu = entity.MOVIMENTOes.First(p => p.UNID_MOVIMIENTO == poco.UNID_MOVIMIENTO);
                    modifiedMenu.IS_ACTIVE = false;
                    entity.SaveChanges();
                }
            }
        }

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


        public object getTraspasos()
        {
            using (var Entity = new TAE2Entities())
            {
                var res = (from p in Entity.MOVIMENTOes
                           select p).ToList();

                List<MOVIMENTO> final = new List<MOVIMENTO>();

                foreach (MOVIMENTO trans in ((List<MOVIMENTO>)res))
                {
                    if (trans.UNID_TIPO_MOVIMIENTO == 17)
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
                    //Sync
                    modifiedMovimiento.IS_MODIFIED = true;
                    modifiedMovimiento.LAST_MODIFIED_DATE = UNID.getNewUNID();
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
                    MOVIMENTO movimiento = (MOVIMENTO)element;
                    //Sync
                    movimiento.IS_MODIFIED = true;
                    movimiento.LAST_MODIFIED_DATE = UNID.getNewUNID();
                    var modifiedSync = entity.SYNCs.First(p => p.UNID_SYNC == 20120101000000000);
                    modifiedSync.ACTUAL_DATE = UNID.getNewUNID();
                    entity.SaveChanges();
                    //
                    entity.MOVIMENTOes.AddObject(movimiento);
                    entity.SaveChanges();
                }
            }
        }

        public void deleteElement(object element)
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
                         LAST_MODIFIED_DATE = row.LAST_MODIFIED_DATE
                     });
                 });
                if (listMovimiento.Count > 0)
                {
                    res = SerializerJson.SerializeParametros(listMovimiento);
                }
                return res;
            }
        }
    }
}
