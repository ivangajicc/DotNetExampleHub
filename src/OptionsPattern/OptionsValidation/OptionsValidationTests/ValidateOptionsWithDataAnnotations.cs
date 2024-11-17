using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace OptionsPattern.OptionsValidation.OptionsValidationTests;

public class ValidateOptionsWithDataAnnotations
{
    [Fact]
    public void Should_PassValidation_WhenEverythingIsValid()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddOptions<SomeOptionsWithDataAnnotation>()
                .Configure(o => o.MyImportantProperty = "A value")
                .ValidateDataAnnotations()
                .ValidateOnStart(); // eager validation

        using var serviceProvider = services.BuildServiceProvider();
        var options = serviceProvider
            .GetRequiredService<IOptionsMonitor<SomeOptionsWithDataAnnotation>>();

        // Act & Assert
        options.CurrentValue.MyImportantProperty.Should().Be("A value");
    }

    [Fact]
    public void Should_FailValidation_WhenRequiredPropertyIsNull()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddOptions<SomeOptionsWithDataAnnotation>()
                .ValidateDataAnnotations()
                .ValidateOnStart();

        using var serviceProvider = services.BuildServiceProvider();

        var options = serviceProvider
            .GetRequiredService<IOptionsMonitor<SomeOptionsWithDataAnnotation>>();

        // Act & Assert
        var action = () => options.CurrentValue;

        action.Should()
              .Throw<OptionsValidationException>()
              .Which
              .Failures
              .Should()
              .BeEquivalentTo(
                "DataAnnotation validation failed for 'SomeOptionsWithDataAnnotation' members: 'MyImportantProperty' with the error: 'The MyImportantProperty field is required.'.");
    }

    private class SomeOptionsWithDataAnnotation
    {
        [Required]
        public string? MyImportantProperty { get; set; }
    }
}
