﻿using System;
using System.Collections.Generic;
using Avalonia.Controls;
using CloudSculpt.Interfaces;

namespace CloudSculpt.Services;

public class NavigationService : INavigationService
{
    private readonly Dictionary<string, Type> _windows = new();

    public void RegisterWindow(string key, Type windowType)
    {
        _windows[key] = windowType;
    }

    public void NavigateTo(string key)
    {
        if (!_windows.TryGetValue(key, out var value)) return;
        var window = Activator.CreateInstance(value) as Window;
        window?.Show();
    }
}