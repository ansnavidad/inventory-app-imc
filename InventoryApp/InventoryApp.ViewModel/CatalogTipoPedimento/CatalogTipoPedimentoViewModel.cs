using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;


namespace InventoryApp.ViewModel.CatalogTipoPedimento
{
    public class CatalogTipoPedimentoViewModel
    {
        private RelayCommand _deleteTipoPedimentoCommand;
        private CatalogTipoPedimentoModel _tipoPedimentoModel;

        public ICommand DeleteTipoPedimentoCommand
        {
            get
            {
                if (_deleteTipoPedimentoCommand == null)
                {
                    _deleteTipoPedimentoCommand = new RelayCommand(p => this.AttempDeleteTipoPedimento(), p => this.CanAttempDeleteTipoPedimento());
                }
                return _deleteTipoPedimentoCommand;
            }
        }

        public CatalogTipoPedimentoViewModel()
        {
            
            try
            {
                IDataMapper dataMapper = new TipoPedimentoDataMapper();
                this._tipoPedimentoModel = new CatalogTipoPedimentoModel(dataMapper);   
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
        public CatalogTipoPedimentoModel TipoPedimentoModel
        {
            get
            {
                return _tipoPedimentoModel;
            }
            set
            {
                _tipoPedimentoModel = value;
            }
        }

        public void loadItems()
        {
            this._tipoPedimentoModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddTipoPedimentoViewModel CreateAddTipoPedimentoViewModel()
        {
            return new AddTipoPedimentoViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyTipoPedimentoViewModel CreateModifyTipoPedimentoViewModel()
        {
            TipoPedimentoModel tipoPedimentoModel = new TipoPedimentoModel(new TipoPedimentoDataMapper());
            if (this._tipoPedimentoModel != null && this._tipoPedimentoModel.SelectedTipoPedimento != null)
            {
                tipoPedimentoModel.UnidTipoPedimento = this._tipoPedimentoModel.SelectedTipoPedimento.UNID_TIPO_PEDIMENTO;
                tipoPedimentoModel.TipoPedimentoName = this._tipoPedimentoModel.SelectedTipoPedimento.TIPO_PEDIMENTO_NAME;
                tipoPedimentoModel.Regimen = this._tipoPedimentoModel.SelectedTipoPedimento.REGIMEN;
                tipoPedimentoModel.Nota = this._tipoPedimentoModel.SelectedTipoPedimento.NOTA;
                tipoPedimentoModel.Clave = this._tipoPedimentoModel.SelectedTipoPedimento.NOTA;
            }
            return new ModifyTipoPedimentoViewModel(this, tipoPedimentoModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteTipoPedimento()
        {
            bool _canDeleteTipoPedimento = false;
            foreach (DeleteTipoPedimento d in this._tipoPedimentoModel.TipoPedimento)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteTipoPedimento = true;
                }
            }

            return _canDeleteTipoPedimento;
        }

        public void AttempDeleteTipoPedimento()
        {
            this._tipoPedimentoModel.deleteTipoPedimento();

            if (this._tipoPedimentoModel != null)
            {
                this._tipoPedimentoModel.loadItems();
            }
        }
        #endregion
    }
}
