using System;
using System.Windows.Input;

namespace Jpp.Common
{
    /// <summary>
    /// Implementation of the ICommand interface for consumption in MVVM applications. Intended for independent use without full MVVM frameworks, which often already provide implementations.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Create new instance of command
        /// </summary>
        /// <param name="execute">Delegate or method to be executed</param>
        /// <param name="canExecute">Function evaluating if command can be executed</param>
        public DelegateCommand(Action execute) : this(execute, null) { }
        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));

            if (canExecute != null) _canExecute = canExecute;
        }

        /// <summary>
        /// Evaluate if command can currently be executed
        /// </summary>
        /// <param name="parameter">Parameter to pass to evaluation</param>
        /// <returns>Boolean indicating if command can be executed</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">Parameter to pass to executing function</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter)) _execute.Invoke();
        }

        /// <summary>
        /// Event triggered if the ability to execute the command has changed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        public DelegateCommand(Action<T> execute) : this(execute, null) { }
        public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));

            if (canExecute != null) _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute.Invoke((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter)) _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
