using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;

namespace InventoryApp.Model
{
    public class CatalogRolModel : INotifyPropertyChanged
    {
        private ROL _selectedRol;
        private FixupCollection<DeleteRol> _rol;
        private AppRolDataMapper _dataMapper;

        public FixupCollection<DeleteRol> Rol
        {
            get
            {
                return _rol;
            }
            set
            {
                if (_rol != value)
                {
                    _rol = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Rol"));
                    }
                }
            }
        }
        public ROL SelectedRol
        {
            get
            {
                return _selectedRol;
            }
            set
            {
                if (_selectedRol != value)
                {
                    _selectedRol = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedRol"));
                    }
                }
            }
        }

        public void loadRoles()
        {
            object element = this._dataMapper.getElementsRol();

            FixupCollection<DeleteRol> ic = new FixupCollection<DeleteRol>();

            if (element != null)
            {
                if (((List<ROL>)element).Count > 0)
                {
                    foreach (ROL item in (List<ROL>)element)
                    {
                        DeleteRol aux = new DeleteRol(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Rol = ic;
        }

        public CatalogRolModel(IDataMapper dataMapper)
        {
            this._dataMapper = new AppRolDataMapper();
            this._rol = new FixupCollection<DeleteRol>();
            this._selectedRol = new ROL();
            this.loadRoles();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
