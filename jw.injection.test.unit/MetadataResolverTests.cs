using jw.injection.api.enums;
using jw.injection.implementation.resolvers;
using jw.test.unit.Data;
using Xunit;
using ExampleClass = jw.injection.test.unit.Data.ExampleClass;

namespace jw.injection.test.unit;

public class MetadataResolverTests
{
    private readonly MetadataResolver sut;

    public MetadataResolverTests()
    {
        sut = new MetadataResolver();
    }
    
    [Fact]
    public void Should_GetMetadata()
    {
        //Arrage
        var type = typeof(ExampleClass);
        
        //Act 
        var result = sut.CreateMetadata(type, null, LifeTime.Singleton);
        
        //Assert
        Assert.True(result.HasInjectedFields);
        Assert.Equal(3, result.InjectedFields.Length);
        Assert.NotNull(result.InjectedConstructor);
        Assert.Null(result.InstanceProvider);
    }
    
    [Fact]
    public void Should_Find_Injectet_Constructor()
    {
        //Arrage
        var type = typeof(ExampleClass);
        
        //Act 
        var result = sut.FindInjectedConstructor(type);
        
        //Assert
        Assert.NotNull(result);
    }
    
    [Fact]
    public void Should_Not_Find_Constructor()
    {
        //Arrage
        var type = typeof(IExampleClass);
        
        //Act 
        var result = sut.FindInjectedConstructor(type);
        
        //Assert
        Assert.Null(result);
    }
    
    [Fact]
    public void Should_Find_Injectet_Fields()
    {
        //Arrage
        var type = typeof(ExampleClass);
        
        //Act 
        var result = sut.CreateMetadata(type, null, LifeTime.Singleton);
        
        //Assert
        Assert.True(result.HasInjectedFields);
        Assert.Equal(3, result.InjectedFields.Length);
    }
}