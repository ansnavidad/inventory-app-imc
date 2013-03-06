﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.Model.Historial;
using System.Collections.ObjectModel;
using InventoryApp.DAL.Historial;
using InventoryApp.DAL.POCOS;
using InventoryApp.Model.Seguridad;
using InventoryApp.Model.CatalogInventario;
using InventoryApp.DAL.CatalogInventario;

namespace InventoryApp.ViewModel.CatalogInventario
{
    public class CatalogInvViewModel : ViewModelBase, IPageViewModel
    {
        #region Relay Command



        #endregion

        #region Properties

        public USUARIO ActualUser;

        public ObservableCollection<InventarioModel> InventarioCollection
        {
            get { return _InventarioCollection; }
            set
            {
                if (_InventarioCollection != value)
                {
                    _InventarioCollection = value;
                    OnPropertyChanged(InventarioCollectionPropertyName);
                }
            }
        }
        protected ObservableCollection<InventarioModel> _InventarioCollection;
        public const string InventarioCollectionPropertyName = "InventarioCollection";

        public InventarioModel SelectedInventario
        {
            get { return _SelectedInventario; }
            set
            {
                if (_SelectedInventario != value)
                {
                    _SelectedInventario = value;
                    OnPropertyChanged(SelectedInventarioPropertyName);
                }
            }
        }
        protected InventarioModel _SelectedInventario;
        public const string SelectedInventarioPropertyName = "SelectedInventario";
        
        #endregion

        #region Constructors

        public CatalogInvViewModel(USUARIO u) {

            this.ActualUser = u;
            this.InventarioCollection = GetInventarios();
        }

        #endregion

        #region Methods

        public AddInventarioViewModel CreateAddMovimientoViewModel()
        {
            AddInventarioViewModel addFacturaViewModel = new AddInventarioViewModel(this);
            return addFacturaViewModel;
        }

        public ObservableCollection<InventarioModel> GetInventarios()
        {
            InventarioDataMapper iDM = new InventarioDataMapper();
            ObservableCollection<InventarioModel> res = new ObservableCollection<InventarioModel>();
            List<INVENTARIO> aux = new List<INVENTARIO>();
            aux = (List<INVENTARIO>)iDM.getElements();

            long segmentoAux = 0;
            InventarioModel fin = new InventarioModel();
            fin.Descriptores = new ObservableCollection<InventarioModel.Descriptor>();

            if(aux != null && aux.Count > 0)
                segmentoAux = aux[0].UNID_SEGMENTO;
            
            foreach (INVENTARIO ii in aux) {

                if (ii.UNID_SEGMENTO != segmentoAux)
                {
                    res.Add(fin);
                    segmentoAux = ii.UNID_SEGMENTO;
                    fin = new InventarioModel();
                    fin.Descriptores = new ObservableCollection<InventarioModel.Descriptor>();
                }

                fin.UnidSegmento = ii.UNID_SEGMENTO;
                fin.SelectedAlmacen = ii.ALMACEN;
                fin.IsChecked = false;
                fin.Finished = ii.FINISHED;
                fin.Fecha = ii.FECHA;
                InventoryApp.Model.CatalogInventario.InventarioModel.Descriptor dd = new InventarioModel.Descriptor();
                dd.Desc = ii.DESCRIPTOR;
                dd.IsChecked = false;
                fin.Descriptores.Add(dd);
                fin.Cantidad = fin.Descriptores.Count;
            }

            res.Add(fin);
  
            return res;
        }

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

        #endregion
    }
}
