using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using System.Collections.ObjectModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogProgramado
{
    public class ModifyProgramadoViewModel : ViewModelBase
    {
        #region Fields
        private ProgramadoModel _modiProgramado;
        private RelayCommand _modifyProgramadoCommand;
        private RelayCommand _deleteArticuloCommand;
        private CatalogProgramadoViewModel _catalogProgramadoViewModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        #endregion

        //Exponer la propiedad programado almacen y articulo
        #region Props

        public ProgramadoModel ModiProgramado
        {
            get
            {
                return _modiProgramado;
            }
            set
            {
                _modiProgramado = value;
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

        public ICommand ModifyProgramadoCommand
        {
            get
            {
                if (_modifyProgramadoCommand == null)
                {
                    _modifyProgramadoCommand = new RelayCommand(p => this.AttempModifyProgramado(), p => this.CanAttempModifyProgramado());
                }
                return _modifyProgramadoCommand;
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

        public ObservableCollection<ProgramadoModel> ModiArticulos
        {
            get { return _ModiArticulos; }
            set
            {
                if (_ModiArticulos != value)
                {
                    _ModiArticulos = value;
                    OnPropertyChanged(ArticulosPropertyName);
                }
            }
        }
        private ObservableCollection<ProgramadoModel> _ModiArticulos;
        public const string ArticulosPropertyName = "ModiArticulos";

        public ObservableCollection<ProgramadoModel> DeleteArticulos
        {
            get { return _DeleteArticulos; }
            set
            {
                if (_DeleteArticulos != value)
                {
                    _DeleteArticulos = value;
                    OnPropertyChanged(DeleteArticulosPropertyName);
                }
            }
        }
        private ObservableCollection<ProgramadoModel> _DeleteArticulos;
        public const string DeleteArticulosPropertyName = "DeleteArticulos";

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyProgramadoViewModel(CatalogProgramadoViewModel catalogProgramadoViewModel, ProgramadoModel selectedProgramadoModel)
        {
            this._modiProgramado = new ProgramadoModel(new ProgramadoDataMapper());
            this._catalogProgramadoViewModel = catalogProgramadoViewModel;
            this._modiProgramado.UnidProgramado = selectedProgramadoModel.UnidProgramado;
            this._modiProgramado.Programado = selectedProgramadoModel.Programado;
            this._modiProgramado.Almacen = selectedProgramadoModel.Almacen;
            this._modiProgramado.Articulo = selectedProgramadoModel.Articulo;
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

            this.init();

        }

        public ModifyProgramadoViewModel() { }
        #endregion

        #region Methods
        public void init()
        {
            this._ModiArticulos = new ObservableCollection<ProgramadoModel>();
            this._DeleteArticulos = new ObservableCollection<ProgramadoModel>();
            this._ModiArticulos = GetArticulos(this._modiProgramado);
        }

        private ObservableCollection<ProgramadoModel> GetArticulos(ProgramadoModel programadoArticulos)
        {
            ObservableCollection<ProgramadoModel> articuloModels = new ObservableCollection<ProgramadoModel>();
            try
            {
                ProgramadoDataMapper artDataMapper = new ProgramadoDataMapper();
                List<PROGRAMADO> articulos = (List<PROGRAMADO>)artDataMapper.getElementArticulos(new PROGRAMADO() { UNID_ALMACEN = programadoArticulos.Almacen.UNID_ALMACEN });

                articulos.ForEach(o => articuloModels.Add(new ProgramadoModel()
                {
                    Articulo = o.ARTICULO,

                    EquipoModel = new EquipoModel(new EquipoDataMapper())
                    {
                        UnidEquipo = o.ARTICULO.EQUIPO.UNID_EQUIPO
                        ,
                        EquipoName = o.ARTICULO.EQUIPO.EQUIPO_NAME
                    }
                    ,
                    Categoria = new CATEGORIA()
                    {
                        CATEGORIA_NAME = o.ARTICULO.CATEGORIA.CATEGORIA_NAME
                        ,
                        UNID_CATEGORIA = o.ARTICULO.CATEGORIA.UNID_CATEGORIA
                    }
                    ,
                    Marca = new MARCA()
                    {
                        UNID_MARCA = o.ARTICULO.MARCA.UNID_MARCA
                        ,
                        MARCA_NAME = o.ARTICULO.MARCA.MARCA_NAME
                    },
                    Modelo = new MODELO()
                    {
                        UNID_MODELO = o.ARTICULO.MODELO.UNID_MODELO
                        ,
                        MODELO_NAME = o.ARTICULO.MODELO.MODELO_NAME
                    },
                    Programado=o.PROGRAMADO1,
                    UnidProgramado = o.UNID_PROGRAMADO
                }));
            }
            catch (Exception)
            {
                ;
            }

            return articuloModels;
        }

        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyProgramado()
        {
            bool _canAddProgramado = false;
            if (this.ModiArticulos.Count != 0)
            {
                foreach (var item in this.ModiArticulos)
                {
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
            }
            return _canAddProgramado;
        }

        public void AttempModifyProgramado()
        {
            //valida si hay un elemento a eliminar
            if (this.DeleteArticulos.Count!=0)
            {
                foreach (var item in this.DeleteArticulos)
                {
                    if (item.UnidProgramado!=0)
                    {
                        //eliminacion logiga
                        this._modiProgramado.DeleteProgramado();    
                    }
                }
            }
            //actualiza si hubo cambios
            foreach (var item in this.ModiArticulos)
            {
                //si el articulo existe actualiza si no inserta
                this._modiProgramado.UnidProgramado = item.UnidProgramado;
                this._modiProgramado.Articulo = item.Articulo;
                this._modiProgramado.Programado = item.Programado;
                
                if (item.UnidProgramado!=0)
                {
                    this._modiProgramado.updateProgramado();    
                }
                else
                {
                    this._modiProgramado.saveProgramado();
                }
                
            }
          
            //Puede ser que para pruebas unitarias catalogProyectoViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogProgramadoViewModel != null)
            {
                this._catalogProgramadoViewModel.loadProgramado();
            }
        }

        public void AttemptDeleteArticuloCommand()
        {
            try
            {
                //agrega a un aux los elementos que van a ser eliminados
                var query= (from o in this.ModiArticulos
                where o.IsChecked == true
                select o).ToList();
                //valida que no venga vacia la el aux
                if (query.Count!=0)
                {
                    foreach (var item in query)
                    {
                        this.DeleteArticulos.Add(item);
                    }    
                }
                
                (from o in this.ModiArticulos
                 where o.IsChecked == true
                 select o).ToList().ForEach(o => this.ModiArticulos.Remove(o));
            }
            catch (Exception)
            {
                ;
            }
        }

        public bool CanAttemptDeleteArticuloCommand()
        {
            bool canDeleteArticulo = false;

            if (this.ModiArticulos != null && this.ModiArticulos.Count > 0)
            {
                int res = 0;
                try
                {
                    res = (from o in this.ModiArticulos
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
