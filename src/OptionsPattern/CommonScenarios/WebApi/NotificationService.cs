using System;
using Microsoft.Extensions.Options;
using OptionsPattern.CommonScenarios.WebApi.Options;

namespace OptionsPattern.CommonScenarios.WebApi;

public sealed class NotificationService : IDisposable
{
    private readonly IOptionsMonitor<EmailOptions> _monitor;
    private readonly ILogger _logger;
    private EmailOptions _emailOptions;
    private IDisposable? _onChangeListener;
    private bool _disposed;

    public NotificationService(IOptionsMonitor<EmailOptions> emailOptionsMonitor, ILogger<NotificationService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        ArgumentNullException.ThrowIfNull(emailOptionsMonitor);
        _monitor = emailOptionsMonitor;
        _emailOptions = emailOptionsMonitor.CurrentValue;
        StartListeningForChanges();
    }

    public Task NotifyAsync(string to)
    {
        _logger.LogInformation(
            "Notification sent by '{SenderEmailAddress}' to '{to}'. ({monitor})",
            _emailOptions.SenderEmailAddress,
            to,
            _monitor.CurrentValue.SenderEmailAddress);

        return Task.CompletedTask;
    }

    // For showcase purpose we keep options class reference so we need to update it each time options are changed. Easier way would be using always monitor
    public void StartListeningForChanges()
    {
        _onChangeListener?.Dispose();
        _onChangeListener = _monitor.OnChange((options) =>
                                                   {
                                                       if (_emailOptions?.SenderEmailAddress == options.SenderEmailAddress)
                                                       {
                                                           _logger.LogInformation("Emails are same. Skipping update....");
                                                           return;
                                                       }

                                                       _logger.LogInformation(
                                                           "EmailOptions changed from {old} to {new}. But monitor already holds the newest value {monitor}",
                                                           _emailOptions?.SenderEmailAddress,
                                                           options.SenderEmailAddress,
                                                           _monitor.CurrentValue.SenderEmailAddress);
                                                       _emailOptions = options;
                                                   });
    }

    public void StopListeningForChanges() => _onChangeListener?.Dispose();

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _onChangeListener?.Dispose();
        _disposed = true;
    }
}
