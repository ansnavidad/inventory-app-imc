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
                        ReciboStatus = new ReciboStatusModel() { UnidReciboStatus = (long)o.UNID_RECIBO_STATUS, StatusName = o.RECIBO_STATUS.RECIBO_STATUS_NAME }
                        ,
                        UnidRecibo = o.UNID_RECIBO
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
