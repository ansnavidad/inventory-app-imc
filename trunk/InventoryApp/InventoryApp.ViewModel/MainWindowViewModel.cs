using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.Model;
using InventoryApp.ViewModel.CatalogArticulo;
using System.ComponentModel;
using InventoryApp.ViewModel.CatalogMarca;
using InventoryApp.ViewModel.Recibo;
using InventoryApp.ViewModel.CatalogBanco;
using InventoryApp.ViewModel.CatalogCategoria;
using InventoryApp.ViewModel.CatalogCiudad;
using InventoryApp.ViewModel.CatalogDepartamento;
using InventoryApp.ViewModel.CatalogEmpresa;
using InventoryApp.ViewModel.CatalogEquipo;
using InventoryApp.ViewModel.CatalogItemStatus;
using InventoryApp.ViewModel.CatalogMedioEnvio;
using InventoryApp.ViewModel.CatalogModelo;
using InventoryApp.ViewModel.CatalogMoneda;
using InventoryApp.ViewModel.CatalogPais;
using InventoryApp.ViewModel.CatalogProveedor;
using InventoryApp.ViewModel.CatalogProveedorCuenta;
using InventoryApp.ViewModel.CatalogProyecto;
using InventoryApp.ViewModel.CatalogSolicitante;
using InventoryApp.ViewModel.CatalogTerminoEnvio;
using InventoryApp.ViewModel.CatalogTipoCotizacion;
using InventoryApp.ViewModel.CatalogTipoEmpresa;
using InventoryApp.ViewModel.CatalogTipoMovimiento;
using InventoryApp.ViewModel.CatalogTipoPedimento;
using InventoryApp.ViewModel.CatalogTransporte;
using InventoryApp.ViewModel.CatalogCliente;
using InventoryApp.ViewModel.CatalogPropiedad;
using InventoryApp.ViewModel.CatalogAlmacen;
using InventoryApp.ViewModel.CatalogServicio;
using InventoryApp.ViewModel.CatalogUnidad;
using InventoryApp.ViewModel.CatalogTecnico;
using InventoryApp.ViewModel.GridMovimientos;
using System.Windows;

