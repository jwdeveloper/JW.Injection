using jw.injection.api.container;
using jw.injection.api.enums;
using jw.injection.implementation.resolvers;

namespace jw.injection.implementation.container;

public class ContainerBuilder : IContainerBuilder
{
    private readonly IContainer _container;

    public ContainerBuilder()
    {
        _container = new Container(
            new InstanceResolver(),
            new MetadateResolver());
    }

    public IContainerBuilder RegisterSingleton<Implementation>()
    {
        _container.Register(null, typeof(Implementation), null, LifeTime.Singleton);
        return this;
    }
    
    public IContainerBuilder RegisterSingleton<Implementation>(Func<Implementation> provider)
    {
        _container.Register(null, typeof(Implementation), provider, LifeTime.Singleton);
        return this;
    }

    public IContainerBuilder RegisterTransient<Implementation>(Func<Implementation> provider)
    {
        _container.Register(null, typeof(Implementation), provider, LifeTime.Transient);
        return this;
    }

    public IContainerBuilder RegisterTransient<Implementation>()
    {
        _container.Register(null, typeof(Implementation), null, LifeTime.Singleton);
        return this;
    }

    public IContainerBuilder RegisterSingleton<Interface, Implementation>() where Implementation : Interface
    {
        _container.Register(typeof(Interface), typeof(Implementation), null, LifeTime.Singleton);
        return this;
    }

    public IContainerBuilder RegisterTransient<Interface, Implementation>() where Implementation : Interface
    {
        _container.Register(typeof(Interface), typeof(Implementation), null, LifeTime.Transient);
        return this;
    }

    public IContainerFind Build()
    {
        return _container;
    }
}