﻿using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

namespace BasicWay.Common.Spectre;

public sealed class SpectreTypeRegistrar : ITypeRegistrar
{
    private readonly IServiceCollection _builder;

    public SpectreTypeRegistrar(IServiceCollection builder)
    {
        _builder = builder ?? throw new ArgumentNullException(nameof(builder));
    }

    public ITypeResolver Build()
    {
        return new SpectreTypeResolver(_builder.BuildServiceProvider());
    }

    public void Register(Type service, Type implementation)
    {
        _builder.AddSingleton(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        _builder.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> func)
    {
        if (func is null)
        {
            throw new ArgumentNullException(nameof(func));
        }

        _builder.AddSingleton(service, _ => func());
    }
}