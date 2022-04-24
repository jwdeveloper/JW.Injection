using jw.injection.api.metadata;
using jw.injection.implementation.exceptions;

namespace jw.injection.implementation.metadata;

public class InjectionMetadataDictionary : IInjectionMetadataDictionary
{
    private readonly Dictionary<Type, IInjectionMetadata> _injections = new ();
    
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