using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using Autofac;
using Autofac.Core;
using OpenCAD.Awesomium;
using OpenCAD.Desktop.ViewModel;
using OpenCAD.Kernel.Application;
using OpenCAD.Kernel.Application.Messaging;
using OpenCAD.Kernel.Application.Messaging.Messages;
using OpenCAD.Kernel.Application.Windowing;
using OpenCAD.Kernel.Geometry;
using OpenCAD.Kernel.Graphics.GUI;
using OpenCAD.Kernel.Modelling;
using OpenCAD.OpenGL;
using IContainer = Autofac.IContainer;

namespace OpenCAD.Desktop
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CADModule());
            Container = builder.Build();

            using (var app = Container.Resolve<IApplication>())
            {
                app.Run();
               // Console.ReadLine();
                Container.Resolve<IMessageAggregator>().Messages.Subscribe(Console.WriteLine);
                //Container.Resolve<IMessageAggregator>().Add(new ViewModelRequest(new PolygonModelOld(new STLReader().Read(@"C:\temp\testelephant.stl").Triangles.ToList<IPolygon>())));
                //Container.Resolve<IMessageAggregator>().Add(new ResizeRequestMessage(Container.Resolve<IWindowManager>().Windows.First(),100,100));
                Console.ReadLine();
            }
        }
    }


    public class CADModule : Module
    {
        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            base.AttachToComponentRegistration(componentRegistry, registration);
            registration.Activated += (sender, args) =>
            {
                if (args == null)
                    return;
                foreach (var i in args.Instance.GetType().GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IHandle<>)))
                {
                    var messageType = i.GetGenericArguments().FirstOrDefault();
                    var method = i.GetMethod("Handle", new[] { messageType });
                    if (messageType != null)
                    {
                        args.Context.Resolve<IMessageAggregator>()
                            .Messages.Where(m => m.GetType() == messageType)
                            .Subscribe(m => method.Invoke(args.Instance, new object[] {m}));
                    }
                }
            };
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<DesktopApplication>().As<IApplication>().SingleInstance();
            builder.RegisterType<MessageAggregator>().As<IMessageAggregator>().SingleInstance();
            builder.RegisterType<AwesomiumGUIManager>().As<IGUIManager>().SingleInstance();
            builder.RegisterType<ShellViewModel>().As<IViewModel>();

            builder.RegisterType<OpenGLWindow>().As<IWindow>();
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();





            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(type => type.Name.EndsWith("ViewModel"))
                .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("ViewModels"))
                .Where(type => type.GetInterface(typeof(INotifyPropertyChanged).Name) != null)
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                   .Where(type => type.Name.EndsWith("View"))
                   .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("Views"))
                   .AsSelf()
                   .InstancePerDependency();

            //builder.Register(c => new DocumentStore { ConnectionStringName = "RavenDB" }.Initialize()).As<IDocumentStore>().SingleInstance();
            //builder.Register(c => c.Resolve<IDocumentStore>().OpenAsyncSession()).As<IAsyncDocumentSession>().InstancePerHttpRequest();
            //builder.Register(c => c.Resolve<IDocumentStore>().OpenSession()).As<IDocumentSession>().InstancePerHttpRequest();

            //builder.Register(c => new RavenUserStore<ApplicationUser>(c.Resolve<IAsyncDocumentSession>(), false)).As<IUserStore<ApplicationUser>>().InstancePerHttpRequest();
            //builder.RegisterType<UserManager<ApplicationUser>>().InstancePerHttpRequest();
        }

        public class ViewFetcher
        {
            private readonly IContainer _container;

            public ViewFetcher(IContainer container)
            {
                _container = container;
            }

            //public IView Fetch(IViewModel viewModel)
            //{

            //    _container.Resolve()
            //}
        }
    }

}
