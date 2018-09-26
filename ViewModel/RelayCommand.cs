using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;


        /// <summary>
        /// Function pointer to a Void method.
        /// </summary>
        private Action<object> _action;

        public RelayCommand(Action<object> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Casts the parameter to the type expected by the Action.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            Debug.WriteLine("ASDASDASDAS");
            //Parameter p = (Parameter)parameter;
            _action(parameter);
        }
    }
}
