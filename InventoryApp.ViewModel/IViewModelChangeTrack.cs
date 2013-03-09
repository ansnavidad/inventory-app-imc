using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.ViewModel
{
    public interface IViewModelChangeTrack
    {
        bool IsModified { get; set; }
        bool IsNew { get; set; }
    }
}
