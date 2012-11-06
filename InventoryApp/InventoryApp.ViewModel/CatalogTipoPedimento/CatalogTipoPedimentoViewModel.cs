using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;


namespace InventoryApp.ViewModel.CatalogTipoPedimento
{
    public class CatalogTipoPedimentoViewModel
    {
        private CatalogTipoPedimentoModel _tipoPedimentoModel;

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
    }
}
