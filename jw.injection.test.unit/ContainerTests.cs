using jw.injection.api.enums;
using jw.injection.implementation.container;
using jw.injection.implementation.resolvers;
using jw.injection.test.unit.Data;
using Xunit;

namespace jw.injection.test.unit;

public class ContainerTests
{
    private readonly Container sut;

    public ContainerTests()
    {
        sut = new Container(new InstanceResolver(), new MetadateResolver());
    }


    [Fact]
    public void ShouldRegister()
    {
        sut.Register(typeof(IExampleSettingsClass), typeof(ExampleSettingsClass), null, LifeTime.Transient);
        
        var f1 = sut.Find<IExampleSettingsClass>();
        var f2 = sut.Find<IExampleSettingsClass>();
        
        Assert.NotEqual(f1,f2);
    }
    
    [Fact]
    public void ShouldRegister2()
    {
        sut.Register(typeof(IExampleSettingsClass), typeof(ExampleSettingsClass), null, LifeTime.Singleton);
        
        var f1 = sut.Find<IExampleSettingsClass>();
        var f2 = sut.Find<IExampleSettingsClass>();
        
        Assert.Equal(f1,f2);
    }

    [Fact]
    public void ShouldRegisterWithoutInterface()
    {
        sut.Register(null, typeof(ExampleSettingsClass), null, LifeTime.Singleton);
        
        var f1 = sut.Find<ExampleSettingsClass>();
        var f2 = sut.Find<ExampleSettingsClass>();
        
        Assert.Equal(f1,f2);
    }
    [Fact]
    public void ShouldRegisterWithProvider()
    {
        sut.Register(
            typeof(IExampleSettingsClass),
            typeof(ExampleSettingsClass),
            () => new ExampleSettingsClass(),
            LifeTime.Singleton);
        
        var f1 = sut.Find<IExampleSettingsClass>();
        var f2 = sut.Find<IExampleSettingsClass>();
        
        Assert.Equal(f1,f2);
    }

    [Fact]
    public void ShouldRegisterWithProvider2()
    {
        sut.Register(
            typeof(IExampleSettingsClass),
            typeof(ExampleSettingsClass),
            () => new ExampleSettingsClass(),
            LifeTime.Transient);
        
        var f1 = sut.Find<IExampleSettingsClass>();
        var f2 = sut.Find<IExampleSettingsClass>();
        
        Assert.NotEqual(f1,f2);
    }
  
}