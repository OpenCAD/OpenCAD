using System;
using System.Linq;
using System.Reactive.Linq;
using Autofac;
using Autofac.Core;
using OpenCAD.Awesomium;
using OpenCAD.Kernel.Application;
using OpenCAD.Kernel.Application.Messaging;
using OpenCAD.Kernel.Application.Messaging.Messages;
using OpenCAD.Kernel.Graphics.GUI;
using OpenCAD.Kernel.Graphics.Window;
using OpenCAD.OpenGL;

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
                Console.ReadLine();
                Container.Resolve<IMessageAggregator>().Add(new ResizeRequestMessage(Container.Resolve<IWindowManager>().Windows.First(),100,100));
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
            builder.RegisterType<AwesomiumGUI>().As<IGUI>().SingleInstance();
            builder.RegisterType<OpenGLWindow>().As<IWindow>();
            builder.RegisterType<WindowManager>().As<IWindowManager>().SingleInstance();

            //builder.Register(c => new DocumentStore { ConnectionStringName = "RavenDB" }.Initialize()).As<IDocumentStore>().SingleInstance();
            //builder.Register(c => c.Resolve<IDocumentStore>().OpenAsyncSession()).As<IAsyncDocumentSession>().InstancePerHttpRequest();
            //builder.Register(c => c.Resolve<IDocumentStore>().OpenSession()).As<IDocumentSession>().InstancePerHttpRequest();

            //builder.Register(c => new RavenUserStore<ApplicationUser>(c.Resolve<IAsyncDocumentSession>(), false)).As<IUserStore<ApplicationUser>>().InstancePerHttpRequest();
            //builder.RegisterType<UserManager<ApplicationUser>>().InstancePerHttpRequest();
        }
    }
}
