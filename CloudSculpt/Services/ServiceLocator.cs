﻿using System;
using System.Collections.Generic;

namespace CloudSculpt.Services;

public class ServiceLocator
{
    private static Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void Register<T>(T service)
    {
        _services[typeof(T)] = service;
    }

    public static T Resolve<T>()
    {
        return (T)_services[typeof(T)];
    }
}