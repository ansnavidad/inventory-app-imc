using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.DAL.Recibo;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model.CatalogInventario
{
    public class Descriptor2 : ModelBase
    {
        #region Properties

        public List<Descriptor> DescriptorCollection
        {
            get { return _DescriptorCollection; }
            set
            {
                if (_DescriptorCollection != value)
                {
                    _DescriptorCollection = value;
                    OnPropertyChanged(DescriptorCollectionPropertyName);
                }
            }
        }
        private List<Descriptor> _DescriptorCollection;
        public const string DescriptorCollectionPropertyName = "DescriptorCollection";

        #endregion

        #region Constructors

        public Descriptor2() {

            this.DescriptorCollection = new List<Descriptor>();
            //this.DescriptorCollection.Add()
        }

        #endregion

        #region Methods

        #endregion
    }
}
