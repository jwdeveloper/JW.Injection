using jw.injection.api.container;
using jw.injection.api.enums;
using jw.injection.api.metadata;
using jw.injection.api.resolvers;
using jw.injection.implementation.metadata;

namespace jw.injection.implementation.container;

public class Container : IContainer
{
    private readonly IInjectionMetadataDictionary _injectionsMetadata;
    private readonly IInstanceResolver _instanceResolver;
    private readonly IMetadataResolver _metadataResolver;

    public Container(IInstanceResolver instanceResolver, IMetadataResolver metadataResolver)
    {
        _injectionsMetadata = new InjectionMetadataDictionary();
        _metadataResolver = metadataResolver;
        _instanceResolver = instanceResolver;
    }
    
    public Container(IInstanceResolver instanceResolver, IMetadataResolver metadataResolver, IInjectionMetadataDictionary metadataDictionary)
    {
        _injectionsMetadata = metadataDictionary;
        _metadataResolver = metadataResolver;
        _instanceResolver = instanceResolver;
    }

    public T Find<T>()
    {
        var type = typeof(T);
        var metadata = _injectionsMetadata.Find(type);
        var instance = (T) _instanceResolver.CreateInstance(metadata, _injectionsMetadata);
        return instance;
    }

    public void Register(Type _interface, Type implementation, object provider, LifeTime lifeTime)
    {
        var registeredType = _interface ?? implementation;
        var metadata = _metadataResolver.CreateMetadata(implementation, (Func<object>)provider, lifeTime);
        _injectionsMetadata.Add(registeredType, metadata);
    }
}