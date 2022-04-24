using System;
using System.Collections.Generic;
using jw.injection.api.enums;
using jw.injection.implementation.metadata;
using jw.injection.implementation.resolvers;
using jw.injection.test.unit.Data;
using Xunit;

namespace jw.injection.test.unit;

public class InstanceProviderTests
{

    private readonly InstanceResolver sut;
    
    
    public InstanceProviderTests()
    {
        this.sut = new InstanceResolver();
    }


    [Fact]
    public void ShouldCreateInstnace()
    {
        //arrage
        var injections = CreateInjections();
        var type = typeof(ExampleClass);
        var metadata = injections.Find(type);
        
        //act
        var instance = (ExampleClass)sut.CreateInstance(metadata, injections);
        
        //assert
        Assert.NotNull(instance);
        Assert.Equal(type,instance.GetType());
        Assert.NotEqual(instance.publicField,instance.fromConsturctorField);
    }

    [Fact]
    public void Should_ReturenProvidedObject()
    {
        //arrage
        var injections = CreateInjections();
        var metadata = injections.Find(typeof(ExampleClass));
        var metadata2 = injections.Find(typeof(ExampleSettingsClass));

        var settings = new ExampleSettingsClass();
        metadata2.Instance = settings;
        metadata2.LifeTime = LifeTime.Singleton;
        //act
        var instance = (ExampleClass)sut.CreateInstance(metadata, injections);
        
        //assert
        Assert.NotNull(instance);
        Assert.Equal(settings,instance.fromConsturctorField);
        Assert.Equal(settings,instance.publicField);
    }
    
    [Fact]
    public void Should_ReturenProvidedObject2()
    {
        //arrage
        var injections = CreateInjections();
        var metadata = injections.Find(typeof(ExampleClass));
        var metadata2 = injections.Find(typeof(ExampleSettingsClass));

        metadata2.InstanceProvider = () => { return new ExampleSettingsClass(); };
        metadata2.LifeTime = LifeTime.Transient;
        //act
        var instance = (ExampleClass)sut.CreateInstance(metadata, injections);
        
        //assert
        Assert.NotNull(instance);
        Assert.NotEqual(instance.publicField,instance.fromConsturctorField);
    }

    public InjectionMetadataDictionary CreateInjections()
    {
        var result = new InjectionMetadataDictionary();
        var metadataResolver = new MetadateResolver();
        var metadata1 = metadataResolver.CreateMetadata(typeof(ExampleClass), null, LifeTime.Transient);
        var metadata2  = metadataResolver.CreateMetadata(typeof(ExampleSettingsClass), null, LifeTime.Transient);
        result.Add(typeof(ExampleClass),metadata1);
        result.Add(typeof(ExampleSettingsClass),metadata2);
        return result;
    }
}