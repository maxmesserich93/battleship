using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class TypedRelayCommand<ParamenterType> : ICommand
    {

        Action<ParamenterType> _action;

        public TypedRelayCommand(Action<ParamenterType> action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var cast = (ParamenterType)parameter;
            _action(cast);

        }
    }
}
