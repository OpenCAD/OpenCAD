using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using OpenCAD.Kernel.Application.Messaging;
using OpenCAD.Kernel.Application.Messaging.Messages;

using OpenCAD.Kernel.Graphics;
using OpenCAD.Kernel.Modelling;

namespace OpenCAD.Kernel.Application
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Method)]
    public class BindAttribute : Attribute
    {
        public BindAttribute()
        {

        }
    }

    public class ShellViewModel : BaseViewModel, IHandle<ViewModelRequest>,IHasScenes
    {
        public IList<IScene> Scenes { get; private set; }
        public Scene CurrentScene { get; private set; }

        private string _test;

        [Bind]
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
            Observable.Interval(TimeSpan.FromSeconds(0.05)).Subscribe(l => Test = l.ToString());


            //CurrentScene = new Scene(new PolygonModelOld(new STLReader().Read(@"C:\temp\testelephant.stl").Triangles.ToList<IPolygon>()), new OrthographicCamera());
            CurrentScene = new Scene(new JSONModel("Part.json"), new OrthographicCamera());
        }

        public string Foo(string test)
        {
            return new string(test.Reverse().ToArray());
        }

        [Bind]
        public void Bar()
        {
            Console.WriteLine("Pressed!!");
        }

        private void Hide()
        {

        }

        public void Handle(ViewModelRequest message)
        {

        }
    }

    public interface IHasScenes
    {
        IList<IScene> Scenes { get; }
        Scene CurrentScene { get; }
    }
}