using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.AbstractFactory.Tests;

public abstract class TestBase
    <TConcreteFactory, TExpectedAlloyWheel, TExpectedSteelWheel>
    where TConcreteFactory : IWheelFactory, new()
{
    [Fact]
    public void Should_create_a_IAlloyWheel_of_type_TExpectedAlloyWheel()
    {
        // Arrange
        IWheelFactory wheelFactory = new TConcreteFactory();
        var expectAlloyWheelType = typeof(TExpectedAlloyWheel);

        // Act
        var result = wheelFactory.CreateAlloyWheel();

        // Assert
        result.Should().BeOfType(expectAlloyWheelType);
    }

    [Fact]
    public void Should_create_a_ISteelWheel_of_type_TExpectedSteelWheel()
    {
        // Arrange
        IWheelFactory wheelFactory = new TConcreteFactory();
        var expectSteelWheelType = typeof(TExpectedSteelWheel);

        // Act
        var result = wheelFactory.CreateSteelWheel();

        // Assert
        result.Should().BeOfType(expectSteelWheelType);
    }
}
