using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.ViewModel.CatalogArticulo;
using System.Windows.Input;
using InventoryApp.DAL;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogProgramado
{
    public class AddProgramadoViewModel:ViewModelBase
    {
        #region Fields
        private ProgramadoModel _addProgramado;
        private RelayCommand _addProgramadoCommand;
        private CatalogProgramadoViewModel _programadoViewModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        #endregion

        //Exponer la propiedad maxMin articulo y almacen
        #region Props
        public ProgramadoModel AddProgramado
        {
            get
            {
                return _addProgramado;
            }
            set
            {
                _addProgramado = value;
            }
        }

        public CatalogAlmacenModel CatalogAlmacenModel
        {
            get
            {
                return _catalogAlmacenModel;
            }
            set
            {
                _catalogAlmacenModel = value;
            }
        }

        

        public ICommand AddProgramadoCommand
        {
            get
            {
                if (_addProgramadoCommand == null)
                {
                    _addProgramadoCommand = new RelayCommand(p => this.AttempAddProgramado(), p => this.CanAttempAddProgramado());
                }
                return _addProgramadoCommand;
            }
        }
        #endregion


        public AddArticulosProgramado CreateAddArticuloProgramadoViewModel()
        {
            return new AddArticulosProgramado(this);
        }

        public ObservableCollection<ProgramadoModel> AddArticulos
        {
            get { return _AddArticulos; }
            set
            {
                if (_AddArticulos != value)
                {
                    _AddArticulos = value;
                    OnPropertyChanged(ArticulosPropertyName);
                }
            }
        }
        private ObservableCollection<ProgramadoModel> _AddArticulos;
        public const string ArticulosPropertyName = "AddArticulos";

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddProgramadoViewModel(CatalogProgramadoViewModel ProgramadoViewModel)
        {
            this._addProgramado = new ProgramadoModel(new ProgramadoDataMapper());
            this._programadoViewModel = ProgramadoViewModel;
            this._AddArticulos = new ObservableCollection<ProgramadoModel>();
            ProgramadoModel prog = new ProgramadoModel();
            ARTICULO art = new ARTICULO();
            art.ARTICULO1 = "Prueba";
            prog.Articulo = art;
            this.AddArticulos.Add(prog);

            try
            {

                this._catalogAlmacenModel = new CatalogAlmacenModel(new AlmacenDataMapper());
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //try
            //{

            //    this._catalogArticuloModel = new CatalogArticuloModel(new ArticuloDataMapper());
            //}
            //catch (ArgumentException ae)
            //{
            //    ;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
        public AddProgramadoViewModel()
        { }
        #endregion

        #region Methods

        //public AddArticulosMaxMin CreateAddArticuloMaxMinViewModel()
        //{
        //    return new AddArticulosMaxMin();
        //}

        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddProgramado()
        {
            bool _canAddMaxMin = true;
            //if (this.CatalogArticuloModel.Articulos.Count != 0)
                _canAddMaxMin = false;

            return _canAddMaxMin;
        }

        public void AttempAddProgramado()
        {
            this._addProgramado.saveProgramado();

            //Puede ser que para pruebas unitarias catalogProyectoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._programadoViewModel != null)
            {
                this._programadoViewModel.loadProgramado();
            }
        }

        
        #endregion
    }
}
