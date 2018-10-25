using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    /// <summary>
    /// Simpe ICommand implementation which invokes an paramenterless Action when executed.
    /// Uses a Func<bool> to determine CanExecute. If no such Func is provided, CanExecute is always true.
    /// /// </summary>
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Update UI Component.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        Func<bool> _canExecuteFunction;
        Action _executeAction;
        protected Command() { }

        internal Command(Func<bool> canExecuteFunction)
        {
            _canExecuteFunction = canExecuteFunction;
        }


        public Command(Action executeAction)
        {
            _executeAction = executeAction;
        }

        public Command(Func<bool> canExecuteFunction, Action executeAction)
        {
            _canExecuteFunction = canExecuteFunction;
            _executeAction = executeAction;
        }

        public virtual bool CanExecute(object parameter)
        {
            //Invoke _canExecute or return true if not present.
            if (_canExecuteFunction != null)
            {
                return _canExecuteFunction.Invoke();
            }
            return true;
        }

        public virtual void Execute(object parameter)
        {
            _executeAction.Invoke();
        }




    }

    /// <summary>
    /// ICommand implementation which invokes an Action with a generic paramenter when Executed.
    /// Uses a Func<bool> to determine CanExecute. If no such Func is provided, CanExecute is always true.
    /// </summary>
    /// <typeparam name="ParameterType"></typeparam>
    public class TypedCommand<ParameterType> : Command
    {



        Action<ParameterType> _executeFunction;

        public TypedCommand(Func<bool> canExecuteAction, Action<ParameterType> action) : base(canExecuteAction)
        {
            _executeFunction = action;
        }

        public TypedCommand(Action<ParameterType> executeFunction) : base()
        {
            
            _executeFunction = executeFunction;

        }


        public override void Execute(object parameter)
        {
            var castedParam = (ParameterType)parameter;
            _executeFunction.Invoke(castedParam);
        }
    }

    /// <summary>
    /// ICommand implementation which invokes an Action with a generic paramenter when Executed.
    /// Uses a generic Predicate to determine CanExecute. If no such Predicate is provided, CanExecute is always true.
    /// </summary>
    /// <typeparam name="EvaluationType"></typeparam>
    /// <typeparam name="ParameterType"></typeparam>
    public class TypedCommand<EvaluationType, ParameterType> : TypedCommand<ParameterType>
    {
        //Generic Precicate for determining whether the command can be executed.
        private Predicate<EvaluationType> _canExecutePredicate;
        //Generic Action to be executed.
        Action<ParameterType> _executeFunction;

        public TypedCommand(Predicate<EvaluationType> canExecutePredicate, Action<ParameterType> executeFunction) : base(executeFunction)
        {
            _canExecutePredicate = canExecutePredicate;

        }


        public override bool CanExecute(object parameter)
        {
            var castedParam = (EvaluationType)parameter;

            if (_canExecutePredicate != null)
            {
                return _canExecutePredicate.Invoke(castedParam);
            }
            return true;
        }


    }
}
