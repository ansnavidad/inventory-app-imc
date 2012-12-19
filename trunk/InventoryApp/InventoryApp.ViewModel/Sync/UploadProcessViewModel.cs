using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using InventoryApp.Model;
using InventoryApp.DAL.Recibo;

namespace InventoryApp.ViewModel.Sync
{
    public class UploadProcessViewModel : ViewModelBase
    {
        #region propiedades
        public static bool IsRunning = false;
        CategoriaDataMapper cat = new CategoriaDataMapper();
        EquipoDataMapper eq = new EquipoDataMapper();
        MarcaDataMapper mar = new MarcaDataMapper();
        ModeloDataMapper mol = new ModeloDataMapper();
        ArticuloDataMapper art = new ArticuloDataMapper();
        CiudadDataMapper ciu = new CiudadDataMapper();
        PaisDataMapper pai = new PaisDataMapper();
        SyncDataMapper syn = new SyncDataMapper();
        AlmacenDataMapper almacenDataMapper = new AlmacenDataMapper();
        AlmacenTecnicoDataMapper almacenTecnicoDataMapper = new AlmacenTecnicoDataMapper();
        AppMenuDataMapper menuDataMapper = new AppMenuDataMapper();
        AppRolDataMapper rolDataMapper = new AppRolDataMapper();
        AppRolMenuDataMapper rolMenuDataMapper = new AppRolMenuDataMapper();
        AppUsuario usuarioDataMapappr = new AppUsuario();
        AppUsuarioRol usuarioRolDataMapper = new AppUsuarioRol();
        ArticuloDataMapper articuloDataMapper = new ArticuloDataMapper();
        BancoDataMapper bancoDataMapper = new BancoDataMapper();
        CategoriaDataMapper categoriaDataMapper = new CategoriaDataMapper();
        CiudadDataMapper ciudadDataMapper = new CiudadDataMapper();
        ClienteDataMapper clienteDataMapper = new ClienteDataMapper();
        CotizacionDataMapper cotizacionDataMapper = new CotizacionDataMapper();
        DepartamentoDataMapper departamentoDataMapper = new DepartamentoDataMapper();
        EmpresaDataMapper empresaDataMapper = new EmpresaDataMapper();
        EquipoDataMapper equipoDataMapper = new EquipoDataMapper();
        FacturaCompraDataMapper factura = new FacturaCompraDataMapper();
        FacturaCompraDetalleDataMapper facturaDetalle = new FacturaCompraDetalleDataMapper();
        FacturaDataMapper facturaDataMapper = new FacturaDataMapper();
        FacturaDetalleDataMapper facturaDetalleDataMapper = new FacturaDetalleDataMapper();
        FacturaCompraDataMapper facturaCompraDataMapper = new FacturaCompraDataMapper();
        FacturaCompraDetalleDataMapper facturaCompraDetalleDataMapper = new FacturaCompraDetalleDataMapper();
        FacturaVentaDataMapper facturaVentaDataMapper = new FacturaVentaDataMapper();
        InfraestructuraDataMapper infraestructuraDataMapper = new InfraestructuraDataMapper();
        ItemDataMapper itemDataMapper = new ItemDataMapper();
        ItemStatusDataMapper itemStatusDataMapper = new ItemStatusDataMapper();
        LoteDataMapper loteDataMapper = new LoteDataMapper();
        MarcaDataMapper marcaDataMapper = new MarcaDataMapper();
        MedioEnvioDataMapper medioEnvioDataMapper = new MedioEnvioDataMapper();
        ModeloDataMapper modeloDataMapper = new ModeloDataMapper();
        MonedaDataMapper monedaDataMapper = new MonedaDataMapper();
        MovimientoDataMapper movimientoDataMapper = new MovimientoDataMapper();
        MovimientoDetalleDataMapper movimientoDetalleDataMapper = new MovimientoDetalleDataMapper();
        PaisDataMapper paisDataMapper = new PaisDataMapper();
        PedimentoDataMapper pedimentoDataMapper = new PedimentoDataMapper();
        PomArticuloDataMapper pomArticuloDataMapper = new PomArticuloDataMapper();
        PomDataMapper pomDataMapper = new PomDataMapper();
        PropiedadDataMapper propiedadDataMapper = new PropiedadDataMapper();
        ProveedorCuentaDataMapper proveedorCuentaDataMapper = new ProveedorCuentaDataMapper();
        ProveedorDataMapper proveedorDataMapper = new ProveedorDataMapper();
        ProveedorCategoriaDataMapper proveedorCategoriaDataMapper = new ProveedorCategoriaDataMapper();
        ProyectoDataMapper proyectoDataMapper = new ProyectoDataMapper();
        ReciboDataMapper reciboDataMapper = new ReciboDataMapper();
        ReciboMovimientoDataMapper reciboMovimientoDataMapper = new ReciboMovimientoDataMapper();
        ReciboStatusDataMapper reciboStatusDataMapper = new ReciboStatusDataMapper();
        ServerLastDataMapper ServerLastDataMapper = new ServerLastDataMapper();
        ServicioDataMapper servicioDataMapper = new ServicioDataMapper();
        SolicitanteDataMapper solicitanteDataMapper = new SolicitanteDataMapper();
        SyncDataMapper syncDataMapper = new SyncDataMapper();
        TecnicoDataMapper tecnicoDataMapper = new TecnicoDataMapper();
        TerminoEnvioDataMapper terminoEnvioDataMapper = new TerminoEnvioDataMapper();
        TipoCotizacionDataMapper tipoCotizacionDataMapper = new TipoCotizacionDataMapper();
        TipoEmpresaDataMapper tipoEmpresaDataMapper = new TipoEmpresaDataMapper();
        TipoMovimientoDataMapper tipoMovimientoDataMapper = new TipoMovimientoDataMapper();
        TipoPedimentoDataMapper tipoPedimentoDataMapper = new TipoPedimentoDataMapper();
        TransporteDataMapper transporteDataMapper = new TransporteDataMapper();
        UltimoMovimientoDataMapper ultimoMovimientoDataMapper = new UltimoMovimientoDataMapper();
        UnidadDataMapper unidadDataMapper = new UnidadDataMapper();
        UploadLogDataMapper uploadLogDataMapper = new UploadLogDataMapper();
        System.Timers.Timer t;
        string _message;
        bool _jobDone;
        //prueba
        //string routeService1 = @"http://localhost:8082/Services/Receiver.svc";
        //servidor
        //string routeService = @"http://192.168.0.116:2020/Services/Receiver.svc";
        //servidor inmeta
        //string routeService2 = @"http://192.168.0.116:2020/Services/Receiver.svc";
        //servidor elara
        string routeService = @"http://10.50.0.131:8080/Services/Receiver.svc";
        //Prueba Adolfo
        string routeDownload = @"http://10.50.0.131:8080/Services/Broadcast.svc";
        

