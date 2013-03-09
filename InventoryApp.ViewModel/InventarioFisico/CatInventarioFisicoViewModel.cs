using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.Model.InventarioFisico;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.InventarioFisico
{
    public class CatInventarioFisicoViewModel:ViewModelBase,IPageViewModel
    {
        public InventariosFisicosModel Inventarios
        {
            get { return _Inventarios; }
            set
            {
                if (_Inventarios != value)
                {
                    _Inventarios = value;
                    OnPropertyChanged(InventariosPropertyName);
                }
            }
        }
        private InventariosFisicosModel _Inventarios;
        public const string InventariosPropertyName = "Inventarios";

        public InventarioFisicoModel SelectedInventarioFisico
        {
            get { return _SelectedInventarioFisico; }
            set
            {
                if (_SelectedInventarioFisico != value)
                {
                    _SelectedInventarioFisico = value;
                    OnPropertyChanged(SelectedInventarioFisicoPropertyName);
                }
            }
        }
        private InventarioFisicoModel _SelectedInventarioFisico;
        public const string SelectedInventarioFisicoPropertyName = "SelectedInventarioFisico";

        #region Contructor Logic
        public CatInventarioFisicoViewModel()
        {
            //Pasar datamapper a inventarios y cargar el objeto con datos
            this._Inventarios = new InventariosFisicosModel(new InventarioFisicoDataMapper());
        }

        public CatInventarioFisicoViewModel(bool noData)
        {
            this._Inventarios = new InventariosFisicosModel(null);
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
