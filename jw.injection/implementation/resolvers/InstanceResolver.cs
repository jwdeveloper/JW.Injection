using jw.injection.api.enums;
using jw.injection.api.metadata;
using jw.injection.api.resolvers;

namespace jw.injection.implementation.resolvers;

public class InstanceResolver : IInstanceResolver
{
    public object CreateInstance(IInjectionMetadata injectionMetadata, IInjectionMetadataDictionary injectionsMetadata)
    {
        if (injectionMetadata.HasInstance && injectionMetadata.LifeTime == LifeTime.Singleton)
        {
            return injectionMetadata.Instance;
        }

        if (injectionMetadata.HasInstanceProvider)
        {
            switch (injectionMetadata.LifeTime)
            {
                case LifeTime.Singleton:
                    injectionMetadata.Instance = injectionMetadata.InstanceProvider.Invoke();
                    return injectionMetadata.Instance;
                case LifeTime.Transient:
                    return  injectionMetadata.InstanceProvider.Invoke();
            }
        }


        var instance = PrepareInstance(injectionMetadata, injectionsMetadata);
        if (injectionMetadata.LifeTime == LifeTime.Singleton)
        {
            injectionMetadata.Instance = instance;
        }

        return instance;
    }

    public object PrepareInstance(IInjectionMetadata injectionMetadata, IInjectionMetadataDictionary injectionsMetadata)
    {
        object instance;
        var parameters = ResolveConstructor(injectionMetadata, injectionsMetadata);
        instance = injectionMetadata.InjectedConstructor.Invoke(parameters);

        if (injectionMetadata.HasInjectedFields)
        {
            var values = ResolveFields(injectionMetadata, injectionsMetadata);
            for (var i = 0; i < values.Length; i++)
            {
                injectionMetadata.InjectedFields[i].SetValue(instance, values[i]);
            }
        }

        return instance;
    }
    
    public object[] ResolveConstructor(IInjectionMetadata injectionMetadata, IInjectionMetadataDictionary injectionsMetadata)
    {
        var parameters = injectionMetadata.InjectedConstructor.GetParameters();
        var constructorValues = new object[parameters.Length];
        for (var i = 0; i < constructorValues.Length; i++)
        {
            var metaData = injectionsMetadata.Find(parameters[i].ParameterType);
            constructorValues[i] = CreateInstance(metaData, injectionsMetadata);
        }

        return constructorValues;
    }

    public object[] ResolveFields(IInjectionMetadata injectionMetadata, IInjectionMetadataDictionary injectionsMetadata)
    {
        var fields = injectionMetadata.InjectedFields;
        var fieldsValues = new object[fields.Length];
        for (var i = 0; i < fieldsValues.Length; i++)
        {
            var metaData = injectionsMetadata.Find(fields[i].FieldType);
            fieldsValues[i] = CreateInstance(metaData, injectionsMetadata);
        }

        return fieldsValues;
    }
}