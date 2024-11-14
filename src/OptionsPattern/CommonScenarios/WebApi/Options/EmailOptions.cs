using System;
using Microsoft.Extensions.Options;

namespace OptionsPattern.CommonScenarios.WebApi.Options;

public class EmailOptions
{
    public string? SenderEmailAddress { get; set; }
}
