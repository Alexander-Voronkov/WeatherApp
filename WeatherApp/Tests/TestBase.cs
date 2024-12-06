using Application.Users;
using Domain.WeatherAggregate;
using Infrastructure.Data.Repositories;
using NUnit.Framework;
using System.Reflection;
using NetArchTest.Rules;

namespace Tests;
public abstract class TestBase
{
    protected static Assembly ApplicationAssembly => typeof(IWeatherRequestsRepository).Assembly;

    protected static Assembly DomainAssembly => typeof(WeatherRequest).Assembly;

    protected static Assembly InfrastructureAssembly => typeof(WeatherRequestsRepository).Assembly;

    protected static void AssertAreImmutable(IEnumerable<Type> types)
    {
        List<Type> failingTypes = [];
        foreach (var type in types)
        {
            if (type.GetFields().Any(x => !x.IsInitOnly) || type.GetProperties().Any(x => x.CanWrite))
            {
                failingTypes.Add(type);
                break;
            }
        }

        AssertFailingTypes(failingTypes);
    }

    protected static void AssertFailingTypes(IEnumerable<Type> types)
    {
        Assert.That(types, Is.Null.Or.Empty);
    }

    protected static void AssertArchTestResult(TestResult result)
    {
        AssertFailingTypes(result.FailingTypes);
    }
}