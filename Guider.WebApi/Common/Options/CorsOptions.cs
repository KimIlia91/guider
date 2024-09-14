namespace Guider.Common.Options;

/// <summary>
/// CORS options
/// </summary>
public class CorsOptions
{
    /// <summary>
    /// Allowed hosts
    /// </summary>
    public List<string> AllowedHosts { get; init; } = [];

    /// <summary>
    /// Allowed headers
    /// </summary>
    public List<string> AllowedHeaders { get; init; } = [];
}