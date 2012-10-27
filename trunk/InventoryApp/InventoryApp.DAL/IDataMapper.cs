using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.DAL
{
    public interface IDataMapper
    {
        object getElements();
        object udpateElement(object element);
        object insertElement(object element);
    }
}
