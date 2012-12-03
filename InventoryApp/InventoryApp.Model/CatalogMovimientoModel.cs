using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogMovimientoModel : INotifyPropertyChanged
    {
        private FixupCollection<CatalogMovimiento> _catalogMovimiento;
        private MOVIMENTO _selectedMovimiento;
        private MovimientoDataMapper _dataMapper;

        public FixupCollection<CatalogMovimiento> CatalogMovimiento
        {
            get
            {
                return _catalogMovimiento;
            }
            set
            {
                if (_catalogMovimiento != value)
                {
                    _catalogMovimiento = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CatalogMovimiento"));
                    }
                }
            }
        }

        public MOVIMENTO SelectedMovimiento
        {
            get
            {
                return _selectedMovimiento;
            }
            set
            {
                if (_selectedMovimiento != value)
                {
                    _selectedMovimiento = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedMovimiento"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsEntrada()
        {
            object element = this._dataMapper.getEntradasElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }


        public void Updateitemsentrada()
        {
            object element = this._dataMapper.getEntradasElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }


        public void loadItemsSalida()
        {
            object element = this._dataMapper.getSalidasElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsTraspaso()
        {
            object element = this._dataMapper.getTraspasos();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public CatalogMovimientoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new MovimientoDataMapper();
            this._catalogMovimiento = new FixupCollection<CatalogMovimiento>();
            this._selectedMovimiento = new MOVIMENTO();
            this.loadItems();
        }

        public CatalogMovimientoModel(IDataMapper dataMapper, string opc)
        {
            this._dataMapper = new MovimientoDataMapper();
            this._catalogMovimiento = new FixupCollection<CatalogMovimiento>();
            this._selectedMovimiento = new MOVIMENTO();
            this.loadItemsEntrada();
        }

        public CatalogMovimientoModel(IDataMapper dataMapper, string opc, string opan)
        {
            this._dataMapper = new MovimientoDataMapper();
            this._catalogMovimiento = new FixupCollection<CatalogMovimiento>();
            this._selectedMovimiento = new MOVIMENTO();
            this.loadItemsSalida();
        }

        public CatalogMovimientoModel(IDataMapper dataMapper, int i)
        {
            this._dataMapper = new MovimientoDataMapper();
            this._catalogMovimiento = new FixupCollection<CatalogMovimiento>();
            this._selectedMovimiento = new MOVIMENTO();
            this.loadItemsTraspaso();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
