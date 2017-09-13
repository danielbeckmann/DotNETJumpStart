using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImageApp.Common
{
    /// <summary>
    /// A basic implementation of the <see cref="ICommand"/> interface for MVVM Commands.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        /// <summary>
        /// Event that gets thrown when the CanExecute value has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.</param>
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="DelegateCommand"/>.
        /// </summary>
        /// <param name="execute">Delegate to execute when Execute is called on the command.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        ///<summary>
        /// Defines the method that determines whether the command can execute in its current state.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        ///<returns>
        /// True if this command can be executed. Otherwise False.
        ///</returns>
        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }

            return this.canExecute(parameter);
        }

        ///<summary>
        /// The method that is called when the command is invoked.
        ///</summary>
        ///<param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        ///<summary>
        /// Invoke this method when changes occur, that affect whether or not the command can execute.
        ///</summary>
        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
