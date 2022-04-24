using System.Reflection;
using jw.injection.api.enums;
using jw.injection.api.metadata;

namespace jw.injection.api.resolvers;

public interface IMetadataResolver
{
    IInjectionMetadata CreateMetadata(Type type, Func<object> provider, LifeTime lifeTime);
    ConstructorInfo FindInjectedConstructor(Type type);
    FieldInfo[] FindInjectedFields(Type type);
}