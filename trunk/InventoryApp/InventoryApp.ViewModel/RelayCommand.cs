using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


namespace InventoryApp.ViewModel
{
    public class RelayCommand : ICommand
    {


        public void Execute(object parameter)
        {
            Debug.WriteLine("Hello, world");
            //throw new NotImplementedException();
        }

        public bool CanExecute(object parameter)
        {
            return true;
            //throw new NotImplementedException();
        }

        public event EventHandler CanExecuteChanged;
    }

}
