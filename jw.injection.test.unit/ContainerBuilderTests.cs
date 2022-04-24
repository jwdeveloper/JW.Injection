using jw.injection.api.container;
using jw.injection.implementation.container;
using jw.injection.test.unit.Data;
using Moq;
using Xunit;

namespace jw.injection.test.unit;

public class ContainerBuilderTests
{
    private readonly IContainerBuilder sut;
    private readonly Mock<IContainer> _containerMock;
    public ContainerBuilderTests()
    {
        _containerMock = new Mock<IContainer>();    
        sut = new ContainerBuilder(_containerMock.Object);
    }
    
    [Fact]
    public void ShouldBuildContainer()
    {
        //arrange
        sut.RegisterSingleton<ExampleClass>();
        sut.RegisterSingleton<ExampleClass>(() =>
        {
            return new ExampleClass(null);
        });
        sut.RegisterSingleton<IExampleClassA, ExampleClass>();
        
        //act
        var container = sut.Build();
        
        //assert
        Assert.NotNull(container);
    }

 
}