namespace jw.injection.implementation.exceptions;

public class JwInjectionException : Exception
{
    public JwInjectionException(string message) : base(message)
    {
    }

    public static JwInjectionException NotFound(Type type)
    {
        return new JwInjectionException($"Type of {type} is not register to DI container");
    }

    public static JwInjectionException UnableToCreateInstance(Type type)
    {
        return new JwInjectionException($"Type of {type} unable to create instance");
    }

    public static JwInjectionException AlreadyRegistered(Type type)
    {
        return new JwInjectionException($"Type of {type.Name} have been already registered");
    }
}