using jw.injection.api.attributes;

namespace jw.test.unit.Data;

public interface IExampleClass
{
}

public class ExampleClass : IExampleClass
{
    [Inject] public IExampleSettingsClass a;
    [Inject] public IExampleSettingsClass b;
    [Inject] public IExampleSettingsClass c;
    [Inject] public IExampleSettingsClass d;
    [Inject] public IExampleSettingsClass e;
    [Inject] public IExampleSettingsClass f;
    [Inject] public IExampleSettingsClass g;
    [Inject] public IExampleSettingsClass h;
    [Inject] public IExampleSettingsClass i;
    [Inject] public IExampleSettingsClass j;
    [Inject] public IExampleSettingsClass k;
}