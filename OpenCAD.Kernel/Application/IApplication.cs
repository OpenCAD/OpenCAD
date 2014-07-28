using System;
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
        }

        public override void Dispose()
        {
            
        }
    }
}
