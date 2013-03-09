using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.Model
{
    public interface IModelDataConverter
    {
        object ConvertTo();
        object ConvertBack(object data);
    }
}
