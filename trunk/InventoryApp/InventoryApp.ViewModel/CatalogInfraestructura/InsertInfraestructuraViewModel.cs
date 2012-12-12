using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.Model;
using System.Windows.Input;

namespace InventoryApp.ViewModel.CatalogInfraestructura
{
    public class InsertInfraestructuraViewModel : IPageViewModel
    {
        #region Fields
        private InfraestructuraModel _infraestructura;
        private RelayCommand _addItemCommand;
        private CatalogInfraestructuraViewModel _catalogInfraestructuraViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public InfraestructuraModel Infraestructura
        {
            get
            {
                return _infraestructura;
            }
            set
            {
                _infraestructura = value;
            }
        }

        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => this.AttempInsertInfraestructura(), p => this.CanAttempInsertInfraestructura());
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
        public InsertInfraestructuraViewModel(CatalogInfraestructuraViewModel cataloginfraestructuraviewmodel)
        {
            this._infraestructura = new InfraestructuraModel(new InfraestructuraDataMapper());
            this._catalogInfraestructuraViewModel = cataloginfraestructuraviewmodel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempInsertInfraestructura()
        {
            bool _canInsertInfraestructura = true;
            if (String.IsNullOrEmpty(this._infraestructura.InfraestructuraName))
                _canInsertInfraestructura = false;

            return _canInsertInfraestructura;
        }

        public void AttempInsertInfraestructura()
        {
            this._infraestructura.saveInfraestructura();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogInfraestructuraViewModel != null)
            {
                this._catalogInfraestructuraViewModel.loadItems();
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
