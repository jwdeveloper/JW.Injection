using jw.injection.api.metadata;
using jw.injection.implementation.exceptions;

namespace jw.injection.implementation.metadata;

public class InjectionMetadataDictionary : IInjectionMetadataDictionary
{
    private readonly IDictionary<Type, IInjectionMetadata> _injections;

    public InjectionMetadataDictionary()
    {
        _injections = new Dictionary<Type, IInjectionMetadata>();
    }
    
    public InjectionMetadataDictionary(IDictionary<Type, IInjectionMetadata> injections)
    {
        _injections = injections;
    }
    
    public IInjectionMetadata Find(Type type)
    {
        if (!_injections.ContainsKey(type))
        {
            throw JwInjectionException.NotFound(type);
        }
        return _injections[type];
    }

    public void Add(Type type, IInjectionMetadata metadata)
    {
        if (_injections.ContainsKey(type))
        {
            throw JwInjectionException.AlreadyRegistered(type);
        }
        _injections.Add(type,metadata);
    }
}