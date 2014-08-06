using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Application;

namespace OpenCAD.Desktop.ViewModel
{
    public class ShellViewModel:BaseViewModel
    {
        private string _test;

        public string Test
        {
            get { return _test; }
            set
            {
                _test = value;
                NotifyOfPropertyChange(() => Test);
            }
        }

        public ShellViewModel()
        {
            Test = "dddd";
            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(l => Test = l.ToString());
        }

        public string Foo(string test)
        {
            return new string(test.Reverse().ToArray());
        }

        public void Bar()
        {
            Console.WriteLine();
        }

        private void Hide()
        {
            
        }
    }
}
