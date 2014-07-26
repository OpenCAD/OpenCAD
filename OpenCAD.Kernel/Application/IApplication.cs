using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenCAD.Kernel.Graphics.GUI;
using OpenCAD.Kernel.Graphics.Window;

namespace OpenCAD.Kernel.Application
{
    public interface IApplication:IDisposable
    {
        void Run();
    }

    public abstract class BaseApplication:IApplication
    {
        public abstract void Dispose();
        public abstract void Run();
    }

    public class DesktopApplication:BaseApplication
    {
        private readonly IWindowManager _windowManager;

        public DesktopApplication(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }

        public override void Run()
        {
            //this blocks...

            _windowManager.Create();
            _windowManager.Create();

        }

        public override void Dispose()
        {
            
        }
    }
}
