using System;
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
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla ALMACEN
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser= user.GetDeserializeUpLoadLog(dataUser);
                
                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla ALMACEN sincronizada" });
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
        //pendiente
        public string LoadAlmacenTecnico(string listPocos, string dataUser)
        {
            throw new NotImplementedException();
        }

        public string LoadAppMenu(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MENU> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla MENU
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla MENU sincronizada" });
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

        public string LoadAppRol(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ROL> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla ROL
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla ROL sincronizada" });
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

        public string LoadAppRolMenu(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ROL_MENU> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla ROL_MENU
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla ROL_MENU sincronizada" });
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

        public string LoadAppUsuario(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<USUARIO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla USUARIO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla USUARIO sincronizada" });
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

        public string LoadAppUsuarioRol(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<USUARIO_ROL> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla USUARIO_ROL
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla USUARIO_ROL sincronizada" });
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

        public string LoadArticulo(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ARTICULO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla ARTICULO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla ARTICULO sincronizada" });
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

        public string LoadBanco(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<BANCO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla BANCO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla BANCO sincronizada" });
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

        public string LoadCategoria(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<CATEGORIA> list=null;
            List<UPLOAD_LOG> listUser=null;
            CategoriaDataMapper dataMapper = new CategoriaDataMapper();
            ServerLastDataMapper server = new ServerLastDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            try
            {

                list = dataMapper.GetDeserializeCategoria(listPocos);
                
                foreach (CATEGORIA item in list)
                {
                    //actualiza o inserta a la tabla CATEGORIA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);
                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla CATEGORIA sincronizada" });
                    }
                }

            }
            catch (Exception ex)
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
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla CIUDAD
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla CIUDAD sincronizada" });
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

        public string LoadCliente(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<CLIENTE> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla CLIENTE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla CLIENTE sincronizada" });
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

        public string LoadCotizacion(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<COTIZACION> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla COTIZACION
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla COTIZACION sincronizada" });
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

        public string LoadDepartamento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<DEPARTAMENTO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla DEPARTAMENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla DEPARTAMENTO sincronizada" });
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

        public string LoadEmpresa(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<EMPRESA> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla EMPRESA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla EMPRESA sincronizada" });
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

        public string LoadEquipo(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<EQUIPO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla EQUIPO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla EQUIPO sincronizada" });
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

        public string LoadFactura(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<FACTURA> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla FACTURA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla FACTURA sincronizada" });
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

        public string LoadFacturaDetalle(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<FACTURA_DETALLE> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla FACTURA_DETALLE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla FACTURA_DETALLE sincronizada" });
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

        public string LoadFacturaVenta(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<FACTURA_VENTA> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla FACTURA_VENTA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla FACTURA_VENTA sincronizada" });
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

        public string LoadItem(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ITEM> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla ITEM
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla ITEM sincronizada" });
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

        public string LoadItemStatus(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ITEM_STATUS> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla ITEM_STATUS
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla ITEM_STATUS sincronizada" });
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

        public string LoadLote(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<LOTE> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla LOTE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla LOTE sincronizada" });
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

        public string LoadMarca(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MARCA> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla MARCA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla MARCA sincronizada" });
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

        public string LoadMedioEnvio(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MEDIO_ENVIO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla MEDIO_ENVIO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla MEDIO_ENVIO sincronizada" });
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

        public string LoadModelo(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MODELO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla MODELO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla MODELO sincronizada" });
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

        public string LoadMoneda(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MONEDA> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla MONEDA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla MONEDA sincronizada" });
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

        public string LoadMovimiento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MOVIMENTO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla MOVIMENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla MOVIMENTO sincronizada" });
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

        public string LoadMovimientoDetalle(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<MOVIMIENTO_DETALLE> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla MOVIMIENTO_DETALLE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla MOVIMIENTO_DETALLE sincronizada" });
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

        public string LoadPais(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PAI> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla PAIS
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla PAIS sincronizada" });
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

        public string LoadPedimento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PEDIMENTO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla PEDIMENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla PEDIMENTO sincronizada" });
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

        public string LoadPomArticulo(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<POM_ARTICULO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla POM_ARTICULO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla POM_ARTICULO sincronizada" });
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

        public string LoadPom(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<POM> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla POM
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla POM sincronizada" });
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

        public string LoadPropiedad(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PROPIEDAD> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla PROPIEDAD
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla PROPIEDAD sincronizada" });
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
        //pendiente
        public string LoadProveedorCategoria(string listPocos, string dataUser)
        {
            throw new NotImplementedException();
        }

        public string LoadProveedorCuenta(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PROVEEDOR_CUENTA> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla PROVEEDOR_CUENTA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla PROVEEDOR_CUENTA sincronizada" });
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

        public string LoadProveedor(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PROVEEDOR> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla PROVEEDOR
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla PROVEEDOR sincronizada" });
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

        public string LoadProyecto(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<PROYECTO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla PROYECTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla PROYECTO sincronizada" });
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

        public string LoadRecibo(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<RECIBO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla RECIBO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla RECIBO sincronizada" });
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

        public string LoadReciboMovimiento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<RECIBO_MOVIMIENTO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla RECIBO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla RECIBO_MOVIMIENTO sincronizada" });
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

        public string LoadReciboStatus(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<RECIBO_STATUS> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla RECIBO_STATUS
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla RECIBO_STATUS sincronizada" });
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

        public string LoadServicio(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<SERVICIO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla SERVICIO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla SERVICIO sincronizada" });
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

        public string LoadSolicitante(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<SOLICITANTE> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla SOLICITANTE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla SOLICITANTE sincronizada" });
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

        public string LoadTecnico(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TECNICO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla TECNICO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla TECNICO sincronizada" });
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

        public string LoadTerminoEnvio(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TERMINO_ENVIO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla TERMINO_ENVIO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla TERMINO_ENVIO sincronizada" });
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

        public string LoadTipoCotizacion(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TIPO_COTIZACION> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla TIPO_COTIZACION
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla TIPO_COTIZACION sincronizada" });
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

        public string LoadTipoEmpresa(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TIPO_EMPRESA> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla TIPO_EMPRESA
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla TIPO_EMPRESA sincronizada" });
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

        public string LoadTipoMovimiento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TIPO_MOVIMIENTO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla TIPO_MOVIMIENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla TIPO_MOVIMIENTO sincronizada" });
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

        public string LoadTipoPedimento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<TIPO_PEDIMENTO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla TIPO_PEDIMENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla TIPO_PEDIMENTO sincronizada" });
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
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla TRANSPORTE
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla TRANSPORTE sincronizada" });
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

        public string LoadUltimoMovimiento(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<ULTIMO_MOVIMIENTO> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla ULTIMO_MOVIMIENTO
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla ULTIMO_MOVIMIENTO sincronizada" });
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

        public string LoadUnidad(string listPocos, string dataUser)
        {
            #region propiedades
            string mensaje = null;
            List<UNIDAD> list;
            List<UPLOAD_LOG> listUser;
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
                    //actualiza o inserta a la tabla UNIDAD
                    dataMapper.loadSync(item);
                }
                //actualiza a la tabla SERVER_LASTDATA LA FECHA ACTUAL
                server.updateDumy();
                // inserta a la tabla UPLOAD_LOG 
                listUser = user.GetDeserializeUpLoadLog(dataUser);

                if (listUser.Count > 0)
                {
                    foreach (UPLOAD_LOG insert in listUser)
                    {
                        return mensaje = user.InsertUploadLog(new UPLOAD_LOG() { UNID_USUARIO = insert.UNID_USUARIO, PC_NAME = insert.PC_NAME, IP_DIR = insert.IP_DIR, MSG = "Tabla UNIDAD sincronizada" });
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
        //------------------------- FIN DE SERVICIOS DE SUBIDA DE ARCHIVOS
    }
}