namespace InventoryApp.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Properties
        public MenuModel SelectedMenu
        {
            get { return _SelectedMenu; }
            set
            {
                if (_SelectedMenu != value)
                {
                    _SelectedMenu = value;
                    OnPropertyChanged(SelectedMenuPropertyName);
                }
            }
        }
        private MenuModel _SelectedMenu;
        public const string SelectedMenuPropertyName = "SelectedMenu";

        public MenuViewModel MenuViewModel
        {
            get { return _MenuViewModel; }
            set
            {
                if (_MenuViewModel != value)
                {
                    _MenuViewModel = value;
                    OnPropertyChanged(MenuViewModelPropertyName);
                }
            }
        }
        private MenuViewModel _MenuViewModel;
        public const string MenuViewModelPropertyName = "MenuViewModel";

        /// <summary>
        /// Pila de menus hoja que han sido visitados. Sirve para en un futuro implementar el "back", regresar a la vista anterior
        /// </summary>
        public ObservableCollection<MenuModel> VisitedMenuStack
        {
            get { return _VisitedMenuStack; }
            set
            {
                if (_VisitedMenuStack != value)
                {
                    _VisitedMenuStack = value;
                    OnPropertyChanged(VisitedMenuStackPropertyName);
                }
            }
        }
        private ObservableCollection<MenuModel> _VisitedMenuStack;
        public const string VisitedMenuStackPropertyName = "VisitedMenuStack";

        /// <summary>
        /// La página (control de usuario) que actualmente está visible
        /// </summary>
        public IPageViewModel CurrentPageViewModel
        {
            get { return _CurrentPageViewModel; }
            set
            {
                if (_CurrentPageViewModel != value)
                {
                    _CurrentPageViewModel = value;
                    OnPropertyChanged(CurrentPageViewModelPropertyName);
                }
            }
        }
        private IPageViewModel _CurrentPageViewModel;
        public const string CurrentPageViewModelPropertyName = "CurrentPageViewModel";
        #endregion

        public MainWindowViewModel()
        {
            this._MenuViewModel = new MenuViewModel(this.ChagePage);
            this._CurrentPageViewModel = new GridMovimientos.MovimientosGridViewModel();
        }

        #region Methods
        public void ChagePage(object sender,EventArgs e)
        {
            if (sender!=null)
            {
                this._SelectedMenu = (MenuModel)sender;
                this.CurrentPageViewModel = this.PageFactory(this._MenuViewModel.SelectedMenu);
            }
        }

        /// <summary>
        /// Crea el page correspondiente de acuerdo al menú seleccionado
        /// </summary>
        /// <param name="selectedMenu">Menú seleccionado</param>
        /// <returns>Page creada, si no se crea ninguna, regresa el currentpage</returns>
        private IPageViewModel PageFactory(MenuModel selectedMenu)
        {
            IPageViewModel page = this._CurrentPageViewModel;
            if (this._SelectedMenu != null && this._SelectedMenu.IsLeaf)
            {
                switch (this._SelectedMenu.MenuName)
                {                                 
                    case "Almacen":
                        page = new CatalogAlmacenViewModel();
                        break;
                    case "Artículos":
                        page = new CatalogArticuloViewModel();
                        break;
                    case "Técnico":
                        page = new CatalogTecnicoViewModel();
                        break;
                    case "Marca":
                        page = new CatalogMarcaViewModel();
                        break;
                    case "Banco":
                        page = new CatalogBancoViewModel();
                        break;
                    case "Categoría":
                        page = new CatalogCategoriaViewModel();
                        break;
                    case "Ciudad":
                        page = new CatalogCiudadViewModel();
                        break;
                    case "Cliente":
                        page = new CatalogClienteViewModel();
                        break;
                    case "Departamento":
                        page = new CatalogDepartamentoViewModel();
                        break;
                    case "Empresa":
                        page = new CatalogEmpresaViewModel();
                        break;
                    case "Equipo":
                        page = new CatalogEquipoViewModel();
                        break;
                    case "Item Status":
                        page = new CatalogItemStatusViewModel();
                        break;
                    case "Medio Envio":
                        page = new CatalogMedioEnvioViewModel();
                        break;
                    case "Modelo":
                        page = new CatalogModeloViewModel();
                        break;
                    case "Moneda":
                        page = new CatalogMonedaViewModel();
                        break;
                    case "País":
                        page = new CatalogPaisViewModel();
                        break;
                    case "Propiedad":
                        page = new CatalogPropiedadViewModel();
                        break;
                    case "Proveedor":
                        page = new CatalogProveedorViewModel();
                        break;
                    case "Proveedor Cuenta":
                        page = new CatalogProveedorCuentaViewModel();
                        break;
                    case "Proyecto":
                        page = new CatalogProyectoViewModel();
                        break;
                    case "Servicio":
                        page = new CatalogServicioViewModel();
                        break;
                    case "Solicitante":
                        page = new CatalogSolicitanteViewModel();
                        break;
                    case "Termino Envio":
                        page = new CatalogTerminoEnvioViewModel();
                        break;
                    case "Tipo Cotización":
                        page = new CatalogTipoCotizacionViewModel();
                        break;
                    case "Tipo Empresa":
                        page = new CatalogTipoEmpresaViewModel();
                        break;
                    case "Tipo Movimiento":
                        page = new CatalogTipoMovimientoViewModel();
                        break;
                    case "Tipo Pedimento":
                        page = new CatalogTipoPedimentoViewModel();
                        break;
                    case "Transporte":
                        page = new CatalogTransporteViewModel();
                        break;
                    case "Unidad":
                        page = new CatalogUnidadViewModel();
                        break;
                    //Entradas
                    case "Entrada por Validación":
                        page = new GridMovimientos.MovimientoGridEntradasViewModel();  
                        break;
                    case "Entrada por Prestamo":
                        page = new GridMovimientos.MovimientoGridEntradasPrestamoViewModel(); 
                        break;
                    case "Entrada por Devolución":
                        page = new GridMovimientos.MovimientoGridEntradasDevolucionViewModel(); 
                        break;
                    case "Entrada por Desinstalación":
                        page = new GridMovimientos.MovimientoGridEntradasDesinstalacionViewModel(); 
                        break;
                    //Salidad
                    case "Salida Renta":
                        //page = new Salidas.SalidaRentaViewModel();
                        page = new GridMovimientos.MovimientoGridSalidaRentaViewModel();
                        break;
                    case "Salida Demo":
                        page = new GridMovimientos.MovimientoGridSalidaDemoViewModel();
                        break;
                    case "Salida Prestamo":
                        page = new GridMovimientos.MovimientoGridSalidaPrestamoViewModel();
                        break;
                    case "Salida Venta":
                        page = new GridMovimientos.MovimientoGridSalidaVentaViewModel();
                        break;
                    case "Salida RMA":
                        page = new GridMovimientos.MovimientoGridSalidaRMAViewModel();
                        break;
                    case "Salida Revisión":
                        page = new GridMovimientos.MovimientoGridSalidaRevisionViewModel();
                        break;
                    case "Salida Pruebas":
                        page = new GridMovimientos.MovimientoGridSalidaPruebasViewModel();
                        break;
                    case "Salida Configuración":
                        page = new GridMovimientos.MovimientoGridSalidaConfiguracionViewModel();
                        break;
                    case "Salida Obsequio":
                        page = new GridMovimientos.MovimientoGridSalidaObsequioViewModel();
                        break;
                    case "Salida Correctivo":
                        page = new GridMovimientos.MovimientoGridSalidaCorrectivoViewModel();
                        break;
                    case "Entregado (Licencia Office)":
                        page = new GridMovimientos.MovimientoGridSalidaOfficeViewModel();
                        break;
                    //Traspasos
                    case "Traspaso Entre Almacenes":
                        page = new GridMovimientos.MovimientoGridTraspasoStockViewModel();
                        break;
                    //Juan
                   case "Nuevo Recibo":
                        page = new Recibo.CatalogReciboViewModel();
                      break;
                   case "Inicio":
                      page = new GridMovimientos.MovimientosGridViewModel();
                      break;
                    default:
                        break;
                }
            }
            return page;
        }

        public IPageViewModel GetCurrentPage()
        {
            return this._CurrentPageViewModel;
        }
        #endregion


    }
}
