using BenchmarkDotNet.Attributes;
using jw.injection.api.container;
using jw.injection.implementation;
using jw.test.unit.Data;

namespace jw.injection.test.benchmark;

public class ContainerBuilderTests
{
    private IContainerFind sut;

    public ContainerBuilderTests()
    {
        sut = JwInjection.CreateContainer()
            .RegisterTransient<IExampleSettingsClass, ExampleSettingsClass>()
            .RegisterTransient<IExampleClass, ExampleClass>()
            .Build();
    }

    [Benchmark]
    public void BenchMark()
    {
        for (int i = 0; i < 1000; i++)
        {
            sut.Find<IExampleClass>();
        }
    }
}