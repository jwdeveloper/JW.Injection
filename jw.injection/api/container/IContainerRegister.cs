using jw.injection.api.enums;

namespace jw.injection.api.container;

public interface IContainerRegister
{
    public void Register(Type _interface, Type implementation, object provider, LifeTime lifeTime);
}