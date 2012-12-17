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
            if (!String.IsNullOrEmpty(dataMapper.GetJsonCategoria(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonCategoria(lastModifiedDate);
            }
            return respuesta;
        }


        public string downloadAlmacen(long lastModifiedDate)
        {
            string respuesta = null;
            AlmacenDataMapper dataMapper = new AlmacenDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonAlmacen(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonAlmacen(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadMenu(long lastModifiedDate)
        {
            string respuesta = null;
            AppMenuDataMapper dataMapper = new AppMenuDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonMenu(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonMenu(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadRol(long lastModifiedDate)
        {
            string respuesta = null;
            AppRolDataMapper dataMapper = new AppRolDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonRol(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonRol(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadRolmenu(long lastModifiedDate)
        {
            string respuesta = null;
            AppRolMenuDataMapper dataMapper = new AppRolMenuDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonRolMenu(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonRolMenu(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadUsuario(long lastModifiedDate)
        {
            string respuesta = null;
            AppUsuario dataMapper = new AppUsuario();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonUsuario(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonUsuario(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadUsuarioRol(long lastModifiedDate)
        {
            string respuesta = null;
            AppUsuarioRol dataMapper = new AppUsuarioRol();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonUsuarioRol(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonUsuarioRol(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadCiudad(long lastModifiedDate)
        {
            string respuesta = null;
            CiudadDataMapper dataMapper = new CiudadDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonCiudad(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonCiudad(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadCliente(long lastModifiedDate)
        {
            string respuesta = null;
            ClienteDataMapper dataMapper = new ClienteDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonCliente(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonCliente(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadCotizacion(long lastModifiedDate)
        {
            string respuesta = null;
            CotizacionDataMapper dataMapper = new CotizacionDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonCotizacion(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonCotizacion(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadDepartamento(long lastModifiedDate)
        {
            string respuesta = null;
            DepartamentoDataMapper dataMapper = new DepartamentoDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonDepartamento(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonDepartamento(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadEmpresa(long lastModifiedDate)
        {
            string respuesta = null;
            EmpresaDataMapper dataMapper = new EmpresaDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonEmpresa(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonEmpresa(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadEquipo(long lastModifiedDate)
        {
            string respuesta = null;
            EquipoDataMapper dataMapper = new EquipoDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonEquipo(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonEquipo(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadFactura(long lastModifiedDate)
        {
            string respuesta = null;
            FacturaCompraDataMapper dataMapper = new FacturaCompraDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonFactura(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonFactura(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadFacturaDetalle(long lastModifiedDate)
        {
            string respuesta = null;
            FacturaCompraDetalleDataMapper dataMapper = new FacturaCompraDetalleDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonFacturaDetalle(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonFacturaDetalle(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadFacturaVenta(long lastModifiedDate)
        {
            string respuesta = null;
            FacturaVentaDataMapper dataMapper = new FacturaVentaDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonFacturaVenta(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonFacturaVenta(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadItem(long lastModifiedDate)
        {
            string respuesta = null;
            ItemDataMapper dataMapper = new ItemDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonItem(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonItem(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadItemStatus(long lastModifiedDate)
        {
            string respuesta = null;
            ItemStatusDataMapper dataMapper = new ItemStatusDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonItemStatus(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonItemStatus(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadLote(long lastModifiedDate)
        {
            string respuesta = null;
            AlmacenDataMapper dataMapper = new AlmacenDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonAlmacen(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonAlmacen(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadMarca(long lastModifiedDate)
        {
            string respuesta = null;
            MarcaDataMapper dataMapper = new MarcaDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonMarca(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonMarca(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadMedioEnvio(long lastModifiedDate)
        {
            string respuesta = null;
            MedioEnvioDataMapper dataMapper = new MedioEnvioDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonMedioEnvio(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonMedioEnvio(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadModelo(long lastModifiedDate)
        {
            string respuesta = null;
            ModeloDataMapper dataMapper = new ModeloDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonModelo(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonModelo(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadMoneda(long lastModifiedDate)
        {
            string respuesta = null;
            MonedaDataMapper dataMapper = new MonedaDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonMoneda(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonMoneda(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadMovimiento(long lastModifiedDate)
        {
            string respuesta = null;
            MovimientoDataMapper dataMapper = new MovimientoDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonMovimiento(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonMovimiento(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadMovimientoDetalle(long lastModifiedDate)
        {
            string respuesta = null;
            MovimientoDetalleDataMapper dataMapper = new MovimientoDetalleDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonMovimientoDetalle(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonMovimientoDetalle(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadPais(long lastModifiedDate)
        {
            string respuesta = null;
            PaisDataMapper dataMapper = new PaisDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonPais(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonPais(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadPedimento(long lastModifiedDate)
        {
            string respuesta = null;
            PedimentoDataMapper dataMapper = new PedimentoDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonPedimento(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonPedimento(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadPomArticulo(long lastModifiedDate)
        {
            string respuesta = null;
            PomArticuloDataMapper dataMapper = new PomArticuloDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonPomArticulo(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonPomArticulo(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadPom(long lastModifiedDate)
        {
            string respuesta = null;
            PomArticuloDataMapper dataMapper = new PomArticuloDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonPomArticulo(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonPomArticulo(lastModifiedDate);
            }
            return respuesta;
        }

        public string downloadPropiedad(long lastModifiedDate)
        {
            string respuesta = null;
            PropiedadDataMapper dataMapper = new PropiedadDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonPropiedad(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonPropiedad(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadProveedorCuenta(long lastModifiedDate)
        {
            string respuesta = null;
            ProveedorCuentaDataMapper dataMapper = new ProveedorCuentaDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonProveedorCuenta(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonProveedorCuenta(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadProveedor(long lastModifiedDate)
        {
            string respuesta = null;
            ProveedorDataMapper dataMapper = new ProveedorDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonProveedor(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonProveedor(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadProyecto(long lastModifiedDate)
        {
            string respuesta = null;
            ProyectoDataMapper dataMapper = new ProyectoDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonProyecto(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonProyecto(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadRecibo(long lastModifiedDate)
        {
            string respuesta = null;
            ReciboDataMapper dataMapper = new ReciboDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonRecibo(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonRecibo(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadReciboMovimiento(long lastModifiedDate)
        {
            string respuesta = null;
            ReciboMovimientoDataMapper dataMapper = new ReciboMovimientoDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonReciboMovimiento(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonReciboMovimiento(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadReciboStatus(long lastModifiedDate)
        {
            string respuesta = null;
            ReciboStatusDataMapper dataMapper = new ReciboStatusDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonReciboStatus(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonReciboStatus(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadTecnico(long lastModifiedDate)
        {
            string respuesta = null;
            TecnicoDataMapper dataMapper = new TecnicoDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonTecnico(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonTecnico(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadTerminoEnvio(long lastModifiedDate)
        {
            string respuesta = null;
            TerminoEnvioDataMapper dataMapper = new TerminoEnvioDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonTerminoEnvio(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonTerminoEnvio(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadTipoCotizacion(long lastModifiedDate)
        {
            string respuesta = null;
            TipoCotizacionDataMapper dataMapper = new TipoCotizacionDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonTipoCotizacion(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonTipoCotizacion(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadTipoEmpresa(long lastModifiedDate)
        {
            string respuesta = null;
            TipoEmpresaDataMapper dataMapper = new TipoEmpresaDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonTipoEmpresa(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonTipoEmpresa(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadTipoMovimiento(long lastModifiedDate)
        {
            string respuesta = null;
            TipoMovimientoDataMapper dataMapper = new TipoMovimientoDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonTipoMovimiento(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonTipoMovimiento(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadTipoPedimento(long lastModifiedDate)
        {
            string respuesta = null;
            TipoPedimentoDataMapper dataMapper = new TipoPedimentoDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonTipoPedimento(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonTipoPedimento(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadTransporte(long lastModifiedDate)
        {
            string respuesta = null;
            TransporteDataMapper dataMapper = new TransporteDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonTransporte(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonTransporte(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadUltimoMovimiento(long lastModifiedDate)
        {
            string respuesta = null;
            UltimoMovimientoDataMapper dataMapper = new UltimoMovimientoDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonUltimoMovimiento(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonUltimoMovimiento(lastModifiedDate);
            }
            return respuesta; 
        }

        public string downloadUnidad(long lastModifiedDate)
        {
            string respuesta = null;
            UnidadDataMapper dataMapper = new UnidadDataMapper();
            if (!String.IsNullOrEmpty(dataMapper.GetJsonUnidad(lastModifiedDate)))
            {
                respuesta = dataMapper.GetJsonUnidad(lastModifiedDate);
            }
            return respuesta; 
        }
    }
}
