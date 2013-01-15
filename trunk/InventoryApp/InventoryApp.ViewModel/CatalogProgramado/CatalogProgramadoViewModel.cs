using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogProgramado
{
    public class CatalogProgramadoViewModel : IPageViewModel
    {
        private RelayCommand _deleteMaxMinCommand;
        private CatalogProgramadoModel _catalogProgramadoModel;

        public CatalogProgramadoViewModel()
        {
            try
            {
                IDataMapper dataMapper = new ProgramadoDataMapper();
                this._catalogProgramadoModel = new CatalogProgramadoModel(dataMapper);
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }

        public CatalogProgramadoModel CatalogProgramadoModel
        {
            get
            {
                return _catalogProgramadoModel;
            }
            set
            {
                _catalogProgramadoModel = value;
            }
        }

        public void loadProgramado()
        {
            this._catalogProgramadoModel.loadProgramado();
        }

        public AddProgramadoViewModel CreateAddProgramadoViewModel()
        {
            return new AddProgramadoViewModel(this);
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
    }
}