        string dataUser = "{'UNID_UPLOAD_LOG':0,'MSG':null,'IP_DIR':'192.168.1.110','PC_NAME':'ISAAC-PC','UNID_USUARIO':1,'USUARIO':null}";
        #endregion

        public string Message
        {
            get { return _message; }
            set
            {
                if (value != _message)
                {
                    this._message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        public bool JobDone
        {
            get { return _jobDone; }
            set
            {
                if (value != _jobDone)
                {
                    this._jobDone = value;
                    OnPropertyChanged("JobDone");
                }
            }
        }

        public UploadProcessViewModel()
        {
            this.Message = "Test";
            this._jobDone = false;
            t = new System.Timers.Timer(100);
            t.Enabled = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(DownloadData);
        }

        public void start()
        {
            UploadProcessViewModel.IsRunning = true;
            t.Start();
        }

        public void UploadData()
        {
            //Poner lógica de consumo de servicios para enviar los datos
            bool res=true;
            #region todos los catalogos de APP
            if (res)
            {
                this.Message = "Enviando MENU ...";
                res = CallServiceMenu();
                if (res)
                {
                    menuDataMapper.ResetMenu();
                }
            }

            if (res)
            {
                this.Message = "Enviando ROL ...";
                res = CallServiceRol();
                if (res)
                {
                    rolDataMapper.ResetRol();
                }
            }

            if (res)
            {
                this.Message = "Enviando ROL_MENU ...";
                res = CallServiceRolMenu();
                if (res)
                {
                    rolMenuDataMapper.ResetRolMenu();
                }
            }

            if (res)
            {
                this.Message = "Enviando USUARIO ...";
                res = CallServiceUsuario();
                if (res)
                {
                    usuarioDataMapappr.ResetUsuario();
                }
            }

            if (res)
            {
                this.Message = "Enviando USUARIO_ROL ...";
                res = CallServiceUsuarioRol();
                if (res)
                {
                    usuarioRolDataMapper.ResetUsuarioRol();
                }
            }

            #endregion

            #region todos los catalogos de ARTICULOS
            if (res)
            {
                this.Message = "Enviando CATEGORIA ...";
                res = CallServiceCategoria();
                if (res)
                {
                    cat.ResetCategoria();
                }
            }

            if (res)
            {
                this.Message = "Enviando EQUIPO ...";
                res = CallServiceEquipo();
                if (res)
                {
                    eq.ResetEquipo();
                }
            }

            if (res)
            {
                this.Message = "Enviando MARCA ...";
                res = CallServiceMarca();
                if (res)
                {
                    mar.ResetMarca();
                }
            }

            if (res)
            {
                this.Message = "Enviando MODELO ...";
                res = CallServiceModelo();
                if (res)
                {
                    mol.ResetModelo();
                }
            }

            if (res)
            {
                this.Message = "Enviando ARTICULO ...";
                res = CallServiceArticulo();
                if (res)
                {
                    art.ResetArticulo();
                    this.Message = "Sincronizacion completada ...";
                    //Esta instrucción cierra la ventana
                    this.JobDone = true;
                }
            }
            #endregion

            #region todos los catalogos de CAT 1

            if (res)
            {
                this.Message = "Enviando BANCO ...";
                res = CallServiceBanco();
                if (res)
                {
                    bancoDataMapper.ResetBanco();
                }
            }
            if (res)
            {
                this.Message = "Enviando DEPARTAMENTO ...";
                res = CallServiceDepartamento();
                if (res)
                {
                    departamentoDataMapper.ResetDepartamento();
                }
            }

            if (res)
            {
                this.Message = "Enviando EMPRESA ...";
                res = CallServiceEmpresa();
                if (res)
                {
                    empresaDataMapper.ResetEmpresa();
                }
            }

            if (res)
            {
                this.Message = "Enviando MEDIO_ENVIO ...";
                res = CallServiceMedioEnvio();
                if (res)
                {
                    medioEnvioDataMapper.ResetMedioEnvio();
                }
            }

            if (res)
            {
                this.Message = "Enviando MONEDA ...";
                res = CallServiceMoneda();
                if (res)
                {
                    modeloDataMapper.ResetModelo();
                }
            }
    
            #endregion

            #region todos los catalogos de GEO
            if (res)
            {
                this.Message = "Enviando CIUDAD ...";
                res = CallServiceCiudad();
                if (res)
                {
                    ciu.ResetCiudad();
                }
            }

            if (res)
            {
                this.Message = "Enviando PAIS ...";
                res = CallServicePais();
                if (res)
                {
                    pai.ResetPais();
                }
            }
            #endregion 

            #region todos los catalogos de CAT 2
            if (res)
            {
                this.Message = "Enviando PROVEEDOR ...";
                res = CallServiceProveedor();
                if (res)
                {
                    proveedorDataMapper.ResetProveedor();
                }
            }

            if (res)
            {
                this.Message = "Enviando PROVEEDOR_CATEGORIA ...";
                res = CallServiceProveedorCategoria();
                if (res)
                {
                    proveedorCategoriaDataMapper.ResetProveedorCategoria();
                }
            }

            if (res)
            {
                this.Message = "Enviando PROVEEDOR_CUENTA ...";
                res = CallServiceProveedorCategoria();
                if (res)
                {
                    proveedorCuentaDataMapper.ResetProveedorCuenta();
                }
            }

            if (res)
            {
                this.Message = "Enviando PROYECTO ...";
                res = CallServiceProyecto();
                if (res)
                {
                    proyectoDataMapper.ResetProyecto();
                }
            }

            if (res)
            {
                this.Message = "Enviando SOLICITANTE ...";
                res = CallServiceSolicitante();
                if (res)
                {
                    solicitanteDataMapper.ResetSolicitante();
                }
            }

            if (res)
            {
                this.Message = "Enviando TERMINO_ENVIO ...";
                res = CallServiceTerminoEnvio();
                if (res)
                {
                    terminoEnvioDataMapper.ResetTerminoEnvio();
                }
            }

            if (res)
            {
                this.Message = "Enviando TIPO_COTIZACION ...";
                res = CallServiceTipoCotizacion();
                if (res)
                {
                    tipoCotizacionDataMapper.ResetTipoCotizacion();
                }
            }

            if (res)
            {
                this.Message = "Enviando TIPO_EMPRESA ...";
                res = CallServiceTipoEmpresa();
                if (res)
                {
                    tipoEmpresaDataMapper.ResetTipoEmpresa();
                }
            }

            if (res)
            {
                this.Message = "Enviando TIPO_PEDIMENTO ...";
                res = CallServiceTipoPedimento();
                if (res)
                {
                    tipoPedimentoDataMapper.ResetTipoPedimento();
                }
            }

            if (res)
            {
                this.Message = "Enviando TRANSPORTE ...";
                res = CallServiceTipoPedimento();
                if (res)
                {
                    transporteDataMapper.ResetTransporte();
                }
            }
            #endregion

            #region todos los catalogos de CATINV
            if (res)
            {
                this.Message = "Enviando ALMACEN ...";
                res = CallServiceAlmacen();
                if (res)
                {
                    almacenDataMapper.ResetAlmacen();
                }
            }

            if (res)
            {
                this.Message = "Enviando TECNICO ...";
                res = CallServiceTecnico();
                if (res)
                {
                    tecnicoDataMapper.ResetTecnico();
                }
            }

            if (res)
            {
                this.Message = "Enviando ALMACEN_TECNICO ...";
                res = CallServiceAlmacenTecnico();
                if (res)
                {
                    almacenTecnicoDataMapper.ResetAlmacenTecnico();
                }
            }

            if (res)
            {
                this.Message = "Enviando CLIENTE ...";
                res = CallServiceCliente();
                if (res)
                {
                    clienteDataMapper.ResetCliente();
                }
            }

            if (res)
            {
                this.Message = "Enviando ITEM_STATUS ...";
                res = CallServiceItemStatus();
                if (res)
                {
                    itemStatusDataMapper.ResetItemStatus();
                }
            }

            if (res)
            {
                this.Message = "Enviando PROPIEDAD ...";
                res = CallServicePropiedad();
                if (res)
                {
                    propiedadDataMapper.ResetPropiedad();
                }
            }

            if (res)
            {
                this.Message = "Enviando SERVICIO ...";
                res = CallServiceServicio();
                if (res)
                {
                    servicioDataMapper.ResetServicio();
                }
            }

            if (res)
            {
                this.Message = "Enviando TIPO_MOVIMIENTO ...";
                res = CallServiceTipoMovimiento();
                if (res)
                {
                    tipoMovimientoDataMapper.ResetTipoMovimiento();
                }
            }

            if (res)
            {
                this.Message = "Enviando UNIDAD ...";
                res = CallServiceUnidad();
                if (res)
                {
                    unidadDataMapper.ResetUnidad();
                }
            }   
            #endregion

            #region todos los catalogos de COT
            if (res)
            {
                this.Message = "Enviando COTIZACION ...";
                res = CallServiceCotizacion();
                if (res)
                {
                    cotizacionDataMapper.ResetCotizacion();
                }
            }
            #endregion

            #region todos los catalogos de INV 1
            if (res)
            {
                this.Message = "Enviando FACTURA_VENTA ...";
                res = CallServiceFacturaVenta();
                if (res)
                {
                    facturaVentaDataMapper.ResetFacturaVenta();
                }
            }
            #endregion
           
            #region todos los catalogos de POM
            if (res)
            {
                this.Message = "Enviando POM ...";
                res = CallServicePom();
                if (res)
                {
                    pomDataMapper.ResetPom();
                }
            }

            if (res)
            {
                this.Message = "Enviando POM_ARTICULO ...";
                res = CallServicePomArticulo();
                if (res)
                {
                    pomArticuloDataMapper.ResetPomArticulo();
                }
            }
            #endregion

            #region todos los catalogos de LOT
            if (res)
            {
                this.Message = "Enviando LOTE ...";
                res = CallServiceLote();
                if (res)
                {
                    loteDataMapper.ResetLote();
                }
            }

            if (res)
            {
                this.Message = "Enviando PEDIMENTO ...";
                res = CallServicePedimento();
                if (res)
                {
                    pedimentoDataMapper.ResetPedimento();
                }
            }

            if (res)
            {
                this.Message = "Enviando FACTURA ...";
                res = CallServiceFactura();
                if (res)
                {
                    factura.ResetFactura();   
                }
            }

            if (res)
            {
                this.Message = "Enviando FACTURA_DETALLE ...";
                res = CallServiceFacturaDetalle();
                if (res)
                {
                    facturaDetalle.ResetFacturaDetalle();
                }
            }
            #endregion

            #region todos los catalogos de INV 2
            if (res)
            {
                this.Message = "Enviando RECIBO_STATUS ...";
                res = CallServiceReciboStatus();
                if (res)
                {
                    reciboStatusDataMapper.ResetReciboStatus();
                }
            }

            if (res)
            {
                this.Message = "Enviando RECIBO ...";
                res = CallServiceRecibo();
                if (res)
                {
                    reciboDataMapper.ResetRecibo();
                }
            }

            if (res)
            {
                this.Message = "Enviando ITEM ...";
                res = CallServiceItem();
                if (res)
                {
                    itemDataMapper.ResetItem();
                }
            }

            if (res)
            {
                this.Message = "Enviando MOVIMIENTO ...";
                res = CallServiceMovimiento();
                if (res)
                {
                    movimientoDataMapper.ResetMovimiento();
                }
            }

            if (res)
            {
                this.Message = "Enviando MOVIMIENTO_DETALLE ...";
                res = CallServiceMovimientoDetalle();
                if (res)
                {
                    movimientoDetalleDataMapper.ResetMovimientoDetalle();
                }
            }

            if (res)
            {
                this.Message = "Enviando ULTIMO_MOVIMIENTO ...";
                res = CallServiceUltimoMovimiento();
                if (res)
                {
                    ultimoMovimientoDataMapper.ResetUltimoMovimiento();
                }
            }

            if (res)
            {
                this.Message = "Enviando RECIBO_MOVIMIENTO ...";
                res = CallServiceReciboMovimiento();
                if (res)
                {
                    reciboMovimientoDataMapper.ResetReciboMovimiento();
                }
            }
            #endregion

            if (res)
            {
                syn.ResetDummy();    
            }
            
            UploadProcessViewModel.IsRunning = false;
            //Esta instrucción cierra la ventana
            this.JobDone = true;
            //********* todos los dd articulo    
            
        }

        public void DownloadData(Object sender, System.Timers.ElapsedEventArgs args)
        {
            this.t.Enabled = false;
            ((System.Timers.Timer)sender).Stop();

            bool res = true;

            long serverDate = CallDownloadServiceGetServerLast();

            if (serverDate == 0)
                return;

            if (res)
            {
                this.Message = "Descargando CATEGORIA ...";
                res = CallDownloadServiceCategoria(serverDate);
            }

            //Si toda la descarga es correcta, ejecutar la subida de información
            if (res)
            {
                ServerLastDataMapper s = new ServerLastDataMapper();
                s.insertElement((object)serverDate);
                this.UploadData();
            }
        }

        public bool CallDownloadServiceCategoria(long serverDate)
        {
            #region propiedades
            bool responseSevice = true;
            string nameService = "downloadCategoria";
            CategoriaDataMapper dataMapper = new CategoriaDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion
            #region metodos

            try
            {
                //prueba
                //var client = new RestClient(routeService1);
                //client.Authenticator = new HttpBasicAuthenticator("ISAAC", "isaac");
                //servidor
                var client = new RestClient(routeDownload);
                client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = serverDate });
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);

                List<CATEGORIA> list;
                list = dataMapper.GetDeserializeCategoria(resx["downloadCategoriaResult"]);

                if (list != null)
                    foreach (CATEGORIA item in list)
                        dataMapper.loadSync(item);

            }
            catch (Exception)
            {
                responseSevice = false;
            }

            return responseSevice;
            #endregion
        }

