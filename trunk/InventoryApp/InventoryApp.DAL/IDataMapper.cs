using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.DAL
{
    public interface IDataMapper
    {
        object getElements();
        object getElement(object element);
        void udpateElement(object element);
        void insertElement(object element);
        void deleteElement(object element, USUARIO u);
        void deleteElement(object element);
    }
}
