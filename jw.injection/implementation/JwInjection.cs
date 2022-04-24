using jw.injection.api.container;
using jw.injection.implementation.container;

namespace jw.injection.implementation;

public class JwInjection
{
    public static IContainerBuilder CreateContainer()
    {
        return new ContainerBuilder();
    }
}