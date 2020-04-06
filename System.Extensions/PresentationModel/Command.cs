using System.Windows.Input;

namespace System.PresentationModel
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action action;

        public Command(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
