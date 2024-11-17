using System.ComponentModel.DataAnnotations;

namespace OptionsPattern.ConfigurationGenerators.WebApi.Options;

public class MyOptions
{
    [Required]
    public string? Name { get; set; }
}
