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
        private RelayCommand _deleteArticuloCommand;
        private CatalogProgramadoViewModel _programadoViewModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        #endregion

        //Exponer la propiedad pogramado articulo y almacen
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

        public ICommand DeleteArticuloCommand
        {
            get
            {
                if (_deleteArticuloCommand == null)
                {
                    _deleteArticuloCommand = new RelayCommand(p => this.AttemptDeleteArticuloCommand(), p => this.CanAttemptDeleteArticuloCommand());
                }
                return _deleteArticuloCommand;
            }
        }
        #endregion

        #region Coleccion en memoria de los articulos

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
        private ObservableCollection<ProgramadoModel> _AddArticulos = new ObservableCollection<ProgramadoModel>();
        public const string ArticulosPropertyName = "AddArticulos";

        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddProgramadoViewModel(CatalogProgramadoViewModel ProgramadoViewModel)
        {
            this._addProgramado = new ProgramadoModel(new ProgramadoDataMapper());
            this._programadoViewModel = ProgramadoViewModel;
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
        }
        public AddProgramadoViewModel()
        { }
        #endregion

        #region Methods

        public AddArticulosProgramado CreateAddArticuloProgramadoViewModel()
        {
            return new AddArticulosProgramado(this);
        }

        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddProgramado()
        {
            bool _canAddProgramado = false;
            if (this.AddArticulos.Count != 0)
            {
                foreach (var item in this.AddArticulos)
                {
                    this._addProgramado.Programado = item.Programado;
                    
                    if (item.Programado >= 0)
                    {
                        _canAddProgramado = true;
                    }
                    else
                    {
                        _canAddProgramado = false;
                        break;
                    }
                }
                if (!_canAddProgramado)
                    this._addProgramado.MensajeError = "Favor de validar que Cantidad a Programar sea mayor o igual a cero.";
                else
                    this._addProgramado.MensajeError = "";

            }

            return _canAddProgramado;
        }

        public void AttempAddProgramado()
        {
            foreach (var item in this.AddArticulos)
            {
                this._addProgramado.Articulo = item.Articulo;
                this._addProgramado.Programado = item.Programado;
                this._addProgramado.saveProgramado();
            }
            //Puede ser que para pruebas unitarias catalogProyectoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._programadoViewModel != null)
            {
                this._programadoViewModel.loadProgramado();
            }
        }

        public void AttemptDeleteArticuloCommand()
        {

            try
            {
                (from o in this.AddArticulos
                 where o.IsChecked == true
                 select o).ToList().ForEach(o => this.AddArticulos.Remove(o));
            }
            catch (Exception)
            {
                ;
            }
        }

        public bool CanAttemptDeleteArticuloCommand()
        {
            bool canDeleteArticulo = false;

            if (this.AddArticulos != null && this.AddArticulos.Count > 0)
            {
                int res = 0;
                try
                {
                    res = (from o in this.AddArticulos
                           where o.IsChecked == true
                           select o).ToList().Count;
                }
                catch (Exception)
                {
                    res = 0;
                }

                if (res > 0)
                {
                    canDeleteArticulo = true;
                }
            }

            return canDeleteArticulo;
        }
    
        #endregion
    }
}
