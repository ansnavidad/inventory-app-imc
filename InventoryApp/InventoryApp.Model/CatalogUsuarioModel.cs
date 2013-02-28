using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogUsuarioModel : INotifyPropertyChanged
    {
        private FixupCollection<DeleteUsuarios> _usuarios;
        private DeleteUsuarios _selectedUsuario;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteUsuarios> Usuarios
        {
            get
            {
                return _usuarios;
            }
            set
            {
                if (_usuarios != value)
                {
                    _usuarios = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Usuarios"));
                    }
                }
            }
        }

        public DeleteUsuarios SelectedUsuario
        {
            get
            {
                return _selectedUsuario;
            }
            set
            {
                if (_selectedUsuario != value)
                {
                    _selectedUsuario = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedUsuario"));
                    }
                }
            }
        }

        public void loadUsuario()
        {         
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteUsuarios> ic = new FixupCollection<DeleteUsuarios>();

            if (element != null)
            {
                if (((List<USUARIO>)element).Count > 0)
                {
                    foreach (USUARIO item in (List<USUARIO>)element)
                    {
                        if (item.UNID_USUARIO!=1)
                        {
                            DeleteUsuarios aux = new DeleteUsuarios(item);
                            ic.Add(aux);   
                        }
                    }
                }
            }
            this.Usuarios = ic;
        }

        public void deleteUsuarios()
        {
            foreach (DeleteUsuarios item in this._usuarios)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public CatalogUsuarioModel(IDataMapper dataMapper)
        {
            this._dataMapper = new AppUsuario();
            this._usuarios = new FixupCollection<DeleteUsuarios>();
            this.loadUsuario();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
