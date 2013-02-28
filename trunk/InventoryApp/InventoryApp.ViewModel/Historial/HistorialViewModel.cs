using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.Model.Historial;
using System.Collections.ObjectModel;
using InventoryApp.DAL.Historial;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.Historial
{
    public class HistorialViewModel: ViewModelBase
    {
        #region Properties

        public ObservableCollection<HistorialModel> HistorialCollection
        {
            get { return _HistorialCollection; }
            set
            {
                if (_HistorialCollection != value)
                {
                    _HistorialCollection = value;
                    OnPropertyChanged(HistorialCollectionPropertyName);
                }
            }
        }
        protected ObservableCollection<HistorialModel> _HistorialCollection;
        public const string HistorialCollectionPropertyName = "HistorialCollection";

        private long unid;

        #endregion

        #region Constructors

        public HistorialViewModel(BancoModel b) {

            unid = b.UnidBanco;
            HistorialCollection = GetHistorial("BANCO");
        }

        public HistorialViewModel(CategoriaModel b)
        {

            unid = b.UnidCategoria;
            HistorialCollection = GetHistorial("CATEGORIA");
        }

        public HistorialViewModel(MarcaModel b)
        {

            unid = b.UnidMarca;
            HistorialCollection = GetHistorial("MARCA");
        }

        public HistorialViewModel(ModeloModel b)
        {

            unid = b.UnidModelo;
            HistorialCollection = GetHistorial("MODELO");
        }

        public HistorialViewModel(EquipoModel b)
        {

            unid = b.UnidEquipo;
            HistorialCollection = GetHistorial("EQUIPO");
        }

        public HistorialViewModel(ArticuloModel b)
        {

            unid = b.UnidArticulo;
            HistorialCollection = GetHistorial("ARTICULO");
        }

        public HistorialViewModel(CiudadModel b)
        {

            unid = b.UnidCiudad;
            HistorialCollection = GetHistorial("CIUDAD");
        }

        public HistorialViewModel(ClienteModel b)
        {

            unid = b.UnidCliente;
            HistorialCollection = GetHistorial("CLIENTE");
        }

        public HistorialViewModel(DepartamentoModel b)
        {

            unid = b.UnidDepartamento;
            HistorialCollection = GetHistorial("DEPARTAMENTO");
        }

        public HistorialViewModel(EmpresaModel b)
        {

            unid = b.UnidEmpresa;
            HistorialCollection = GetHistorial("EMPRESA");
        }

        public HistorialViewModel(InfraestructuraModel b)
        {

            unid = b.UnidInfraestructura;
            HistorialCollection = GetHistorial("INFRAESTRUCTURA");
        }

        public HistorialViewModel(ItemStatusModel b)
        {

            unid = b.UnidItemStatus;
            HistorialCollection = GetHistorial("ITEMSTATUS");
        }
        public HistorialViewModel(MedioEnvioModel b)
        {

            unid = b.UnidMedioEnvio;
            HistorialCollection = GetHistorial("MEDIOENVIO");
        }

        public HistorialViewModel(MonedaModel b)
        {

            unid = b.UnidMoneda;
            HistorialCollection = GetHistorial("MONEDA");
        }

        public HistorialViewModel(PaisModel b)
        {

            unid = b.UnidPais;
            HistorialCollection = GetHistorial("PAIS");
        }

        public HistorialViewModel(ProyectoModel b)
        {

            unid = b.UnidProyecto;
            HistorialCollection = GetHistorial("PROYECTO");
        }

        public HistorialViewModel(ServicioModel b)
        {

            unid = b.UnidServicio;
            HistorialCollection = GetHistorial("SERVICIO");
        }
        public HistorialViewModel(SolicitanteModel b)
        {

            unid = b.UnidSolicitante;
            HistorialCollection = GetHistorial("SOLICITANTE");
        }

        public HistorialViewModel(TerminoEnvioModel b)
        {

            unid = b.UnidTerminoEnvio;
            HistorialCollection = GetHistorial("TERMINOENVIO");
        }

        public HistorialViewModel(TipoCotizacionModel b)
        {

            unid = b.UnidTipoCotizacion;
            HistorialCollection = GetHistorial("TIPOCOTIZACION");
        }

        public HistorialViewModel(InsertTipoEmpresaModel b)
        {

            unid = b.UnidTipoEmpresa;
            HistorialCollection = GetHistorial("TIPOEMPRESA");
        }

        public HistorialViewModel(TipoPedimentoModel b)
        {

            unid = b.UnidTipoPedimento;
            HistorialCollection = GetHistorial("TIPOPEDIMENTO");
        }
        public HistorialViewModel(InsertTransporteModel b)
        {

            unid = b.UnidTransporte;
            HistorialCollection = GetHistorial("TRANSPORTE");
        }

        public HistorialViewModel(UnidadModel b)
        {

            unid = b.UnidUnidad;
            HistorialCollection = GetHistorial("UNIDAD");
        }

        public HistorialViewModel(PropiedadModel b)
        {

            unid = b.UnidPropiedad;
            HistorialCollection = GetHistorial("PROPIEDAD");
        }

        #endregion

        #region Methods

        public ObservableCollection<HistorialModel> GetHistorial(string s) {

            HistorialDataMapper hd = new HistorialDataMapper();
            List<MASTER_INVENTARIOS> l = new List<MASTER_INVENTARIOS>();
            ObservableCollection<HistorialModel> res = new ObservableCollection<HistorialModel>();

            l = hd.GetElementsByUNID(unid, s);

            foreach (MASTER_INVENTARIOS mi in l) {

                HistorialModel aux = new HistorialModel();
                aux.Fecha = mi.FECHA;
                aux.Roles = mi.ROLES;
                aux.Tipo = mi.MODIFICACIONES;
                aux.User = mi.USUARIO2;
                
                res.Add(aux);
             }

            return res;
        }

        #endregion
    }
}
