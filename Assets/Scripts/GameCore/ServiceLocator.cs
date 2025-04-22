using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    public static void RegisterService<T>(T service) where T : class
    {
        var type = typeof(T);

        if (services.ContainsKey(type))
        {
            throw new Exception($"Service of type {type} is already registered.");
        }

        services[type] = service;
    }

    public static T GetService<T>() where T : class
    {
        var type = typeof(T);

        if (services.TryGetValue(type, out var service))
        {
            return service as T;
        }

        throw new Exception($"Service of type {type} is not registered.");
    }

    public static void UnregisterService<T>() where T : class
    {
        var type = typeof(T);

        if (!services.ContainsKey(type))
        {
            throw new Exception($"Service of type {type} is not found.");
        }

        services.Remove(type);
    }

    public static void ClearServices()
    {
        services.Clear();
    }
}