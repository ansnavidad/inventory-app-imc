﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogTecnico
{
    public class ModifyTecnicoViewModel
    {
        #region Fields
        private TecnicoModel _modiTecnico;
        private RelayCommand _modifyTecnicoCommand;
        private CatalogTecnicoViewModel _catalogTecnicoViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public TecnicoModel ModiTecnico
        {
            get
            {
                return _modiTecnico;
            }
            set
            {
                _modiTecnico = value;
            }
        }

        public ICommand ModifyTecnicoCommand
        {
            get 
            {
                if (_modifyTecnicoCommand == null)
                {
                    _modifyTecnicoCommand = new RelayCommand(p => this.AttempModifyTecnico(), p => this.CanAttempModifyTecnico());
                }
                return _modifyTecnicoCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyTecnicoViewModel(CatalogTecnicoViewModel catalogTecnicoViewModel, TecnicoModel selectedTecnicoModel)
        {
            this._modiTecnico = new TecnicoModel(new TecnicoDataMapper());
            this._catalogTecnicoViewModel = catalogTecnicoViewModel;
            this._modiTecnico.Ciudad = selectedTecnicoModel.Ciudad;
            this._modiTecnico.Mail = selectedTecnicoModel.Mail;
            this._modiTecnico.TecnicoName = selectedTecnicoModel.TecnicoName;
            this._modiTecnico.UnidTecnico = selectedTecnicoModel.UnidTecnico;            
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyTecnico()
        {
            bool _canAddTecnico = true;
            if (String.IsNullOrEmpty(this._modiTecnico.TecnicoName) || String.IsNullOrEmpty(this._modiTecnico.Mail))
                _canAddTecnico = false;

            return _canAddTecnico;
        }

        public void AttempModifyTecnico()
        {
            this._modiTecnico.updateTipoPedimento();
        }
        #endregion
    }
}
