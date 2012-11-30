using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.Model.Recibo;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;

namespace InventoryApp.ViewModel.Recibo
{
    public class CatalogReciboViewModel : ViewModelBase, IPageViewModel
    {
        
        public ICommand AddReciboCmd
        {
            get
            {
                if (_AddReciboCmd == null)
                {
                    _AddReciboCmd = new RelayCommand(p => this.AttemptAddReciboCmd(), p => this.CanAttemptAddReciboCmd());
                }
                return _AddReciboCmd;
            }
        }
        private RelayCommand _AddReciboCmd;
        
        public ObservableCollection<ReciboModel> Recibos
        {
            get { return _Recibos; }
            set
            {
                if (_Recibos != value)
                {
                    _Recibos = value;
                    OnPropertyChanged(RecibosPropertyName);
                }
            }
        }
        private ObservableCollection<ReciboModel> _Recibos;
        public const string RecibosPropertyName = "Recibos";

        public CatalogReciboViewModel()
        {
            this.init();
        }

        private void init()
        {
            this._Recibos = this.GetRecibos();
        }

        public ObservableCollection<ReciboModel> GetRecibos()
        {
            ObservableCollection<ReciboModel> recibos = new ObservableCollection<ReciboModel>();

            try
            {
                ReciboDataMapper rmp = new ReciboDataMapper();
                List<RECIBO> reciboList = rmp.getListElements();
                reciboList.ForEach(o => recibos.Add(new ReciboModel()
                    {
                        FechaCreacion = (DateTime)o.FECHA_CREACION
                        ,
                        UnidRecibo = o.UNID_RECIBO
                        ,
                        TroubleTicket=o.TT
                        ,PO=o.PO
                    }));
            }
            catch (Exception ex)
            {
                ;
            }
            
            return recibos;
        }

        public AddReciboViewModel CraeteAddReciboViewModel()
        {
            AddReciboViewModel addReciboViewModel = new AddReciboViewModel(this);

            return addReciboViewModel;
        }

        public void updateCollection()
        {
            this.Recibos = this.GetRecibos();
        }

        private void AttemptAddReciboCmd()
        {

        }

        private bool CanAttemptAddReciboCmd()
        {
            bool CanAttempt = false;

            CanAttempt = true;

            return CanAttempt;
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
    }//endclass
}
