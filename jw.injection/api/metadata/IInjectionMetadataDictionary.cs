namespace jw.injection.api.metadata;

public interface IInjectionMetadataDictionary
{
    IInjectionMetadata Find(Type type);
    void Add(Type type, IInjectionMetadata metadata);
}