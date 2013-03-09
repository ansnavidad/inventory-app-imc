using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model 
{
    public class CatalogEquipoModel: INotifyPropertyChanged 
    {
        private FixupCollection<DeleteEquipo> _equipos;
        private EQUIPO _selectedEquipo;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteEquipo> Equipos
        {
            get
            {
                return _equipos;
            }
            set
            {
                if (_equipos != value)
                {
                    _equipos = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Equipos"));
                    }
                }
            }
        }

        public EQUIPO SelectedEquipo
        {
            get
            {
                return _selectedEquipo;
            }
            set
            {
                if (_selectedEquipo != value)
                {
                    _selectedEquipo = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedEquipo"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<DeleteEquipo> ic = new FixupCollection<DeleteEquipo>();

            if (element != null)
            {
                if (((List<EQUIPO>)element).Count > 0)
                {
                    foreach (EQUIPO item in (List<EQUIPO>)element)
                    {
                        DeleteEquipo aux = new DeleteEquipo(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Equipos = ic;
        }

        public void loadItems(CategoriaModel categoria)
        {
            FixupCollection<EQUIPO> equipos = (this._dataMapper as EquipoDataMapper).GetArticuloEquipoByCategoria(new CATEGORIA() { UNID_CATEGORIA=categoria.UnidCategoria });
            FixupCollection<DeleteEquipo> ic = new FixupCollection<DeleteEquipo>();

            if (equipos != null)
            {
                if (equipos.Count > 0)
                {
                    equipos.ToList<EQUIPO>().ForEach(o=> ic.Add(new DeleteEquipo(o)));
                }
            }
            this.Equipos = ic;
        }

        public void deleteEquipo(USUARIO u)
        {
            foreach (DeleteEquipo item in this._equipos)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item,u);
                }
            }            
        }

        public CatalogEquipoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new EquipoDataMapper();
            this._equipos = new FixupCollection<DeleteEquipo>();
            this._selectedEquipo = new EQUIPO();
            this.loadItems();
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
