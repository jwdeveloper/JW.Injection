using jw.injection.api.metadata;

namespace jw.injection.api.resolvers;

public interface IInstanceResolver
{
    object CreateInstance(IInjectionMetadata injectionMetadata, IInjectionMetadataDictionary injectionsMetadata);
    object PrepareInstance(IInjectionMetadata injectionMetadata, IInjectionMetadataDictionary injectionsMetadata);
    object[] ResolveConstructor(IInjectionMetadata injectionMetadata, IInjectionMetadataDictionary injectionsMetadata);
    object[] ResolveFields(IInjectionMetadata injectionMetadata, IInjectionMetadataDictionary injectionsMetadata);
}