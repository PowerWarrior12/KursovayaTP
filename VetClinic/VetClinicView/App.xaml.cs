using _VetCliniсBusinessLogic_.BusinessLogic;
using _VetCliniсBusinessLogic_.Interfaces;
using VetClinikEntitiesImplements.Implements;
using Unity;
using Unity.Lifetime;
using System.Windows;

namespace VetClinicView
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            var container = BuildUnityContainer();
            var form = container.Resolve<RegisterLoginFrame>();
            form.ShowDialog();
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IMedicationStorage, MedicationStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMedicineStorage, MedicineStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IUserStorage, UserStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IServiceStorage, ServiceStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<MedicationBusinessLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<MedicineBusinessLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ServiceBusinessLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<UserBusinessLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
