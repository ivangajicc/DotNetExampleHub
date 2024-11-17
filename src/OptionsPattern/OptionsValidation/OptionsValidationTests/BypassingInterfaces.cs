using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace OptionsPattern.OptionsValidation.OptionsValidationTests;

public class BypassingInterfaces
{
    [Fact]
    public void Should_SupportAnyScope_WhenInterfaceIsBypassed()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddOptions<MyOptions>().Configure(opt => opt.Name = "Ivan G.");

        services.AddScoped(serviceProvider =>
        {
            var snapshot = serviceProvider.GetRequiredService<IOptionsSnapshot<MyOptions>>();

            return snapshot.Value;
        });

        using var serviceProvider = services.BuildServiceProvider();

        // Act and assert
        using var scope1 = serviceProvider.CreateScope();

        var options1 = scope1.ServiceProvider.GetService<MyOptions>();
        var options2 = scope1.ServiceProvider.GetService<MyOptions>();

        options1.Should().BeSameAs(options2);

        using var scope2 = serviceProvider.CreateScope();

        var options3 = scope2.ServiceProvider.GetService<IOptions<MyOptions>>();

        options3.Should().NotBeSameAs(options2);
    }

    private class MyOptions
    {
        public string? Name { get; set; }
    }
}
