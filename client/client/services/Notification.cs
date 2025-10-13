using client.services.interfaces;
using System;

namespace client.services;

public class Notification : IReadable
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime? ReadDate { get; set; }
}
