﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using InventoryApp.DAL;
using InventoryApp.Model;
using InventoryApp.DAL.Recibo;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Service.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Receiver : IReceiver
    {
        //-------------------------SERVICIOS DE SUBIDA DE ARCHIVOS 
        public string LoadAlmacen(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ALMACEN> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            AlmacenDataMapper dataMapper = new AlmacenDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeAlmacen(listPocos);
                
                foreach (ALMACEN item in list)
                {
                    //actualiza o pocoa a la tabla ALMACEN
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco= user.GetDeserializeUpLoadLog(dataUser);
                
                if (poco!=null)
                {
 
                    mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla ALMACEN sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadAlmacenTecnico(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ALMACEN_TECNICO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            AlmacenDataMapper dataMapper = new AlmacenDataMapper();
            AlmacenTecnicoDataMapper dataMapperRelation = new AlmacenTecnicoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapperRelation.GetDeserializeAlmacenTecnico(listPocos);

                foreach (ALMACEN_TECNICO item in list)
                {
                    //actualiza o pocoa a la tabla ALMACEN_TECNICO
                    dataMapper.loadSyncRelation(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco != null)
                {

                    mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla ALMACEN_TECNICO sincronizada" });

                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadAppMenu(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MENU> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            AppMenuDataMapper dataMapper = new AppMenuDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion 

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeMenu(listPocos);

                foreach (MENU item in list)
                {
                    //actualiza o pocoa a la tabla MENU
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla MENU sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadAppRol(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ROL> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            AppRolDataMapper dataMapper = new AppRolDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeRol(listPocos);

                foreach (ROL item in list)
                {
                    //actualiza o pocoa a la tabla ROL
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla ROL sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadAppRolMenu(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ROL_MENU> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            AppRolMenuDataMapper dataMapper = new AppRolMenuDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeRolMenu(listPocos);

                foreach (ROL_MENU item in list)
                {
                    //actualiza o pocoa a la tabla ROL_MENU
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla ROL_MENU sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadAppUsuario(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<USUARIO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            AppUsuario dataMapper = new AppUsuario();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeUsuario(listPocos);

                foreach (USUARIO item in list)
                {
                    //actualiza o pocoa a la tabla USUARIO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla USUARIO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadAppUsuarioRol(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<USUARIO_ROL> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            AppUsuarioRol dataMapper = new AppUsuarioRol();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeUsuarioRol(listPocos);

                foreach (USUARIO_ROL item in list)
                {
                    //actualiza o pocoa a la tabla USUARIO_ROL
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla USUARIO_ROL sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadArticulo(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ARTICULO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ArticuloDataMapper dataMapper = new ArticuloDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeArticulo(listPocos);

                foreach (ARTICULO item in list)
                {
                    //actualiza o pocoa a la tabla ARTICULO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla ARTICULO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadBanco(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<BANCO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            BancoDataMapper dataMapper = new BancoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeBanco(listPocos);

                foreach (BANCO item in list)
                {
                    //actualiza o pocoa a la tabla BANCO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla BANCO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadCategoria(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<CATEGORIA> list=null;
            UPLOAD_LOG poco=null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            CategoriaDataMapper dataMapper = new CategoriaDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                //deserializa la lista de pocos
                list = dataMapper.GetDeserializeCategoria(listPocos);   
                foreach (CATEGORIA item in list)
                {
                    //actualiza o pocoa a la tabla CATEGORIA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();       
                //deserializa el poco
                poco = user.GetDeserializeUpLoadLog(dataUser);
                if (poco!=null)
                {
                    // pocoa a la tabla UPLOAD_LOG 
                    mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla CATEGORIA sincronizada" });        
                }
            }
            catch (Exception)
            {
                mensaje = null;
            }
            return mensaje;
            
            #endregion
        }

        public string LoadCiudad(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<CIUDAD> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            CiudadDataMapper dataMapper = new CiudadDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeCiudad(listPocos);

                foreach (CIUDAD item in list)
                {
                    //actualiza o pocoa a la tabla CIUDAD
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco !=null)
                {
                    mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla CIUDAD sincronizada" });
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadCliente(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<CLIENTE> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ClienteDataMapper dataMapper = new ClienteDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeCliente(listPocos);

                foreach (CLIENTE item in list)
                {
                    //actualiza o pocoa a la tabla CLIENTE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco != null)
                {

                    mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla CLIENTE sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadCotizacion(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<COTIZACION> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            CotizacionDataMapper dataMapper = new CotizacionDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeCotizacion(listPocos);

                foreach (COTIZACION item in list)
                {
                    //actualiza o pocoa a la tabla COTIZACION
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco != null)
                {
                    mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla COTIZACION sincronizada" });
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadDepartamento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<DEPARTAMENTO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            DepartamentoDataMapper dataMapper = new DepartamentoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeDepartamento(listPocos);

                foreach (DEPARTAMENTO item in list)
                {
                    //actualiza o pocoa a la tabla DEPARTAMENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla DEPARTAMENTO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadEmpresa(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<EMPRESA> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            EmpresaDataMapper dataMapper = new EmpresaDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeEmpresa(listPocos);

                foreach (EMPRESA item in list)
                {
                    //actualiza o pocoa a la tabla EMPRESA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla EMPRESA sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadEquipo(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<EQUIPO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            EquipoDataMapper dataMapper = new EquipoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeEquipo(listPocos);

                foreach (EQUIPO item in list)
                {
                    //actualiza o pocoa a la tabla EQUIPO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla EQUIPO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadFactura(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<FACTURA> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG= UNID.getNewUNID(), UNID_USUARIO= 1, IP_DIR="USUARIO DE PRUEBA", PC_NAME="ALMACEN"};
            FacturaCompraDataMapper dataMapper = new FacturaCompraDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeFactura(listPocos);

                foreach (FACTURA item in list)
                {
                    //actualiza o pocoa a la tabla FACTURA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                    mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla FACTURA sincronizada" });
                }

            }
            catch (Exception ex)
            {
                bitacora.MSG = ex.Message + "FACTURA" + ex.InnerException;
                mensaje = null;
                user.InsertUploadLog(bitacora);
            }
            return mensaje;
            #endregion
        }

        public string LoadFacturaDetalle(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<FACTURA_DETALLE> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            FacturaCompraDetalleDataMapper dataMapper = new FacturaCompraDetalleDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeFacturaDetalle(listPocos);

                foreach (FACTURA_DETALLE item in list)
                {
                    //actualiza o pocoa a la tabla FACTURA_DETALLE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla FACTURA_DETALLE sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadFacturaVenta(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<FACTURA_VENTA> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            FacturaVentaDataMapper dataMapper = new FacturaVentaDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeFacturaVenta(listPocos);

                foreach (FACTURA_VENTA item in list)
                {
                    //actualiza o pocoa a la tabla FACTURA_VENTA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla FACTURA_VENTA sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadItem(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ITEM> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ItemDataMapper dataMapper = new ItemDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeItem(listPocos);

                foreach (ITEM item in list)
                {
                    //actualiza o pocoa a la tabla ITEM
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
 
                    mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla ITEM sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadItemStatus(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ITEM_STATUS> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ItemStatusDataMapper dataMapper = new ItemStatusDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeItemStatus(listPocos);

                foreach (ITEM_STATUS item in list)
                {
                    //actualiza o pocoa a la tabla ITEM_STATUS
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla ITEM_STATUS sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadInfraestructura(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<INFRAESTRUCTURA> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            InfraestructuraDataMapper dataMapper = new InfraestructuraDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeInfraestructura(listPocos);

                foreach (INFRAESTRUCTURA item in list)
                {
                    //actualiza o pocoa a la tabla ITEM_STATUS
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco != null)
                {


                    mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla ITEM_STATUS sincronizada" });

                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadLote(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<LOTE> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            LoteDataMapper dataMapper = new LoteDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeLote(listPocos);

                foreach (LOTE item in list)
                {
                    //actualiza o pocoa a la tabla LOTE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla LOTE sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadMarca(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MARCA> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            MarcaDataMapper dataMapper = new MarcaDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeMarca(listPocos);

                foreach (MARCA item in list)
                {
                    //actualiza o pocoa a la tabla MARCA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla MARCA sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadMedioEnvio(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MEDIO_ENVIO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            MedioEnvioDataMapper dataMapper = new MedioEnvioDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeMedioEnvio(listPocos);

                foreach (MEDIO_ENVIO item in list)
                {
                    //actualiza o pocoa a la tabla MEDIO_ENVIO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla MEDIO_ENVIO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadModelo(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MODELO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ModeloDataMapper dataMapper = new ModeloDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeModelo(listPocos);

                foreach (MODELO item in list)
                {
                    //actualiza o pocoa a la tabla MODELO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla MODELO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion

        }

        public string LoadMoneda(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MONEDA> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            MonedaDataMapper dataMapper = new MonedaDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeMoneda(listPocos);

                foreach (MONEDA item in list)
                {
                    //actualiza o pocoa a la tabla MONEDA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla MONEDA sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadMovimiento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MOVIMENTO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            MovimientoDataMapper dataMapper = new MovimientoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeMovimiento(listPocos);

                foreach (MOVIMENTO item in list)
                {
                    //actualiza o pocoa a la tabla MOVIMENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla MOVIMENTO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadMovimientoDetalle(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MOVIMIENTO_DETALLE> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            MovimientoDetalleDataMapper dataMapper = new MovimientoDetalleDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeMovimientoDetalle(listPocos);

                foreach (MOVIMIENTO_DETALLE item in list)
                {
                    //actualiza o pocoa a la tabla MOVIMIENTO_DETALLE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla MOVIMIENTO_DETALLE sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadPais(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PAI> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            PaisDataMapper dataMapper = new PaisDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetdeserializePais(listPocos);

                foreach (PAI item in list)
                {
                    //actualiza o pocoa a la tabla PAIS
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla PAIS sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadPedimento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PEDIMENTO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            PedimentoDataMapper dataMapper = new PedimentoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializePedimento(listPocos);

                foreach (PEDIMENTO item in list)
                {
                    //actualiza o pocoa a la tabla PEDIMENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla PEDIMENTO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadPomArticulo(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<POM_ARTICULO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            PomArticuloDataMapper dataMapper = new PomArticuloDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializePomArticulo(listPocos);

                foreach (POM_ARTICULO item in list)
                {
                    //actualiza o pocoa a la tabla POM_ARTICULO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla POM_ARTICULO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadPom(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<POM> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            PomDataMapper dataMapper = new PomDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializePom(listPocos);

                foreach (POM item in list)
                {
                    //actualiza o pocoa a la tabla POM
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla POM sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadPropiedad(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PROPIEDAD> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            PropiedadDataMapper dataMapper = new PropiedadDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializePropiedad(listPocos);

                foreach (PROPIEDAD item in list)
                {
                    //actualiza o pocoa a la tabla PROPIEDAD
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla PROPIEDAD sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }
        
        public string LoadProveedorCategoria(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PROVEEDOR_CATEGORIA> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ProveedorDataMapper dataMapper = new ProveedorDataMapper();
            ProveedorCategoriaDataMapper dataMapperRelation = new ProveedorCategoriaDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapperRelation.GetDeserializeProveedorCategoria(listPocos);

                foreach (PROVEEDOR_CATEGORIA item in list)
                {
                    //actualiza o pocoa a la tabla PROVEEDOR_CATEGORIA
                    dataMapper.loadSyncRelation(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco != null)
                {

                    mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla PROVEEDOR_CATEGORIA sincronizada" });

                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadProveedorCuenta(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PROVEEDOR_CUENTA> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ProveedorCuentaDataMapper dataMapper = new ProveedorCuentaDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetdeserializeProveedorCuenta(listPocos);

                foreach (PROVEEDOR_CUENTA item in list)
                {
                    //actualiza o pocoa a la tabla PROVEEDOR_CUENTA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla PROVEEDOR_CUENTA sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadProveedor(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PROVEEDOR> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ProveedorDataMapper dataMapper = new ProveedorDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeProveedor(listPocos);

                foreach (PROVEEDOR item in list)
                {
                    //actualiza o pocoa a la tabla PROVEEDOR
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla PROVEEDOR sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadProyecto(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PROYECTO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ProyectoDataMapper dataMapper = new ProyectoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeProyecto(listPocos);

                foreach (PROYECTO item in list)
                {
                    //actualiza o pocoa a la tabla PROYECTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla PROYECTO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadRecibo(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<RECIBO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ReciboDataMapper dataMapper = new ReciboDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeRecibo(listPocos);

                foreach (RECIBO item in list)
                {
                    //actualiza o pocoa a la tabla RECIBO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla RECIBO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadReciboMovimiento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<RECIBO_MOVIMIENTO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ReciboMovimientoDataMapper dataMapper = new ReciboMovimientoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeReciboMovimiento(listPocos);

                foreach (RECIBO_MOVIMIENTO item in list)
                {
                    //actualiza o pocoa a la tabla RECIBO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla RECIBO_MOVIMIENTO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadReciboStatus(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<RECIBO_STATUS> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ReciboStatusDataMapper dataMapper = new ReciboStatusDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeReciboStatus(listPocos);

                foreach (RECIBO_STATUS item in list)
                {
                    //actualiza o pocoa a la tabla RECIBO_STATUS
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla RECIBO_STATUS sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadServicio(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<SERVICIO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            ServicioDataMapper dataMapper = new ServicioDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeServicio(listPocos);

                foreach (SERVICIO item in list)
                {
                    //actualiza o pocoa a la tabla SERVICIO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla SERVICIO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadSolicitante(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<SOLICITANTE> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            SolicitanteDataMapper dataMapper = new SolicitanteDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeSolicitante(listPocos);

                foreach (SOLICITANTE item in list)
                {
                    //actualiza o pocoa a la tabla SOLICITANTE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla SOLICITANTE sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadTecnico(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TECNICO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            TecnicoDataMapper dataMapper = new TecnicoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeTecnico(listPocos);

                foreach (TECNICO item in list)
                {
                    //actualiza o pocoa a la tabla TECNICO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla TECNICO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadTerminoEnvio(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TERMINO_ENVIO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            TerminoEnvioDataMapper dataMapper = new TerminoEnvioDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeTerminoEnvio(listPocos);

                foreach (TERMINO_ENVIO item in list)
                {
                    //actualiza o pocoa a la tabla TERMINO_ENVIO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla TERMINO_ENVIO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadTipoCotizacion(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TIPO_COTIZACION> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            TipoCotizacionDataMapper dataMapper = new TipoCotizacionDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeTipoCotizacion(listPocos);

                foreach (TIPO_COTIZACION item in list)
                {
                    //actualiza o pocoa a la tabla TIPO_COTIZACION
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla TIPO_COTIZACION sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadTipoEmpresa(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TIPO_EMPRESA> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            TipoEmpresaDataMapper dataMapper = new TipoEmpresaDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeTipoEmpresa(listPocos);

                foreach (TIPO_EMPRESA item in list)
                {
                    //actualiza o pocoa a la tabla TIPO_EMPRESA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                                
                    mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla TIPO_EMPRESA sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadTipoMovimiento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TIPO_MOVIMIENTO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            TipoMovimientoDataMapper dataMapper = new TipoMovimientoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
            
                list = dataMapper.GetDeserializeTipoMovimiento(listPocos);

                foreach (TIPO_MOVIMIENTO item in list)
                {
                    //actualiza o pocoa a la tabla TIPO_MOVIMIENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {       
                    
                    mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla TIPO_MOVIMIENTO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadTipoPedimento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TIPO_PEDIMENTO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            TipoPedimentoDataMapper dataMapper = new TipoPedimentoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeTipoPedimento(listPocos);

                foreach (TIPO_PEDIMENTO item in list)
                {
                    //actualiza o pocoa a la tabla TIPO_PEDIMENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    {
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla TIPO_PEDIMENTO sincronizada" });
                    }
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadTransporte(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TRANSPORTE> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            TransporteDataMapper dataMapper = new TransporteDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeTransporte(listPocos);

                foreach (TRANSPORTE item in list)
                {
                    //actualiza o pocoa a la tabla TRANSPORTE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla TRANSPORTE sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadUltimoMovimiento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ULTIMO_MOVIMIENTO> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            UltimoMovimientoDataMapper dataMapper = new UltimoMovimientoDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeUltimoMovimiento(listPocos);

                foreach (ULTIMO_MOVIMIENTO item in list)
                {
                    //actualiza o pocoa a la tabla ULTIMO_MOVIMIENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla ULTIMO_MOVIMIENTO sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }

        public string LoadUnidad(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<UNIDAD> list;
            UPLOAD_LOG poco = null;
            UPLOAD_LOG bitacora = new UPLOAD_LOG() { UNID_UPLOAD_LOG = UNID.getNewUNID(), UNID_USUARIO = 1, IP_DIR = "USUARIO DE PRUEBA", PC_NAME = "ALMACEN" };
            UnidadDataMapper dataMapper = new UnidadDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {
                list = dataMapper.GetDeserializeUnidad(listPocos);

                foreach (UNIDAD item in list)
                {
                    //actualiza o pocoa a la tabla UNIDAD
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // pocoa a la tabla UPLOAD_LOG 
                poco = user.GetDeserializeUpLoadLog(dataUser);

                if (poco!=null)
                {
                   
                    
                        mensaje= user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = poco.UNID_USUARIO, PC_NAME = poco.PC_NAME, IP_DIR = poco.IP_DIR, MSG = "Tabla UNIDAD sincronizada" });
                    
                }

            }
            catch (Exception)
            {

                mensaje = null;
            }
            return mensaje;
            #endregion
        }
        //------------------------- FIN DE SERVICIOS DE SUBIDA DE ARCHIVOS
    }
}
