using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogTecnico
{
    public class CatalogTecnicoViewModel : IPageViewModel
    {
        private RelayCommand _deleteTecnicoCommand;
        private CatalogTecnicoModel _catalogTecnicoModel;

        public CatalogTecnicoViewModel()
        {
            try
            {
                IDataMapper dataMapper = new TecnicoDataMapper();
                this._catalogTecnicoModel = new CatalogTecnicoModel(dataMapper);
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

        public ICommand DeleteTecnicoCommand
        {
            get
            {
                if (_deleteTecnicoCommand == null)
                {
                    _deleteTecnicoCommand = new RelayCommand(p => this.AttempDeleteTecnico(), p => this.CanAttempDeleteTecnico());
                }
                return _deleteTecnicoCommand;
            }
        }

        public CatalogTecnicoModel CatalogTecnicoModel
        {
            get
            {
                return _catalogTecnicoModel;
            }
            set
            {
                _catalogTecnicoModel = value;
            }
        }


        /// <summary>
        /// Crea una nueva instancia de AddTipoMovimiento y se pasa asi mismo como parámetro
        /// </summary>
        /// <returns></returns>
        public AddTecnicoViewModel CreateAddTecnicoViewModel()
        {
            return new AddTecnicoViewModel(this);
        }
        /// <summary>
        /// Crea una nueva instancia de ModifyTipoMovimiento y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyTecnicoViewModel CreateModifyTecnicoViewModel()
        {
            TecnicoModel tecnicoModel = new TecnicoModel(new TecnicoDataMapper());
            if (this._catalogTecnicoModel != null && this._catalogTecnicoModel.SelectedTecnico != null)
            {
                tecnicoModel.TecnicoName = this._catalogTecnicoModel.SelectedTecnico.TECNICO_NAME;
                tecnicoModel.UnidTecnico = this._catalogTecnicoModel.SelectedTecnico.UNID_TECNICO;
                tecnicoModel.Mail = this._catalogTecnicoModel.SelectedTecnico.MAIL;
                tecnicoModel.Ciudad = this._catalogTecnicoModel.SelectedTecnico.CIUDAD;
            }
            return new ModifyTecnicoViewModel(this, tecnicoModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteTecnico()
        {
            bool _canDeleteTecnico = false;
            foreach (DeleteTecnico d in this._catalogTecnicoModel.Tecnico)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteTecnico = true;
                }
            }

            return _canDeleteTecnico;
        }

        public void AttempDeleteTecnico()
        {
            this._catalogTecnicoModel.deleteTecnico();

            if (this._catalogTecnicoModel != null)
            {
                this._catalogTecnicoModel.loadItems();
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
