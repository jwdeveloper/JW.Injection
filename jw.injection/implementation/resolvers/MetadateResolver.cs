using System.Reflection;
using jw.injection.api.attributes;
using jw.injection.api.enums;
using jw.injection.api.metadata;
using jw.injection.api.resolvers;
using jw.injection.implementation.metadata;

namespace jw.injection.implementation.resolvers;

public class MetadateResolver : IMetadateResolver
{
    public IInjectionMetadata CreateMetadata(Type type, Func<object> provider, LifeTime lifeTime)
    {
        return new InjectionMetadata
        {
            Type = type,
            InstanceProvider = provider,
            LifeTime = lifeTime,
            InjectedConstructor = FindInjectedConstructor(type),
            InjectedFields = FindInjectedFields(type),
        };
    }

    public ConstructorInfo FindInjectedConstructor(Type type)
    {
        if (type.IsInterface)
        {
            return null;
        }
        
        var constructors = type.GetConstructors();
        foreach (var constructor in constructors)
        {
            if (constructor.GetCustomAttribute<Inject>() is not null)
            {
                return constructor;
            }
        }
        return constructors[0];
    }

    public FieldInfo[] FindInjectedFields(Type type)
    {
        if (type.IsInterface)
        {
            return null;
        }
        
        var result = new List<FieldInfo>();
        foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
        {
            if (field.GetCustomAttribute<Inject>() is null)
            {
                continue;
            }

            result.Add(field);
        }

        return result.ToArray();
    }
}