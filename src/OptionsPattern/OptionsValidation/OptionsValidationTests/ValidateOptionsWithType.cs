using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace OptionsPattern.OptionsValidation.OptionsValidationTests;

public class ValidateOptionsWithType
{
    [Fact]
    public void Should_PassValidation_WhenEverythingIsValid()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddSingleton<IValidateOptions<SomeOptions>, SomeOptionsValidator>();

        services.AddOptions<SomeOptions>()
                .Configure(opt => opt.MyImportantProperty = "SomeValue")
                .ValidateOnStart();

        using var serviceProvider = services.BuildServiceProvider();

        // Act and assert
        var options = serviceProvider.GetRequiredService<IOptionsMonitor<SomeOptions>>();

        options.CurrentValue.MyImportantProperty.Should().Be("SomeValue");
    }

    [Fact]
    public void Should_FailValidation_WhenRequiredPropertyIsNull()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddSingleton<IValidateOptions<SomeOptions>, SomeOptionsValidator>();
        services.AddOptions<SomeOptions>()
                .ValidateOnStart();

        using var serviceProvider = services.BuildServiceProvider();

        // Act and assert
        var options = serviceProvider.GetRequiredService<IOptionsMonitor<SomeOptions>>();

        var action = () => options.CurrentValue;

        action.Should()
              .Throw<OptionsValidationException>()
              .Which
              .Failures
              .Should()
              .BeEquivalentTo($"{nameof(SomeOptions.MyImportantProperty)} is required.");
    }

    private class SomeOptionsValidator : IValidateOptions<SomeOptions>
    {
        public ValidateOptionsResult Validate(string? name, SomeOptions options)
        {
            if (string.IsNullOrEmpty(options.MyImportantProperty))
            {
                return ValidateOptionsResult.Fail($"{nameof(options.MyImportantProperty)} is required.");
            }

            return ValidateOptionsResult.Success;
        }
    }

    private class SomeOptions
    {
        public string? MyImportantProperty { get; set; }
    }
}
