using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AssemblyBrowser
{
    class RealCommand: ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public void Execute(object obj)
        {
            this.execute(obj);
        }

        public RealCommand(Action<object> execute)
        {
            this.execute = execute;
        }

        public bool CanExecute(object obj)
        {
            return true;
        }
    }
}
