using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InventoryApp.DAL;
using System.ServiceModel.Activation;
using InventoryApp.Model;
using InventoryApp.DAL.Recibo;

namespace InventoryApp.Service.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    [DataContractFormat]
    public class Broadcast : IBroadcast
    {
        public long GetServerLast()
        {
            long mensaje = 0;
            ServerLastDataMapper server = new ServerLastDataMapper();

            if (server.GetServerLastFecha() != 0)
            {
                mensaje = server.GetServerLastFecha();
            }
            return mensaje;
        }

        public string downloadCategoria(long lastModifiedDate)
        {
            string respuesta = null;
            CategoriaDataMapper dataMapper = new CategoriaDataMapper();
            
            respuesta = dataMapper.GetJsonCategoria(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadAlmacen(long lastModifiedDate)
        {
            string respuesta = null;
            AlmacenDataMapper dataMapper = new AlmacenDataMapper();
            
            respuesta = dataMapper.GetJsonAlmacen(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadMenu(long lastModifiedDate)
        {
            string respuesta = null;
            AppMenuDataMapper dataMapper = new AppMenuDataMapper();
            
            respuesta = dataMapper.GetJsonMenu(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadRol(long lastModifiedDate)
        {
            string respuesta = null;
            AppRolDataMapper dataMapper = new AppRolDataMapper();
            
            respuesta = dataMapper.GetJsonRol(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadRolmenu(long lastModifiedDate)
        {
            string respuesta = null;
            AppRolMenuDataMapper dataMapper = new AppRolMenuDataMapper();
            
            respuesta = dataMapper.GetJsonRolMenu(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadUsuario(long lastModifiedDate)
        {
            string respuesta = null;
            AppUsuario dataMapper = new AppUsuario();
            
            respuesta = dataMapper.GetJsonUsuario(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadUsuarioRol(long lastModifiedDate)
        {
            string respuesta = null;
            AppUsuarioRol dataMapper = new AppUsuarioRol();
            
            respuesta = dataMapper.GetJsonUsuarioRol(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadArticulo(long lastModifiedDate)
        {
            string respuesta = null;
            ArticuloDataMapper dataMapper = new ArticuloDataMapper();

            respuesta = dataMapper.GetJsonArticulo(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadCiudad(long lastModifiedDate)
        {
            string respuesta = null;
            CiudadDataMapper dataMapper = new CiudadDataMapper();
            
            respuesta = dataMapper.GetJsonCiudad(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadCliente(long lastModifiedDate)
        {
            string respuesta = null;
            ClienteDataMapper dataMapper = new ClienteDataMapper();
            
            respuesta = dataMapper.GetJsonCliente(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadCotizacion(long lastModifiedDate)
        {
            string respuesta = null;
            CotizacionDataMapper dataMapper = new CotizacionDataMapper();
            
            respuesta = dataMapper.GetJsonCotizacion(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadDepartamento(long lastModifiedDate)
        {
            string respuesta = null;
            DepartamentoDataMapper dataMapper = new DepartamentoDataMapper();
            
            respuesta = dataMapper.GetJsonDepartamento(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadEmpresa(long lastModifiedDate)
        {
            string respuesta = null;
            EmpresaDataMapper dataMapper = new EmpresaDataMapper();
            
            respuesta = dataMapper.GetJsonEmpresa(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadEquipo(long lastModifiedDate)
        {
            string respuesta = null;
            EquipoDataMapper dataMapper = new EquipoDataMapper();
            
            respuesta = dataMapper.GetJsonEquipo(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadFactura(long lastModifiedDate)
        {
            string respuesta = null;
            FacturaCompraDataMapper dataMapper = new FacturaCompraDataMapper();
            
            respuesta = dataMapper.GetJsonFactura(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadFacturaDetalle(long lastModifiedDate)
        {
            string respuesta = null;
            FacturaCompraDetalleDataMapper dataMapper = new FacturaCompraDetalleDataMapper();
            
            respuesta = dataMapper.GetJsonFacturaDetalle(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadFacturaVenta(long lastModifiedDate)
        {
            string respuesta = null;
            FacturaVentaDataMapper dataMapper = new FacturaVentaDataMapper();
            
            respuesta = dataMapper.GetJsonFacturaVenta(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadItem(long lastModifiedDate)
        {
            string respuesta = null;
            ItemDataMapper dataMapper = new ItemDataMapper();
            
            respuesta = dataMapper.GetJsonItem(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadItemStatus(long lastModifiedDate)
        {
            string respuesta = null;
            ItemStatusDataMapper dataMapper = new ItemStatusDataMapper();
            
            respuesta = dataMapper.GetJsonItemStatus(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadLote(long lastModifiedDate)
        {
            string respuesta = null;
            LoteDataMapper dataMapper = new LoteDataMapper();
            
            respuesta = dataMapper.GetJsonLote(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadMarca(long lastModifiedDate)
        {
            string respuesta = null;
            MarcaDataMapper dataMapper = new MarcaDataMapper();
            
            respuesta = dataMapper.GetJsonMarca(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadMedioEnvio(long lastModifiedDate)
        {
            string respuesta = null;
            MedioEnvioDataMapper dataMapper = new MedioEnvioDataMapper();
            
            respuesta = dataMapper.GetJsonMedioEnvio(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadModelo(long lastModifiedDate)
        {
            string respuesta = null;
            ModeloDataMapper dataMapper = new ModeloDataMapper();
            
            respuesta = dataMapper.GetJsonModelo(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadMoneda(long lastModifiedDate)
        {
            string respuesta = null;
            MonedaDataMapper dataMapper = new MonedaDataMapper();
            
            respuesta = dataMapper.GetJsonMoneda(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadMovimiento(long lastModifiedDate)
        {
            string respuesta = null;
            MovimientoDataMapper dataMapper = new MovimientoDataMapper();
            
            respuesta = dataMapper.GetJsonMovimiento(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadMovimientoDetalle(long lastModifiedDate)
        {
            string respuesta = null;
            MovimientoDetalleDataMapper dataMapper = new MovimientoDetalleDataMapper();
            
            respuesta = dataMapper.GetJsonMovimientoDetalle(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadPais(long lastModifiedDate)
        {
            string respuesta = null;
            PaisDataMapper dataMapper = new PaisDataMapper();
            
            respuesta = dataMapper.GetJsonPais(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadPedimento(long lastModifiedDate)
        {
            string respuesta = null;
            PedimentoDataMapper dataMapper = new PedimentoDataMapper();
            
            respuesta = dataMapper.GetJsonPedimento(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadPomArticulo(long lastModifiedDate)
        {
            string respuesta = null;
            PomArticuloDataMapper dataMapper = new PomArticuloDataMapper();
            
            respuesta = dataMapper.GetJsonPomArticulo(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadPom(long lastModifiedDate)
        {
            string respuesta = null;
            PomDataMapper dataMapper = new PomDataMapper();
            
            respuesta = dataMapper.GetJsonPom(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadPropiedad(long lastModifiedDate)
        {
            string respuesta = null;
            PropiedadDataMapper dataMapper = new PropiedadDataMapper();
            
            respuesta = dataMapper.GetJsonPropiedad(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadProveedorCuenta(long lastModifiedDate)
        {
            string respuesta = null;
            ProveedorCuentaDataMapper dataMapper = new ProveedorCuentaDataMapper();
            
            respuesta = dataMapper.GetJsonProveedorCuenta(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadProveedor(long lastModifiedDate)
        {
            string respuesta = null;
            ProveedorDataMapper dataMapper = new ProveedorDataMapper();
            
            respuesta = dataMapper.GetJsonProveedor(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadProyecto(long lastModifiedDate)
        {
            string respuesta = null;
            ProyectoDataMapper dataMapper = new ProyectoDataMapper();
            
            respuesta = dataMapper.GetJsonProyecto(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadRecibo(long lastModifiedDate)
        {
            string respuesta = null;
            ReciboDataMapper dataMapper = new ReciboDataMapper();
            
            respuesta = dataMapper.GetJsonRecibo(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadReciboMovimiento(long lastModifiedDate)
        {
            string respuesta = null;
            ReciboMovimientoDataMapper dataMapper = new ReciboMovimientoDataMapper();
            
            respuesta = dataMapper.GetJsonReciboMovimiento(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadReciboStatus(long lastModifiedDate)
        {
            string respuesta = null;
            ReciboStatusDataMapper dataMapper = new ReciboStatusDataMapper();
            
            respuesta = dataMapper.GetJsonReciboStatus(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadServicio(long lastModifiedDate)
        {
            string respuesta = null;
            ServicioDataMapper dataMapper = new ServicioDataMapper();

            respuesta = dataMapper.GetJsonServicio(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadTecnico(long lastModifiedDate)
        {
            string respuesta = null;
            TecnicoDataMapper dataMapper = new TecnicoDataMapper();
            
            respuesta = dataMapper.GetJsonTecnico(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadTerminoEnvio(long lastModifiedDate)
        {
            string respuesta = null;
            TerminoEnvioDataMapper dataMapper = new TerminoEnvioDataMapper();
            
            respuesta = dataMapper.GetJsonTerminoEnvio(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadTipoCotizacion(long lastModifiedDate)
        {
            string respuesta = null;
            TipoCotizacionDataMapper dataMapper = new TipoCotizacionDataMapper();
            
            respuesta = dataMapper.GetJsonTipoCotizacion(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadTipoEmpresa(long lastModifiedDate)
        {
            string respuesta = null;
            TipoEmpresaDataMapper dataMapper = new TipoEmpresaDataMapper();
            
            respuesta = dataMapper.GetJsonTipoEmpresa(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadTipoMovimiento(long lastModifiedDate)
        {
            string respuesta = null;
            TipoMovimientoDataMapper dataMapper = new TipoMovimientoDataMapper();
            
            respuesta = dataMapper.GetJsonTipoMovimiento(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadTipoPedimento(long lastModifiedDate)
        {
            string respuesta = null;
            TipoPedimentoDataMapper dataMapper = new TipoPedimentoDataMapper();
            
            respuesta = dataMapper.GetJsonTipoPedimento(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadTransporte(long lastModifiedDate)
        {
            string respuesta = null;
            TransporteDataMapper dataMapper = new TransporteDataMapper();
            
            respuesta = dataMapper.GetJsonTransporte(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadUltimoMovimiento(long lastModifiedDate)
        {
            string respuesta = null;
            UltimoMovimientoDataMapper dataMapper = new UltimoMovimientoDataMapper();
            
            respuesta = dataMapper.GetJsonUltimoMovimiento(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadUnidad(long lastModifiedDate)
        {
            string respuesta = null;
            UnidadDataMapper dataMapper = new UnidadDataMapper();
            
            respuesta = dataMapper.GetJsonUnidad(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta; 
        }

        public string downloadBanco(long lastModifiedDate)
        {
            string respuesta = null;
            BancoDataMapper dataMapper = new BancoDataMapper();

            respuesta = dataMapper.GetJsonBanco(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadProveedorCategoria(long lastModifiedDate)
        {
            string respuesta = null;
            ProveedorCategoriaDataMapper dataMapper = new ProveedorCategoriaDataMapper();

            respuesta = dataMapper.GetJsonProveedorCategoria(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadAlmacenTecnico(long lastModifiedDate)
        {
            string respuesta = null;
            AlmacenTecnicoDataMapper dataMapper = new AlmacenTecnicoDataMapper();

            respuesta = dataMapper.GetJsonAlmacenTecnico(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string downloadSolicitante(long lastModifiedDate)
        {
            string respuesta = null;
            SolicitanteDataMapper dataMapper = new SolicitanteDataMapper();

            respuesta = dataMapper.GetJsonSolicitante(lastModifiedDate);

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }
    }
}
