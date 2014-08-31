using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCAD.Kernel.Application;

namespace OpenCAD.OpenGL.Views
{
    public class ShellView:IView
    {
        public ShellViewModel ViewModel { get; private set; }

        public ShellView(ShellViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }
}
