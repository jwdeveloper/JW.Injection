using System.Reflection;
using jw.injection.api.enums;
using jw.injection.api.metadata;

namespace jw.injection.implementation.metadata;

public class InjectionMetadata : IInjectionMetadata
{
    private Type _type;

    private LifeTime _lifeTime;

    private FieldInfo[] _injectedFields;

    private ConstructorInfo _injectedConstructor;

    private Func<object> _instanceProvider;

    private object _instnace;

    public bool HasInjectedFields => _injectedFields != null && _injectedFields.Length != 0;
    public bool HasInstance => _instnace != null;
    public bool HasInstanceProvider => _instanceProvider != null;
    
    public Type Type
    {
        get => _type;
        set => _type = value;
    }

    public LifeTime LifeTime
    {
        get => _lifeTime;
        set => _lifeTime = value;
    }

    public FieldInfo[] InjectedFields
    {
        get => _injectedFields;
        set => _injectedFields = value;
    }

    public ConstructorInfo InjectedConstructor
    {
        get => _injectedConstructor;
        set => _injectedConstructor = value;
    }

    public Func<object> InstanceProvider
    {
        get => _instanceProvider;
        set => _instanceProvider = value;
    }
    
    public object Instance
    {
        get => _instnace;
        set => _instnace = value;
    }

}