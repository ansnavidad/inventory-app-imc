using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InventoryApp.ViewModel
{
    interface ICommand
    {
        void Execute(object parameter);

        bool CanExecute(object parameter);
        event EventHandler CanExecuteChanged;
    }
}
