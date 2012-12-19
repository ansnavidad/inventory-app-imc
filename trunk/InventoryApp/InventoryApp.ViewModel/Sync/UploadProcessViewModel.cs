﻿using System;
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
        SyncDataMapper syn = new SyncDataMapper();
        AlmacenDataMapper almacenDataMapper = new AlmacenDataMapper();
        AppMenuDataMapper menuDataMapper = new AppMenuDataMapper();
        AppRolDataMapper rolDataMapper = new AppRolDataMapper();
        AppRolMenuDataMapper rolMenuDataMapper = new AppRolMenuDataMapper();
        AppUsuario usuarioDataMapappr = new AppUsuario();
        AppUsuarioRol usarioRolDataMapper = new AppUsuarioRol();
        ArticuloDataMapper articuloDataMapper = new ArticuloDataMapper();
        BancoDataMapper bancoDataMapper = new BancoDataMapper();
        CategoriaDataMapper categoriaDataMapper = new CategoriaDataMapper();
        CiudadDataMapper ciudadDataMapper = new CiudadDataMapper();
        ClienteDataMapper clienteDataMapper = new ClienteDataMapper();
        CotizacionDataMapper cotizacionDataMapper = new CotizacionDataMapper();
        DepartamentoDataMapper departamentoDataMapper = new DepartamentoDataMapper();
        EmpresaDataMapper empresaDataMapper = new EmpresaDataMapper();
        EquipoDataMapper equipoDataMapper = new EquipoDataMapper();
        FacturaDataMapper facturaDataMapper = new FacturaDataMapper();
        FacturaDetalleDataMapper facturaDetalleDataMapper = new FacturaDetalleDataMapper();
        FacturaCompraDataMapper facturaCompraDataMapper = new FacturaCompraDataMapper();
        FacturaCompraDetalleDataMapper facturaCompraDetalleDataMapper = new FacturaCompraDetalleDataMapper();
        FacturaVentaDataMapper facturaVentaDataMapper = new FacturaVentaDataMapper();
        InfraestructuraDataMapper infraestructuraDataMapper = new InfraestructuraDataMapper();
        ItemDataMapper itemDataMapper = new ItemDataMapper();
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
        ProyectoDataMapper proyectoDataMapper = new ProyectoDataMapper();
        ReciboDataMapper reciboDataMapper = new ReciboDataMapper();
        ReciboMovimientoDataMapper reciboMovimientoDataMapper = new ReciboMovimientoDataMapper();
        ReciboStatusDataMapper reciboStatusDataMapper = new ReciboStatusDataMapper();
        ServerLastDataMapper ServerLastDataMapper = new ServerLastDataMapper();
        ServicioDataMapper ServicioDataMapper = new ServicioDataMapper();
        SolicitanteDataMapper SolicitanteDataMapper = new SolicitanteDataMapper();
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
        string routeService1 = @"http://localhost:8082/Services/Receiver.svc";
        //servidor
        //string routeService = @"http://192.168.0.116:2020/Services/Receiver.svc";

        //Prueba Adolfo
        string routeService = @"http://localhost:3033/Services/Broadcast.svc";
        

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
            //t.Elapsed += new System.Timers.ElapsedEventHandler(UploadData);
            t.Elapsed += new System.Timers.ElapsedEventHandler(DownloadData);
        }

        public void start()
        {
            UploadProcessViewModel.IsRunning = true;
            t.Start();
        }

        public void UploadData(Object sender, System.Timers.ElapsedEventArgs args)
        {
            //No borrar! Esto debe ir en la primera sentencia
            this.t.Enabled = false;
            ((System.Timers.Timer)sender).Stop();

            //Poner lógica de consumo de servicios para enviar los datos
            bool res=true;
            //********* todos los dd articulo
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
            syn.ResetDummy();
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

            //Consumir servicio de Isaac que compara UNID's, en caso de que el servidor sea mayor que el actual
            //Actualizar y comenzar todo el proceso

            //********* todos los dd articulo
            if (res)
            {
                this.Message = "Descargando CATEGORIA ...";
                res = CallDownloadServiceCategoria();                
            }

            //if (res)
            //{
            //    this.Message = "Enviando ARTICULO ...";
            //    res = CallServiceArticulo();
            //    if (res)
            //    {
            //        art.ResetArticulo();
            //        this.Message = "Sincronizacion completada ...";
            //        //Esta instrucción cierra la ventana
            //        this.JobDone = true;
            //    }
            //}
            
            UploadProcessViewModel.IsRunning = false;
            //Esta instrucción cierra la ventana
            this.JobDone = true;
        }

        public bool CallDownloadServiceCategoria()
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
                var client = new RestClient(routeService);
                client.Authenticator = new HttpBasicAuthenticator("Administrator", "Passw0rd1!");
                var request = new RestRequest(Method.POST);
                request.Resource = nameService;
                request.RequestFormat = RestSharp.DataFormat.Json;
                request.AddHeader("Content-type", "application/json");
                request.AddBody(new { lastModifiedDate = dataMapper.LastModifiedDate() });
                IRestResponse response = client.Execute(request);

                Dictionary<string,string> resx= dataMapper.GetResponseDictionary(response.Content);

                List<CATEGORIA> list;
                list = dataMapper.GetDeserializeCategoria(resx["downloadCategoriaResult"]);
                
                if(list != null)
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
            bool responseSevice;
            string nameService = "LoadEquipo";
            EquipoDataMapper dataMapper = new EquipoDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
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
        }

        public bool CallServiceMarca()
        {
            bool responseSevice;
            string nameService = "LoadMarca";
            MarcaDataMapper dataMapper = new MarcaDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
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
        }

        public bool CallServiceModelo()
        {
            bool responseSevice;
            string nameService = "LoadModelo";
            ModeloDataMapper dataMapper = new ModeloDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
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
        }

        public bool CallServiceArticulo()
        {
            bool responseSevice;
            string nameService = "LoadArticulo";
            ArticuloDataMapper dataMapper = new ArticuloDataMapper();
            UploadLogDataMapper user = new UploadLogDataMapper();
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
        }
    }
}
