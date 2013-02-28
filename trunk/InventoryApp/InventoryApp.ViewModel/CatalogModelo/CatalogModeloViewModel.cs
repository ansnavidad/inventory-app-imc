using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;


namespace InventoryApp.ViewModel.CatalogModelo
{
    public class CatalogModeloViewModel : IPageViewModel
    {
        private RelayCommand _deleteModeloCommand;

        private CatalogModeloModel _catalogModeloModel;
        public USUARIO ActualUser;

        public ICommand DeleteModeloCommand
        {
            get
            {
                if (_deleteModeloCommand == null)
                {
                    _deleteModeloCommand = new RelayCommand(p => this.AttempDeleteModelo(), p => this.CanAttempDeleteModelo());
                }
                return _deleteModeloCommand;
            }
        }

        public CatalogModeloViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new ModeloDataMapper();
                this._catalogModeloModel = new CatalogModeloModel(dataMapper);   
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch(Exception ex)
            {
                throw ex;
            }   
            
        }

        public CatalogModeloModel CatalogModeloModel
        {
            get
            {
                return _catalogModeloModel;
            }
            set
            {
                _catalogModeloModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogModeloModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddModeloViewModel CreateAddModeloViewModel()
        {
            return new AddModeloViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyModeloViewModel CreateModifyModeloViewModel()
        {
            ModeloModel modeloModel=new ModeloModel(new ModeloDataMapper());
            if (this._catalogModeloModel != null && this._catalogModeloModel.SelectedModelo != null)
            {
                modeloModel.ModeloName = this._catalogModeloModel.SelectedModelo.MODELO_NAME;
                modeloModel.UnidModelo = this._catalogModeloModel.SelectedModelo.UNID_MODELO;
            }
            return new ModifyModeloViewModel(this, modeloModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteModelo()
        {
            bool _canDeleteModelo = false;
            foreach (DeleteModelo d in this._catalogModeloModel.Modelos)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteModelo = true;
                }
            }

            return _canDeleteModelo;
        }

        public void AttempDeleteModelo()
        {
            this._catalogModeloModel.deleteModelo();

            if (this._catalogModeloModel != null)
            {
                this._catalogModeloModel.loadItems();
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
