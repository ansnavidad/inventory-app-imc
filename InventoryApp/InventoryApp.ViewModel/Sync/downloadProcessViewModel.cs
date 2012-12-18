using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using InventoryApp.DAL;
using System.Timers;
using InventoryApp.Model;
using InventoryApp.DAL.Recibo;

namespace InventoryApp.ViewModel.Sync
{
    public class downloadProcessViewModel : ViewModelBase
    {
        #region propiedades
        public static bool IsRunning = false;
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
        Timer t;
        string _message;
        bool _jobDone;
        //prueba
        string routeService1 = @"http://localhost:8082/Services/Receiver.svc";
        //servidor
        string routeService = @"http://192.168.0.116:2020/Services/Receiver.svc";

        string dataUser = "{'UNID_UPLOAD_LOG':0,'MSG':null,'IP_DIR':'192.168.1.110','PC_NAME':'ISAAC-PC','UNID_USUARIO':1,'USUARIO':null}";

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

        #endregion

        public downloadProcessViewModel()
        {
            this.Message = "Test";
            this._jobDone = false;
            t = new System.Timers.Timer(100);
            t.Enabled = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(DownloadData);
        }

        public void start()
        {
            downloadProcessViewModel.IsRunning = true;
            t.Start();
        }

        public void DownloadData(Object sender, System.Timers.ElapsedEventArgs args)
        {
            //No borrar! Esto debe ir en la primera sentencia
            this.t.Enabled = false;
            ((System.Timers.Timer)sender).Stop();

            //Poner lógica de consumo de servicios para enviar los datos
            bool res = true;
            //********* todos los dd articulo
            //if (res)
            //{
            //    this.Message = "Descargando CATEGORIA ...";
            //    res = CallServiceCategoria();
            //    if (res)
            //    {
            //        cat.ResetCategoria();
            //    }
            //}

            syncDataMapper.ResetDummy();
            downloadProcessViewModel.IsRunning = false;
            //Esta instrucción cierra la ventana
            this.JobDone = true;
            //********* todos los dd articulo    

        }
    }
}
