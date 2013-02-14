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
using InventoryApp.DAL.POCOS;

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

        public string downloadCategoria(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                CategoriaDataMapper dataMapper = new CategoriaDataMapper();

                respuesta = dataMapper.GetJsonCategoria(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadAlmacen(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                AlmacenDataMapper dataMapper = new AlmacenDataMapper();

                respuesta = dataMapper.GetJsonAlmacen(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadMenu(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                AppMenuDataMapper dataMapper = new AppMenuDataMapper();

                respuesta = dataMapper.GetJsonMenu(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadRol(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                AppRolDataMapper dataMapper = new AppRolDataMapper();

                respuesta = dataMapper.GetJsonRol(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadRolmenu(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                AppRolMenuDataMapper dataMapper = new AppRolMenuDataMapper();

                respuesta = dataMapper.GetJsonRolMenu(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadUsuario(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                AppUsuario dataMapper = new AppUsuario();

                respuesta = dataMapper.GetJsonUsuario(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadUsuarioRol(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                AppUsuarioRol dataMapper = new AppUsuarioRol();

                respuesta = dataMapper.GetJsonUsuarioRol(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadArticulo(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ArticuloDataMapper dataMapper = new ArticuloDataMapper();

                respuesta = dataMapper.GetJsonArticulo(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null; 
            }

            return respuesta;
        }

        public string downloadCiudad(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                CiudadDataMapper dataMapper = new CiudadDataMapper();

                respuesta = dataMapper.GetJsonCiudad(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadCliente(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ClienteDataMapper dataMapper = new ClienteDataMapper();

                respuesta = dataMapper.GetJsonCliente(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadCotizacion(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                CotizacionDataMapper dataMapper = new CotizacionDataMapper();

                respuesta = dataMapper.GetJsonCotizacion(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadDepartamento(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                DepartamentoDataMapper dataMapper = new DepartamentoDataMapper();

                respuesta = dataMapper.GetJsonDepartamento(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadEmpresa(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                EmpresaDataMapper dataMapper = new EmpresaDataMapper();

                respuesta = dataMapper.GetJsonEmpresa(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadEquipo(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                EquipoDataMapper dataMapper = new EquipoDataMapper();

                respuesta = dataMapper.GetJsonEquipo(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadFactura(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                FacturaCompraDataMapper dataMapper = new FacturaCompraDataMapper();

                respuesta = dataMapper.GetJsonFactura(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadFacturaDetalle(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                FacturaCompraDetalleDataMapper dataMapper = new FacturaCompraDetalleDataMapper();

                respuesta = dataMapper.GetJsonFacturaDetalle(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadFacturaVenta(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                FacturaVentaDataMapper dataMapper = new FacturaVentaDataMapper();

                respuesta = dataMapper.GetJsonFacturaVenta(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadItem(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ItemDataMapper dataMapper = new ItemDataMapper();

                respuesta = dataMapper.GetJsonItem(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadItemStatus(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ItemStatusDataMapper dataMapper = new ItemStatusDataMapper();

                respuesta = dataMapper.GetJsonItemStatus(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadLote(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                LoteDataMapper dataMapper = new LoteDataMapper();

                respuesta = dataMapper.GetJsonLote(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadMarca(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                MarcaDataMapper dataMapper = new MarcaDataMapper();

                respuesta = dataMapper.GetJsonMarca(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadMedioEnvio(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                MedioEnvioDataMapper dataMapper = new MedioEnvioDataMapper();

                respuesta = dataMapper.GetJsonMedioEnvio(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadModelo(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ModeloDataMapper dataMapper = new ModeloDataMapper();

                respuesta = dataMapper.GetJsonModelo(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadMoneda(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                MonedaDataMapper dataMapper = new MonedaDataMapper();

                respuesta = dataMapper.GetJsonMoneda(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadMovimiento(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                MovimientoDataMapper dataMapper = new MovimientoDataMapper();

                respuesta = dataMapper.GetJsonMovimiento(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadMovimientoDetalle(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                MovimientoDetalleDataMapper dataMapper = new MovimientoDetalleDataMapper();

                respuesta = dataMapper.GetJsonMovimientoDetalle(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadPais(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                PaisDataMapper dataMapper = new PaisDataMapper();

                respuesta = dataMapper.GetJsonPais(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadPedimento(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                PedimentoDataMapper dataMapper = new PedimentoDataMapper();

                respuesta = dataMapper.GetJsonPedimento(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadPomArticulo(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                PomArticuloDataMapper dataMapper = new PomArticuloDataMapper();

                respuesta = dataMapper.GetJsonPomArticulo(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadPom(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                PomDataMapper dataMapper = new PomDataMapper();

                respuesta = dataMapper.GetJsonPom(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadPropiedad(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                PropiedadDataMapper dataMapper = new PropiedadDataMapper();

                respuesta = dataMapper.GetJsonPropiedad(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadProveedorCuenta(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ProveedorCuentaDataMapper dataMapper = new ProveedorCuentaDataMapper();

                respuesta = dataMapper.GetJsonProveedorCuenta(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadProveedor(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ProveedorDataMapper dataMapper = new ProveedorDataMapper();

                respuesta = dataMapper.GetJsonProveedor(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadProyecto(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ProyectoDataMapper dataMapper = new ProyectoDataMapper();

                respuesta = dataMapper.GetJsonProyecto(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadRecibo(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ReciboDataMapper dataMapper = new ReciboDataMapper();

                respuesta = dataMapper.GetJsonRecibo(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadReciboMovimiento(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ReciboMovimientoDataMapper dataMapper = new ReciboMovimientoDataMapper();

                respuesta = dataMapper.GetJsonReciboMovimiento(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadReciboStatus(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ReciboStatusDataMapper dataMapper = new ReciboStatusDataMapper();

                respuesta = dataMapper.GetJsonReciboStatus(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadServicio(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ServicioDataMapper dataMapper = new ServicioDataMapper();

                respuesta = dataMapper.GetJsonServicio(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta;
        }

        public string downloadTecnico(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                TecnicoDataMapper dataMapper = new TecnicoDataMapper();

                respuesta = dataMapper.GetJsonTecnico(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadTerminoEnvio(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                TerminoEnvioDataMapper dataMapper = new TerminoEnvioDataMapper();

                respuesta = dataMapper.GetJsonTerminoEnvio(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadTipoCotizacion(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                TipoCotizacionDataMapper dataMapper = new TipoCotizacionDataMapper();

                respuesta = dataMapper.GetJsonTipoCotizacion(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadTipoEmpresa(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                TipoEmpresaDataMapper dataMapper = new TipoEmpresaDataMapper();

                respuesta = dataMapper.GetJsonTipoEmpresa(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadTipoMovimiento(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                TipoMovimientoDataMapper dataMapper = new TipoMovimientoDataMapper();

                respuesta = dataMapper.GetJsonTipoMovimiento(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadTipoPedimento(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                TipoPedimentoDataMapper dataMapper = new TipoPedimentoDataMapper();

                respuesta = dataMapper.GetJsonTipoPedimento(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null; 
            }

            return respuesta; 
        }

        public string downloadTransporte(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                TransporteDataMapper dataMapper = new TransporteDataMapper();

                respuesta = dataMapper.GetJsonTransporte(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
                
            }
            return respuesta; 
        }

        public string downloadUltimoMovimiento(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                UltimoMovimientoDataMapper dataMapper = new UltimoMovimientoDataMapper();

                respuesta = dataMapper.GetJsonUltimoMovimiento(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null; 
            }

            return respuesta; 
        }

        public string downloadUnidad(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                UnidadDataMapper dataMapper = new UnidadDataMapper();

                respuesta = dataMapper.GetJsonUnidad(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null; 
            }

            return respuesta; 
        }

        public string downloadBanco(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                BancoDataMapper dataMapper = new BancoDataMapper();

                respuesta = dataMapper.GetJsonBanco(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null; 
            }

            return respuesta;
        }

        public string downloadProveedorCategoria(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ProveedorCategoriaDataMapper dataMapper = new ProveedorCategoriaDataMapper();

                respuesta = dataMapper.GetJsonProveedorCategoria(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null; 
            }

            return respuesta;
        }

        public string downloadAlmacenTecnico(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                AlmacenTecnicoDataMapper dataMapper = new AlmacenTecnicoDataMapper();

                respuesta = dataMapper.GetJsonAlmacenTecnico(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null; 
            }

            return respuesta;
        }

        public string downloadSolicitante(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                SolicitanteDataMapper dataMapper = new SolicitanteDataMapper();

                respuesta = dataMapper.GetJsonSolicitante(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null; 
            }

            return respuesta;
        }
        
        public string downloadInfraestructura(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                InfraestructuraDataMapper dataMapper = new InfraestructuraDataMapper();

                respuesta = dataMapper.GetJsonInfraestructura(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null; 
            }

            return respuesta;
        }

        public string downloadMaxMin(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                MaxMinDataMapper dataMapper = new MaxMinDataMapper();

                respuesta = dataMapper.GetJsonMaxMin(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
            }

            return respuesta;
        }

        public string downloadProgramado(long? lastModifiedDate)
        {
            string respuesta = null;
            if (lastModifiedDate != null)
            {
                ProgramadoDataMapper dataMapper = new ProgramadoDataMapper();

                respuesta = dataMapper.GetJsonProgramado(lastModifiedDate);

                if (String.IsNullOrEmpty(respuesta))
                    respuesta = null;
            }

            return respuesta;
        }

        public string GetVersion()
        {
            string respuesta = null;

            UpDateVersionDataMapper dataMapper = new UpDateVersionDataMapper();

            respuesta = dataMapper.GetJsonVersion();

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public string GetProcessBach()
        {
            string respuesta = null;

            ProcessBachDataMapper dataMapper = new ProcessBachDataMapper();

            respuesta = dataMapper.GetJsonProcessBach();

            if (String.IsNullOrEmpty(respuesta))
                respuesta = null;

            return respuesta;
        }

        public bool GetLogin(string dataUser)
        {
            #region propiedades
            bool respuesta = false;
            USUARIO user;
            AppUsuario dataMapper = new AppUsuario();
            #endregion

            #region metodos
            if (dataUser != null)
            {
                user = dataMapper.GetDeserializePocoUsuario(dataUser);

                if (user!=null)
                    respuesta = dataMapper.GetElementLogin(user);   
            }
            return respuesta;
            #endregion
        }

        public bool GetRecoverPassword(long? dataUser)
        {
            bool respuesta = false;
            if (dataUser != null)
            {
                AppUsuario dataMapper = new AppUsuario();

                respuesta= dataMapper.GetRecoverPassword(dataUser);
            }
            return respuesta;
        }

        public string GetRegisterUser(string dataUser)
        {
            throw new NotImplementedException();
        }

        public bool GetActivationUser(string idActivation)
        {
            bool respuesta = false;
            if (idActivation != null)
            {
                
                AppUsuario dataMapper = new AppUsuario();

                respuesta = dataMapper.GetActivationResponse(long.Parse(idActivation));
            }
            return respuesta;
        }
    }
}
