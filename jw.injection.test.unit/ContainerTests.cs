using System;
using jw.injection.api.container;
using jw.injection.api.enums;
using jw.injection.api.metadata;
using jw.injection.api.resolvers;
using jw.injection.implementation.container;
using jw.injection.implementation.metadata;
using jw.injection.test.unit.Data;
using Moq;
using Xunit;

namespace jw.injection.test.unit;

public class ContainerTests
{
    private readonly IContainer sut;
    private readonly Mock<IInstanceResolver> _instanceResolverMock;
    private readonly Mock<IMetadataResolver> _metadataResolverMock;
    private readonly Mock<IInjectionMetadataDictionary> _injectionMetadataDictionaryMock;

    public ContainerTests()
    {
        _instanceResolverMock = new Mock<IInstanceResolver>();
        _metadataResolverMock = new Mock<IMetadataResolver>();
        _injectionMetadataDictionaryMock = new Mock<IInjectionMetadataDictionary>();
        sut = new Container(_instanceResolverMock.Object, _metadataResolverMock.Object,
            _injectionMetadataDictionaryMock.Object);
    }


    [Fact]
    public void ShouldRegister()
    {
        //Arrange
        var interfaceType = typeof(IExampleSettingsClass);
        var classType = typeof(ExampleSettingsClass);
        var lifeTime = LifeTime.Transient;
        Func<object> provider = null;

        var metadata = new InjectionMetadata();
        
        _metadataResolverMock.Setup(c => c.CreateMetadata(
                It.IsAny<Type>(),
                It.IsAny<Func<object>>(),
                It.IsAny<LifeTime>()))
            .Returns(metadata);

        //Act
          sut.Register(interfaceType, classType, provider, lifeTime);

        //Assert
        _metadataResolverMock.Verify(x => x.CreateMetadata(
            classType,
            provider,
            LifeTime.Transient),
            Times.Once());
        
        _injectionMetadataDictionaryMock.Verify(x => x.Add(interfaceType, metadata), Times.Once());
    }

    [Fact]
    public void ShouldRegisterWithoutInterface()
    {
        //Arrange
        Type interfaceType = null;
        var classType = typeof(ExampleSettingsClass);
        var lifeTime = LifeTime.Transient;
        Func<object> provider = null;

        var metadata = new InjectionMetadata();
        
        _metadataResolverMock.Setup(c => c.CreateMetadata(
                It.IsAny<Type>(),
                It.IsAny<Func<object>>(),
                It.IsAny<LifeTime>()))
            .Returns(metadata);

        //Act
        sut.Register(interfaceType, classType, provider, lifeTime);

        //Assert
        _metadataResolverMock.Verify(x => x.CreateMetadata(
                classType,
                provider,
                LifeTime.Transient),
            Times.Once());
        
        _injectionMetadataDictionaryMock.Verify(x => x.Add(classType, metadata), Times.Once());
    }
    
    [Fact]
    public void ShouldFind()
    {
        //Arrange
        var interfaceType = typeof(IExampleSettingsClass);
        Func<object> provider = null;

        var metadata = new InjectionMetadata();
        var exampleSettings = new ExampleSettingsClass();
        
        _injectionMetadataDictionaryMock.Setup(c => c.Find(interfaceType)).Returns(metadata);
        _instanceResolverMock.Setup(c => c.CreateInstance( metadata,_injectionMetadataDictionaryMock.Object)).Returns(exampleSettings);
        //Act
        var result = sut.Find<IExampleSettingsClass>();
        //Assert

        Assert.Equal(exampleSettings, result);
        _injectionMetadataDictionaryMock.Verify(x => x.Find(interfaceType),Times.Once());
        _instanceResolverMock.Verify(x => x.CreateInstance(metadata,_injectionMetadataDictionaryMock.Object), Times.Once());
    }
}