        public long CallDownloadServiceGetServerLast()
        {
            #region propiedades
            long responseSevice = 0;
            string nameService = "GetServerLast";
            ServerLastDataMapper dataMapper = new ServerLastDataMapper();
            #endregion
            #region metodos

            try
            {
                var client = new RestClient(routeDownload);
                client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { });
                IRestResponse response = client.Execute(request);

                Dictionary<string, string> resx = dataMapper.GetResponseDictionary(response.Content);

                responseSevice = dataMapper.GetDeserializeServerLast(resx["GetServerLastResult"]);
            }
            catch (Exception)
            {
                return 0;
            }

            return responseSevice;
            #endregion
        }

        #region todos los metodos de APP
        public bool CallServiceMenu()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadAppMenu";
            AppMenuDataMapper dataMapper = new AppMenuDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonMenu();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    //prueba
                    //var client = new RestClient(routeService1);
                    //client.Authenticator = new HttpBasicAuthenticator("ISAAC", "isaac");
                    //servidor
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceRol()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadAppRol";
            AppRolDataMapper dataMapper = new AppRolDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonRol();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    //prueba
                    //var client = new RestClient(routeService1);
                    //client.Authenticator = new HttpBasicAuthenticator("ISAAC", "isaac");
                    //servidor
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceRolMenu()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadAppRolMenu";
            AppRolMenuDataMapper dataMapper = new AppRolMenuDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonRolMenu();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    //prueba
                    //var client = new RestClient(routeService1);
                    //client.Authenticator = new HttpBasicAuthenticator("ISAAC", "isaac");
                    //servidor
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceUsuario()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadAppUsuario";
            AppUsuario dataMapper = new AppUsuario();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonUsuario();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    //prueba
                    //var client = new RestClient(routeService1);
                    //client.Authenticator = new HttpBasicAuthenticator("ISAAC", "isaac");
                    //servidor
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceUsuarioRol()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadAppUsuarioRol";
            AppUsuarioRol dataMapper = new AppUsuarioRol();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonUsuarioRol();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    //prueba
                    //var client = new RestClient(routeService1);
                    //client.Authenticator = new HttpBasicAuthenticator("ISAAC", "isaac");
                    //servidor
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }
        #endregion

        #region todos los metodos de ARTICULOS
        public bool CallServiceCategoria()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadCategoria";
            CategoriaDataMapper dataMapper = new CategoriaDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonCategoria();

            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    //prueba
                    //var client = new RestClient(routeService1);
                    //client.Authenticator = new HttpBasicAuthenticator("ISAAC", "isaac");
                    //servidor
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceEquipo()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadEquipo";
            EquipoDataMapper dataMapper = new EquipoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonEquipo();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceMarca()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadMarca";
            MarcaDataMapper dataMapper = new MarcaDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonMarca();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceModelo()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadModelo";
            ModeloDataMapper dataMapper = new ModeloDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonModelo();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceArticulo()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadArticulo";
            ArticuloDataMapper dataMapper = new ArticuloDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonArticulo();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }
        #endregion

        #region todos los metodos de CAT
        public bool CallServiceBanco()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadBanco";
            BancoDataMapper dataMapper = new BancoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonBanco();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceDepartamento()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadDepartamento";
            DepartamentoDataMapper dataMapper = new DepartamentoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonDepartamento();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceEmpresa()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadEmpresa";
            EmpresaDataMapper dataMapper = new EmpresaDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonEmpresa();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceMedioEnvio()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadMedioEnvio";
            MedioEnvioDataMapper dataMapper = new MedioEnvioDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonMedioEnvio();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceMoneda()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadMoneda";
            MonedaDataMapper dataMapper = new MonedaDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonMoneda();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceProveedor()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadProveedor";
            ProveedorDataMapper dataMapper = new ProveedorDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonProveedor();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }
        
        public bool CallServiceProveedorCategoria()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadProveedorCategoria";
            ProveedorCategoriaDataMapper dataMapper = new ProveedorCategoriaDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonProveedorCategoria();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceProveedorCuenta()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadProveedorCuenta";
            ProveedorCuentaDataMapper dataMapper = new ProveedorCuentaDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonProveedorCuenta();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceProyecto()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadProyecto";
            ProyectoDataMapper dataMapper = new ProyectoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonProyecto();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceSolicitante()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadSolicitante";
            SolicitanteDataMapper dataMapper = new SolicitanteDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonSolicitante();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceTipoCotizacion()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadTipoCotizacion";
            TipoCotizacionDataMapper dataMapper = new TipoCotizacionDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonTipoCotizacion();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceTerminoEnvio()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadTerminoEnvio";
            TerminoEnvioDataMapper dataMapper = new TerminoEnvioDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonTerminoEnvio();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceTipoEmpresa()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadTipoEmpresa";
            TipoEmpresaDataMapper dataMapper = new TipoEmpresaDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonTipoEmpresa();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceTipoPedimento()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadTipoPedimento";
            TipoPedimentoDataMapper dataMapper = new TipoPedimentoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonTipoPedimento();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceTransporte()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadTransporte";
            TransporteDataMapper dataMapper = new TransporteDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonTransporte();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        #endregion

        #region todos los metodos de COT

        public bool CallServiceCotizacion()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadCotizacion";
            CotizacionDataMapper dataMapper = new CotizacionDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonCotizacion();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }
        #endregion

        #region todos los metodos de CAT_INV
        public bool CallServiceAlmacen()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadAlmacen";
            AlmacenDataMapper dataMapper = new AlmacenDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonAlmacen();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceAlmacenTecnico()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadAlmacenTecnico";
            AlmacenTecnicoDataMapper dataMapper = new AlmacenTecnicoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonAlmacenTecnico();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceCliente()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadCliente";
            ClienteDataMapper dataMapper = new ClienteDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonCliente();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceItemStatus()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadItemStatus";
            ItemStatusDataMapper dataMapper = new ItemStatusDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonItemStatus();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServicePropiedad()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadPropiedad";
            PropiedadDataMapper dataMapper = new PropiedadDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonPropiedad();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceServicio()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadServicio";
            ServicioDataMapper dataMapper = new ServicioDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonServicio();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceTecnico()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadTecnico";
            TecnicoDataMapper dataMapper = new TecnicoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonTecnico();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceTipoMovimiento()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadTipoMovimiento";
            TipoMovimientoDataMapper dataMapper = new TipoMovimientoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonTipoMovimiento();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceUnidad()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadUnidad";
            UnidadDataMapper dataMapper = new UnidadDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonUnidad();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }
        #endregion

        #region todos los metodos de GEO

        public bool CallServiceCiudad()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadCiudad";
            CiudadDataMapper dataMapper = new CiudadDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonCiudad();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServicePais()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadPais";
            PaisDataMapper dataMapper = new PaisDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonPais();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }
        #endregion

        #region todos los metodos de INV
        public bool CallServiceFacturaVenta()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadFacturaVenta";
            FacturaVentaDataMapper dataMapper = new FacturaVentaDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonFacturaVenta();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceItem()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadItem";
            ItemDataMapper dataMapper = new ItemDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonItem();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceMovimiento()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadMovimiento";
            MovimientoDataMapper dataMapper = new MovimientoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonMovimiento();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceMovimientoDetalle()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadMovimientoDetalle";
            MovimientoDetalleDataMapper dataMapper = new MovimientoDetalleDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonMovimientoDetalle();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceRecibo()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadRecibo";
            ReciboDataMapper dataMapper = new ReciboDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonRecibo();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceReciboMovimiento()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadReciboMovimiento";
            ReciboMovimientoDataMapper dataMapper = new ReciboMovimientoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonReciboMovimiento();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceReciboStatus()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadReciboStatus";
            ReciboStatusDataMapper dataMapper = new ReciboStatusDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonReciboStatus();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceUltimoMovimiento()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadUltimoMovimiento";
            UltimoMovimientoDataMapper dataMapper = new UltimoMovimientoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonUltimoMovimiento();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        #endregion

        #region todos los metodos de LOT
        public bool CallServiceFactura()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadFactura";
            FacturaCompraDataMapper dataMapper = new FacturaCompraDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonFactura();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceFacturaDetalle()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadFacturaDetalle";
            FacturaCompraDetalleDataMapper dataMapper = new FacturaCompraDetalleDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonFacturaDetalle();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServiceLote()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadLote";
            LoteDataMapper dataMapper = new LoteDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonLote();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServicePedimento()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadPedimento";
            PedimentoDataMapper dataMapper = new PedimentoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonPedimento();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }
        #endregion

        #region todos los metodos de POM
        public bool CallServicePom()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadPom";
            PomDataMapper dataMapper = new PomDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonPom();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }

        public bool CallServicePomArticulo()
        {
            #region propiedades
            bool responseSevice;
            string nameService = "LoadPomArticulo";
            PomArticuloDataMapper dataMapper = new PomArticuloDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
            #endregion

            #region metodos
            //madamos a llamar el metodo que serializa list de pocos
            string listPocos = dataMapper.GetJsonPomArticulo();
            if (!String.IsNullOrEmpty(listPocos))
            {
                try
                {
                    var client = new RestClient(routeService);
                    client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                    var request = new RestRequest(Method.POST);
                    request.Resource = nameService;
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    request.AddHeader("Content-type", "application/json");
                    request.AddBody(new { listPocos = listPocos, dataUser = dataUser });
                    IRestResponse response = client.Execute(request);
                    responseSevice = user.GetDeserializeUpLoad(response.Content);
                }
                catch (Exception)
                {
                    responseSevice = false;
                }
            }
            else
            {
                responseSevice = true;
            }
            return responseSevice;
            #endregion
        }
        #endregion
    }
}
