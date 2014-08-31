using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Autofac;
using OpenCAD.Desktop.ViewModel;
using OpenCAD.Kernel.Application;
using OpenCAD.Kernel.Application.Windowing;
using OpenCAD.Kernel.Graphics.GUI;

namespace OpenCAD.Desktop
{
    public class DesktopApplication : BaseApplication
    {
        private readonly IGUIManager _guiManager;
        private readonly IWindowManager _windowManager;

        public DesktopApplication(IGUIManager guiManager, IWindowManager windowManager)
        {
            _guiManager = guiManager;
            _windowManager = windowManager;
        }

        private IEnumerable<ILoadMessage> Load()
        {
            foreach (var loadMessage in _guiManager.Load())
            {
                yield return loadMessage;
            }
            foreach (var loadMessage in _windowManager.Load())
            {
                yield return loadMessage;
            }
        } 

        public override void Run()
        {
            foreach (var message in Load())
            {
                Console.WriteLine(message.Message);
            }

            var window = _windowManager.Create(new Size(800, 600), new ShellViewModel());
        }


        public override void Dispose()
        {

        }
    }
}