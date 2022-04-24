using jw.injection.implementation.container;
using jw.injection.test.unit.Data;
using Xunit;

namespace jw.injection.test.unit;

public class ContainerBuilderTests
{
    [Fact]
    public void ShouldBuildContainer()
    {
        var builder = new ContainerBuilder();
        builder.RegisterSingleton<IExampleSettingsClass, ExampleSettingsClass>();
        builder.RegisterSingleton<IExampleClassA, ExampleClass>();
        builder.RegisterSingleton<IExampleClassB, ExampleClass>();
        var di = builder.Build();
        di.Find<IExampleSettingsClass>();
        di.Find<IExampleClassA>();
        di.Find<IExampleClassB>();
    }

 
}