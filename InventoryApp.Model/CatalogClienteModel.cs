using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogClienteModel : INotifyPropertyChanged
    {
        private CLIENTE _selectedCliente;
        private FixupCollection<DeleteCliente> _cliente;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteCliente> Cliente
        {
            get { 
                return _cliente; 
                }
            set
            {
                if (_cliente !=value)
                {
                    _cliente = value;
                        if (PropertyChanged !=null)
	                    {
                            PropertyChanged(this, new PropertyChangedEventArgs("Cliente"));
	                    }
                }
            }
        }
        public CLIENTE SelectedCliente
        {
            get
            {
                return _selectedCliente;
            }
            set
            {
                if (_selectedCliente != value)
                {
                    _selectedCliente = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedCliente"));
                    }
                }
            }
        }

        public void loadCliente()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteCliente> ic = new FixupCollection<DeleteCliente>();

            if (element != null)
            {
                if (((List<CLIENTE>)element).Count > 0)
                {
                    foreach (CLIENTE item in (List<CLIENTE>)element)
                    {
                        DeleteCliente aux = new DeleteCliente(item);
                        ic.Add(aux);
                    }
                }
             }
            this.Cliente = ic;
        }

        public void deleteCliente()
        {
            foreach (DeleteCliente item in this._cliente)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public void deleteCliente(USUARIO u)
        {
            foreach (DeleteCliente item in this._cliente)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item, u);
                }
            }
        }

        public CatalogClienteModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ClienteDataMapper();
            this._cliente = new FixupCollection<DeleteCliente>();
            this._selectedCliente = new CLIENTE();
            this.loadCliente();
            
        }
    
public event PropertyChangedEventHandler  PropertyChanged;
}
}
