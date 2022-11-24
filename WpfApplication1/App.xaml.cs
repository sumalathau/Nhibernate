using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly WindsorContainer container = new WindsorContainer();
        public App()
        {
            RegisterComponents();
        }
        private void RegisterComponents()
        {
            container.Register(Component.For<IContactDAO>().ImplementedBy<ContactDAO>());
        }
    }
}
