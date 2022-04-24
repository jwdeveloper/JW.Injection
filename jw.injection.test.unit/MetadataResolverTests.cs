using jw.injection.api.enums;
using jw.injection.implementation.resolvers;
using jw.injection.test.unit.Data;
using Xunit;

namespace jw.injection.test.unit;

public class MetadataResolverTests
{
    private readonly MetadateResolver sut;

    public MetadataResolverTests()
    {
        sut = new MetadateResolver();
    }
    
    [Fact]
    public void Should_GetMetadata()
    {
        //Arrage
        var example = () =>
        {
            return new ExampleClass(null);
        };
        
        //Act 
        var result = sut.CreateMetadata(example.GetType(), example, LifeTime.Singleton);
        
        //Assert
        
        Assert.True(result.HasInjectedFields);
        Assert.Equal(3, result.InjectedFields.Length);
    }
    
}