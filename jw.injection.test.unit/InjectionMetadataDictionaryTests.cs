using System;
using System.Collections.Generic;
using jw.injection.api.metadata;
using jw.injection.implementation.exceptions;
using jw.injection.implementation.metadata;
using jw.test.unit.Data;
using Xunit;

namespace jw.injection.test.unit;

public class InjectionMetadataDictionaryTests
{
    private readonly IInjectionMetadataDictionary sut;
    private readonly IDictionary<Type, IInjectionMetadata> _injections;

    public InjectionMetadataDictionaryTests()
    {
        _injections = new Dictionary<Type, IInjectionMetadata>();
        sut = new InjectionMetadataDictionary(_injections);
    }

    [Fact]
    public void Should_Find()
    {
        //Arrange
        var type = typeof(IExampleClass);
        var metadata = new InjectionMetadata();
        _injections.Add(type,metadata);
        
        //act
        var result = sut.Find(type);
        
        //assert
        Assert.Equal(metadata,result);

    }
    [Fact]
    public void Should_Not_Find()
    {
        //Arrange
        var type = typeof(IExampleClass);
        
        //act
        //assert
        Assert.Throws<JwInjectionException>(() =>  sut.Find(type)); 
    }
    [Fact]
    public void Should_Add()
    {
        //Arrange
        var type = typeof(IExampleClass);
        var metadata = new InjectionMetadata();
        
        //act
        sut.Add(type, metadata);
    }

    [Fact]
    public void Should_Not_Add()
    {
        //Arrange
        var type = typeof(IExampleClass);
        var metadata = new InjectionMetadata();
        _injections.Add(type,metadata);
        //act
        //assert
        Assert.Throws<JwInjectionException>(() =>  sut.Add(type, metadata)); 
    }
    
}