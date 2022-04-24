namespace jw.injection.api.container;

public interface IContainerBuilder
{
    IContainerBuilder RegisterSingleton<Implementation>();
    IContainerBuilder RegisterSingleton<Implementation>(Func<Implementation> provider);
    IContainerBuilder RegisterSingleton<Interface, Implementation>() where Implementation : Interface;
    IContainerBuilder RegisterTransient<Implementation>(Func<Implementation> provider);
    IContainerBuilder RegisterTransient<Implementation>();
    IContainerBuilder RegisterTransient<Interface, Implementation>() where Implementation : Interface;
    IContainerFind Build();
}