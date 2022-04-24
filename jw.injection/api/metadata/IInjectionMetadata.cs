using System.Reflection;
using jw.injection.api.enums;

namespace jw.injection.api.metadata;

public interface IInjectionMetadata
{
    bool HasInjectedFields { get; }
    bool HasInstance { get; }
    bool HasInstanceProvider { get; }
    Type Type { get; set; }
    LifeTime LifeTime { get; set; }
    FieldInfo[] InjectedFields { get; set; }
    ConstructorInfo InjectedConstructor { get; set; }
    Func<object> InstanceProvider { get; set; }
    object Instance { get; set; }
}