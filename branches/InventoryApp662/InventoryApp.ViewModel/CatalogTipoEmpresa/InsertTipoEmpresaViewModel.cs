using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogTipoEmpresa
{
    public class InsertTipoEmpresaViewModel : IPageViewModel
    {
        #region Fields
        private InsertTipoEmpresaModel _tipoEmpresa;
        private RelayCommand _addItemCommand;
        private CatalogTipoEmpresaViewModel _catalogTipoEmpresaViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public InsertTipoEmpresaModel TipoEmpresa
        {
            get
            {
                return _tipoEmpresa;
            }
            set
            {
                _tipoEmpresa = value;
            }
        }

        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => this.AttempInsertTipoEmpresa(), p => this.CanAttempInsertTipoEmpresa());
                }
                return _addItemCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public InsertTipoEmpresaViewModel(CatalogTipoEmpresaViewModel catalogtipoempresaviewmodel)
        {
            this._tipoEmpresa = new InsertTipoEmpresaModel(new TipoEmpresaDataMapper(), catalogtipoempresaviewmodel.ActualUser);
            this._catalogTipoEmpresaViewModel = catalogtipoempresaviewmodel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempInsertTipoEmpresa()
        {
            bool _canInsertTipoEmpresa = true;
            if (String.IsNullOrEmpty(this._tipoEmpresa.TipoEmpresaName))
                _canInsertTipoEmpresa = false;

            return _canInsertTipoEmpresa;
        }

        public void AttempInsertTipoEmpresa()
        {
            this._tipoEmpresa.saveTipoEmpresa();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogTipoEmpresaViewModel != null)
            {
                this._catalogTipoEmpresaViewModel.loadItems();
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
