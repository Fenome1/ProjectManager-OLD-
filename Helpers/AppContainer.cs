using System.Net.Http;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using ProjectManager.App.Services;
using ProjectManager.App.Services.Interfaces;

namespace ProjectManager.App.Helpers;

public static class AppContainer
{
    private static IContainer container;

    public static void ConfigureContainer()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<HttpClient>().SingleInstance();
        builder.RegisterType<ProjectService>().As<IProjectService>();
        builder.RegisterType<UserService>().As<IUserService>();
        builder.RegisterType<UserViewService>().As<IUserViewService>();
        builder.RegisterType<CompletedProjectService>().As<ICompletedProjectService>();
        builder.RegisterType<UsersProjectsViewService>().As<IUsersProjectsViewService>();
        builder.RegisterType<RoleService>().As<IRoleService>();
        builder.RegisterType<StatusService>().As<IStatusService>();


        builder.RegisterType<MainWindow>().AsSelf();

        container = builder.Build();

        var csl = new AutofacServiceLocator(container);
        ServiceLocator.SetLocatorProvider(() => csl);
    }

    public static T Resolve<T>()
    {
        return container.Resolve<T>();
    }
}