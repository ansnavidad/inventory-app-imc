using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model.Recibo;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace InventoryApp.ViewModel.Recibo
{
    public class AddMovimientoDetalleViewModel:ViewModelBase
    {
        public ICommand AddMovimientoDetalleCmd
        {
            get
            {
                if (_AddMovimientoDetalleCmd == null)
                {
                    _AddMovimientoDetalleCmd = new RelayCommand(p => this.AttemptAddMovimientoDetalleCmd(), p => this.CanAttemptAddMovimientoDetalleCmd());
                }
                return _AddMovimientoDetalleCmd;
            }
        }
        private RelayCommand _AddMovimientoDetalleCmd;

        private AddMovimientoViewModel _AddMovimientoViewModel;
        private AddReciboViewModel _AddReciboViewModel;
        
        public FacturaCompraModel SelectedFactura
        {
            get { return _SelectedFactura; }
            set
            {
                if (_SelectedFactura != value)
                {
                    _SelectedFactura = value;
                    OnPropertyChanged(SelectedFacturaPropertyName);
                    //this.FacturaDetalles = _SelectedFactura.FacturaDetalle;
                }
            }
        }
        private FacturaCompraModel _SelectedFactura;
        public const string SelectedFacturaPropertyName = "SelectedFactura";

        public ObservableCollection<FacturaCompraModel> Facturas
        {
            get 
            {
                ObservableCollection<FacturaCompraModel> facturas;
                if (_AddReciboViewModel != null)
                {
                    facturas = _AddReciboViewModel.Facturas;
                }
                else
                {
                    facturas = new ObservableCollection<FacturaCompraModel>();
                }
                return facturas; 
            }
        }
        private ObservableCollection<FacturaCompraModel> _Facturas;
        public const string FacturasPropertyName = "Facturas";

        //public ObservableCollection<FacturaCompraDetalleModel> FacturaDetalles
        //{
        //    get { return _FacturaDetalles; }
        //    set
        //    {
        //        if (_FacturaDetalles != value)
        //        {
        //            _FacturaDetalles = value;
        //            OnPropertyChanged(FacturaDetallesPropertyName);
        //        }
        //    }
        //}
        //private ObservableCollection<FacturaCompraDetalleModel> _FacturaDetalles;
        //public const string FacturaDetallesPropertyName = "FacturaDetalles";

        public AddMovimientoDetalleViewModel()
        {

        }

        public AddMovimientoDetalleViewModel(AddMovimientoViewModel addMovimientoViewModel,AddReciboViewModel addReciboViewModel)
        {
            this._AddMovimientoViewModel = addMovimientoViewModel;
            this._AddReciboViewModel = addReciboViewModel;
        }

        private void AttemptAddMovimientoDetalleCmd()
        {
            if (this._AddMovimientoViewModel != null)
            {
                var sel = (from o in this._SelectedFactura.FacturaDetalle
                           where o.IsSelected == true
                           select o).ToList();
                sel.ForEach(o => {
                    for (int i = 0; i < o.CantidadElegida; i++)
                    {
                        this._AddMovimientoViewModel.Items.Add(new ReciboItemModel()
                        {
                            Articulo = o.Articulo,
                            FacturaDetalle = o,
                        });
                    }
                });
            }
        }

        private bool CanAttemptAddMovimientoDetalleCmd()
        {
            bool canAttempt = true;
            int countSelected = 0;


            try
            {
                countSelected = (from o in this.SelectedFactura.FacturaDetalle
                                where o.IsSelected == true
                                select o).ToList().Count;
                if (countSelected > 0)
                {
                    var res = (from o in this.SelectedFactura.FacturaDetalle
                               where o.CantidadElegida > o.Cantidad
                               select o).ToList().Count;
                    if (res > 0)
                    {
                        canAttempt = false;
                    }
                }
                else
                {
                    canAttempt = false;
                }
            }
            catch (Exception)
            {
                ;
            }

            return canAttempt;
        }
    }
}
