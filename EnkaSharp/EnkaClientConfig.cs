namespace EnkaSharp;

public class EnkaClientConfig
{
    /// <summary>
    /// UserAgent to use for API requests
    /// </summary>
    public string UserAgent { get; set; } = null!;
    /// <summary>
    /// Language to use for Localization purposes.
    /// Defaults to "en" - english.
    /// </summary>
    public string Language { get; set; } = "en";

    /// <summary>
    /// Number of retries.
    /// Defaults to 3.
    /// </summary>
    public int RetryCount { get; set; } = 3;
    
    /// <summary>
    /// Delay between retries.
    /// Defaults to 1 minute.
    /// </summary>
    public TimeSpan RetryDelay { get; set; } = TimeSpan.FromMinutes(1);
}