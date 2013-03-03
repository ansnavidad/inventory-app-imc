using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogProgramado
{
    public class CatalogProgramadoViewModel : IPageViewModel
    {
        private RelayCommand _deleteMaxMinCommand;
        private CatalogProgramadoModel _catalogProgramadoModel;
        private ProgramadoModel _addGridArticulos = new ProgramadoModel();
        public USUARIO ActualUser;

        public ICommand DeleteProgramadoCommand
        {
            get
            {
                if (_deleteMaxMinCommand == null)
                {
                    _deleteMaxMinCommand = new RelayCommand(p => this.AttempDeleteProgramado(), p => this.CanAttempDeleteProgramado());
                }
                return _deleteMaxMinCommand;
            }
        }

        public CatalogProgramadoViewModel(USUARIO u)
        {
            try
            {
                IDataMapper dataMapper = new ProgramadoDataMapper();
                this._catalogProgramadoModel = new CatalogProgramadoModel(dataMapper);
                this.ActualUser = u;
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }

        public CatalogProgramadoViewModel()
        {
            try
            {
                IDataMapper dataMapper = new ProgramadoDataMapper();
                this._catalogProgramadoModel = new CatalogProgramadoModel(dataMapper);
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatalogProgramadoModel CatalogProgramadoModel
        {
            get
            {
                return _catalogProgramadoModel;
            }
            set
            {
                _catalogProgramadoModel = value;
            }
        }

        public void loadProgramado()
        {
            this._catalogProgramadoModel.loadProgramado();
        }

        public AddProgramadoViewModel CreateAddProgramadoViewModel()
        {
            return new AddProgramadoViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyMaxMinViewModel y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyProgramadoViewModel CreateModifyProgramadoViewModel()
        {
            ProgramadoModel programadoModel = new ProgramadoModel(new ProgramadoDataMapper(), this.ActualUser);
            if (this._catalogProgramadoModel != null && this._catalogProgramadoModel.SelectedProgramado != null)
            {
                programadoModel.UnidProgramado = this._catalogProgramadoModel.SelectedProgramado.UNID_PROGRAMADO;
                programadoModel.Programado = this._catalogProgramadoModel.SelectedProgramado.PROGRAMADO1;
                programadoModel.Articulo = this._catalogProgramadoModel.SelectedProgramado.Articulo;
                programadoModel.Almacen = this._catalogProgramadoModel.SelectedProgramado.Almacen;
            }
            return new ModifyProgramadoViewModel(this, programadoModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteProgramado()
        {
            bool _canDeleteProgramado = false;
            foreach (DeleteProgramado d in this._catalogProgramadoModel.Programado)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteProgramado = true;
                }
            }

            return _canDeleteProgramado;
        }

        public void AttempDeleteProgramado()
        {
            this._catalogProgramadoModel.deleteProgramado();

            if (this._catalogProgramadoModel != null)
            {
                this._catalogProgramadoModel.loadProgramado();
            }
        }
        #endregion

        public string PageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
