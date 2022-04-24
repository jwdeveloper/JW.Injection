using System;
using jw.injection.api.attributes;

namespace jw.injection.test.unit.Data;


public interface IExampleClassA
{
    
}
public interface IExampleClassB
{
    
}

public class ExampleClass : IExampleClassA, IExampleClassB
{
    [Inject]
    public IExampleSettingsClass publicField;

    [Inject]
    private IExampleSettingsClass privateField;

    [Inject]
    protected IExampleSettingsClass protectedField;

    public IExampleSettingsClass fromConsturctorField;

    
    public Boolean noInjectedField;
    
  
    public ExampleClass(String test, Boolean something)
    {
        
    }
    
    [Inject]
    public ExampleClass(IExampleSettingsClass a)
    {
        this.fromConsturctorField = a;
    }
}