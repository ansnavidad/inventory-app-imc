using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Model
{
    public interface IModelChangeTrack
    {
        bool IsModified { get; set; }
        bool IsNew { get; set; }
    }
}
