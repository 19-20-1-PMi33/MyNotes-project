using System;
using System.Windows.Input;

namespace MyNotes.Binding
{
    public class RegisterButton : ICommand
    {
        private readonly Action handler;
        private bool isEnabled;

        /// <summary>
        /// Bind method to be executed to the handler
        /// So that it can direct on event execution
        /// </summary>
        /// <param name="handler"></param>
        public RegisterButton(Action handler)
        {
            this.handler = handler;
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (value != isEnabled)
                {
                    isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// method to specify if the event will execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            handler();
        }
    }
}
